using MovieDbApi.Data;
using System.Text.Json;

namespace MovieDbApi.Dingoids
{
    public interface IUtilities
    {
        void LogSomething();
        List<string> fieldsOfThis(Type T);

    }
    public class IntraStringDateComparer : IComparer<string>
    {
        public int Compare(string? a, string? b)
        {
            string date1 = "0";
            string date2 = "0";
            if (a is not null && b is not null)
            {
                date1 = String.Join("", a.Where(char.IsDigit));
                date2 = String.Join("", b.Where(char.IsDigit));
            }
            if (Convert.ToInt32(date1) > Convert.ToInt32(date2))
            {
                return 1;

            }
            else if (date1 == date2)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
    }

    public class ApiConfig
    {
        public string? API_KEY { get; set; }
        public string? API_READ_TOKEN { get; set; }
    }

    public static class Dingus //: IUtilities
    {
        public static ILogger<Object>? _logger { get; set; }
        public static string mdb_dl_root = "http://files.tmdb.org/p/exports/";
        public static string mdb_tvnetwork_segment = "tv_network_ids_{MM_dd_yyyy}.json.gz";
        public static List<string> export_urls = new List<string>()
        {
            "movie_ids_MM_DD_YYYY.json.gz",
            "tv_series_ids_MM_DD_YYYY.json.gz",
            "person_ids_MM_DD_YYYY.json.gz",
            "collection_ids_MM_DD_YYYY.json.gz",
            "tv_network_ids_MM_DD_YYYY.json.gz",
            "keyword_ids_MM_DD_YYYY.json.gz",
            "production_company_ids_MM_DD_YYYY.json.gz"
        };

        public static Dictionary<string, Type> modelDict = new Dictionary<string, Type>
        {
            { "movie", typeof(movie) },
            { "keyword", typeof(movie) },
            { "collection", typeof(movie) },
            { "production_company", typeof(movie) },
            { "person", typeof(movie) },
            { "tv_network", typeof(movie) },
            { "tv_series", typeof(movie) },

        };

        //static Utilities() //ILogger<Object> logger)
        //{
        //    //_logger = logger;
        //    _logger.LogInformation("_logger speaks in ctor");

        //}

        public static void LogSomething()
        {
            _logger.LogInformation("_logger speaks");
            Type movee = modelDict["movie"];
            var x = Activator.CreateInstance(movee);
            _logger.LogInformation(x.ToString());
        }

        //Custom comparer for strings containing numbers (dates) to sort only by those numbers.



        public static Task DateTheExportUrls(bool yesterday)
        {
            // yesterday flag is set for the option of using yesterday's date in the file names
            //  instead of whatever is already there. this will probably be deprecated, because why?
            if (!yesterday)
            {
                export_urls.Sort();

                // get a list of .json files in Downloads directory
                List<string> download_names =
                    Directory.GetFiles("./Downloads").Where(g => g.EndsWith(".json"))
                    .ToList();

                List<DateTime> filedates = new List<DateTime>();
                List<string> filestrings = new List<string>();

                for (int d = 0; d < download_names.Count; d++)
                {
                    // we don't want the full path, just the file name, so trim it up
                    download_names[d] = Path.GetFileName(download_names[d]);

                    string dn = download_names[d];
                    try
                    {
                        string s = dn.Substring(dn.IndexOf("ids_") + 4, dn.LastIndexOf('.')
                            - dn.IndexOf("ids_") - 4);
                        filedates.Add(DateTime.Parse(s.Replace('_', '/')));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                // download_names now has MM_dd_yyyy formatted actual number dates

                filedates.Sort((a, b) => a.CompareTo(b));
                download_names.Sort(new IntraStringDateComparer());

                // shorthand - you'll probably change these later for readability justifications
                string xu, fd, xn;

                // the export_urls list loop
                // (pre-filled/hardcoded: the filename portion of the urls wanted by the mdb API)
                for (int current_xu = 0; current_xu < export_urls.Count; current_xu++)
                {

                    // the already-downloaded files list loop ((download_names.count == filedates.count) = TRUE)
                    for (int current_name = 0; current_name < download_names.Count; current_name++)
                    {
                        xn = download_names[current_name];
                        xu = export_urls[current_xu];

                        //we're trying to get the date of the most recent file OF THIS NAME
                        // (download_names[current_name] from the downloads
                        fd = filedates[current_name].ToString("MM_dd_yyyy");
                        int startindex = 0;

                        var len_xn = xn.LastIndexOf("ids_") + 4;
                        var len_xu = xu.LastIndexOf("ids_") + 4;


                        // CRITIC: Can't we just be looking for the first isdigit or something? LINQ?
                        //      I mean, this method seems too complicated.
                        // CRITIC: Maybe it's just all these comments making it look too complicated.

                        //Is this >= second replacement?
                        //If so, it will have the full http://... (mdb_dl_root) prefix.
                        //If so, do not re-add the mdb_dl_root prefix (i.e. mdb_dl_root). (or we get http://...http://...http://...)
                        //If so, look for an actual date instead of MM_DD_YYYY to replace.
                        if (xu.StartsWith(mdb_dl_root))
                        {
                            startindex = mdb_dl_root.Length + 1;
                            var one = xu.Substring(startindex - 1, len_xu - startindex + 1);
                            var two = xn.Substring(0, len_xn);
                            if (one == two) //then this is a match
                            {

                                //load the date portion into a var. it's 10 chars long. last _ occurs after the dd part.
                                string datepart = xu.Substring(xu.LastIndexOf('_') - 5, 10);

                                export_urls[current_xu] = export_urls[current_xu].Replace(datepart,
                                    filedates[current_name].ToString("MM_dd_yyyy"));
                            }
                        }
                        else
                        {
                            if (xu.Substring(startindex, len_xu)
                                == xn.Substring(startindex, len_xn)) //then this is a match
                            {
                                export_urls[current_xu] = mdb_dl_root + export_urls[current_xu].Replace("MM_DD_YYYY",
                                    filedates[current_name].ToString("MM_dd_yyyy"));
                            }
                        }
                        //}  end of filename matching


                    }// end of download_names (current_name) loop

                }//end of xurls loop (j) -- BTW WTF with these letters? j and q? d?

                _logger.LogInformation("so where are we now?");
            }
            else // (yesterday = true)
            {
                for (int i = 0; i < export_urls.Count; i++)
                {
                    export_urls[i] = mdb_dl_root + export_urls[i].Replace("MM_DD_YYYY",
                        DateTime.Now.AddDays(-1).ToString("MM_dd_yyyy"));
                }
            } // end function

            _logger.LogInformation(export_urls[3]);
            return Task.CompletedTask;

        }

        public static string MakePathFromType(Type T)
        {
            // the date section need to come from export_urls list
            // so you know, fix that.

            string fileEnding = export_urls[0]
                .Substring(export_urls[0].IndexOf("_ids"),
                export_urls[0].IndexOf(".gz") - export_urls[0].IndexOf("_ids"));

            string file = @".\Downloads\"
                + System.IO.Path.GetFileName(T.Name + fileEnding);
            return file;
        }


        public static async Task<string> ReadJsonFile(string file)
        {
            string json = await File.ReadAllTextAsync(file);
            return json;
        }

        public static List<string> FieldsOfThis(Type T)
        {
            var fields = new List<string>(); // return this

            string file = MakePathFromType((Type)T);

            string json = ReadJsonFile(file).Result;

            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true,
            };

            using (JsonDocument document = JsonDocument.Parse(json, options))
            {
                JsonElement root = document.RootElement;

                if (_logger is not null) _logger.LogInformation(root.ValueKind.ToString());

                var thing = root.EnumerateObject();
                using var thingy = thing.GetEnumerator();

                //first up, the root object
                while (thingy.MoveNext())
                {
                    var current = thingy.Current;

                    // current.Name will be the property name of the array,
                    //  e.g. "production_company-list"
                    //  deal with this plural spelling problem and that will be your table name
                    if (_logger is not null) _logger.LogInformation(current.Name);
                    var doodad = current.Value.EnumerateArray();

                    //secondly, the big array of all things
                    while (doodad.MoveNext())
                    {
                        var currant = doodad.Current.EnumerateObject();

                        //lastly, the object #1, where we can get the property names
                        while (currant.MoveNext())
                        {
                            var merrywang = currant.Current;
                            if (_logger is not null) _logger.LogInformation("And the winner is: " + merrywang.Name);
                            fields.Add(merrywang.Name);
                        }

                        break;
                    }

                    break;

                    //I only want the first jsonproperty, but I don't yet know
                    //how to just grab the first from the outset
                    //so I had to enumerate the entire thing first;
                    //but I would like to stop once I have the property names
                    //so I am just breaking out of the whiles once I have them
                    //until I figure out the secret ingredient to just taking one...
                    //if I continue to care, that is.
                }

            } // end using

            return fields;

        } // end trans
    }
}

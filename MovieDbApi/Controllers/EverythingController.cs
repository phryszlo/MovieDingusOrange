using Microsoft.AspNetCore.Mvc;
using MovieDbApi.Data;
using System.Reflection;
using System.Text.Json;

using Microsoft.EntityFrameworkCore;

namespace MovieDbApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EverythingController : ControllerBase
    {
        public readonly ILogger<EverythingController> _logger;
        private readonly MovieDbContext _context;

        public EverythingController(ILogger<EverythingController> logger, MovieDbContext context)
        {
            _logger = logger;
            _context = context;

        }


        class TypeValue
        {
            public TypeValue(Type t, string v)
            {
                type = t;
                value = v;
            }
            public Type type { get; set; }
            public string? value { get; set; }

        }



        /*
 * edb=# select * from (
moviedb(# select "id", row_number() over (order by id)
moviedb(# from "TVNetworks"
moviedb(# ) as foo
moviedb-# where row_number = 799;
id  | row_number
*/
        [HttpGet("GetIDFromRowNumber/{row_num}")]
        public int GetIDFromRowNumber(int row_num)
        {
            Stopwatch sw = new Stopwatch();

            var pg_sql = @"select * from (select ""id"", row_number() over (order by id)
                    from ""TVNetworks"") AS foo WHERE row_number = "
                    + row_num.ToString() + ";";
            System.FormattableString ms_sql = $@"select * from (select id, name, row_number() over (order by id)
                    as row_num from TVNetworks) foo WHERE row_num = {row_num}";
            var result = _context.TVNetworks.FromSqlInterpolated(ms_sql);
            
            sw.Stop();


            //Testing idea of generically logging with current method name
            //so you could use this snippet as-is everywhere.
            //Add m.ReflectedType.Name to include classname.
            MethodBase? m = MethodBase.GetCurrentMethod();
            Debug.WriteLine("{0}: {1} seconds", 
                m.Name, 
                sw.Elapsed.TotalSeconds.ToString());
            
            

            return result.Count() > 0 ? result.First().id : -1;
        }




        [HttpGet("DoMovieStuff/{type}")]
        public void DoMovieStuff(string type)
        {

            Stopwatch sw = new Stopwatch();

            Dingus._logger = _logger; //inject our logger, because who cares?
            Dingus.DateTheExportUrls(false);
            _logger.LogInformation(Dingus.export_urls.ToString());
            List<string> fields = Dingus.FieldsOfThis(typeof(production_company));
            foreach (string field in fields)
            {
                _logger.LogInformation(field);
            }
            switch (type)
            {
                case "movie":
                    StoreExportData(typeof(movie), fields);

                    break;
                case "keyword":
                    StoreExportData(typeof(keyword), fields);

                    break;
                case "collection":
                    StoreExportData(typeof(collection), fields);

                    break;
                case "person":
                    StoreExportData(typeof(person), fields);

                    break;
                case "production_company":
                    StoreExportData(typeof(production_company), fields);
                    break;
                case "tv_network":
                    StoreExportData(typeof(tv_network), fields);

                    break;
                case "tv_series":
                    StoreExportData(typeof(tv_series), fields);

                    break;
                default:
                    _logger.LogInformation("no switch found");
                    break;

            }
            sw.Stop();
            MethodBase? m = MethodBase.GetCurrentMethod();
            Debug.WriteLine("{0}: {1} seconds",
                m.Name,
                sw.Elapsed.TotalSeconds.ToString());
        }

        [HttpGet("loglog")]
        public void loglog()
        {
            Dingus.LogSomething();
        }

        [NonAction]
        private List<JsonElement> JsonData(string filepath, List<TypeValue> fields)
        {
            string json = System.IO.File.ReadAllText(filepath);
            List<JsonElement> list = new List<JsonElement>();

            object[] objarr = new object[0];

            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true,
            };
            using (JsonDocument document = JsonDocument.Parse(json, options))
            {
                JsonElement root = document.RootElement;
                var clone = root.Clone();

                _logger.LogInformation(root.ValueKind.ToString());

                var thing = root.EnumerateObject();
                using var thingy = thing.GetEnumerator();

                while (thingy.MoveNext())
                {
                    //root object
                    var current = thingy.Current;
                    _logger.LogInformation(current.Name);
                    var array = current.Value.EnumerateArray();

                    //the big array of objects
                    while (array.MoveNext())
                    {
                        var currant = array.Current;
                        list.Add(currant.Clone());
                    }
                }

            }

            return list;
        }


        [NonAction]
        public void StoreExportData(Type type, List<string> fields)
        {
            // getting names of DbSet properties from MovieDbContext
            //   for some reason : TBD.

            MemberInfo[] members = typeof(MovieDbContext).GetMembers();
            MovieDbContext foo = new MovieDbContext();
            foreach (var prop in foo.GetType().GetProperties())
            {
                if (prop.DeclaringType == typeof(MovieDbContext))
                {
                    //_logger.LogInformation(prop.DeclaringType.Name);
                    _logger.LogInformation(prop.Name);
                }

            }
            // and That, was how to get the DbSet names from the DbContext


            using (var dbcontext = new MovieDbContext())
            {
                List<TypeValue> allexportfields = new List<TypeValue>();
                TypeValue id = new TypeValue(typeof(int), "id");
                TypeValue original_title = new TypeValue(typeof(string), "original_title");
                TypeValue name = new TypeValue(typeof(string), "original_title");
                TypeValue adult = new TypeValue(typeof(bool), "adult");
                TypeValue popularity = new TypeValue(typeof(float), "popularity");
                TypeValue video = new TypeValue(typeof(bool), "video");
                allexportfields.AddRange(new List<TypeValue> {
                    id, original_title , name , adult , popularity, video
                });

                List<TypeValue> TypeValsForThisThingHere = new List<TypeValue>();
                foreach(var field in fields)
                {
                    var TV = new TypeValue(typeof(object), "foo");
                    foreach(var tv in allexportfields)
                    {
                        if (field == tv.value)
                        {
                            TV.type = tv.type;
                            TV.value = tv.value;
                        }

                    }
                    TypeValsForThisThingHere.Add(TV);
                }

                // at this point we have a Type and value (fieldname) for each
                //   member of the class type which was passed to this method.

                System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                sw.Start();
                List<JsonElement> list = JsonData(
                        Dingus.MakePathFromType(type),
                        TypeValsForThisThingHere);
                sw.Stop();
                _logger.LogInformation("Elapsed: {0} seconds", sw.Elapsed.TotalSeconds.ToString());

                List<production_company> productions = new List<production_company>();
                List<movie> movies = new List<movie>();
                List<keyword> keywords = new List<keyword>();
                List<person> persons = new List<person>();
                List<collection> collections = new List<collection>();
                List<tv_network> tv_networks = new List<tv_network>();
                List<tv_series> tv_serieses = new List<tv_series>();

                for(int i = 0; i < list.Count; i++)
                {
                    // THE COP OUT
                    switch (type.Name)
                    {
                        case "movie":
                            movie? _movie = Json.GetJsonGenericType<movie>(list[i].ToString());
                            dbcontext.Movies.Add(_movie);
                            break;
                        case "keyword":
                            keyword? _keyword = Json.GetJsonGenericType<keyword>(list[i].ToString());
                            dbcontext.Keywords.Add(_keyword);
                            break;
                        case "collection":
                            collection? _collection = Json.GetJsonGenericType<collection>(list[i].ToString());
                            dbcontext.Collections.Add(_collection);
                            break;
                        case "person":
                            person? _person = Json.GetJsonGenericType<person>(list[i].ToString());
                            dbcontext.Persons.Add(_person);
                            break;
                        case "production_company":
                            production_company? _production = Json.GetJsonGenericType<production_company>(list[i].ToString());
                            dbcontext.ProductionCompanies.Add(_production);
                            break;
                        case "tv_network":
                            tv_network? _tv_network = Json.GetJsonGenericType<tv_network>(list[i].ToString());
#pragma warning disable CS8604 // Possible null reference argument.
                            _ = dbcontext.TVNetworks.Add(_tv_network);
#pragma warning restore CS8604 // Possible null reference argument.
                            break;
                        case "tv_series":
                            tv_series? _tv_series = Json.GetJsonGenericType<tv_series>(list[i].ToString());
                            dbcontext.TVSerieses.Add(_tv_series);
                            break;
                        default:
                            _logger.LogInformation("no switch found");
                            break;

                    }

                }

                dbcontext.SaveChanges();
            }
        }
    }
}

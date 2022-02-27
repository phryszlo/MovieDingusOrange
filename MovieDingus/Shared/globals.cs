//using Microsoft.Extensions.Lo

using System.IO.Compression;

namespace MovieDingus.Shared
{
    public static class Globals
    {





        public enum ReturnStatus
        {
            Success,
            Error,
            Warning,
            Shite,
            Twinkies,
            Whatever
        }

        private static readonly HttpClient g_httpClient = new HttpClient();

        public static async Task DownloadFileAsync(string uri, string outfile)
        {
            try
            {
                using (var stream = await g_httpClient.GetStreamAsync(uri))
                {
                    using (var fileStream = new FileStream(outfile, FileMode.Create))
                    {
                        await stream.CopyToAsync(fileStream);
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        public static void UnzipFile(string file)
        {
            //the ReturnStatus stuff is stupid
            FileStream fInStream = null;
            try
            {
                fInStream = new FileStream(file, FileMode.Open, FileAccess.Read);


                using (System.IO.Compression.GZipStream zipStream =
                    new GZipStream(fInStream, CompressionMode.Decompress))
                {
                    using (FileStream fOutStream =
                        new FileStream(file.Substring(0, file.LastIndexOf('.')),
                        FileMode.Create, FileAccess.Write))
                    {
                        byte[] tempBytes = new byte[4096];
                        int i;
                        while ((i = zipStream.Read(tempBytes, 0, tempBytes.Length)) != 0)
                        {
                            fOutStream.Write(tempBytes, 0, i);
                        }
                    }
                }
            }
            catch (Exception ex) { Console.Error.WriteLine(ex.Message); }
        }

        public static void JsonizeFile(string file)
        {
            StreamReader sr = new StreamReader(file);
            var foo = sr.ReadToEnd();
            sr.Close();
            foo = foo.Replace((char)(0x1F), ' ');

            //_ids_MM_DD_YYYY.json.gz << cut this much off the end
            var collection_name =
                file.Substring(file.LastIndexOf('/'),
                (file.Length - 20) - file.LastIndexOf("/"));

            foo = "{\"" + collection_name + "-list\": [" + foo.Replace('\n', ',') + "]}";
            StreamWriter sw = new StreamWriter(file);
            sw.Write(foo);
            sw.Close();
        }

    } //class
}//namespace

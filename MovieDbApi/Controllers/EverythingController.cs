using Microsoft.AspNetCore.Mvc;
using MovieDbApi.Data;
using System.Reflection;
using System.Text.Json;

namespace MovieDbApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EverythingController : ControllerBase
    {
        public readonly ILogger<EverythingController> _logger;
        public EverythingController(ILogger<EverythingController> logger)
        {
            _logger = logger;
        }

        [HttpGet("StoreExportData")]
        public void StoreExportData()
        {
            //getting names of DbSet properties from MovieDbContext
            //for some reason yet to be determined.

            MemberInfo[] fields = typeof(MovieDbContext).GetMembers();

            MovieDbContext foo = new MovieDbContext();
            foreach (var prop in foo.GetType().GetProperties())
            {
                if (prop.DeclaringType == typeof(MovieDbContext))
                {
                    //_logger.LogInformation(prop.DeclaringType.Name);
                    _logger.LogInformation(prop.Name);
                }

            }


            using (var dbcontext = new MovieDbContext())
            {
                List<TypeValue> allexportfields = new List<TypeValue>();
                TypeValue id = new TypeValue(typeof(int), "id");
                TypeValue original_title = new TypeValue(typeof(string), "original_title");
                TypeValue name = new TypeValue(typeof(string), "original_title");
                TypeValue adult = new TypeValue(typeof(bool), "adult");
                TypeValue popularity = new TypeValue(typeof(int), "popularity");
                TypeValue video = new TypeValue(typeof(bool), "video");
                allexportfields.AddRange(new List<TypeValue> {
                    id, original_title , name , adult , popularity, video
                });


                foreach(var field in allexportfields)
                {

                }

                List<TypeValue> Moviefields = new List<TypeValue>();
                Moviefields.Add(id);
                Moviefields.Add(original_title);
                Moviefields.Add(adult);
                Moviefields.Add(popularity);
                Moviefields.Add(video);

                List<TypeValue> Collectionfields = new List<TypeValue>();
                Collectionfields.Add(id);
                Collectionfields.Add(name);

                List<TypeValue> Keywordfields = new List<TypeValue>();
                Keywordfields.Add(id);
                Keywordfields.Add(name);

                List<TypeValue> Personfields = new List<TypeValue>();
                Personfields.Add(id);
                Personfields.Add(name);
                Personfields.Add(adult);
                Personfields.Add(popularity);
                Personfields.Add(video);

                List<TypeValue> ProductionCompanyfields = new List<TypeValue>();
                ProductionCompanyfields.Add(id);
                ProductionCompanyfields.Add(name);

                List<TypeValue> TVNetworkfields = new List<TypeValue>();
                TVNetworkfields.Add(id);
                TVNetworkfields.Add(name);

                List<TypeValue> TVSeriesfields = new List<TypeValue>();
                TVSeriesfields.Add(id);
                TVSeriesfields.Add(name);
                TVSeriesfields.Add(popularity);




                var thingy = JsonData<collection>(
                        @".\Downloads\collection_ids_02_15_2022.json",
                        Collectionfields, typeof(collection));
                dbcontext.Collections
                    .AddRange((collection[])thingy);
                dbcontext.SaveChanges();
            }
        }

        private Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            return
              assembly.GetTypes()
                      .Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
                      .ToArray();
        }

        class TypeValue
        {
            public TypeValue(Type t, string v)
            {
                type = t;
                value = v;
            }
            public Type? type { get; set; }
            public string? value { get; set; }

        }

        private Object[] JsonData<T>(string filepath, List<TypeValue> fields, Type type)
        {
            string json = System.IO.File.ReadAllText(filepath);

            Type t = type.GetType().GetProperty("Value").PropertyType;
            var proptype = typeof(T);
            var OT = proptype.MakeGenericType(t);


            //dynamic objecttype = type.GetType().GetProperty("Value").GetValue(type, null);

            List<T> collections = new List<T>();

            return null;

            //var options = new JsonDocumentOptions
            //{
            //    AllowTrailingCommas = true,
            //};
            //using (JsonDocument document = JsonDocument.Parse(json, options))
            //{
            //    JsonElement root = document.RootElement;

            //    _logger.LogInformation(root.ValueKind.ToString());

            //    var thing = root.EnumerateObject();
            //    using var thingy = thing.GetEnumerator();

            //    while (thingy.MoveNext())
            //    {
            //        //root object
            //        var current = thingy.Current;
            //        _logger.LogInformation(current.Name);
            //        var array = current.Value.EnumerateArray();


            //        //the big array of objects
            //        while (array.MoveNext())
            //        {
            //            var currant = array.Current;
            //            collection collectio = new collection();
            //            collectio.id = Convert.ToInt32(currant.GetProperty("id").ToString());
            //            collectio.name = currant.GetProperty("name").ToString();
            //            collections.Add(collectio);
            //        }
            //    }

            //}

            //_logger.LogInformation(returnType.ToString());
            //return collections.ToArray<collection>();

        }


        [HttpGet(Name = "GetCollections")]
        public IEnumerable<collection> GetCollections()
        {
            string file = @".\Downloads\collection_ids_02_15_2022.json";
            string json = System.IO.File.ReadAllText(file);

            List<collection> collections = new List<collection>();

            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true,
            };
            using (JsonDocument document = JsonDocument.Parse(json, options))
            {
                JsonElement root = document.RootElement;

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
                        collection collection = new collection();
                        collection.id = Convert.ToInt32(currant.GetProperty("id").ToString());
                        collection.name = currant.GetProperty("name").ToString();
                        collections.Add(collection);
                    }
                }

            }
            return collections;

        }

        //[HttpGet(Name = "GetKeywords")]
        //public IEnumerable<Keyword> GetKeywords()
        //{

        //}
        //[HttpGet(Name = "GetMovies")]
        //public IEnumerable<Movie> GetMovies()
        //{

        //}
        //[HttpGet(Name = "GetPeople")]
        //public IEnumerable<Person> GetPeople()
        //{

        //}
        //[HttpGet(Name = "GetProductionCompanies")]
        //public IEnumerable<ProductionCompany> GetProductionCompanies()
        //{

        //}
        //[HttpGet(Name = "GetTVNetworks")]
        //public IEnumerable<TVNetwork> GetTVNetworks()
        //{

        //}
        //[HttpGet(Name = "GetTVSeries")]
        //public IEnumerable<TVSeries> GetTVSeries()
        //{

        //}
    }
}

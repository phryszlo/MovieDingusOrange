using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MovieDbApi.Data
{
    public class Json
    {
        public static T? GetJsonGenericType<T>(string json)
        {
            return json is not null ?
                JsonSerializer.Deserialize<T>(json)
                : default(T);
        }
    }
    public class person
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }
        public bool adult { get; set; }
        public string? name { get; set; }
        public float? popularity { get; set; }
        public bool video { get; set; }
    }
    public class production_company
    {
        [JsonPropertyName("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [JsonPropertyName("name")]
        public string? name { get; set; }

        public production_company(int id, string? name)
        {
            this.id = id;
            this.name = name;
        }



        public List<production_company> convertList(List<JsonElement> incoming)
        {
            var converted = new List<production_company>();

            //where'd you get this?

            try
            {
                var v = Json.GetJsonGenericType<production_company>(incoming.ToString());

                //production_company pc = new production_company(incoming.)
            }
            catch (Exception)
            {

                throw;
            }

            return converted;
        }
    }
    public class tv_network
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }
        public string? name { get; set; }
    }
    public class movie
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }
        public bool adult { get; set; }
        public string? original_title { get; set; }
        public float? popularity { get; set; }
        public bool video { get; set; }
    }
    public class collection
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }
        public string? name { get; set; }
    }
    public class keyword
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }
        public string? name { get; set; }
    }
    public class tv_series
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }
        public string? original_name { get; set; }
        public float? popularity { get; set; }
    }

}

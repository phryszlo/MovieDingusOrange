using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDbApi.Data
{
    public class person
    {
        public int id { get; set; }
        public bool adult { get; set; }
        public string? name { get; set; }
        public int? popularity { get; set; }
        public bool video { get; set; }
    }
    public class production_company
    {
        public int id { get; set; }
        public string? name { get; set; }
    }
    public class tv_network
    {
        public int id { get; set; }
        public string? name { get; set; }
    }
    public class movie
    {
        public int id { get; set; }
        public bool adult { get; set; }
        public string? original_title { get; set; }
        public int? popularity { get; set; }
        public bool video { get; set; }
    }
    public class collection
    {
        public int id { get; set; }
        public string? name { get; set; }
    }
    public class keyword
    {
        public int id { get; set; }
        public string? name { get; set; }
    }
    public class tv_series
    {
        public int id { get; set; }
        public string? name { get; set; }
        public int? popularity { get; set; }
    }

}

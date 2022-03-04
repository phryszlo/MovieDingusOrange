using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDbApi.Data
{
    public class Person
    {
        public int Id { get; set; }
        public bool Adult { get; set; }
        public string? Name { get; set; }
        public int? Popularity { get; set; }
        public bool Video { get; set; }
    }
    public class Production_Company
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
    public class TV_Network
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
    public class Movie
    {
        public int Id { get; set; }
        public bool Adult { get; set; }
        public string? Original_Title { get; set; }
        public int? Popularity { get; set; }
        public bool Video { get; set; }
    }
    public class Collection
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
    public class Keyword
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
    public class TV_Series
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Popularity { get; set; }
    }

}

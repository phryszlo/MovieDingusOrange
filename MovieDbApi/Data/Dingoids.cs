namespace MovieDbApi.Data
{
    public class Person
    {
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
        public string? Name { get; set; }
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
    }
}

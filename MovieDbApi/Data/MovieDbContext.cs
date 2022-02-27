using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MovieDbApi.Data
{
    internal class MovieDbContext : IdentityDbContext
    {
        public MovieDbContext(DbContextOptions options) : base(options)
        {
        }

        protected MovieDbContext()
        {

        }

        //we are trying out what I assume is a readonly property set.
        //i.e. => Set<> instead of {get;set;} . Via:
        //https://docs.microsoft.com/en-us/learn/modules/persist-data-ef-core/3-migrations
        public DbSet<Person> Persons => Set<Person>();
        public DbSet<Production_Company> Production_Companies => Set<Production_Company>();
        public DbSet<TV_Network> TV_Networks => Set<TV_Network>();
        public DbSet<Movie> Movies => Set<Movie>();
        public DbSet<Collection> Collections => Set<Collection>();
        public DbSet<Keyword> Keywords => Set<Keyword>();
        public DbSet<TV_Series> TV_Serieses => Set<TV_Series>();
    }
}

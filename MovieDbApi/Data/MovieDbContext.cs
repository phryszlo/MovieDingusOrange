using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace MovieDbApi.Data
{
    public class MovieDbContext : IdentityDbContext
    {
        private readonly ILogger<MovieDbContext> _logger;
        public MovieDbContext(DbContextOptions options, ILogger<MovieDbContext> logger) : base(options)
        {
            _logger = logger;
            //Collections = (DbSet<collection>?)GetCollections();
            //_logger.LogInformation(Collections.Count<collection>().ToString());
        }

        public MovieDbContext(DbContextOptions options) : base(options)
        {
            //_logger = logger;
            //Collections = (DbSet<collection>?)GetCollections();
            //_logger.LogInformation(Collections.Count<collection>().ToString());
        }

        public MovieDbContext()
        {


        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("PGConnection");
                optionsBuilder.UseNpgsql(connectionString);
            }
        }

        //we are trying out what I assume is a readonly property set.
        //i.e. => Set<> instead of {get;set;} . Via:
        //https://docs.microsoft.com/en-us/learn/modules/persist-data-ef-core/3-migrations

        //FYI the names of the DbSets become the table name in the migration
        public DbSet<person> Persons => Set<person>();
        public DbSet<production_company> ProductionCompanies => Set<production_company>();
        public DbSet<tv_network> TVNetworks => Set<tv_network>();
        public DbSet<movie> Movies => Set<movie>();
        public DbSet<collection> Collections { get; set; }
        public DbSet<keyword> Keywords => Set<keyword>();
        public DbSet<tv_series> TVSerieses => Set<tv_series>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            

            //builder.Entity<collection>().HasData((DbSet<collection>?)GetCollections());
        }

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


    }
}

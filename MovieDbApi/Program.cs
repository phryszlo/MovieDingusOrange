global using static MovieDingus.Shared.Globals;
using Microsoft.EntityFrameworkCore;
using MovieDingus.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("PGConnection");

//Per some site.
//NuGet: ms.aspnetcore.diagnostics.entityfr..
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDbContext<MovieDbContext>(opt =>
    opt.UseNpgsql(connectionString));


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();



//ConfigurationManager configaro = builder.Configuration;
//IWebHostEnvironment environs = builder.Environment;
//string mdb_export_url = builder.Configuration.GetConnectionString("mdb_download_root");
//mdb_dl_root = builder.Configuration.GetConnectionString("mdb_download_root"); ;
//mdb_tvnetwork_segment = builder.Configuration.GetConnectionString("mdb_tv_network_ids"); ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

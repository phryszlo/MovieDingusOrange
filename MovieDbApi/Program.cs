using Microsoft.EntityFrameworkCore;
using MovieDbApi.Data;

var builder = WebApplication.CreateBuilder(args);

//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();
//builder.Services.AddLogging(config => config.AddConsole())
//    .AddTransient<Utilities>();

//Log.Logger = new LoggerConfiguration()
//    .MinimumLevel.Debug()
//    .WriteTo.Console()   //.File(@"logs\loglog.txt")
//    .CreateLogger();

//Log.Information("starting");

//builder.Logging.AddSerilog();






var connectionString = builder.Configuration.GetConnectionString("PGConnection");
var connStringSQL = builder.Configuration.GetConnectionString("SQLConnection");
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//NuGet: ms.aspnetcore.diagnostics.entityfr..
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDbContext<MovieDbContext>(opt =>
    //opt.UseNpgsql(connectionString));
    opt.UseSqlServer(connStringSQL));


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

//app.UseStaticFiles();

//app.UseRouting();

//app.MapBlazorHub();
//app.MapFallbackToPage("/_Host");
app.UseAuthorization();
app.MapControllers();

app.Run();

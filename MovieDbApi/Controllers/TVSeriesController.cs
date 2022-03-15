#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieDbApi.Data;
using System.Text.Json;
using System.Net;

namespace MovieDbApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TVSeriesController : ControllerBase
    {
        private readonly MovieDbContext _context;
        private readonly ILogger<TVSeriesController> _logger;
        private readonly IConfiguration _configuration;
        internal static HttpClient httpClient = new HttpClient();
        public TVSeriesController(MovieDbContext context, IConfiguration configuration, ILogger<TVSeriesController> logger)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        public class TVSeriesParameters
        {
            const int maxPageSize = 500;
            public int PageNumber { get; set; } = 1;

            private int _pageSize = 10;
            public int PageSize
            {
                get
                {
                    return _pageSize;
                }
                set
                {
                    _pageSize = (value > maxPageSize) ? maxPageSize : value;
                }
            }
        }

        // GET: api/TVNetwork
        //[FromQuery]TVSeriesParameters parameters
        [HttpGet]
        public async Task<ActionResult<List<tv_series>>> GetTVSerieses([FromQuery] TVSeriesParameters parameters)
        {
            var foo = await _context.TVSerieses
                .Skip(parameters.PageNumber)
                .Take(parameters.PageSize)
                .ToListAsync();


            Stopwatch sw = Stopwatch.StartNew();


            sw.Stop();
            MethodBase? m = MethodBase.GetCurrentMethod();
            Debug.WriteLine("{0}.{1}: {2} seconds",
                m.ReflectedType.Name,
                m.Name,
                sw.Elapsed.TotalSeconds.ToString());

            return foo;


        }

        public string imageUrlRoot 
            = "https://image.tmdb.org/t/p/w500";
        public string tvShowRequestRoot
            = "https://api.themoviedb.org/3/tv/";  //70796?api_key=532b7a2c6a824e4b0529b971cea5ae63&language=en-US";


        // GET: api/TVNetwork/5
        [HttpGet("all/{id}")]
        public async Task<ActionResult<List<string>>> GetTVSeries(int id)
        {
            //var tv_series = await _context.TVSerieses.FindAsync(id);
            //var tv_id = tv_series.id;
            string api_key = _configuration["API_KEY"];

            HttpResponseMessage json_response =
                await httpClient.GetAsync(tvShowRequestRoot
                + id.ToString() + "?api_key=" + api_key);

            var stringifiedJson = await json_response.Content.ReadAsStringAsync();
            var document = JsonDocument.Parse(stringifiedJson);
            var enumerator = document.RootElement.EnumerateObject()
                   .GetEnumerator();
            List<string> props = new List<string>();
            string prop;
            while (enumerator.MoveNext())
            {
                try
                {
                    prop = enumerator.Current.Name;
                    props.Add(enumerator.Current.Value.ValueKind.ToString()
                        + ": " + prop);

                }
                catch (Exception e)
                {
                    _logger?.LogInformation(e.ToString());
                    throw;
                }
            }

            return props;
            /*
             * string, object, array, number, true, false
             */
        }

        // GET: api/TVNetwork/5
        [HttpGet("backdrop/{id}")]
        public async Task<ActionResult> GetTVSeriesBackdrop(int id)
        {
            //var tv_series = await _context.TVSerieses.FindAsync(id);
            //var tv_id = tv_series.id;
            string api_key = _configuration["API_KEY"];

            HttpResponseMessage json_response = 
                await httpClient.GetAsync(tvShowRequestRoot 
                + id.ToString() + "?api_key=" + api_key);

            var stringifiedJson = await json_response.Content.ReadAsStringAsync();
            var document = JsonDocument.Parse(stringifiedJson);
            var next_episode = document.RootElement.GetProperty("next_episode_to_air").ToString();
            var enummer = document.RootElement.EnumerateObject()
                   .Where(it => it.Name.Contains("backdrop_path") )
                   //&& it.Value.ValueKind == JsonValueKind.String)
                   .GetEnumerator();
            string img_path = "";
            Byte[] b = new Byte[] { };
            while (enummer.MoveNext())
            {
                try
                {
                    if (enummer.Current.Value.ValueKind != JsonValueKind.False
                        && enummer.Current.Value.ValueKind != JsonValueKind.Undefined)
                    {
                        _logger.LogInformation("rootenum: {0}", enummer.Current.ToString());
                        img_path = imageUrlRoot + enummer.Current.Value.ToString();
                        HttpResponseMessage response = await httpClient.GetAsync(img_path);
                        b = await response.Content.ReadAsByteArrayAsync();
                    }

                }
                catch (Exception e)
                {
                    _logger?.LogInformation(e.ToString());
                    throw;
                }
            }

            return File(b, "image/jpeg");
        }

        // PUT: api/TVNetwork/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Puttv_series(int id, tv_series tv_series)
        {
            if (id != tv_series.id)
            {
                return BadRequest();
            }

            _context.Entry(tv_series).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tv_seriesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TVNetwork
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<tv_series>> Posttv_series(tv_series tv_series)
        {
            _context.TVSerieses.Add(tv_series);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Gettv_series", new { id = tv_series.id }, tv_series);
        }

        // DELETE: api/TVNetwork/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletetv_series(int id)
        {
            var tv_series = await _context.TVSerieses.FindAsync(id);
            if (tv_series == null)
            {
                return NotFound();
            }

            _context.TVSerieses.Remove(tv_series);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool tv_seriesExists(int id)
        {
            return _context.TVSerieses.Any(e => e.id == id);
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace MovieDbApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieExportsController : Controller
    {
        private readonly ILogger<MovieExportsController> _logger;

        public MovieExportsController(ILogger<MovieExportsController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}

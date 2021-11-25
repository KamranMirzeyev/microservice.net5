using ASP.NET_Web.Exceptions;
using ASP.NET_Web.Models;
using ASP.NET_Web.Services.Interfaces;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ASP.NET_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICatalogService _catalogService;

        public HomeController(ILogger<HomeController> logger, ICatalogService catalogService)
        {
            _logger = logger;
            _catalogService = catalogService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Detail(string id)
        {
            return View(await _catalogService.GetByCourseId(id));
        }

       
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var errorFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();

            if (errorFeature != null && errorFeature.Error is UnAuthorizeException)
            {
                return RedirectToAction(nameof(AuthController.Logout), "Auth");
            }

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

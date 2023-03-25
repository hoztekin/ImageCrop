using Crop3.Models;
using LazZiya.ImageResize;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Drawing;

namespace Crop3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        
        {
            var img = Image.FromFile("wwwroot\\uploads\\orjinal.jpg");
            var scaleImage = ImageResize.Scale(img, 100, 100);
            scaleImage.SaveAs("wwwroot\\uploads\\resize.jpg");
            var cropimage = ImageResize.Crop(img, 200, 200, spot: TargetSpot.Center);
            cropimage.SaveAs("wwwroot\\uploads\\crop.jpg");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
using Crop4.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Crop4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveCropedImage(string filename, IFormFile blob) 
        {
            try
            {
                using (var image = Image.Load(blob.OpenReadStream()))
                {
                    var uploadDir = @"Images";
                    filename = Guid.NewGuid().ToString() + "-" + filename;
                    var path = Path.Combine(webHostEnvironment.WebRootPath, uploadDir, filename);
                    image.Mutate(x => x.Resize(300,300));
                    image.Save(path);   
                }
                return Json(new { Message="Ok"});
            }
            catch (Exception)
            {

                return Json(new { Message = "Error" });
            }
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
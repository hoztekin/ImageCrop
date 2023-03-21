using Microsoft.AspNetCore.Mvc;

namespace Crop1.Controllers
{
	public class ImageController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Index(IFormFile file)
		{
			string fileName = string.Empty;
			string path = string.Empty;
			if (file != null)
			{
				fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
				path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/upload"));
				string fullPath = Path.Combine(path, fileName);
				using (var image = Image.Load(file.OpenReadStream()))
				{
					string newSize = ReSizeImage(image, 400, 400);
					string[] aSize = newSize.Split(',');
					image.Mutate(h => h.Resize(Convert.ToInt32(aSize[1]), Convert.ToInt32(aSize[0])));
					image.Save(fullPath);
					TempData["success"] = "Success";
				}
			}
			return View();
		}
		public string ReSizeImage(Image img, int maxWidth, int maxHeight)
		{
			if (img.Width > maxWidth || img.Height > maxHeight)
			{
				double widthRatio = (double)img.Width / (double)maxWidth;
				double heightRatio = (double)img.Height / (double)maxHeight;
				double ratio = Math.Max(widthRatio, heightRatio);
				int newWidth = (int)(img.Width / ratio);
				int newHeight = (int)(img.Height / ratio);
				return newHeight.ToString() + "," + newWidth.ToString();
			}
			else
			{
				return img.Height.ToString() + "," + img.Width.ToString();
			}
		}
	}
}

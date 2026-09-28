using Microsoft.AspNetCore.Mvc;

namespace StudentsMVC.Controllers
{
    public class HomeController : Controller
    {
        // ContentResult writes the specified content directly to the HTTP response.
        // When an action returns a string, ASP.NET Core creates a ContentResult automatically.
        public string Square(int a, int h)
        {
            double s = a * h / 2;
            return "<h2>Triangle area with base " + a +
                    " and height " + h + " is " + s + "</h2>";
        }

        public IActionResult GetHtml()
        {
            return new HtmlResult("<h2>Hello, world!</h2>");
        }

        public FileResult GetFile()
        {
            // File path
            string file_path = "~/Image/IMG_20170504_170840.jpg";
            // File content type
            string file_type = "image/jpeg";
            // Download file name (optional)
            string file_name = "Capri.jpg";
            return File(file_path, file_type, file_name);
        }

        public ViewResult SomeMethod()
        {
            ViewBag.Name = "MS SQL Server";
            ViewData["Head"] = "Entity Framework Core";
            return View("~/Views/Home/Index.cshtml");
        }

        public IActionResult Index()
        {
            ViewBag.Name = "ASP.NET Core MVC";
            ViewData["Head"] = "ASP.NET Core Razor Pages";
            return View();
        }

        public RedirectResult RedirectMethod()
        {
            return Redirect("/Home/Index");
        }
    }
}
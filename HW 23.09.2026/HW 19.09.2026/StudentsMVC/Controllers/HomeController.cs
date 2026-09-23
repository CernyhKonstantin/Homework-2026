using Microsoft.AspNetCore.Mvc;

namespace StudentsMVC.Controllers
{
    public class HomeController : Controller
    {
        // ContentResult: записує вказаний контент безпосередньо у відповідь у вигляді рядка
        // Якщо як результат повертається тип string, фреймворк
        // автоматично створить об'єкт ContentResult для рядка, що повертається.
        public string Square(int a, int h)
        {
            double s = a * h / 2;
            return "<h2>Площа трикутника з основою " + a +
                    " та висотою " + h + " дорівнює " + s + "</h2>";
        }

        public IActionResult GetHtml()
        {
            return new HtmlResult("<h2>Привіт, світе!</h2>");
        }

        public FileResult GetFile()
        {
            // Шлях до файлу
            string file_path = "~/Image/IMG_20170504_170840.jpg";
            // Тип файлу — content-type
            string file_type = "image/jpeg";
            // Ім'я файлу — необов'язково
            string file_name = "Капрі.jpg";
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
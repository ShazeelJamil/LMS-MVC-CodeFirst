using LMS_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using System.Diagnostics;
using System.Web;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LMS_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public ViewResult Index()
        {
            ViewBag.Title = "Home";
            return View();
        }
        public ViewResult Contact()
        {
            ViewBag.Title = "Contact";
            return View();
        }
       
        [HttpGet,HttpPost]
        public ActionResult SignIn()
        {
            ViewBag.Title = "Sign In";
            if (Request.Method == "POST")
            {

                string id = Request.Form["id"];
                string password = Request.Form["password"];
                if (char.ToLower(id[0]) == 'l' && char.ToLower(id[1]) == 'b')
                {

                    LmsMvcContext handle = new LmsMvcContext();
                    var usr = handle.Users.Where(usr => usr.Id == id && usr.Password == password).First();
                    CookieOptions option = new CookieOptions();
                    HttpContext.Response.Cookies.Append("user_id", id, option);
                    if (usr != null && usr.Id == id && usr.Password == password)
                        return RedirectToAction("Index", "Librarian");

                }
                if (char.ToLower(id[0]) == 'a' && char.ToLower(id[1]) == 'd')
                {
                    CookieOptions option = new CookieOptions();
                    HttpContext.Response.Cookies.Append("user_id", id, option);
                    LmsMvcContext handle = new LmsMvcContext();
                    var Admin = handle.Users.First();
                    if (Admin.Id == id && Admin.Password == password)
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                }
                return View();
            }
            else
            {
                return View();
            }
            /*return View();*/
        }
        public ViewResult SearchBook()
        {
            ViewBag.Title = "Search Book";
            return View();
        }
        public ViewResult EBook()
        {
            ViewBag.Title = "E-Book";
            return View();
        }
        public ViewResult EMagzine()
        {
            ViewBag.Title = "E-Magzine";
            return View();
        }



    }
}
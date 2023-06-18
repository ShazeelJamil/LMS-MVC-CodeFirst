using LMS_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace LMS_MVC.Views.Shared.Components
{
    public class SignIn : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            if (Request.Method == "POST")
            {
                Console.WriteLine("Insode Invoke");
                string id = Request.Form["id"];
                string password = Request.Form["password"];
                if (char.ToLower(id[0]) == 'l' && char.ToLower(id[1]) == 'b')
                {

                    LmsMvcContext handle = new LmsMvcContext();
                    var usr = handle.Users.Where(usr => usr.Id == id && usr.Password == password).First();
                    CookieOptions option = new CookieOptions();
                    HttpContext.Response.Cookies.Append("user_id", id, option);
                    if (usr != null && usr.Id == id && usr.Password == password)
                        return RedirectToActionResult("Index", "Librarian");

                }
                if (char.ToLower(id[0]) == 'a' && char.ToLower(id[1]) == 'd')
                {
                    CookieOptions option = new CookieOptions();
                    HttpContext.Response.Cookies.Append("user_id", id, option);
                    LmsMvcContext handle = new LmsMvcContext();
                    var Admin = handle.Users.First();
                    if (Admin.Id == id && Admin.Password == password)
                    {
                        return RedirectToActionResult("Index", "Admin");
                    }
                }
                return View();
            }
            else
            {
                return View();
            }
        }

        private IViewComponentResult RedirectToActionResult(string v1, string v2)
        {
            throw new NotImplementedException();
        }
    }
}

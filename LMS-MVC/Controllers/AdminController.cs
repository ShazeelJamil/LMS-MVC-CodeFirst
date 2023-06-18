using Microsoft.AspNetCore.Mvc;
using LMS_MVC.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Hosting.Server;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Text.Json;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace LMS_MVC.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            if (!HttpContext.Request.Cookies.ContainsKey("user_id"))
            {
                return RedirectToAction("SignIn", "Home");
            }
            ViewBag.Title = "Index";
            string usr_id = HttpContext.Request.Cookies["user_id"];
            LmsMvcContext handle = new LmsMvcContext();
            var user_data = handle.Users.Where(usr => usr.Id == usr_id).First();
            ViewBag.name=user_data.Name;
            List<Book> ls = handle.Books.ToList();
            ViewBag.total_books = 0;
            foreach (var item in ls)
            {
                ViewBag.total_books += item.Stock;
            }
            ViewBag.total_borrow = handle.Borrows.Where(e => e.IssueDate.Date == DateTime.Now.Date).Count();
            return View();
        }
        [HttpGet,HttpPost]
        public IActionResult Change_Password()
        {
            if (!HttpContext.Request.Cookies.ContainsKey("user_id"))
            {
                return RedirectToAction( "SignIn" , "Home" );
            }
            ViewBag.Title = "Change-Password";
            if (Request.Method=="POST")
            {
                try
                {
                    string pass = Request.Form["password"];
                    LmsMvcContext handle = new LmsMvcContext();
                    if (HttpContext.Request.Cookies.ContainsKey("user_id"))
                    {
                        string usr_id = HttpContext.Request.Cookies["user_id"];
                        var user_data = handle.Users.Where(usr => usr.Id == usr_id).First();
                        user_data.Password = pass;
                        handle.SaveChanges();   
                        ViewBag.status = "success";
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.status = "error";
                }
                return View();
            }
            return View();
        }
        public IActionResult Profile()
        {
            if (!HttpContext.Request.Cookies.ContainsKey("user_id"))
            {
                return RedirectToAction( "SignIn" , "Home" );
            }
            string id = HttpContext.Request.Cookies["user_id"];
            LmsMvcContext handle = new LmsMvcContext();
            User user = handle.Users.Where(u => u.Id == id).First();
            Admin admin = handle.Admins.Where(u => u.UserId == id).First();
            ViewBag.Title = "Profile";
            return View(admin);  
        }
        public IActionResult Delete_Librarian()
        {
            if (!HttpContext.Request.Cookies.ContainsKey("user_id"))
            {
                return RedirectToAction( "SignIn" , "Home" );
            }
            ViewBag.Title = "Delete-Librarian";
            return View();
        }
        [HttpGet,HttpPost]
        public IActionResult Update_Librarian(string u_id="")
        {
            if (!HttpContext.Request.Cookies.ContainsKey("user_id"))
            {
                return RedirectToAction( "SignIn" , "Home" );
            }
            ViewBag.Title = "Update-Librarian";
            LmsMvcContext handle = new LmsMvcContext();
            if (Request.Method=="GET" && u_id != "")
            {
                var s = handle.Users.Where(e => e.Id == u_id && e.Id.StartsWith("LB")).FirstOrDefault();
                if (s == null)
                {

                    var resp = new
                    {
                        Status = "NotFinded",
                    };
                    return Json(JsonSerializer.Serialize(resp));
                }

                var z = handle.Librarians.Where(e => e.UserId == s.Id).First();
                var respn = new
                {
                    Status = "Finded",
                    user = s,
                    qual=z.Qualification,
                    work=z.WorkShift,
                    sallary=z.Salary,
                    gender=z.Gender,
                    cnic=z.Cnic,
            };
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    MaxDepth = 64
                };

                string jsonString = JsonSerializer.Serialize(respn,options);
                return Json(jsonString);
            }

            if (Request.Method == "POST")
            {
                try
                {
                   
                    if (HttpContext.Request.Cookies.ContainsKey("user_id"))
                    {
                        bool active_status = false;
                        string gender = "female";
                        string shift = "evening";
                        string id = Request.Form["id"];
                        DateTime dob = DateTime.Parse(Request.Form["dob"]);
                        if (Request.Form["active"] == "1")
                        {
                            active_status = true;
                        }
                        if (Request.Form["gender"] == "male")
                        {
                            gender = "male";
                        }
                        if (Request.Form["workshift"] == "morning")
                        {
                            shift = "morning";
                        }
                        var usr=handle.Users.Where(e => e.Id == id).First();
                        usr.Password = Request.Form["password"];
                        usr.Email = Request.Form["email"];
                        usr.Name = Request.Form["name"];
                        usr.IsActive = active_status;
                        usr.Contact = Request.Form["contact"];
                        usr.DateOfBirth = dob;

                        var lb = handle.Librarians.Where(e => e.UserId == id).First();

                        lb.Cnic = Request.Form["cnic"];
                        lb.DateOfJoining = DateTime.Now;
                        lb.Salary = Convert.ToInt32(Request.Form["sallary"]);
                        lb.Qualification = Request.Form["qualification"];
                        lb.Gender = gender;
                        lb.WorkShift = shift;
                        lb.IsActive = active_status;
                        handle.SaveChanges();
                        ViewBag.status = "success";
                        ViewBag.assignid = id;
                        return View();
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.status = "error";
                }
                return View();
            }

            return View();
        }
        [HttpGet,HttpPost]
        public IActionResult Search_Book()
        {
            if (!HttpContext.Request.Cookies.ContainsKey("user_id"))
            {
                return RedirectToAction( "SignIn" , "Home" );
            }
            ViewBag.Title = "Search-Book";
            if (Request.Method=="POST")
            {
    
                LmsMvcContext handle = new LmsMvcContext();
                string book = Request.Form["nameofbook"];
                List<Book> listofbook = handle.Books.Where(e => e.Title.ToLower() == book.ToLower()).ToList();
                if (listofbook.Count ==0)
                {
                    ViewBag.status = "NotFinded";
                    return View(new List<Book>());
                }    
                return View(listofbook);
            }
           
            return View(new List<Book>());
        }

        [HttpDelete,HttpGet]
        public IActionResult Request_Librarian(string user_id)
        {
            if (!HttpContext.Request.Cookies.ContainsKey("user_id"))
            {
                return RedirectToAction( "SignIn" , "Home" );
            }
            LmsMvcContext handle = new LmsMvcContext();
            var s = handle.Users.Where(e => e.Id == user_id && e.Id.StartsWith("LB")).FirstOrDefault();
   
            if (Request.Method == "GET")
            {
                if (s== null)
                {

                    var resp = "{\"Status\":\"NotFinded\"}";
                    return Content(resp, "application/json");
                }
                var res = "{\"Status\":\"Finded\", \"id\":\"" + s.Id + "\", \"name\":\"" + s.Name + "\"}";
                return Content(res, "application/json");


            }
            if (s==null)
            {
                var respon = "{\"Status\":\"NotDeleted\"}";
                return Content(respon, "application/json");
            }
            DeleteFile(s.Image);
            Librarian dl = handle.Librarians.Where(e => e.UserId == s.Id).First();
            handle.Librarians.Remove(dl);
            handle.SaveChanges();
            handle.Users.Remove(s);
            handle.SaveChanges();
            var response = "{\"Status\":\"Deleted\"}";
            return Content(response, "application/json");
        }
        [HttpGet,HttpPost]
        public IActionResult Add_Librarian()
        {
            if (!HttpContext.Request.Cookies.ContainsKey("user_id"))
            {
                return RedirectToAction( "SignIn" , "Home" );
            }
            ViewBag.Title = "Add-Librarian";
            if (Request.Method == "POST")
            {
                try
                {
                    LmsMvcContext handle = new LmsMvcContext();
                    if (HttpContext.Request.Cookies.ContainsKey("user_id"))
                    {
                        string usr_id = HttpContext.Request.Cookies["user_id"];
                        var user_data = handle.Users.Where(usr => usr.Id == usr_id).First();
                        int unique_id = Gen_uni_random();
                        string new_id = "LB-" + unique_id.ToString();
                        bool active_status = false;
                        string gender = "female";
                        string shift = "evening";

                        DateTime dob = DateTime.Parse(Request.Form["dob"]);
                        if (Request.Form["active"] == "1")
                        {
                            active_status = true;
                        }
                        if (Request.Form["gender"] == "male")
                        {
                            gender = "male";
                        }
                        if (Request.Form["workshift"] == "morning")
                        {
                            shift = "morning";
                        }
                        string image = SaveImage(Request.Form.Files["img"], unique_id.ToString());
                        User usr = new User()
                        {
                            Id = new_id,
                            Password = Request.Form["password"],
                            Email = Request.Form["email"],
                            Name = Request.Form["name"],
                            IsActive = active_status,
                            Contact = Request.Form["contact"],
                            DateOfBirth = dob,
                            Image = image
                        };
                        Librarian lb = new Librarian()
                        {
                            Cnic = Request.Form["cnic"],
                            DateOfJoining = DateTime.Now,
                            Salary = Convert.ToInt32(Request.Form["sallary"]),
                            Qualification = Request.Form["qualification"],
                            Gender = gender,
                            WorkShift = shift,
                            UserId = new_id,
                            IsActive = active_status
                        };
                        handle.Users.Add(usr);
                        handle.SaveChanges();
                        handle.Librarians.Add(lb);
                        handle.SaveChanges();
                        ViewBag.assignid = new_id;
                        ViewBag.status = "success";
                        return View();
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.status = "error";
                }
                return View();
            }

                return View();
        }
        [HttpGet]
        public IActionResult Search_Borrower()
        {
            if (!HttpContext.Request.Cookies.ContainsKey("user_id"))
            {
                return RedirectToAction( "SignIn" , "Home" );
            }
            ViewBag.Title = "Search_Borrower";
            LmsMvcContext handle = new LmsMvcContext();
            List<Borrow> ls = handle.Borrows.Where(e => e.IsReturn == false).ToList();
            return View(ls);
        }
        [HttpGet,HttpPost]
        public IActionResult Borrowers_List()
        {
            if (!HttpContext.Request.Cookies.ContainsKey("user_id"))
            {
                return RedirectToAction( "SignIn" , "Home" );
            }
            ViewBag.Title = "Borrowers_List";
            LmsMvcContext handle = new LmsMvcContext();
            if (Request.Method == "POST")
            {
                DateTime todate = DateTime.Parse(Request.Form["todate"]);
                DateTime fromdate = DateTime.Parse(Request.Form["fromdate"]);
                List<Borrow> lst = handle.Borrows.Where(e => e.IssueDate.Date >= fromdate.Date && e.IssueDate <= todate.Date).ToList();
                return View(lst);
            }
            DateTime currentDate = DateTime.Now.Date;
            List<Borrow> ls = handle.Borrows.Where(e => e.IssueDate.Date == currentDate).ToList();
            return View(ls);
        }
        private int Gen_uni_random()
        {
            Random random = new Random();
            List<int> generatedNumbers = new List<int>();

            int randomNumber;
            do
            {
                randomNumber = random.Next(10000, 99999);
            }
            while (generatedNumbers.Contains(randomNumber));

            generatedNumbers.Add(randomNumber);
            return randomNumber;
        }

        public IActionResult logout()
        {
            if (!HttpContext.Request.Cookies.ContainsKey("user_id"))
            {
                return RedirectToAction( "SignIn" , "Home" );
            }
            HttpContext.Response.Cookies.Delete("user_id");
            return RedirectToAction( "SignIn" , "Home" );
        }

        private string DeleteFile(string image_name)
        {
            string Directory = Path.Combine(_hostingEnvironment.WebRootPath, "media", "images", image_name);
            if (System.IO.File.Exists(Directory))
            {
                System.IO.File.Delete(Directory);
                return "Deleted";
            }
            else
            {
                return "NotDeleted";
            }
        }
        private readonly IWebHostEnvironment _hostingEnvironment;

        public AdminController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        private string SaveImage(IFormFile fileobj,string name)
        {
           
                IFormFile file = fileobj;
                string saveDirectory = Path.Combine(_hostingEnvironment.WebRootPath, "media", "images");
                string fileExtension = Path.GetExtension( fileobj.FileName);
                string fileName = name+fileExtension;
                string filePath = Path.Combine(saveDirectory, fileName);
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fs);
                }
                return fileName;
        }

    }
}

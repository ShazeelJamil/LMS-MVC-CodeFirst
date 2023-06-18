using LMS_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.Reflection.Metadata;
using System.Linq.Expressions;
using System.Linq;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace LMS_MVC.Controllers
{
    public class LibrarianController : Controller
    {
        public IActionResult Index()
        {
            if (!HttpContext.Request.Cookies.ContainsKey("user_id"))
            {
                return RedirectToAction( "SignIn" , "Home" );
            }
            ViewBag.Title = "Index";
            string usr_id = HttpContext.Request.Cookies["user_id"];
            LmsMvcContext handle = new LmsMvcContext();
            var user_data = handle.Users.Where(usr => usr.Id == usr_id).First();
            ViewBag.name = user_data.Name;
            List<Book> ls = handle.Books.ToList();
             ViewBag.total_books = 0;
            foreach (var item in ls)
            {
                ViewBag.total_books += item.Stock;
            }
            ViewBag.total_borrow = handle.Borrows.Where(e => e.IssueDate.Date == DateTime.Now.Date).Count();
            return View();
        }
        [HttpGet, HttpPost]
        public IActionResult Change_Password()
        {
            if (!HttpContext.Request.Cookies.ContainsKey("user_id"))
            {
                return RedirectToAction( "SignIn" , "Home" );
            }
            ViewBag.Title = "Change-Password";
            if (Request.Method == "POST")
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
        [HttpGet, HttpPost]
        public IActionResult Add_Student()
        {
            if (!HttpContext.Request.Cookies.ContainsKey("user_id"))
            {
                return RedirectToAction( "SignIn" , "Home" );
            }
            ViewBag.Title = "Add Student";
            if (Request.Method == "POST")
            {
                try
                {
                    LmsMvcContext handle = new LmsMvcContext();
                    if (HttpContext.Request.Cookies.ContainsKey("user_id"))
                    {
                        if (Request.Method == "POST")
                        {
                            int unique_id = Gen_uni_random();
                            string new_id = "ST-" + unique_id.ToString();
                            string image = SaveImage(Request.Form.Files["img"], unique_id.ToString());

                            DateTime dob = DateTime.Parse(Request.Form["dob"]);

                            User usr = new User()
                            {
                                Id = new_id,
                                Name = Request.Form["name"],
                                Password = Request.Form["password"],
                                Email = Request.Form["email"],
                                DateOfBirth = dob,
                                Contact = Request.Form["contact"],
                                Image = image,
                                IsActive = true
                            };
                            Student student = new Student()
                            {
                                StudentId = Request.Form["studentid"],
                                Session = Request.Form["session"],
                                Degree = Request.Form["degree"],
                                Department = Request.Form["departement"],
                                SessionStartYear = Convert.ToInt32(Request.Form["syear"]),
                                SessionEndYear = Convert.ToInt32(Request.Form["eyear"]),
                                UserId = new_id

                            };
                            handle.Users.Add(usr);
                            handle.SaveChanges();
                            handle.Students.Add(student);
                            handle.SaveChanges();
                            ViewBag.assignid = new_id;
                            ViewBag.status = "success";
                            return View();
                        }
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
            string usr_id = HttpContext.Request.Cookies["user_id"];
            LmsMvcContext handle = new LmsMvcContext();
            User usr = handle.Users.Where(e => e.Id == usr_id).FirstOrDefault();
            Librarian lbr = handle.Librarians.Where(e => e.UserId == usr_id).FirstOrDefault();

            MyViewModel viewModel = new MyViewModel
            {
                User = usr,
                Librarian = lbr
            };
            ViewBag.Title = "Profile";
            return View(viewModel);
        }
        [HttpGet, HttpPost]
        public ViewResult Search_Book()
        {
            ViewBag.Title = "Search-Book";
            if (Request.Method == "POST")
            {

                LmsMvcContext handle = new LmsMvcContext();
                string book = Request.Form["nameofbook"];
                List<Book> listofbook = handle.Books.Where(e => e.Title.ToLower() == book.ToLower()).ToList();
                if (listofbook.Count == 0)
                {
                    ViewBag.status = "NotFinded";
                    return View(new List<Book>());
                }
                return View(listofbook);
            }

            return View(new List<Book>());
        }
        [HttpGet, HttpPost]
        public IActionResult Add_Book()
        {
            if (!HttpContext.Request.Cookies.ContainsKey("user_id"))
            {
                return RedirectToAction( "SignIn" , "Home" );
            }
            ViewBag.Title = "Add-Book";
            try
            {
                if (HttpContext.Request.Cookies.ContainsKey("user_id"))
                {
                    if (Request.Method == "POST")
                    {
                        string usr_id = HttpContext.Request.Cookies["user_id"];
                        LmsMvcContext handle = new LmsMvcContext();
                        int unique_id = Gen_uni_random();
                        string new_id = "BK-" + unique_id.ToString();
                        string title = Request.Form["title"];
                        string author = Request.Form["author"];
                        string isbn = Request.Form["isbn"];
                        string edition = Request.Form["edition"];
                        string price = Request.Form["price"];
                        string publisher = Request.Form["publisher"];
                        string stock = Request.Form["stock"];
                        string description = Request.Form["description"];
                        string image = SaveImage(Request.Form.Files["img"], unique_id.ToString(), "Book");

                        Book book = new Book()
                        {
                            Title = title,
                            Description = description,
                            Isbn = isbn,
                            Price = Convert.ToDouble(price),
                            Author = author,
                            Publisher = publisher,
                            Stock = Convert.ToInt32(stock),
                            Available = Convert.ToInt32(stock),
                            Edition = Convert.ToInt32(edition),
                            Image = image,
                            AddBookByLibrarianId = usr_id

                        };
                        handle.Books.Add(book);
                        handle.SaveChanges();
                        ViewBag.status = "success";
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.status = "error";
            }
            return View();
        }
        [HttpGet,HttpPost]
        public IActionResult Add_EBook()
        {
            if (!HttpContext.Request.Cookies.ContainsKey("user_id"))
            {
                return RedirectToAction( "SignIn" , "Home" );
            }
            ViewBag.Title = "Add-EBook";
            if (Request.Method == "POST")
            {
                try
                {
                    LmsMvcContext handle = new LmsMvcContext();
                    if (HttpContext.Request.Cookies.ContainsKey("user_id"))
                    {
                        if (Request.Method == "POST")
                        {
                            int num = Gen_uni_random();
                          
                            string path = SaveImage(Request.Form.Files["pdf"], "eb-" + num.ToString(), "E-Book");
                            string cover = SaveImage(Request.Form.Files["coverphoto"], "eb-" + num.ToString(), "E-Book");
                            Ebook bk = new Ebook()
                            {
                                Title = Request.Form["title"],
                                Description = Request.Form["description"],
                                Isbn = Request.Form["isbn"],
                                Author = Request.Form["author"],
                                Edition = Convert.ToInt32(Request.Form["edition"]),
                                Publisher = Request.Form["publisher"],
                                PathOfFile=path,
                                CoverImage=cover

                            };
                            handle.Ebooks.Add(bk);
                            handle.SaveChanges();   
                            ViewBag.status = "success";
                            return View();
                        }

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
        [HttpGet, HttpPost]
        public IActionResult Add_EMagzine()
        {
            if (!HttpContext.Request.Cookies.ContainsKey("user_id"))
            {
                return RedirectToAction( "SignIn" , "Home" );
            }
            ViewBag.Title = "Add-EMagzine";
            if (Request.Method == "POST")
            {
                try
                {
                    LmsMvcContext handle = new LmsMvcContext();
                    if (HttpContext.Request.Cookies.ContainsKey("user_id"))
                    {
                        if (Request.Method == "POST")
                        {
                            int num = Gen_uni_random();
                            string path = SaveImage(Request.Form.Files["pdf"], "mg-"+num.ToString(), "E-Magzine");
                            string cover = SaveImage(Request.Form.Files["coverphoto"], "mg-"+num.ToString(), "E-Magzine");

                            Emagazine mz = new Emagazine()
                            {
                                Title=Request.Form["title"],
                                Writer= Request.Form["writer"],
                                Issn = Request.Form["issn"],
                                PathOfFile=path,
                                CoverImage=cover
                            };
                            handle.Emagazines.Add(mz);
                            handle.SaveChanges();
                            ViewBag.status = "success";
                            return View();
                        }
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
        public IActionResult Update_Book(string isbn="")
        {
            if (!HttpContext.Request.Cookies.ContainsKey("user_id"))
            {
                return RedirectToAction( "SignIn" , "Home" );
            }
            ViewBag.Title = "Update-Book";
            if (Request.Method == "GET" && isbn == "")
            {
                return View();
            }
            LmsMvcContext handle = new LmsMvcContext();
            var s = handle.Books.Where(e => e.Isbn == isbn).FirstOrDefault();
            if (Request.Method == "GET" && isbn != "")
            {
                if (s == null)
                {
                    var resp = "{\"Status\":\"NotFinded\"}";
                    return Content(resp, "application/json");
                }
                var response = new
                {
                    Status = "Finded",
                    book = s
                };
              

                string jsonResponse = JsonSerializer.Serialize(response);
                return Content(jsonResponse, "application/json");
            }
            string isbnn=Request.Form["isbn"];
            var z = handle.Books.Where(e => e.Isbn == isbnn).FirstOrDefault();
            z.Title = Request.Form["title"];
            z.Author = Request.Form["author"];
            z.Description = Request.Form["description"];
            z.Publisher = Request.Form["publisher"];
            z.Price =Convert.ToDouble( Request.Form["price"]);
            z.Edition = Convert.ToInt32( Request.Form["edition"]);
            handle.SaveChanges();
            ViewBag.update = "true";
            return View();
        }
        [HttpGet,HttpDelete]
        public IActionResult Delete_Book(string isbn="")
        {
            if (!HttpContext.Request.Cookies.ContainsKey("user_id"))
            {
                return RedirectToAction( "SignIn" , "Home" );
            }
            ViewBag.Title = "Delete-Book";
            if (Request.Method=="GET" && isbn=="")
            {
                return View();
            }
            LmsMvcContext handle = new LmsMvcContext();
            var s = handle.Books.Where(e => e.Isbn == isbn).FirstOrDefault();

            if (Request.Method == "GET" && isbn!="")
            {
                if (s == null)
                {
                    var resp = "{\"Status\":\"NotFinded\"}";
                    return Content(resp, "application/json");
                }
                var res = "{\"Status\":\"Finded\", \"author\":\"" + s.Author + "\", \"name\":\"" + s.Title + "\"}";
                return Content(res, "application/json");


            }
        
            if (s == null || s.Stock != s.Available)
            {
                var respon = "{\"Status\":\"NotDeleted\"}";
                return Content(respon, "application/json");
            }
            DeleteFile(s.Image,"images");
            handle.Books.Remove(s);
            handle.SaveChanges();
            var response = "{\"Status\":\"Deleted\"}";
            return Content(response, "application/json");
           
        }
        [HttpGet,HttpPost]
        public IActionResult Borrow_Book()
        {
            if (!HttpContext.Request.Cookies.ContainsKey("user_id"))
            {
                return RedirectToAction( "SignIn" , "Home" );
            }
            ViewBag.Title = "Borrow-Book";
            if (Request.Method=="GET")
                return View();
            string lb_id = HttpContext.Request.Cookies["user_id"];
            LmsMvcContext handle = new LmsMvcContext();
            DateTime isuedata = DateTime.Parse(Request.Form["brwdate"]);
            DateTime returndata = DateTime.Parse(Request.Form["rtndate"]);

            string isbn = Request.Form["isbn"];
            string stdid = Request.Form["stdid"];
            Student sd= handle.Students.Where(e => e.StudentId == stdid).FirstOrDefault();
            User usr = handle.Users.Where(e => e.Id == sd.UserId).FirstOrDefault();

            Book bok = handle.Books.Where(e => e.Isbn == isbn).FirstOrDefault();
            Librarian lab= handle.Librarians.Where(e=>e.UserId==lb_id).FirstOrDefault();
            if (sd != null)
            {
                if (sd.Fine >0)
                {
                    ViewBag.status = "undone";
                    ViewBag.message = "You are not Eligle to get another book Please pay your fine Rs."+sd.Fine.ToString()+"/-";
                    return View();
                }
                else if (bok.Available==0)
                {
                    ViewBag.status = "undone";
                    ViewBag.message = "Sorry Book Not Available !!!";
                    return View();
                }
                bok.Available = bok.Available - 1;
                handle.SaveChanges();
                Borrow bk = new Borrow()
                {
                    StudentId = stdid,
                    StudentName = usr.Name,
                    BookId = bok.BookId,
                    Title=bok.Title,
                    IssueDate=isuedata,
                    ReturnDate=returndata,
                    Fine=0.0,
                    IsReturn=false,
                    LibrarianId=lab.LibrarianId
                };
                handle.Borrows.Add(bk);
                handle.SaveChanges();
            }
            ViewBag.status = "Done";
            return View();
        }
        [HttpGet,HttpPost]
        public IActionResult Return_Book()
        {
            if (!HttpContext.Request.Cookies.ContainsKey("user_id"))
            {
                return RedirectToAction( "SignIn" , "Home" );
            }
            ViewBag.Title = "Returned-Book";
            if (Request.Method=="GET")
                return View();
            LmsMvcContext handle = new LmsMvcContext();
            string isbn = Request.Form["isbn"];
            string std_id = Request.Form["id"];
            Book bk = handle.Books.Where(e => e.Isbn == isbn).First();
            Student st = handle.Students.Where(e => e.StudentId == std_id).First();
            Borrow br=handle.Borrows.Where(e=>e.StudentId==std_id && e.BookId== bk.BookId).First();
            bk.Available = bk.Available + 1;
            int diff=CalculateDaysDifference(br.ReturnDate);
            br.IsReturn = true;
            br.Fine = diff * 20;
            st.Fine = st.Fine + br.Fine;
            handle.SaveChanges();
            ViewBag.panelty = br.Fine > 0 ? br.Fine.ToString() : "";
            ViewBag.status = "success";
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
            List<Borrow> ls = handle.Borrows.Where(e =>e.IsReturn==false).ToList();
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
        public IActionResult logout()
        {
            if (!HttpContext.Request.Cookies.ContainsKey("user_id"))
            {
                return RedirectToAction( "SignIn" , "Home" );
            }
            HttpContext.Response.Cookies.Delete("user_id");
            return RedirectToAction( "SignIn" , "Home" );
        }

        private readonly IWebHostEnvironment _hostingEnvironment;
        public LibrarianController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        private string SaveImage(IFormFile fileobj, string name, string nextlocation= "images")
        {

            IFormFile file = fileobj;
            string saveDirectory = Path.Combine(_hostingEnvironment.WebRootPath, "media", nextlocation);
            string fileExtension = Path.GetExtension(fileobj.FileName);
            string fileName = name + fileExtension;
            string filePath = Path.Combine(saveDirectory, fileName);
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fs);
            }
            return fileName;
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
        private string DeleteFile(string image_name, string foldername= "images")
        {
            string Directory = Path.Combine(_hostingEnvironment.WebRootPath, "media", foldername, image_name);
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
        private int CalculateDaysDifference(DateTime givenDate)
        {
            DateTime currentDate = DateTime.Now.Date; // Get the current date without the time portion

            if (currentDate > givenDate)
            {
                TimeSpan timeDifference = currentDate - givenDate;
                int daysDifference = timeDifference.Days;
                return daysDifference;
            }

            return 0;
        }


    }
}

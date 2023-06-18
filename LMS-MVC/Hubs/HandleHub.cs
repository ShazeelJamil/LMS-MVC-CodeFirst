using LMS_MVC.Models;
using Microsoft.AspNetCore.SignalR;

namespace LMS_MVC.Hubs
{
    public class HandleHub:Hub
    {
        public async Task SendAddBook()
        {
            LmsMvcContext handle=new LmsMvcContext();
             List <Book> ls =handle.Books.ToList();
            int total_books = 0;
            foreach (var item in ls)
            {
                total_books += item.Stock;
            }
            await Clients.All.SendAsync("ReceiveAddBook", total_books.ToString());
        }
        public async Task SendBorrowBook()
        {
            LmsMvcContext handle = new LmsMvcContext();
            int total_borrow = handle.Borrows.Where(e=>e.IssueDate.Date == DateTime.Now.Date).Count();  
            await Clients.All.SendAsync("ReceiveBorrowBook", total_borrow);
        }
    }
}

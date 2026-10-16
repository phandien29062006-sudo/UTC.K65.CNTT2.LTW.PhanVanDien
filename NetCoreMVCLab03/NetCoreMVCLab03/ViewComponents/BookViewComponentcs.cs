using Microsoft.AspNetCore.Mvc;
using NetCoreMVCLab03.Models;

namespace NetCoreMVCLab03.ViewComponents
{
    public class BookViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var books = Book.GetBookList();

            return View(books);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NKKDotNetCoreHomeWork.MvcApp.Models.BookModels;

namespace NKKDotNetCoreHomeWork.MvcApp.Controllers
{
    public class BookController : Controller
    {
        private readonly AppDbContext _context;
        private BookResModel _bookResModel = new();

        public BookController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int pageNo = 1, int pageSize = 10)
        {
            //var rowCount = await _context.BookDataModel.CountAsync();
            //var pageCount = rowCount / pageSize;
            //if (rowCount % pageSize > 0) pageCount++;

            //List<BookModel> lst = await _context.BookDataModel
            //    .OrderByDescending(x => x.BookId)
            //    .Skip((pageNo - 1) * pageSize)
            //    .Take(pageSize).ToListAsync();

            //ViewBag.Data = lst;
            //ViewBag.PageCount = pageCount;
            //ViewBag.PageSize = pageSize;
            //ViewBag.PageNo = pageNo;
            //return View();

            var rowCount = await _context.BookDataModel.CountAsync();
            var pageCount = rowCount / pageSize;
            if (rowCount % pageSize > 0) pageCount++;

            var lst = await _context.BookDataModel
                .OrderByDescending(x => x.BookId)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            List<BookreqModel> result = lst.AsEnumerable().Select(x => x.Change()).ToList();
            ViewBag.Data = result;
            ViewBag.PageCount = pageCount;
            ViewBag.PageSize = pageSize;
            ViewBag.PageNo = pageNo;
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> BookSave(BookModel model)
        {
            await _context.BookDataModel.AddAsync(model);
            var result = await _context.SaveChangesAsync();
            TempData["message"] = result > 0 ? "Save Successfully!" : "Save Fail !";
            TempData["isSuccess"] = result > 0;
            return Redirect("/book/index");
        }

        [ActionName("Edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var data = await _context.BookDataModel.FirstOrDefaultAsync(x => x.BookId == id);
            if (data == null)
            {
                TempData["message"] = "No Data Found!";
                TempData["isSuccess"] = false;
                return Redirect("/book/index");
            }

            return View("Edit", data);
        }

        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> BookUpdate(int id, BookModel model)
        {
            var data = await _context.BookDataModel.FirstOrDefaultAsync(x => x.BookId == id);
            if (data == null)
            {
                TempData["message"] = "No Data Found!";
                TempData["isSuccess"] = false;
                return Redirect("/book/index");
            }
            data.BookTitle = model.BookTitle;
            data.BookAuthor = model.BookAuthor;
            data.BookCategory = model.BookCategory;
            data.BookContent = model.BookContent;

            var result = await _context.SaveChangesAsync();
            TempData["message"] = result > 0 ? "Update Successfully!" : "Update Fail !";
            TempData["isSuccess"] = result > 0;
            return Redirect("/book/index");
        }

        [ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _context.BookDataModel.FirstOrDefaultAsync(x => x.BookId == id);
            if (data == null)
            {
                TempData["message"] = "No Data Found!";
                TempData["isSuccess"] = false;
                return Redirect("/book/index");
            }

            return View("Delete", data);
        }


        [ActionName("BookDelete")]
        public async Task<IActionResult> BookDelete(int id)
        {
            var data = await _context.BookDataModel.FirstOrDefaultAsync(x => x.BookId == id);
            _context.BookDataModel.Remove(data);
            var result = await _context.SaveChangesAsync();
            TempData["message"] = result > 0 ? "Delete Successfully!" : "Delete Fail !";
            TempData["isSuccess"] = result > 0;
            return Redirect("/book/index");

        }

    }
}

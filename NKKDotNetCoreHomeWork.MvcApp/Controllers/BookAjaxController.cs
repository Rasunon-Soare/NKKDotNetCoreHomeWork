using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NKKDotNetCoreHomeWork.MvcApp.Models.BookModels;

namespace NKKDotNetCoreHomeWork.MvcApp.Controllers
{
    public class BookAjaxController : Controller
    {
        private readonly AppDbContext _context;

        public BookAjaxController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int pageNo = 1, int pageSize = 10)
        {
            var rowCount = await _context.BookDataModel.CountAsync();
            var pageCount = rowCount / pageSize;
            if (rowCount % pageSize > 0) pageCount++;

            List<BookModel> lst = await _context.BookDataModel
                .OrderByDescending(x => x.BookId)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            ViewBag.Data = lst;
            ViewBag.PageCount = pageCount;
            ViewBag.PageSize = pageSize;
            ViewBag.PageNo = pageNo;
            return View();
        }

        public IActionResult AjaxCreate()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> AjaxSave(BookModel model)
        {
            await _context.BookDataModel.AddAsync(model);
            var result = await _context.SaveChangesAsync();
            var message = result > 0 ? "Save Successfully !" : "Save Fail!";
            var isSuccess = result > 0;

            //TempData["message"] = message;
            //TempData["isSuccess"] = isSuccess;
            return Json(new { Message = message, IsSuccess = isSuccess });
        }

        [ActionName("Edit")]
        public async Task<IActionResult> AjaxEdit(int id)
        {
            var data = await _context.BookDataModel.FirstOrDefaultAsync(x => x.BookId == id);
            if (data == null)
            {
                TempData["message"] = "No Data Found!";
                TempData["isSuccess"] = false;
                return Redirect("/bookajax/index");
            }

            return View("AjaxEdit", data);
        }

        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> AjaxUpdate(int id, BookModel model)
        {
            var data = await _context.BookDataModel.FirstOrDefaultAsync(x => x.BookId == id);
            if (data == null)
            {
                TempData["message"] = "No Data Found!";
                TempData["isSuccess"] = false;
                return Redirect("/bookajax/index");
            }
            data.BookTitle = model.BookTitle;
            data.BookAuthor = model.BookAuthor;
            data.BookCategory = model.BookCategory;
            data.BookContent = model.BookContent;

            var result = await _context.SaveChangesAsync();
            var message = result > 0 ? "Update Successfully!" : "Update Fail !";
            var isSuccess = result > 0;
            //TempData["message"] = message;
            //TempData["isSuccess"] = isSuccess;
            return Json(new { Message = message, IsSuccess = isSuccess });
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> AjaxDelete(int id)
        {
            var data = await _context.BookDataModel.FirstOrDefaultAsync(x => x.BookId == id);
            if (data == null)
            {
                TempData["message"] = "No Data Found!";
                TempData["isSuccess"] = false;
                return Redirect("/bookajax/index");
            }
            _context.BookDataModel.Remove(data);
            var result = await _context.SaveChangesAsync();
            var message = result > 0 ? "Delete Successfully!" : "Delete Fail !";
            var isSuccess = result > 0;
            //TempData["message"] = message;
            //TempData["isSuccess"] = isSuccess;
            return Json(new { Message = message, IsSuccess = isSuccess });

        }
    }
}

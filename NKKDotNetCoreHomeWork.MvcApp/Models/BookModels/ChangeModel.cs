using System.Collections.Generic;

namespace NKKDotNetCoreHomeWork.MvcApp.Models.BookModels
{
    public static class ChangeModel
    {
        public static BookreqModel Change (this BookModel model)
        {
            BookreqModel result = new BookreqModel
            {
                BookId = model.BookId,
                BookCategory = model.BookCategory,
                BookTitle = model.BookTitle,
                BookContent = model.BookContent,
                BookAuthor = model.BookAuthor,
            };
            return result;
        }
    }
}

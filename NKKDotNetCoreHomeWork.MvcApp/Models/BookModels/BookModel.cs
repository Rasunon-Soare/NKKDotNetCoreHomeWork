using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NKKDotNetCoreHomeWork.MvcApp.Models.BookModels
{
    [Table("Tbl_Book")]
    public class BookModel
    {
        [Key]
        [Column("Book_Id")]
        public int BookId { get; set; }
        [Column("Book_Title")]
        public string? BookTitle { get; set; }
        [Column("Book_Author")]
        public string? BookAuthor { get; set; }
        [Column("Book_Category")]
        public string? BookCategory { get; set; }
        [Column("Book_Content")]
        public string? BookContent { get; set; }
    }
}

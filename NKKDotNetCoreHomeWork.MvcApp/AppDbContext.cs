using Microsoft.EntityFrameworkCore;
using NKKDotNetCoreHomeWork.MvcApp.Models.BookModels;

namespace NKKDotNetCoreHomeWork.MvcApp
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<BookModel> BookDataModel { get; set; }
    }


}

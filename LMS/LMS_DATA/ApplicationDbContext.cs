using LMS_DATA.ViewModels;
using Microsoft.EntityFrameworkCore;


namespace LMS_DATA
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BorrowdBooks> BorrowdBooks { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}

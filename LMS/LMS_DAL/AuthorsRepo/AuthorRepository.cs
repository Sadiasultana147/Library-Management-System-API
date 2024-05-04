
using LMS_BAL.AuthorsRepo;
using LMS_DATA;
using LMS_DATA.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace LMS_DAL.AuthorsRepo
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDbContext _context;
        public AuthorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteAuthor(int authorId)
        {
            var author = await _context.Authors.FindAsync(authorId);
            if (author == null)
            {
                return false;
            }
            else
            {
                _context.Authors.Remove(author);
                // Save changes to the database
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task InsertAuthorData(Author Authors)
        {
            _context.Authors.Add(Authors);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Author>> GetAllAuthor()
        {
            return await _context.Authors.ToListAsync();
        }
        public async Task<Author> GetAuthorById(int authorId)
        {
            var author = await _context.Authors.Where(x => x.AuthorID == authorId).FirstOrDefaultAsync();
            if (author == null)
                Console.WriteLine($"Author ID not found.");
            // Map database fields to the view model properties
            var authormodel = new Author
            {
                AuthorID = author.AuthorID,
                AuthorName = author.AuthorName,
                AuthorBio = author.AuthorBio
              
            };
            return authormodel;
        }
        public async Task<bool> UpdateAuthor(Author author)
        {
            var result = await _context.Authors.FindAsync(author.AuthorID);

            if (result == null)
            {
                return false;
            }
            
            _context.Entry(result).CurrentValues.SetValues(author);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

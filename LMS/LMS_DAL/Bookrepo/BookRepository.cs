using LMS_BAL.BooksRepo;
using LMS_DATA;
using LMS_DATA.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_DAL.Bookrepo
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;
        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<BookViewMode>> GetAllBook()
        {
            try
            {
                return await _context.Books
                    .Join(_context.Authors, b => b.AuthorID, a => a.AuthorID, (b, a) => new { b = b, a = a })
                    .Select(x => new BookViewMode
                    {
                        BookID=x.b.BookID,
                        Title=x.b.Title,
                        AuthorID=x.b.AuthorID,
                        ISBN=x.b.ISBN,
                        PublishedDate=x.b.PublishedDate,
                        TotalCopies=x.b.TotalCopies,
                        AvailableCopies=x.b.AvailableCopies,
                        AuthorName= x.a.AuthorName
                    }).ToListAsync();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public async Task<BookViewMode> GetBookById(int BookID)
        {
            try
            {
                var book = await _context.Books.FirstOrDefaultAsync(x => x.BookID == BookID);

                if (book != null)
                {
                    var author = await _context.Authors.FirstOrDefaultAsync(x => x.AuthorID == book.AuthorID);

                    if (author != null)
                    {
                        var result = new BookViewMode
                        {
                            BookID = book.BookID,
                            Title = book.Title,
                            AuthorID = book.AuthorID,
                            ISBN = book.ISBN,
                            PublishedDate = book.PublishedDate,
                            TotalCopies = book.TotalCopies,
                            AvailableCopies = book.AvailableCopies,
                            AuthorName = author.AuthorName
                        };

                        return result;
                    }
                    else
                    {
                       
                        return null;
                    }
                }
                else
                {
                    
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Properly handle the exception here, such as logging it
                throw ex;
            }
        }

        public async Task<bool> DeleteBook(int BookId)
        {
            var book = await _context.Books.FindAsync(BookId);
            if (book == null)
            {
                return false;
            }
           
            _context.Books.Remove(book);
            // Save changes to the database
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task InsertBookData(Book Book)
        {
            try
            {
                _context.Books.Add(Book);
                await _context.SaveChangesAsync();

                
            }
            catch (Exception ex)
            {
                // Log exception
                Console.WriteLine($"Error: {ex.Message}");
                // Throw the exception
                throw;
            }
        }

        public async Task<bool> UpdateBook(Book Book)
        {
            var result = await _context.Books.FindAsync(Book.BookID);

            if (result == null)
            {
                return false;
            }
            _context.Entry(result).CurrentValues.SetValues(Book);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

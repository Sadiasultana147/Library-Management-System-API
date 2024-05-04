using LMS_BAL.BorrowdBooksRepo;
using LMS_DATA;
using LMS_DATA.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LMS_DAL.BorrowedBooksRepo
{
    public class BorrowdBooksRepository : IBorrowdBooksRepository
    {
        private readonly ApplicationDbContext _context;
        public BorrowdBooksRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> DeleteBorrowdBooks(int BorrowID)
        {
            var book = await _context.BorrowdBooks.FindAsync(BorrowID);
            if (book == null)
            {
                return false;
            }

            _context.BorrowdBooks.Remove(book);
            // Save changes to the database
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<BorrowdBooksViewModel>> GetAllBorrowedBook()
        {
            try
            {
                return await _context.BorrowdBooks
                    .Join(_context.Members, b => b.MemberID, m => m.MemberID, (b, m) => new { b = b, m = m })
                    .Join(_context.Books, bb => bb.b.BookID, br => br.BookID, (bb, br) => new { bb = bb, br = br })
                    .Select(x => new BorrowdBooksViewModel
                    {
                        BorrowID = x.bb.b.BorrowID,
                        BookID = x.bb.b.BookID,
                        MemberID = x.bb.b.MemberID,
                        BorrowDate = x.bb.b.BorrowDate,
                        ReturnDate = x.bb.b.ReturnDate,
                        status=x.bb.b.status,
                        MemberName= x.bb.m.FirstName + ' ' + x.bb.m.LastName,
                        BookName= x.br.Title
                    }).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<BorrowdBooksViewModel> GetBorrowBookById(int BorrowId)
        {

            try
            {
                var borrowedbook = await _context.BorrowdBooks.FirstOrDefaultAsync(x => x.BorrowID == BorrowId);

                if (borrowedbook != null)
                {
                    var member = await _context.Members.FirstOrDefaultAsync(x => x.MemberID == borrowedbook.MemberID);
                    var book = await _context.Books.FirstOrDefaultAsync(x => x.BookID == borrowedbook.BookID);
                    var result = new BorrowdBooksViewModel
                    {
                        BorrowID = borrowedbook.BorrowID,
                        BookID = borrowedbook.BookID,
                        MemberID = borrowedbook.MemberID,
                        BorrowDate = borrowedbook.BorrowDate,
                        ReturnDate = borrowedbook.ReturnDate,
                        status = borrowedbook.status,
                        MemberName = member.FirstName + ' ' + member.LastName,
                        BookName = book.Title
                    };

                    return result;
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
            

        public async Task InsertBorrowedBookData(BorrowdBooks borrowedBook)
        {
            try
            {
                _context.BorrowdBooks.Add(borrowedBook);
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

        public async Task<bool> UpdateBorrowedBook(BorrowdBooks borrowedBook)
        {
            var result = await _context.BorrowdBooks.FindAsync(borrowedBook.BorrowID);

            if (result == null)
            {
                return false;
            }
            _context.Entry(result).CurrentValues.SetValues(borrowedBook);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

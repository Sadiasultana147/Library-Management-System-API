using LMS_DATA.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_BAL.BooksRepo
{
    public interface IBookRepository
    {
        Task InsertBookData(Book Book);
        Task<bool> DeleteBook(int BookId);
        Task<IEnumerable<BookViewMode>> GetAllBook();
        Task<BookViewMode> GetBookById(int BookID);
        Task<bool> UpdateBook(Book Book);
    }
}

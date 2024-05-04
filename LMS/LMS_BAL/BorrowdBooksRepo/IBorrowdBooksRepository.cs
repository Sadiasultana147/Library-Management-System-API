using LMS_DATA.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_BAL.BorrowdBooksRepo
{
    public interface IBorrowdBooksRepository
    {
        Task InsertBorrowedBookData(BorrowdBooks borrowedBook);
        Task<bool> DeleteBorrowdBooks(int BorrowID);
        Task<IEnumerable<BorrowdBooksViewModel>> GetAllBorrowedBook();
        Task<BorrowdBooksViewModel> GetBorrowBookById(int BookID);
        Task<bool> UpdateBorrowedBook(BorrowdBooks borrowedBook);
    }
}

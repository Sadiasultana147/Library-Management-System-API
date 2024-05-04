
using LMS_DATA.ViewModels;

namespace LMS_BAL.AuthorsRepo
{
    public interface IAuthorRepository
    {
        Task InsertAuthorData(Author Authors);
        Task<bool> DeleteAuthor(int authorId);
        Task<IEnumerable<Author>> GetAllAuthor();
        Task<Author> GetAuthorById(int authorID);
        Task<bool> UpdateAuthor(Author author);

    }
}

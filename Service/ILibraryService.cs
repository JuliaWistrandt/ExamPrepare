/// <summary>
/// applying SOLID principles.
/// Open/Closed Principle (OCP): To follow this principle, 
/// we'll create interfaces for our services. 
/// This will allow us to extend their functionality 
/// without modifying the existing code.
/// </summary>
using LibrarySystem.Models;

namespace LibrarySystem.Service
{
    public interface ILibraryService // Kind of Database 
    {
        // Book Service
        void AddBook(Book book);
        void RemoveBook(int bookId);
        IEnumerable<Book> GetAllBooks();
        void BorrowBook(int memberId, int bookId);
        void ReturnBook(int memberId, int bookId);
        public Book GetBookById(int bookId);

        // Member Service async
        Task AddMemberAsync(Member member);
        Task RemoveMemberAsync(int memberId);
        Task<IEnumerable<Member>> GetAllMembersAsync();
        Task<Member> GetMemberByIdAsync(int memberId);
        Task UpdateMemberAsync(int memberId, Member member);

    }
}

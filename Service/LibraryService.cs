using LibrarySystem.Models;

namespace LibrarySystem.Service
{
    public class LibraryService : ILibraryService
    {
        private readonly List<Book> _books = new List<Book>();
        private readonly List<Member> _members = new List<Member>();
        private int _bookIdCounter = 1;
        private int _memberIdCounter = 1;
        
        public void AddBook(Book book)
        {
            book.Id = _bookIdCounter++;
            book.IsAvailable = true;
            _books.Add(book);
        }

        public void BorrowBook(int memberId, int bookId)
        {
            var member = _members.FirstOrDefault(m => m.Id == memberId);
            var book = _books.FirstOrDefault(b => b.Id == bookId && b.IsAvailable);

            if (member != null && book != null)
            {
                book.IsAvailable = false;
                member.BorrowedBooks.Add(book);
            }

        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _books;
        }

        public void RemoveBook(int bookId)
        {
            var book = _books.FirstOrDefault(b => b.Id == bookId);
            if (bookId != null) _books.Remove(book);
        }

        public void ReturnBook(int memberId, int bookId)
        {
            var member = _members.FirstOrDefault(m => m.Id == memberId);
            var book = member?.BorrowedBooks.FirstOrDefault(b => b.Id == bookId);

            if (book != null)
            {
                book.IsAvailable = true;
                member.BorrowedBooks.Remove(book);
            }
        }

        public Book GetBookById(int bookId)
        {
            return _books.FirstOrDefault(m => m.Id == bookId);
        }


        // Member async methods

        public async Task AddMemberAsync(Member member)
        {
            member.Id = _memberIdCounter++;
            member.BorrowedBooks = new List<Book>();
            _members.Add(member);
            await Task.CompletedTask;

        }

        public async Task RemoveMemberAsync(int memberId)
        {
            var member = _members.FirstOrDefault(m => m.Id == memberId);
            if (member != null) _members.Remove(member);
            await Task.CompletedTask;

        }

        public async Task<IEnumerable<Member>> GetAllMembersAsync()
        {
            return await Task.FromResult(_members);
        }

        public async Task<Member> GetMemberByIdAsync(int memberId)
        {
            var member = _members.FirstOrDefault(m => m.Id == memberId);
            return await Task.FromResult(member);
        }

        public async Task UpdateMemberAsync(int memberId, Member member)
        {
            var existingMember = _members.FirstOrDefault(m => m.Id == memberId);
            if (existingMember != null)
            {
                existingMember.Name = member.Name;
                existingMember.BorrowedBooks = member.BorrowedBooks;
            }
            await Task.CompletedTask;
        }
      
    }
}

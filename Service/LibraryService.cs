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

        public void AddMember(Member member)
        {
            member.Id = _memberIdCounter++;
            member.BorrowedBooks = new List<Book>();
            _members.Add(member);
        }

        public void RemoveMember(int memberId)
        {
            var member = _members.FirstOrDefault(m => m.Id == memberId);
            if (member != null) _members.Remove(member);
            
        }

        public IEnumerable<Member> GetAllMembers()
        {
            return _members;
        }

        public Member GetMemberById(int memberId)
        {
            return _members.FirstOrDefault(m => m.Id == memberId);
        }
    }
}

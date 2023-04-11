
using System.ComponentModel.DataAnnotations;
/// <summary>
/// applying SOLID principles.
/// Single Responsibility Principle (SRP): 
/// Each entity should have only one responsibility. 
/// In our case, the Book and Member classes will only store 
/// the information related to books and members.
/// </summary>
namespace LibrarySystem.Models
{
    public class Member
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public ICollection<Book>? BorrowedBooks { get; set; }
    }
}

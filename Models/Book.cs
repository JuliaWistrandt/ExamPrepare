
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
    
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public bool IsAvailable { get; set; }
    }

}

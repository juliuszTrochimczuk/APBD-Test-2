using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class Student
{
    public int Id { get; set; }
    [StringLength(100)] public required string FirstName { get; set; }
    [StringLength(100)] public required string LastName { get; set; }
    [StringLength(250)] public required string Email { get; set; }
    
    public ICollection<Record> Records { get; set; }
}
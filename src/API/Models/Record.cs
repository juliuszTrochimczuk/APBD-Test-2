using System.Numerics;

namespace API.Models;

public class Record
{
    public int Id { get; set; }
    public long ExecutionTime { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public int StudentId { get; set; }
    public required Student Student { get; set; }
    
    public int TaskId { get; set; }
    public required Task Task { get; set; }
    
    public int LanguageId { get; set; }
    public required Language Language { get; set; }
}
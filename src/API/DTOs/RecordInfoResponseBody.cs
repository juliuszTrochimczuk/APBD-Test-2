using API.Models;

namespace API.DTOs;

public class RecordInfoResponseBody(int id, Language language, Models.Task task, Student student, 
    long executionTime, DateTime created)
{
    public int Id { get; } = id;
    public Language Language { get; } = language;
    public Models.Task Task { get; } = task;
    public Student Student { get; } = student;
    public long ExecutionTime { get; } = executionTime;
    public DateTime Created { get; } = created;
}
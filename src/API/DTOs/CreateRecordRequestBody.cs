namespace API.DTOs;

public class CreateRecordRequestBody(int languageId, int studentId, TaskRequestBody taskRequestBody, 
    long executionTime, DateTime created)
{
    public int LanguageId { get; } = languageId;
    public int StudentId { get; } = studentId;
    public TaskRequestBody Task { get; } = taskRequestBody;
    public long ExecutionTime { get; } = executionTime;
    public DateTime Created { get; } = created;
}
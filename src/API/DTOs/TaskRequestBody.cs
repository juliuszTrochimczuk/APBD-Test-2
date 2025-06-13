namespace API.DTOs;

public class TaskRequestBody(int id)
{
    public int Id { get; } = id;
}
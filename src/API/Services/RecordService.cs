using API.DTOs;
using API.Repositories;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class RecordService : IRecordService
{
    private readonly ApplicationDbContext db;

    public RecordService(ApplicationDbContext db)
    {
        this.db = db;
    }

    public async Task<IEnumerable<RecordInfoResponseBody>> GetAllRecordsAsync(string filteredBy)
    {
        var result = filteredBy.ToLower() switch
        {
            "datacreation" => db.Record.OrderBy(r => r.CreatedAt),
            "languageid" => db.Record.OrderBy(r => r.LanguageId),
            "taskid" => db.Record.OrderBy(r => r.TaskId),
            "languageid&taskid" => db.Record.OrderBy(r => new { r.LanguageId, r.TaskId }),
            _ => throw new ArgumentException($"Cannot filter by this filter type: {filteredBy}"),
        };
        return await result.Select(r => new RecordInfoResponseBody(r.Id, r.Language, r.Task, r.Student, r.ExecutionTime, r.CreatedAt)).ToListAsync();
    }

    public async Task<IEnumerable<RecordInfoResponseBody>> GetAllRecordsAsync()
    {
        return await db.Record
            .Select(r => new RecordInfoResponseBody(r.Id, r.Language, r.Task, r.Student, r.ExecutionTime, r.CreatedAt))
            .ToListAsync();;
    }
}
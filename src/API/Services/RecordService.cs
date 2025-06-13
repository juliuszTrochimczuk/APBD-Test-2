using API.DTOs;
using API.Models;
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

    public async Task<RecordInfoResponseBody> CreateNewRecordAsync(CreateRecordRequestBody body)
    {
        var foundLanguage = await db.Language.FirstAsync(l => l.Id == body.LanguageId);
        if (foundLanguage == null)
            throw new KeyNotFoundException($"Language {body.LanguageId} not found");
        
        var foundStudent = await db.Student.FirstAsync(s => s.Id == body.StudentId);
        if (foundStudent == null)
            throw new KeyNotFoundException($"Student {body.StudentId} not found");

        var foundTask = await db.Task.FirstAsync(t => t.Id == body.Task.Id);
        if (foundTask == null)
            throw new KeyNotFoundException($"Task {body.Task.Id} not found");

        Record newRecord = new()
        {
            Id = db.Record.Count() + 1,
            CreatedAt = body.Created,
            ExecutionTime = body.ExecutionTime,
            Language = foundLanguage,
            Student = foundStudent,
            Task = foundTask,
        };
        
        await db.Record.AddAsync(newRecord);
        await db.SaveChangesAsync();
        
        return new RecordInfoResponseBody(newRecord.Id, newRecord.Language, newRecord.Task, newRecord.Student, newRecord.ExecutionTime, newRecord.CreatedAt);
    }
}
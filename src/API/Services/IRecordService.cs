using API.DTOs;

namespace API.Services;

public interface IRecordService
{
    Task<IEnumerable<RecordInfoResponseBody>> GetAllRecordsAsync(string filteredBy);
    Task<IEnumerable<RecordInfoResponseBody>> GetAllRecordsAsync();
}
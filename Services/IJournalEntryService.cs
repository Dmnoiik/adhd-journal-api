using AdhdJournalApi.Models;

namespace AdhdJournalApi.Services
{
    public interface IJournalEntryService
    {
        Task SaveToDbAsync(JournalEntryModel entry);

        Task<JournalEntryModel[]> GetAllEntries();
        Task DeleteEntry(int id);

        Task<JournalEntryModel> GetEntry(int id);

        Task UpdateEntry(int id, JournalEntryModel entry);

        Task<JournalEntryModel> GetEntryById(int id);
    }
}

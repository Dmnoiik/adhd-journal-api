using AdhdJournalApi.Data;
using AdhdJournalApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection.Metadata.Ecma335;

namespace AdhdJournalApi.Services
{
    public class JournalEntryService : IJournalEntryService
    {

        private AppDbContext _context;
        public JournalEntryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<JournalEntryModel[]> GetAllEntries()
        {
            return await _context.JournalEntries.ToArrayAsync();
        }

        public async Task SaveToDbAsync(JournalEntryModel entry)
        {
            await _context.AddAsync(entry);
            await _context.SaveChangesAsync();
            
        }

        public async Task DeleteEntry(int id)
        {

            var entry = await GetEntryById(id);
            _context.JournalEntries.Remove(entry);
            await _context.SaveChangesAsync();
        }

        public async Task<JournalEntryModel> GetEntry(int id)
        {
            return await GetEntryById(id);
        }

        public async Task UpdateEntry(int id, JournalEntryModel journalEntry)
        {
            var entry = await GetEntryById(id);
            if (AreFieldsChanged(entry, journalEntry))
            {
                entry.Summary = journalEntry.Summary;
                entry.MoodScale = journalEntry.MoodScale;
                entry.Date = journalEntry.Date;
                entry.ReflectionNote = journalEntry.ReflectionNote;
                entry.Description = journalEntry.Description;
                entry.IsPhoneUsed = journalEntry.IsPhoneUsed;
                entry.IsSatisfied = journalEntry.IsSatisfied;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<JournalEntryModel> GetEntryById(int id)
        {
            var entry = await _context.JournalEntries.SingleOrDefaultAsync(x => x.Id == id);
            if (entry is null)
            {
                throw new Exception("Entry not found");
            }
            return entry;
        }

        private bool AreFieldsChanged(JournalEntryModel entry, JournalEntryModel journalEntryModel)
        {

            bool output = !(entry.Summary.Equals(journalEntryModel.Summary)
                && entry.MoodScale == journalEntryModel.MoodScale
                && entry.Date == journalEntryModel.Date
                && entry.Description.Equals(journalEntryModel.Description)
                && entry.ReflectionNote.Equals(journalEntryModel.ReflectionNote)
                && entry.IsPhoneUsed == journalEntryModel.IsPhoneUsed
                && entry.IsSatisfied == journalEntryModel.IsSatisfied);

            return output;
        }
    }
}

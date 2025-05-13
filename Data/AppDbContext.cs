using Microsoft.EntityFrameworkCore;
using AdhdJournalApi.Models;

namespace AdhdJournalApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<JournalEntryModel> JournalEntries { get; set; }

    }
}

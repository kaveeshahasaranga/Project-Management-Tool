using KanbanApi.Models;
using Microsoft.EntityFrameworkCore;

namespace KanbanApi.Data
{
    // DbContext කියන්නේ Database එකත් එක්ක කතා කරන Phone එක
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // අපේ Models දෙක Database Tables විදියට හඳුන්වා දෙනවා
        public DbSet<KanbanColumn> Columns { get; set; }
        public DbSet<KanbanCard> Cards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KanbanColumn>()
                .HasMany(c => c.Cards)
                .WithOne()
                .HasForeignKey(c => c.ColumnId);
        }
    }
}
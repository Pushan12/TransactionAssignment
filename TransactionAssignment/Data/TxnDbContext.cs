using Microsoft.EntityFrameworkCore;
using TransactionAssignment.Models;

namespace TransactionAssignment.Data
{
    public class TxnDbContext : DbContext
    {
        public DbSet<TransactionModel> transactionModels { get; set; }

        public TxnDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.Entity<TransactionModel>().HasKey(x => x.TransactionId);
        }
    }
}

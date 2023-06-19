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

    public sealed class LazySingletonDBManager
    {
        private static int instanceCreationCount = 0;
        // lazily initialize an object by passing the delegate to create instance as () => new LazyDbContext()
        private static readonly Lazy<TxnDbContext> lazyInstance =
            new Lazy<TxnDbContext>(() =>
            {
                instanceCreationCount++;
                return new TxnDbContext(CreateOptions());
            });

        public static TxnDbContext Instance => lazyInstance.Value;

        private LazySingletonDBManager()
        {
        }

        public static int GetInstanceCreationCount()
        {
            return instanceCreationCount;
        }

        private static DbContextOptions<TxnDbContext> CreateOptions()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TxnDbContext>();
            optionsBuilder.UseInMemoryDatabase("Transaction");
            return optionsBuilder.Options;
        }
    }

    public sealed class SingletonDbManager
    {
        private static readonly TxnDbContext instance = new TxnDbContext(CreateOptions());
        private SingletonDbManager()
        {

        }

        public static TxnDbContext Instance => instance;        

        private static DbContextOptions<TxnDbContext> CreateOptions()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TxnDbContext>();
            optionsBuilder.UseInMemoryDatabase("Transaction");
            return optionsBuilder.Options;
        }
    }
}

using System.Linq.Expressions;
using TransactionAssignment.Models;

namespace TransactionAssignment.Services
{    
    public interface ITxnService
    {
        Task<IList<TransactionModel>> GetTransactionsAsync(Expression<Func<TransactionModel, bool>> where);
        Task<int> AddTransactionAsync(TransactionModel transaction);
        Task<int> AddTransactionsAsync(List<TransactionModel> transactions);
    }

    public interface ISTxnService
    {
        Task<IList<TransactionModel>> GetTransactionsAsync(Expression<Func<TransactionModel, bool>> where);
        Task<int> AddTransactionAsync(TransactionModel transaction);
        Task<int> AddTransactionsAsync(List<TransactionModel> transactions);
    }

    public class TxnService : ITxnService
    {
        readonly IRepository<TransactionModel> _transactionRepository;

        public TxnService(IRepository<TransactionModel> transactionRepository) 
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<IList<TransactionModel>> GetTransactionsAsync(Expression<Func<TransactionModel, bool>> where)
        {
            return await _transactionRepository.GetAllAsync(where);
        }

        public async Task<int> AddTransactionAsync(TransactionModel transaction)
        {
            var exist = await _transactionRepository.IsExist(x => x.TransactionId == transaction.TransactionId);
            if (!exist)
            {
                return await _transactionRepository.InsertAsync(transaction);
            }
            else
                return 1;
        }

        public async Task<int> AddTransactionsAsync(List<TransactionModel> transactions)
        {
            foreach (var transaction in transactions)
            {
                await AddTransactionAsync(transaction);
            }

            return 1;
        }
    }

    public class STxnService : ISTxnService
    {
        readonly ISingletonRepo<TransactionModel> _transactionRepository;

        public STxnService(ISingletonRepo<TransactionModel> transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<IList<TransactionModel>> GetTransactionsAsync(Expression<Func<TransactionModel, bool>> where)
        {
            return await _transactionRepository.GetAllAsync(where);
        }

        public async Task<int> AddTransactionAsync(TransactionModel transaction)
        {
            var exist = await _transactionRepository.IsExist(x => x.TransactionId == transaction.TransactionId);
            if (!exist)
            {
                return await _transactionRepository.InsertAsync(transaction);
            }
            else
                return 1;
        }

        public async Task<int> AddTransactionsAsync(List<TransactionModel> transactions)
        {
            foreach (var transaction in transactions)
            {
                await AddTransactionAsync(transaction);
            }

            return 1;
        }
    }
}

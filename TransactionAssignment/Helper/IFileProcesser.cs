using TransactionAssignment.Models;

namespace TransactionAssignment.Helper
{
    public interface IFileProcesser
    {
        (bool isSuccess, List<TransactionModel> transactions,string err) ReadData(IFormFile file);
    }
}

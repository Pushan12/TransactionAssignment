using TransactionAssignment.Models;

namespace TransactionAssignment.AdapterPattern
{
    public static class TransactionDataProvider
    {
        public static List<TransactionModel> GetTransactions =>
            new List<TransactionModel>()
            {
                new TransactionModel() { Amount =  100, CurrencyCode="USD",TransactionDate=new DateTime(2023,07,01),TransactionId = Guid.NewGuid().ToString(), Status="A" },
                new TransactionModel() { Amount =  200, CurrencyCode="SGD",TransactionDate=new DateTime(2023,07,02),TransactionId = Guid.NewGuid().ToString(), Status="RF" },
                new TransactionModel() { Amount =  300, CurrencyCode="VND",TransactionDate=new DateTime(2023,07,03),TransactionId = Guid.NewGuid().ToString(), Status="CB" }
            };
    }
}

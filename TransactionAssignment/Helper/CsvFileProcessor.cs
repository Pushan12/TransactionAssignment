using System.Globalization;
using System.Text;
using TransactionAssignment.Models;

namespace TransactionAssignment.Helper
{
    public class CsvFileProcessor : IFileProcesser
    {        
        public CsvFileProcessor()
        {
            
        }

        public (bool isSuccess, List<TransactionModel> transactions, string err) ReadData(IFormFile file)
        {
            try
            {
                var contents = new StreamReader(file.OpenReadStream(), Encoding.UTF8, detectEncodingFromByteOrderMarks: true).ReadToEnd().Split('\n');
                if (contents.Length > 0)
                {
                    var list = new List<TransactionModel>();
                    foreach (var line in contents)
                    {
                        var csvArr = line.Replace("\"", "").Replace("\r", "").Split(',');
                        var model = new TransactionModel()
                        {
                            TransactionId = csvArr[0],
                            Amount = Convert.ToDecimal(csvArr[1]),
                            CurrencyCode = csvArr[2],
                            TransactionDate = DateTime.ParseExact(csvArr[3], "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                            Status = MapStatus(csvArr[4])
                        };

                        list.Add(model);
                    }

                    return (true, list,string.Empty);
                }
                else
                    return (false, null, string.Empty);
            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);                
            }
        }

        private string MapStatus(string status)
        {
            switch(status)
            {
                case "Approved":
                    return "A";
                case "Failed":
                    return "R";
                case "Finished":
                    return "D";
                default:
                    return string.Empty;
            }
        }
    }
}

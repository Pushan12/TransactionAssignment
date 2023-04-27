using System.Globalization;
using System.Text;
using System.Xml.Linq;
using TransactionAssignment.Models;

namespace TransactionAssignment.Helper
{
    public class XmlFileProcessor: IFileProcesser
    {        
        public (bool isSuccess, List<TransactionModel> transactions, string err) ReadData(IFormFile file)
        {
            try
            {
                XDocument doc = XDocument.Load(file.OpenReadStream());
                var list = doc.Root.Elements("Transaction")
                    .Select(x => new TransactionModel
                    {
                        TransactionId = (string)x.Attribute("id").Value,
                        TransactionDate = DateTime.ParseExact(((string)x.Element("TransactionDate").Value), "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture),
                        Amount = Convert.ToDecimal((string)x.Element("PaymentDetails").Element("Amount").Value),
                        CurrencyCode = (string)x.Element("PaymentDetails").Element("CurrencyCode").Value,
                        Status = Mapper.MapStatus((string)x.Element("Status").Value)
                    }).ToList();

                return (true, list,string.Empty);
            }
            catch (Exception ex)
            {
                return (false,null,ex.Message);
            }
        }
    }

    public class CsvFileProcessor: IFileProcesser
    {
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
                            Status = Mapper.MapStatus(csvArr[4])
                        };

                        list.Add(model);
                    }

                    return (true, list, string.Empty);
                }
                else
                    return (false, null, string.Empty);
            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }
        }
    }

    public class DefaultFileProcessor: IFileProcesser
    {
        public (bool isSuccess, List<TransactionModel> transactions, string err) ReadData(IFormFile file)
        {
            return (false, null, string.Empty);
        }
    }

    public static class Mapper
    {
        public static string MapStatus(string status)
        {
            switch(status)
            {
                case "Approved":
                    return "A";
                case "Failed":
                case "Rejected":
                    return "R";
                case "Finished":
                case "Done":
                    return "D";
                default:
                    return string.Empty;
            }
        }
    }
}

using System.Globalization;
using System.Xml.Linq;
using TransactionAssignment.Models;

namespace TransactionAssignment.Helper
{
    public class XmlFileProcessor : IFileProcesser
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
                        Status = MapStatus((string)x.Element("Status").Value)
                    }).ToList();

                return (true, list,string.Empty);
            }
            catch (Exception ex)
            {
                return (false,null,ex.Message);
            }
        }

        private string MapStatus(string status)
        {
            switch (status)
            {
                case "Approved":
                    return "A";
                case "Rejected":
                    return "R";
                case "Done":
                    return "D";
                default:
                    return string.Empty;
            }
        }
    }
}

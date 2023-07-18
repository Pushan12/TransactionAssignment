using Newtonsoft.Json;
using System.Xml.Linq;

namespace TransactionAssignment.AdapterPattern
{
    public class ThirdPartyApi
    {
        public string GetTransactionsJson()
        {
            return JsonConvert.SerializeObject(TransactionDataProvider.GetTransactions);
        }

        public XDocument GetTransactionsXml()
        {

            var xDocument = new XDocument();

            var xElement = new XElement("Transactions", TransactionDataProvider.GetTransactions
                .Select(m => new XElement("Transaction",
                                        new XElement("TransactionId", m.TransactionId),
                                        new XElement("Amount", m.Amount),
                                        new XElement("CurrencyCode", m.CurrencyCode),
                                        new XElement("TransactionDate", m.TransactionDate),
                                        new XElement("Status", m.Status)
                                    )));
            xDocument.Add(xElement);
            return xDocument;
        }
    }
}

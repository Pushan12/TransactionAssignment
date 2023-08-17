using Newtonsoft.Json;

namespace TransactionAssignment.AdapterPattern
{
    public interface IConvertor
    {
        string ConvertXmlToJson();
    }

    public class XmlToJsonAdapter : IConvertor
    {
        private readonly ThirdPartyApi _xmlConverter;

        public XmlToJsonAdapter(ThirdPartyApi xmlConverter)
        {
            _xmlConverter = xmlConverter;
        }

        public string ConvertXmlToJson()
        {
            string jsonText = JsonConvert.SerializeXNode(_xmlConverter.GetTransactionsXml());
            var ss = JsonConvert.DeserializeObject<dynamic>(jsonText);
            return JsonConvert.SerializeObject(ss.Transactions.Transaction);
        }
    }
}

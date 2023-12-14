using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Newtonsoft.Json;

namespace AwsSecretManager
{
    public interface ISecretManagerProvider
    {
        Dictionary<string, string> Get(string secretName);
    }

    public class SecretManagerProvider : ISecretManagerProvider
    {
        public Dictionary<string, string> Get(string secretName)
        {
            Dictionary<string, string> parameter = new Dictionary<string, string>();
            var config = new AmazonSecretsManagerConfig { RegionEndpoint = RegionEndpoint.APSoutheast1 };
            var client = new AmazonSecretsManagerClient(config);
            var request = new GetSecretValueRequest
            {
                SecretId = secretName
            };
            try
            {
                if (secretName != null)
                {
                    var response = Task.Run(async () => await client.GetSecretValueAsync(request)).Result;
                    if (!string.IsNullOrEmpty(response.SecretString))
                    {
                        parameter = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.SecretString);
                    }
                }
            }
            catch (AggregateException ex)
            {
                if (!(ex.InnerException is ResourceNotFoundException))
                    throw ex.InnerException;
            }

            return parameter;
        }
    }
}

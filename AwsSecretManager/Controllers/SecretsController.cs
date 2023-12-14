using AwsSecretManager;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace TransactionAssignment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SecretsController : ControllerBase
    {
        readonly ISecretManagerProvider _awsSecretManager;

        public SecretsController(ISecretManagerProvider awsSecretManager)
        {
            _awsSecretManager = awsSecretManager;
        }

        [HttpGet("GetSecretValue/{secretName}")]
        public async Task<IActionResult> GetAllByCurrency(string secretName)
        {
            var value = _awsSecretManager.Get(secretName);
            return Ok(JsonConvert.SerializeObject(value));
        }
    }
}
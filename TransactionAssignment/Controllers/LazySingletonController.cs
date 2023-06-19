using Microsoft.AspNetCore.Mvc;
using TransactionAssignment.Data;
using TransactionAssignment.Helper;
using TransactionAssignment.Services;

namespace TransactionAssignment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LazySingletonController : ControllerBase
    {
        readonly ISTxnService _txnService;
        readonly IFileProcesserFactory _fileProcesserFactory;

        public LazySingletonController(ISTxnService txnService, IFileProcesserFactory fileProcesserFactory)
        {
            _txnService = txnService;
            _fileProcesserFactory = fileProcesserFactory;
        }

        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            //var fileFactory =  _fileProcesserFactory.GetProcessor(file);
            var fileFactory =  FileFactory.CreateFileFactory(file);
            if (fileFactory != null)
            {
                var result = fileFactory.GetProcesser().ReadData(file);
                if(result.isSuccess)
                {
                    await _txnService.AddTransactionsAsync(result.transactions);
                    return Ok();
                }
                else
                    return BadRequest(result.err);
                
            }
            else
                return BadRequest("Invalid File format");
        }

        [HttpGet("GetAllByCurrency/{currency}")]
        public async Task<IActionResult> GetAllByCurrency(string currency)
        {
            var list = await _txnService.GetTransactionsAsync(x=>x.CurrencyCode == currency);
            return Ok(list.Select(x =>
            {
                return new
                {
                    id = x.TransactionId,
                    payment = $"{x.Amount} {x.CurrencyCode}",
                    status = x.Status
                };
            }).ToList());
        }

        [HttpGet("GetAllByStatus/{status}")]
        public async Task<IActionResult> GetAllByStatus(string status)
        {
            var list = await _txnService.GetTransactionsAsync(x => x.Status == status);
            return Ok(list.Select(x =>
            {
                return new
                {
                    id = x.TransactionId,
                    payment = $"{x.Amount} {x.CurrencyCode}",
                    status = x.Status
                };
            }).ToList());
        }
    }
}
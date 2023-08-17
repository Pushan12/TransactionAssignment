using Microsoft.AspNetCore.Mvc;
using System.Xml;
using TransactionAssignment.AdapterPattern;
using TransactionAssignment.Data;
using TransactionAssignment.Helper;
using TransactionAssignment.Services;

namespace TransactionAssignment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        readonly ITxnService _txnService;
        readonly IFileProcesserFactory _fileProcesserFactory;

        public TransactionController(ITxnService txnService, IFileProcesserFactory fileProcesserFactory)
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

        [HttpGet("TpaResultJson")]
        public IActionResult TpaResultJson()
        {
            return Ok(new ThirdPartyApi().GetTransactionsJson());
        }        

        [HttpGet("NewTpaResult")]
        public IActionResult NewTpaResult()
        {
            return Ok(new XmlToJsonAdapter(new ThirdPartyApi()).ConvertXmlToJson());
        }
    }
}
using IO.Swagger.Attributes;
using IO.Swagger.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IO.Swagger.Controllers
{
    [SwaggerTag("Store Api Operations")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("{version:apiVersion}/order")]

    public class StoreApiController : ControllerBase
    { 
        [HttpDelete("{orderId}")]
        [ValidateModelState]
        [SwaggerOperation("Delete purchase order by ID", "For valid response try integer IDs with positive integer value.\\ \\ Negative or non-integer values will generate API errors")]
        [SwaggerResponse(400, "Invalid ID supplied")]
        [SwaggerResponse(404, "Order not found")]
        public virtual IActionResult DeleteOrder([FromRoute, Required, SwaggerParameter("ID of the order that needs to be deleted")] long? orderId)
        { 
            throw new NotImplementedException();
        }

        [HttpGet("inventory")]
        [ValidateModelState]
        [SwaggerOperation("Returns pet inventories by status", "Returns a map of status codes to quantities")]
        [SwaggerResponse(200, "successful operation", typeof(Dictionary<string, int?>))]
        public virtual IActionResult GetInventory()
        { 
            var exampleJson = "{\n  \"key\" : 0\n}";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<Dictionary<string, int?>>(exampleJson)
                        : default(Dictionary<string, int?>);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        [HttpGet("{orderId}")]
        [ValidateModelState]
        [SwaggerOperation("Find purchase order by ID", "For valid response try integer IDs with value &gt;&#x3D; 1 and &lt;&#x3D; 10.\\ \\ Other values will generated exceptions")]
        [SwaggerResponse(200, "successful operation", typeof(Order))]
        [SwaggerResponse(400, "Invalid ID supplied")]
        [SwaggerResponse(404, "Order not found")]
        public virtual IActionResult GetOrderById([FromRoute, Required, Range(1, 10), SwaggerParameter("ID of pet that needs to be fetched")] long? orderId)
        { 
            var exampleJson = "{\n  \"petId\" : 6,\n  \"quantity\" : 1,\n  \"id\" : 0,\n  \"shipDate\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"complete\" : false,\n  \"status\" : \"placed\"\n}";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<Order>(exampleJson)
                        : default(Order);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        [HttpPost("create")]
        [ValidateModelState]
        [SwaggerOperation("Place an order for a pet")]
        [SwaggerResponse(200, "successful operation", typeof(Order))]
        [SwaggerResponse(400, "Invalid Order")]
        public virtual IActionResult PlaceOrder([FromBody,SwaggerRequestBody("order placed for purchasing the pet")] Order body)
        { 
            var exampleJson = "{\n  \"petId\" : 6,\n  \"quantity\" : 1,\n  \"id\" : 0,\n  \"shipDate\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"complete\" : false,\n  \"status\" : \"placed\"\n}";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<Order>(exampleJson)
                        : default(Order);            //TODO: Change the data returned
            return new ObjectResult(example);
        }
    }
}

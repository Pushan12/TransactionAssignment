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
    [SwaggerTag("Pet Api Operations")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("{version:apiVersion}/pet")]
    public class PetApiController : ControllerBase
    {       
        [HttpGet("list")]        
        [ValidateModelState]
        [SwaggerOperation("Returns Pet List","Returns pet list by filter")]
        [SwaggerResponse(405, "Invalid input")]        
        public virtual IActionResult Pets([FromForm,SwaggerSchema] Pet body)
        {
            throw new NotImplementedException();
        }
        
        [HttpPost("add")]
        [ValidateModelState]
        [SwaggerOperation("Add a new pet to the store", "Pet object that needs to be added to the store")]
        [SwaggerResponse(405, "Invalid input")]
        public virtual IActionResult AddPet([FromBody] Pet body)
        { 
            throw new NotImplementedException();
        }        
        
        [HttpDelete("{petId}")]
        [ValidateModelState]
        [SwaggerOperation("Deletes a pet", "Pet that needs to be deleted from store")]
        [SwaggerResponse(400, "Invalid ID supplied")]
        [SwaggerResponse(404, "Pet not found")]
        public virtual IActionResult DeletePet([FromRoute, Required, SwaggerParameter("Pet id to delete")] long? petId)
        { 
            throw new NotImplementedException();
        }
                
        [HttpGet("findByStatus")]
        [ValidateModelState]
        [SwaggerOperation("Finds Pets by status", "Multiple status values can be provided with comma separated strings")]
        [SwaggerResponse(200, "successful operation", typeof(List<Pet>))]
        [SwaggerResponse(400, "Invalid status value")]
        public virtual IActionResult FindPetsByStatus([FromQuery, Required, SwaggerParameter("Status values that need to be considered for filter")] List<string> status)
        {
            var exampleJson = "[ {\n  \"photoUrls\" : [ \"photoUrls\", \"photoUrls\" ],\n  \"name\" : \"doggie\",\n  \"id\" : 0,\n  \"category\" : {\n    \"name\" : \"name\",\n    \"id\" : 6\n  },\n  \"tags\" : [ {\n    \"name\" : \"name\",\n    \"id\" : 1\n  }, {\n    \"name\" : \"name\",\n    \"id\" : 1\n  } ],\n  \"status\" : \"available\"\n}, {\n  \"photoUrls\" : [ \"photoUrls\", \"photoUrls\" ],\n  \"name\" : \"doggie\",\n  \"id\" : 0,\n  \"category\" : {\n    \"name\" : \"name\",\n    \"id\" : 6\n  },\n  \"tags\" : [ {\n    \"name\" : \"name\",\n    \"id\" : 1\n  }, {\n    \"name\" : \"name\",\n    \"id\" : 1\n  } ],\n  \"status\" : \"available\"\n} ]";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<List<Pet>>(exampleJson)
                        : default(List<Pet>);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        [HttpGet("findByTags")]
        [ValidateModelState]
        [SwaggerOperation("Finds Pets by tags", "Muliple tags can be provided with comma separated strings. Use\\ \\ tag1, tag2, tag3 for testing.")]
        [SwaggerResponse(200, "successful operation", typeof(List<Pet>))]
        [SwaggerResponse(400, "Invalid status value")]
        public virtual IActionResult FindPetsByTags([FromQuery, Required, SwaggerParameter("Tags to filter by")] List<string> tags)
        { 
            var exampleJson = "[ {\n  \"photoUrls\" : [ \"photoUrls\", \"photoUrls\" ],\n  \"name\" : \"doggie\",\n  \"id\" : 0,\n  \"category\" : {\n    \"name\" : \"name\",\n    \"id\" : 6\n  },\n  \"tags\" : [ {\n    \"name\" : \"name\",\n    \"id\" : 1\n  }, {\n    \"name\" : \"name\",\n    \"id\" : 1\n  } ],\n  \"status\" : \"available\"\n}, {\n  \"photoUrls\" : [ \"photoUrls\", \"photoUrls\" ],\n  \"name\" : \"doggie\",\n  \"id\" : 0,\n  \"category\" : {\n    \"name\" : \"name\",\n    \"id\" : 6\n  },\n  \"tags\" : [ {\n    \"name\" : \"name\",\n    \"id\" : 1\n  }, {\n    \"name\" : \"name\",\n    \"id\" : 1\n  } ],\n  \"status\" : \"available\"\n} ]";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<List<Pet>>(exampleJson)
                        : default(List<Pet>);            //TODO: Change the data returned
            return new ObjectResult(example);
        }
                
        [HttpGet("{petId}")]
        [ValidateModelState]
        [SwaggerOperation("Find pet by ID", "Returns a single pet")]
        [SwaggerResponse(200, "successful operation", typeof(Pet))]
        [SwaggerResponse(400, "Invalid ID supplied")]
        [SwaggerResponse(404, "Pet not found")]
        public virtual IActionResult GetPetById([FromRoute, Required, SwaggerParameter("ID of pet to return")] long? petId)
        { 
            var exampleJson = "{\n  \"photoUrls\" : [ \"photoUrls\", \"photoUrls\" ],\n  \"name\" : \"doggie\",\n  \"id\" : 0,\n  \"category\" : {\n    \"name\" : \"name\",\n    \"id\" : 6\n  },\n  \"tags\" : [ {\n    \"name\" : \"name\",\n    \"id\" : 1\n  }, {\n    \"name\" : \"name\",\n    \"id\" : 1\n  } ],\n  \"status\" : \"available\"\n}";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<Pet>(exampleJson)
                        : default(Pet);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        [HttpPut("update")]
        [ValidateModelState]
        [SwaggerOperation("Update an existing pet")]
        [SwaggerResponse(400, "Invalid ID supplied")]
        [SwaggerResponse(404, "Pet not found")]
        [SwaggerResponse(405, "Validation exception")]
        public virtual IActionResult UpdatePet([FromBody, SwaggerRequestBody("Pet object that needs to be added to the store")] Pet body)
        { 
            throw new NotImplementedException();
        }
        
        [HttpPost("{petId}/uploadImage")]
        [ValidateModelState]
        [SwaggerOperation("uploads an image")]
        [SwaggerResponse(200, "successful operation", typeof(ModelApiResponse))]
        public virtual IActionResult UploadFile([FromRoute, Required, SwaggerParameter("ID of pet to update")] long? petId, [FromBody] Object body)
        { 
            var exampleJson = "{\n  \"code\" : 0,\n  \"type\" : \"type\",\n  \"message\" : \"message\"\n}";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<ModelApiResponse>(exampleJson)
                        : default(ModelApiResponse);            //TODO: Change the data returned
            return new ObjectResult(example);
        }
    }
}

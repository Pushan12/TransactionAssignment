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
    [ApiController]
    [SwaggerTag("User operations")]
    [ApiVersion("1.0")]
    [Route("{version:apiVersion}/user")]
    
    public class UserApiController : ControllerBase
    {
        [HttpPost("create")]        
        [ValidateModelState]
        [SwaggerOperation("Create User", "This can only be done by the logged in user.")]
        [SwaggerResponse(201, "successful operation")]
        public virtual IActionResult CreateUser([FromBody, SwaggerRequestBody("Create user object")] User body)
        { 
            throw new NotImplementedException();
        }

        [HttpPost("createWithArray")]
        [ValidateModelState]
        [SwaggerOperation("Creates list of users with given input array")]
        [SwaggerResponse(201, "successful operation")]
        public virtual IActionResult CreateUsersWithArrayInput([FromBody, SwaggerRequestBody("List of user object")] List<User> body)
        { 
            throw new NotImplementedException();
        }

        [HttpPost("createWithList")]
        [ValidateModelState]
        [SwaggerOperation("Creates list of users with given input array")]
        [SwaggerResponse(201, "successful operation")]
        public virtual IActionResult CreateUsersWithListInput([FromBody, SwaggerRequestBody("List of user object")] List<User> body)
        { 
            throw new NotImplementedException();
        }

        [HttpDelete("{username}")]
        [ValidateModelState]
        [SwaggerOperation("Delete user", "This can only be done by the logged in user")]
        [SwaggerResponse(400, "Invalid username supplied")]
        [SwaggerResponse(404, "User not found")]
        public virtual IActionResult DeleteUser([FromRoute, Required, SwaggerParameter("The name that needs to be deleted")] string username)
        { 
            throw new NotImplementedException();
        }

        [HttpGet("{username}")]
        [ValidateModelState]
        [SwaggerOperation("Get user by user name")]
        [SwaggerResponse(200, "successful operation", typeof(User))]
        [SwaggerResponse(400, "Invalid username supplied")]
        [SwaggerResponse(404, "User not found")]
        public virtual IActionResult GetUserByName([FromRoute, Required, SwaggerParameter("The name that needs to be fetched")] string username)
        { 
            var exampleJson = "{\n  \"firstName\" : \"firstName\",\n  \"lastName\" : \"lastName\",\n  \"password\" : \"password\",\n  \"userStatus\" : 6,\n  \"phone\" : \"phone\",\n  \"id\" : 0,\n  \"email\" : \"email\",\n  \"username\" : \"username\"\n}";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<User>(exampleJson)
                        : default(User);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        [HttpGet("login")]
        [ValidateModelState]
        [SwaggerOperation("Logs user into the system")]
        [SwaggerResponse(200, "successful operation", typeof(string))]
        [SwaggerResponse(400, "Invalid username/password supplied")]
        public virtual IActionResult LoginUser([FromQuery, Required, SwaggerParameter("The user name for login")]string username, [FromQuery, Required, SwaggerParameter("The password for login in clear text")]string password)
        {
            throw new NotImplementedException();
        }
                
        [HttpGet("logout")]
        [ValidateModelState]
        [SwaggerOperation("Logs out current logged in user session")]
        [SwaggerResponse(200, "successful operation")]
        public virtual IActionResult LogoutUser()
        { 
            throw new NotImplementedException();
        }

        [HttpPut("{username}")]
        [ValidateModelState]
        [SwaggerOperation("Update user", "This can only be done by the logged in user")]
        [SwaggerResponse(400, "Invalid user supplied")]
        [SwaggerResponse(404, "User not found")]
        public virtual IActionResult UpdateUser([FromBody, SwaggerRequestBody("Updated user object")] User body, [FromRoute, Required, SwaggerParameter("name that need to be updated")] string username)
        { 
            throw new NotImplementedException();
        }        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.IServices;
using Microsoft.AspNetCore.Mvc;
using ProductDomainModels.Models;

namespace AspNetCoreSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        readonly IUserService userService;
        public UserController(IUserService _userService)
        {
            userService = _userService;
        }


        // GET api/values
        [HttpGet , Route("[action]")]
        public async Task<ActionResult<User>> GetUser()
        {
            var user  = await userService.GetUserAsync(1);
            return Ok(user);
        }

       
    }
}

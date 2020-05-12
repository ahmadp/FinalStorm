using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FinalStorm.Controllers
{
    public class ResendCredentialsController : ApiController
    {

        UserManager userManager = new UserManager();
        // GET: api/ResendCredentials/5
        public IHttpActionResult Get(string PhoneNumber = null, string Email = null)
        {
            if (!string.IsNullOrEmpty(PhoneNumber))
            {
                //send SMS
            }
            else if (!string.IsNullOrEmpty(Email))
            {
                //send Email

            }
            return Ok();
        }

        public IHttpActionResult CheckIfValidCode(string Code, string PhoneNumber = null, string Email = null)
        {
            if (userManager.IsValidCode(Code, Email, PhoneNumber))
            {
                return Ok();
            }
            return BadRequest();
          
        }

    }
}

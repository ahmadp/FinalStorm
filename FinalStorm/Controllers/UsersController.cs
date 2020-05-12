using BL;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FinalStorm.Controllers
{
    public class UsersController : ApiController
    {
        UserManager userManager = new UserManager();
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public IHttpActionResult ForgetPassword(string email, string phoneNumber, string password)
        {
            var user = userManager.UpdateVarificationCode(email, phoneNumber, password);
            if (user != null)
            {
                return Ok(user);
            }
            return BadRequest();
        }

        // POST api/<controller>
        public IHttpActionResult Post(Users users)
        {
            try
            {
                users.JoiningDate = DateTime.Now;
                if (!string.IsNullOrEmpty(users.PhoneNumber))
                {
                    //send SMS
                }
                else if (!string.IsNullOrEmpty(users.Email))
                {
                    //send Email

                }
                var user = userManager.Register(users);
                if (user != null)
                {
                    return Ok(user);
                }
                return BadRequest();
            }
            catch (Exception)
            {

                return BadRequest();
            }
           
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(string email,string phoneNumber, string password)
        {
           var user =  userManager.UpdatePassword(email, phoneNumber, password);
            if (user != null)
            {
                return Ok(user);
            }
            return BadRequest();
        }


        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model
{
   public class Users
    {
        public int id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string NickName { get; set; }
        public DateTime JoiningDate { get; set; }
        public string VerificationCode { get; set; }
        public DateTime VerificationCodeExpiry { get; set; }
        [ForeignKey("City")]
        public int CityId { get; set; }
        public City City { get; set; }
    }
}

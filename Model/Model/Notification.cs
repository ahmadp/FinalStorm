using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model
{
   public class Notification
    {
        public int Id { get; set; }
        public string Description { get; set; }
        [ForeignKey("Users")]
        public int UserId { get; set; }
        public Users Users { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model
{
   public class UserSubscrption
    {
        public int Id { get; set; }
        [ForeignKey("Users")]
        public int UserId { get; set; }
        [ForeignKey("SubscriptionType")]
        public int SubscriptionId { get; set; }      
        public Users Users { get; set; }      
        public SubscriptionType SubscriptionType { get; set; }
    }
}

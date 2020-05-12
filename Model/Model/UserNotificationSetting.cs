using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model
{
   public class UserNotificationSetting
    {
        public int Id { get; set; }
        public bool AdminstrationTopics { get; set; }
        public bool NewComment { get; set; }
        public bool NewLikes { get; set; }
        public bool NewFriendRequest { get; set; }
        public bool NewTopicsAdded { get; set; }
        [ForeignKey("Users")]
        public int UserId { get; set; }
        public Users Users { get; set; }

    }
}

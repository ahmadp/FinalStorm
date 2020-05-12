using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model
{
    public class UserFollower
    {
        public int Id { get; set; }
        [ForeignKey("Users")]
        public int UserId { get; set; }
        public int FollowerId { get; set; }
        public bool Isblocked { get; set; }
        public Users Users { get; set; }
    }
}

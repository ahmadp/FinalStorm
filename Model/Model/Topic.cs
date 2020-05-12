using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model
{
    public class Topic
    {
        public int Id { get; set; }
        public string TopicName { get; set; }
        [ForeignKey("Users")]
        public int UserId { get; set; }
        public Users Users { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model
{
  public class Comments
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        [ForeignKey("Users")]
        public int? CommentOwner { get; set; }
        [ForeignKey("Topic")]
        public int TopicId { get; set; }
        [ForeignKey("ParentComments")]
        public int? ParentCommentId { get; set; }
        public Users Users { get; set; }
        public Topic Topic { get; set; }
        public Comments ParentComments { get; set; }
    }
}

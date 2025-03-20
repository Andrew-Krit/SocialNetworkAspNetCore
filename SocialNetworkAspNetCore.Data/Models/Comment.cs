using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAspNetCore.Data.Models
{
   public class Comment
    {
        [Key]
        public int Id { get; set; }

        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public int PostId { get; set; }
        public int UserId { get; set; }
        public Post Post { get; set; }
        public User User { get; set; }

    }
}

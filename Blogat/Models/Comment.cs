using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blogat.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required,MaxLength(500)]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }
        public DateTime TimeCommented { get; set; }
        public int CommentLikes { get; set; }
        public Person PersonCommented { get; set; }
        public int PersonId { get; set; }
        public Blog Blog { get; set; }
        public int BlogId { get; set; }
    }
}
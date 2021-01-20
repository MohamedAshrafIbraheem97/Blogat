using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blogat.Models
{
    public class Blog
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Heading { get; set; }

        [Required]
        [MaxLength(5000)]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }

        [DisplayName("Time Bloged")]
        public DateTime TimeBloged { get; set; }

        [DisplayName("Likes")]
        public int BlogLikes { get; set; }

        public Person PersonBloged { get; set; }
        public int PersonId { get; set; }

        public Category Category { get; set; }
        [DisplayName("Category")]
        public int CategoryId { get; set; }

    }
}
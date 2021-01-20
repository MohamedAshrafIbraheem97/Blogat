using Blogat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blogat.ViewModels
{
    public class BlogsViewModel
    {
        public Blog Blog { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Person> People { get; set; }
        public Person Person { get; set; }
        public Category Category { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public Comment Comment { get; set; }
    }
}
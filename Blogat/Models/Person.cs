using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Blogat.Models
{
    public class Person
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [DisplayName("Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        private string _fullName;

        [NotMapped]
        [DisplayName("Full Name")]
        public string FullName
        {
            get { return _fullName; }
            set { _fullName = FirstName +" "+ LastName; }
        }

        public string GetName()
        {
            return FirstName + " " + LastName;
        }

        

    }
}
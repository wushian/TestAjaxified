using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestAjax.Models
{
    public class Person
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [Required]
        [StringLength(255, MinimumLength = 3)]
        public string Name { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        [StringLength(255, MinimumLength = 3)]
        public string  Surname { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
        

    }
}
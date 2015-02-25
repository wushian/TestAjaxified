using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace TestAjax.Models
{
    public class Address
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 3)]
        public string City { get; set; }
        
        [Display(Name = "Street Address")]
        public string Street { get; set; }

        [Phone]
        public string Phone { get; set; }

        public int PersonID { get; set; }
        public virtual Person Person  { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Database.Entities
{
    public class Iata
    {
        [Required(ErrorMessage = "Please enter Iata Code.")]
        [StringLength(3)]
        [Display(Name = "Iata Code")]
        [Key]
        public string Code { get; set; }

        [Required(ErrorMessage = "Please enter Airport name.")]
        [StringLength(255)]
        [Display(Name = "Airport name")]
        public string Name { get; set; }
    }
}

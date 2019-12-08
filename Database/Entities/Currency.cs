using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Database.Entities
{
    public class Currency
    {
        [Required(ErrorMessage = "Please enter Currency Code.")]
        [StringLength(3)]
        [Display(Name = "Currency Code")]
        [Key]
        public string Code { get; set; }

        [Required(ErrorMessage = "Please enter Currency Name.")]
        [StringLength(100)]
        [Display(Name = "Currency Name")]
        public string Name { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebUI.Models
{
    public class BoatRegistration
    {
        public string BoatName { get; set; }
        [Required(ErrorMessage = "Please select photo")]
        public IFormFile BoatImage { get; set; }
        [Required]
        public decimal HourRate { get; set; }
    }
}

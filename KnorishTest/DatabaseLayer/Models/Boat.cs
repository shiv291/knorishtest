using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseLayer.Models
{
    public partial class Boats
    {        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long BoatId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }        
        public decimal HourRate { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

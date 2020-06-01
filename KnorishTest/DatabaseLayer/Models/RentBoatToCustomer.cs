using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DatabaseLayer.Models
{
    public partial class RentBoatToCustomer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long BoatId { get; set; }
        public string CustomerName { get; set; }
        public bool IsReturn { get; set; }
    }
}

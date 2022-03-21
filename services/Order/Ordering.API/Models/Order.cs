using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string OrderName { get; set; }
        [Required]
        public string Price { get; set; }
        public string Description { get; set; }
    }
}

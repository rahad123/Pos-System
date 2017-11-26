using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pos.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter New Item Name!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Enter New Item Price!")]
        public int Price { get; set; }
    }

}
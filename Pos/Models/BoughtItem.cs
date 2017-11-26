using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pos.Models
{
    public class BoughtItem
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }
        public Guid UserId { get; set; }
        public Item Item { get; set; }
        public int ItemId { get; set; }
    }

}
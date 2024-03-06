using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment2Entity.Models
{
    public partial class TlOrder
    {
        public TlOrder()
        {
            OrderDetails = new HashSet<TlOrderDetail>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public decimal? Freight { get; set; }
        public string? ShipAddress { get; set; }

        public virtual TlCustomer? Customer { get; set; }
        public virtual ICollection<TlOrderDetail> OrderDetails { get; set; }
    }
}

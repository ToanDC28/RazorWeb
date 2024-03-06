using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment2Entity.Models
{
    public partial class TlProduct
    {
        public TlProduct()
        {
            OrderDetails = new HashSet<TlOrderDetail>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }
        public string? QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public byte[]? ProductImage { get; set; }

        public virtual TlCategory? Category { get; set; }
        public virtual TlSupplier? Supplier { get; set; }
        public virtual ICollection<TlOrderDetail> OrderDetails { get; set; }
    }
}

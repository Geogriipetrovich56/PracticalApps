using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Packt.Shared
{
    [Table("Order Details")]
    [Index(nameof(OrderID), Name = "OrderID")]
    [Index(nameof(OrderID), Name = "OrdersOrder_Details")]
    [Index(nameof(ProductID), Name = "ProductID")]
    [Index(nameof(ProductID), Name = "ProductsOrder_Details")]
    public partial class OrderDetail
    {
        [Key]
        public int OrderID { get; set; }
        [Key]
        public int ProductID { get; set; }
        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }

        [ForeignKey(nameof(OrderID))]
        [InverseProperty("OrderDetails")]
        public virtual Order Order { get; set; } = null!;
        [ForeignKey(nameof(ProductID))]
        [InverseProperty("OrderDetails")]
        public virtual Product Product { get; set; } = null!;
    }
}

namespace CITUCrate.Models
{
    public class OrderItem
    {
        public int Id { get; set; } // Primary Key
        public int ProductId { get; set; } // Foreign Key to the Product table
        public virtual Product Product { get; set; } // Virtual for lazy loading

        public int Quantity { get; set; } // Quantity of the product ordered
        public decimal Subtotal { get; set; } // Total for this product (Quantity * Product Price)

        public int OrderId { get; set; } // Foreign Key to the Order table
        public virtual Order Order { get; set; } // Virtual for lazy loading
    }
}
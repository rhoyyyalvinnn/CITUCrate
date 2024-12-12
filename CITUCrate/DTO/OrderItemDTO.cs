namespace CITUCrate.DTO
{
    public class OrderItemDTO
    {
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }
        public ProductDTO Product { get; set; }  // Add ProductDTO
    }

}

namespace CITUCrate.DTO
{
    public class ProductDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ShortDescription { get; set; }
        public string ImageUrl { get; set; }
        public int TotalSales { get; set; }
    }
}

namespace CITUCrate.DTO
{
    public class SellerDashboardDTO
    {
        public string Username { get; set; }
        public decimal Balance { get; set; }
        public List<ProductDTO> Products { get; set; }
    }

    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

}

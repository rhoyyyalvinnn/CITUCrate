namespace CITUCrate.DTO
{
    public class AddUpdateProductDTO
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ShortDescription { get; set; }
    }
}

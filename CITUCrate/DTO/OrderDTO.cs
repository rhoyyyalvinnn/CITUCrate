using CITUCrate.DTO;

public class OrderDTO
{
    public int Id { get; set; }
    public UserDashboardDTO User { get; set; }  // Add UserDTO
    public List<OrderItemDTO> OrderItems { get; set; }
    public decimal Total { get; set; }
    public string Status { get; set; }
}

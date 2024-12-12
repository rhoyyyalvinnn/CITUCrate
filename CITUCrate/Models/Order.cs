using CITUCrate.Models;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public virtual User User { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; }

    public decimal Total { get; set; }
    public string Status { get; set; } = "Pending";
    public string deliveryLocation { get; set; }
}
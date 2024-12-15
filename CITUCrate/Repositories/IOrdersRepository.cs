namespace CITUCrate.Repositories
{
    public interface IOrdersRepository
    {
        Task<List<Order>> GetOrdersByStatusAsync(string status);
        Task<Order> GetOrderByIdAsync(int id);
        Task UpdateOrderStatusAsync(int orderId, string status);
        Task CreateOrderAsync(Order order);
    }
}

using CITUCrate.DTO;

namespace CITUCrate.Services
{
    public interface IOrderService
    {

        Task<List<OrderDTO>> GetOrdersByStatusAsync(string status);

        Task UpdateOrderStatusAsync(int orderId, string status);
    }
}

using CITUCrate.DTO;
using CITUCrate.Models;
using CITUCrate.Repositories;
using CITUCrate.Services;

public class OrderService : IOrderService
{
    private readonly IOrdersRepository _repository;

    public OrderService(IOrdersRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<OrderDTO>> GetOrdersByStatusAsync(string status)
    {
        var orders = await _repository.GetOrdersByStatusAsync(status);
        return orders.Select(o => new OrderDTO
        {
            Id = o.Id,
            User = o.User != null ? new UserDashboardDTO
            {
                Username = o.User.Username
            } : null,
            OrderItems = o.OrderItems?.Select(oi => new OrderItemDTO
            {
                Product = oi.Product != null ? new ProductDTO
                {
                    Name = oi.Product.Name
                } : null,
                Quantity = oi.Quantity,
                Subtotal = oi.Subtotal
            }).ToList() ?? new List<OrderItemDTO>(),
            Total = o.Total,
            Status = o.Status
        }).ToList();
    }



    public async Task UpdateOrderStatusAsync(int orderId, string status)
    {
        await _repository.UpdateOrderStatusAsync(orderId, status);
    }
}

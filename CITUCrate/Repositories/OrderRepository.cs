using CITUCrate.Models;
using CITUCrate.Repositories;
using Microsoft.EntityFrameworkCore;

public class OrderRepository : IOrdersRepository
{
    private readonly UserContext _context;

    public OrderRepository(UserContext context)
    {
        _context = context;
    }

    public async Task<List<Order>> GetOrdersByStatusAsync(string status)
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .Where(o => o.Status == status)
            .ToListAsync();
    }

    public async Task<Order> GetOrderByIdAsync(int id)
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task UpdateOrderStatusAsync(int orderId, string status)
    {
        var order = await _context.Orders.FindAsync(orderId);
        if (order != null)
        {
            order.Status = status;
            await _context.SaveChangesAsync();
        }
    }
}


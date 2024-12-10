using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CITUCrate.Models;
using System.Linq;
using Microsoft.Extensions.Logging;  // For ILogger

namespace CITUCrate.Pages.Seller
{
    public class OrderManagementModel : PageModel
    {
        private readonly UserContext _context;
        private readonly ILogger<OrderManagementModel> _logger;  // Injecting ILogger

        public OrderManagementModel(UserContext context, ILogger<OrderManagementModel> logger)
        {
            _context = context;
            _logger = logger;  // Storing the logger
        }

        public List<Order> PendingOrders { get; set; }
        public List<Order> CompletedOrders { get; set; }

        public void OnGet()
        {
            // Fetch orders based on their status
            PendingOrders = _context.Orders.Include(o => o.OrderItems)
                                           .ThenInclude(oi => oi.Product)
                                           .Where(o => o.Status == "Pending")
                                           .ToList() ?? new List<Order>();  // Default to empty list if null

            CompletedOrders = _context.Orders.Include(o => o.OrderItems)
                                              .ThenInclude(oi => oi.Product)
                                              .Where(o => o.Status == "Completed")
                                              .ToList() ?? new List<Order>();  // Default to empty list if null
        }

        // Method to update order status when the form is submitted
        public IActionResult OnPost(int orderId, string orderStatus)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == orderId);
            if (order != null)
            {
                order.Status = orderStatus;
                _context.SaveChanges();
            }

            // Reload orders
            PendingOrders = _context.Orders.Include(o => o.OrderItems)
                                           .ThenInclude(oi => oi.Product)
                                           .Where(o => o.Status == "Pending")
                                           .ToList();

            CompletedOrders = _context.Orders.Include(o => o.OrderItems)
                                             .ThenInclude(oi => oi.Product)
                                             .Where(o => o.Status == "Completed")
                                             .ToList();

            return RedirectToPage(); // Redirect to the same page to refresh the table
        }
    }
}

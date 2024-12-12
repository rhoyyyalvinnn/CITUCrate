using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging; // For ILogger
using CITUCrate.DTO;
using CITUCrate.Services; // For IOrderService
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CITUCrate.Pages.Seller
{
    public class OrderManagementModel : PageModel
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrderManagementModel> _logger; // Injecting ILogger

        public OrderManagementModel(IOrderService orderService, ILogger<OrderManagementModel> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        public List<OrderDTO> PendingOrders { get; set; }
        public List<OrderDTO> CompletedOrders { get; set; }

        public async Task OnGetAsync()
        {
            try
            {
                PendingOrders = await _orderService.GetOrdersByStatusAsync("Pending") ?? new List<OrderDTO>();
                CompletedOrders = await _orderService.GetOrdersByStatusAsync("Completed") ?? new List<OrderDTO>();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error fetching orders on GET");
                PendingOrders = new List<OrderDTO>();
                CompletedOrders = new List<OrderDTO>();
            }
        }

        public async Task<IActionResult> OnPostAsync(int orderId, string orderStatus)
        {
            try
            {
                _logger.LogInformation("Updating order ID {OrderId} to status {OrderStatus}", orderId, orderStatus);
                await _orderService.UpdateOrderStatusAsync(orderId, orderStatus);
                _logger.LogInformation("Order ID {OrderId} updated successfully", orderId);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error updating order ID {OrderId} to status {OrderStatus}", orderId, orderStatus);
            }

            // Refresh the page
            return RedirectToPage();
        }
    }
}

using CITUCrate.DTO;
using CITUCrate.Services;
using CITUCrate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CITUCrate.Pages.Seller;
using CITUCrate.Repositories;

namespace CITUCrate.Pages.Buyer
{
    public class BuyerCategoriesModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly ILogger<BuyerCategoriesModel> _logger;
        private readonly IOrdersRepository _ordersRepository;

        public BuyerCategoriesModel(IProductService productService, ILogger<BuyerCategoriesModel> logger, IOrdersRepository ordersRepository)

        // Constructor
        public BuyerCategoriesModel(IProductService productService, ILogger<BuyerCategoriesModel> logger)
        {
            _productService = productService;
            _logger = logger;
            _ordersRepository = ordersRepository;
        }

        // Property to store product data
        public List<ProductDTO> Products { get; set; }

        public string Category { get; set; }
        // Fetch products from the service and populate the Products property
        public async Task OnGetAsync(string category)
        {
            try
            {
                Category = category;
                Products = await _productService.GetProductsByCategoryAsync(category);

                if (Products == null || !Products.Any())
                {
                    _logger.LogWarning($"No products found for category: {category}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching products by category.");
                Products = new List<ProductDTO>();
            }
        }

        [BindProperty]
        public Order Order { get; set; }

        public async Task<IActionResult> OnPostAddToOrderAsync(int productId, int quantity, string deliveryLocation)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId.HasValue)
            {
                // Fetch pending orders for the user
                var orders = await _ordersRepository.GetOrdersByStatusAsync("Pending");
                var order = orders.FirstOrDefault(o => o.UserId == userId.Value);

                if (order == null)
                {
                    // Create a new order if none exists
                    order = new Order
                    {
                        UserId = userId.Value,
                        Status = "Pending",
                        OrderItems = new List<OrderItem>(),
                        deliveryLocation = deliveryLocation // Set the delivery location
                    };
                    await _ordersRepository.CreateOrderAsync(order); // Use the repository method
                }
                else
                {
                    // Update the delivery location for an existing order
                    order.deliveryLocation = deliveryLocation;
                }

                // Add the product as an order item
                var product = await _productService.GetProductByIdAsync(productId); // Assuming this method exists
                var orderItem = new OrderItem
                {
                    ProductId = productId,
                    Quantity = quantity,
                    Subtotal = product.Price * quantity
                };

                order.OrderItems.Add(orderItem);

                // Save the changes to the repository
                await _ordersRepository.UpdateOrderStatusAsync(order.Id, order.Status); // Update via existing method

                return RedirectToPage("/Buyer/MyOrder");
            }

            return BadRequest();
        }


    }



}


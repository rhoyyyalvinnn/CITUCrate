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
        {
            _productService = productService;
            _logger = logger;
            _ordersRepository = ordersRepository;
        }


        // Property to store product data
        public List<ProductDTO> Products { get; set; }

        public string Category { get; set; }
        // Fetch users from the service and populate the Users property
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

        public async Task<IActionResult> OnPostAddToCartAsync([FromForm] AddToCartRequest request)
        {
            try
            {
                var userId = HttpContext.Session.GetInt32("UserId");
                if (!userId.HasValue) return Unauthorized();

                var product = await _productService.GetProductByIdAsync(request.ProductId);
                if (product == null) return NotFound();

                var orderItem = new OrderItem
                {
                    ProductId = product.ID,
                    Quantity = request.Quantity,
                    Subtotal = request.Quantity * product.Price
                };

                var order = new Order
                {
                    UserId = userId.Value,
                    Total = orderItem.Subtotal,
                    OrderItems = new List<OrderItem> { orderItem }
                };

                await _ordersRepository.CreateOrderAsync(order);
                return RedirectToPage();  // Redirect back to the same page after adding to cart
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding to cart.");
                return StatusCode(500);
            }
        }



    }
}

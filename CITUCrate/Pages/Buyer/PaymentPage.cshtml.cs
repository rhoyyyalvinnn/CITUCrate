using CITUCrate.Models;
using CITUCrate.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace CITUCrate.Pages.Buyer
{
    public class PaymentPageModel : PageModel
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly UserContext _context; // Inject UserContext

        public PaymentPageModel(IOrdersRepository ordersRepository, UserContext context)
        {
            _ordersRepository = ordersRepository;
            _context = context; // Initialize the context
        }

        public Order Order { get; set; }

        public async Task OnGetAsync(int id)
        {
            Order = await _ordersRepository.GetOrderByIdAsync(id);
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            // Retrieve the order by ID
            var order = await _ordersRepository.GetOrderByIdAsync(id);

            if (order == null)
            {
                return NotFound(); // Return if order is not found
            }

            // Increment TotalSales for each product in the order
            foreach (var orderItem in order.OrderItems)
            {
                var product = orderItem.Product;

                // Increment TotalSales by the quantity of the product ordered
                product.TotalSales += orderItem.Quantity; // Assuming TotalSales is updated based on quantity

                // Update product in the context
                _context.Products.Update(product);
            }

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Redirect to homepage or another page
            return RedirectToPage("/Buyer/buyerhomepage");
        }
    }
}

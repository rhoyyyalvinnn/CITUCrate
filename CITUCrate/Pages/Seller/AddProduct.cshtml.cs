using CITUCrate.DTO;
using CITUCrate.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace CITUCrate.Pages.Seller
{
    public class AddProductModel : PageModel
    {
        private readonly IProductService _productService;

        public AddProductModel(IProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public AddUpdateProductDTO NewProduct { get; set; }
        [BindProperty]
        public IFormFile ImageFile { get; set; } // For handling file uploads

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var success = await _productService.AddProductAsync(NewProduct, ImageFile);
            if (success == null) // Example: Return `null` for unexpected failures
            {
                TempData["ErrorMessage"] = "An unexpected error occurred.";
                return Page();
            }
            else if (success == false) // Product already exists
            {
                TempData["ProductAlreadyExists"] = true;
                return Page();
            }

            return RedirectToPage("/Seller/SellerHomepage");
        }
    }
}

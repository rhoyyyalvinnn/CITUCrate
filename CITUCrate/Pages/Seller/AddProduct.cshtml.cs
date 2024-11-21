using CITUCrate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CITUCrate.Pages.Seller
{
    public class AddProductModel : PageModel
    {
        private readonly UserContext _context;

        public AddProductModel(UserContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product NewProduct { get; set; }
        public IFormFile ImageFile { get; set; }  // Updated to IFormFile for file handling

        public void OnGet()
        {
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Check if the product name already exists
            var existingProduct = await _context.Products
                .FirstOrDefaultAsync(p => p.Name == NewProduct.Name);

            if (existingProduct != null)
            {
                // Set a flag to indicate product exists (we'll use this in JavaScript)
                TempData["ProductExists"] = true;
                return Page();  // Return to the same page to show the message
            }

            // Handle image upload (same as your original code)
            if (ImageFile != null && ImageFile.Length > 0)
            {
                var fileName = Path.GetFileName(ImageFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                NewProduct.ImageUrl = "/images/productimages/" + fileName;
            }
            else
            {
                NewProduct.ImageUrl = "/images/default.jpg";
            }

            if (string.IsNullOrEmpty(NewProduct.Category))
            {
                NewProduct.Category = "Uncategorized";
            }

            try
            {
                _context.Products.Add(NewProduct);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log error or set a message for the user
                TempData["ErrorMessage"] = "An error occurred: " + ex.Message;
                return Page();  // Stay on the same page to show the error
            }

            //_context.Products.Add(NewProduct);
            //await _context.SaveChangesAsync();

            return RedirectToPage("/Seller/SellerHomepage");
        }
    }
}

using CITUCrate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

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

            // Handle image upload
            if (ImageFile != null)
            {
                // Ensure a unique filename for the image
                var fileName = Path.GetFileName(ImageFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                // Save the image to the server
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                // Store the relative image path in the database
                NewProduct.ImageUrl = "/images/productimages/" + fileName;
            }
            else
            {
                // Default image if no file is uploaded
                NewProduct.ImageUrl = "/images/default.jpg";
            }

            // Set default category if not provided
            if (string.IsNullOrEmpty(NewProduct.Category))
            {
                NewProduct.Category = "Uncategorized"; // Default category
            }

            // Add the new product to the database
            _context.Products.Add(NewProduct);
            await _context.SaveChangesAsync();

            // Redirect to the product listing page
            return RedirectToPage("/Seller/SellerHomepage");
        }
    }
}

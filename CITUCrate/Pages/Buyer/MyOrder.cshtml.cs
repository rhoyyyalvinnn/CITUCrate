using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CITUCrate.Models;
using CITUCrate.Repositories;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CITUCrate.Pages.Buyer
{
    public class MyOrderModel : PageModel
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IUserRepository _userRepository;

        public MyOrderModel(IOrdersRepository ordersRepository, IUserRepository userRepository)
        {
            _ordersRepository = ordersRepository;
            _userRepository = userRepository;
        }

        public List<Order> Orders { get; set; }

        public async Task OnGetAsync()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId.HasValue)
            {
                Orders = await _ordersRepository.GetOrdersByStatusAsync("Pending");
                Orders = Orders.Where(o => o.UserId == userId.Value).ToList();
            }
        }
    }
}


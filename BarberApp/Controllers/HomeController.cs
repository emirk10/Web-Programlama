using BarberApp.Models;
using BarberApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BarberApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly BarberDbContext _context;
        public HomeController(BarberDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var services = _context.Services.Include(x => x.Category).ToList();
            var barbers = _context.Barbers.ToList();
            ServiceBarberViewModel serviceBarber = new ServiceBarberViewModel { Barbers = barbers,Services = services};
            return View(serviceBarber);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
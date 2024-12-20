using BarberApp.Models;
using BarberApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BarberApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly BarberDbContext _context;
        public AdminController(BarberDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var admin1 = _context.Admins.FirstOrDefault() ?? throw new Exception("Admin bulunamadı. Lütfen bir admin kaydı ekleyin.");
            _context.Barbers.ToList().ForEach(item => item.AdminID = admin1.AdminID);
            _context.Services.ToList().ForEach(item => item.AdminID = admin1.AdminID);
            _context.Appointments.ToList().ForEach(item => item.AdminID = admin1.AdminID);
            _context.Expanses.ToList().ForEach(item => item.AdminID = admin1.AdminID);
            _context.SaveChanges();
            var admin = _context.Admins.Include(x => x.Expanses)
                                       .Include(x => x.Services)
                                       .Include(x => x.Appointments)
                                       .Include(x => x.Barbers)
                                       .ThenInclude(x => x.Schedules)
                                       .Include(x => x.Barbers)
                                       .ThenInclude(x => x.Appointments)
                                       .ThenInclude(x => x.Customer)
                                       .Include(x => x.Barbers)
                                       .ThenInclude(x => x.Appointments)
                                       .ThenInclude(x => x.ServiceAppointments)
                                       .ThenInclude(x => x.Service)
                                       .FirstOrDefault();
            return View(admin);
        }

        public IActionResult ManageBarbers()
        {
            var barbers = _context.Barbers
                                  .Include(x => x.Schedules)
                                  .ToList();
            return View(barbers);
        }

        public IActionResult CreateBarber()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateBarber(Barber model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("ManageBarbers");
            }
            return View(model);
        }

        public IActionResult EditBarber(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult EditBarber(Barber model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("ManageBarbers");
            }
            return View(model);
        }

        public IActionResult DeleteBarber(int id)
        {
            var barber = _context.Barbers
                                 .Include(x => x.Appointments)
                                 .FirstOrDefault(x => x.BarberID == id);
            DateTime today = DateTime.UtcNow;
            if (barber != null)
            {
                foreach (var item in barber.Appointments)
                {
                    if(item.AppointmentDate.Date >= today.Date)
                    {
                        TempData["msg"] = $"{barber.BarberID}-{barber.Name} isimli berberin randevusu oldugu icin silinemedi.";
                        return RedirectToAction("ManageBarbers");
                    }
                }
                _context.Barbers.Remove(barber);
                _context.SaveChanges();
                TempData["msg"] = $"{barber.BarberID}-{barber.Name} isimli berber başarıyla silindi.";
            }
            else 
            {
                TempData["msg"] = $"Id'si {id} olan berber silinirken bir hata meydana geldi.";
            }
            return RedirectToAction("ManageBarbers");
        }

        public IActionResult ManageAppointments()
        {
            var appointments = _context.Appointments
                               .Include(x => x.Barber)
                               .Include(x => x.Customer)
                               .ToList();
            return View(appointments);
        }

        public IActionResult ManageAppointmentStatus(int appointmentId)
        {
            return View();
        }

        public IActionResult ManageServices()
        {
            var services = _context.Services
                          .Include(s => s.Category)
                          .ToList();
            return View(services);
        }

        public IActionResult CreateService()
        {
            var categories = _context.Categories.ToList();
            return View(categories); // Direkt listeyi döndür
        }

        [HttpPost]
        public IActionResult CreateService(Service model)
        {
            if (ModelState.IsValid)
            {
                var category = _context.Categories.Find(model.CategoryID);
                int adminId = _context.Admins.FirstOrDefault().AdminID;
                model.Category = category;
                model.AdminID = adminId;
                _context.Services.Add(model);
                _context.SaveChanges();
                TempData["msg"] = "Service created successfully.";
                return RedirectToAction("ManageServices");
            }

            // Hata mesajlarını toplayarak kullanıcıya ilet
            var categories = _context.Categories.ToList();
            ViewBag.Errors = ModelState.Values
                                        .SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
            return View(categories);
        }



        public IActionResult EditService(int id)
        {
            var service = _context.Services.Include(x => x.Category).FirstOrDefault(x => x.ServiceID == id);
            var categories = _context.Categories.ToList();
            List<Service> serviceList = new List<Service>();
            serviceList.Add(service); ;
            ServiceCategoryViewModel serviceCategoryViewModel = new ServiceCategoryViewModel
            {
                Services = serviceList,
                Categories = categories
            };
            return View(serviceCategoryViewModel);
        }

        [HttpPost]
        public IActionResult EditService(Service model)
        {
            if (ModelState.IsValid)
            {
                int adminId = _context.Admins.FirstOrDefault().AdminID;
                model.AdminID = adminId;
                _context.Services.Update(model);
                _context.SaveChanges();
                TempData["msg"] = "Service updated successfully.";
                return RedirectToAction("ManageServices");
            }
            return View(model);
        }

        public IActionResult DeleteService(int id)
        {
            var service = _context.Services
                                  .Include(s => s.ServiceAppointments)
                                  .FirstOrDefault(s => s.ServiceID == id);

            if (service == null)
            {
                TempData["msg"] = "Service not found.";
                return RedirectToAction("ManageServices");
            }

            if (service.ServiceAppointments.Any())
            {
                TempData["msg"] = "Service cannot be deleted because it is associated with appointments.";
                return RedirectToAction("ManageServices");
            }

            _context.Services.Remove(service);
            _context.SaveChanges();
            TempData["msg"] = "Service deleted successfully.";
            return RedirectToAction("ManageServices");
        }


        [HttpGet]
        [Route("Admin/GetAppointmentsByDateAndBarber")]
        public IActionResult GetAppointmentsByDateAndBarber(int barberId, DateTime date)
        {
            var appointments = _context.Appointments
                                      .Include(x => x.Customer)
                                      .Include(x => x.ServiceAppointments)
                                          .ThenInclude(x => x.Service)
                                      .Where(x => x.BarberID == barberId &&
                                                  x.AppointmentDate.ToUniversalTime().Date == date.ToUniversalTime().Date)
                                      .ToList();

            if (appointments == null || appointments.Count == 0)
            {
                return NoContent();
            }

            var totalEarnings = appointments
                                .SelectMany(a => a.ServiceAppointments)
                                .Sum(sa => sa.Service.Price);

            var result = new
            {
                Appointments = appointments,
                DailyEarnings = totalEarnings
            };

            return Json(result);
        }

        [HttpPost]
        public IActionResult ChangeAppointmentStatus(int appointmentId, int status)
        {
            var appointment = _context.Appointments.FirstOrDefault(a => a.AppointmentID == appointmentId);
            if (appointment == null)
            {
                TempData["msg"] = "Randevu bulunamadı.";
                return RedirectToAction("ManageAppointments");
            }

            if (Enum.IsDefined(typeof(AppointmentStatus), status))
            {
                appointment.Status = (AppointmentStatus)status;
                _context.SaveChanges();
                TempData["msg"] = $"Appointment status is successfully set to {(AppointmentStatus)status}.";
            }
            else
            {
                TempData["msg"] = "Geçersiz bir durum değeri girildi.";
            }

            return RedirectToAction("ManageAppointments");
        }

        [HttpPost]
        public IActionResult DeleteAppointment(int id)
        {
            var appointment = _context.Appointments
                                       .Include(a => a.ServiceAppointments)
                                       .FirstOrDefault(a => a.AppointmentID == id);

            if (appointment == null)
            {
                TempData["msg"] = "Randevu bulunamadı.";
                return RedirectToAction("ManageAppointments");
            }

            // ServiceAppointment ilişkilerini kaldır
            _context.ServiceAppointments.RemoveRange(appointment.ServiceAppointments);

            // Randevuyu sil
            _context.Appointments.Remove(appointment);
            _context.SaveChanges();
            TempData["msg"] = "Randevu başarıyla silindi.";
            return RedirectToAction("ManageAppointments");
        }



    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FitnessCenterSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessCenterSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Dashboard()
        {
            var stats = new
            {
                TotalMembers = _context.Members.Count(),
                TotalTrainers = _context.Trainers.Count(),
                TotalAppointments = _context.Appointments.Count(),
                TotalGyms = _context.Gyms.Count()
            };

            return View(stats);
        }

        // CRUD işlemleri için action'lar
        public IActionResult ManageMembers()
        {
            var members = _context.Members.ToList();
            return View(members);
        }

        public IActionResult ManageTrainers()
        {
            var trainers = _context.Trainers.Include(t => t.Gym).ToList();
            return View(trainers);
        }

        public IActionResult ManageAppointments()
        {
            var appointments = _context.Appointments
                .Include(a => a.Member)
                .Include(a => a.Trainer)
                .Include(a => a.Gym)
                .ToList();
            return View(appointments);
        }

        [HttpPost]
        public IActionResult UpdateAppointmentStatus(int id, string status)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment != null)
            {
                appointment.Status = status;
                _context.SaveChanges();
            }
            return RedirectToAction("ManageAppointments");
        }
    }
}

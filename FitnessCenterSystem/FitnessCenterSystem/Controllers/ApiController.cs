using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FitnessCenterSystem.Models;

namespace FitnessCenterSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Api/trainers
        [HttpGet("trainers")]
        public IActionResult GetTrainers()
        {
            var trainers = _context.Trainers
                .Include(t => t.Gym)
                .Select(t => new
                {
                    t.Id,
                    t.FirstName,
                    t.LastName,
                    t.Specialization,
                    t.AvailableHours,
                    t.HourlyRate,
                    GymName = t.Gym.Name
                })
                .ToList();

            return Ok(trainers);
        }

        // GET: api/Api/availabletrainers?date=2024-01-15
        [HttpGet("availabletrainers")]
        public IActionResult GetAvailableTrainers(DateTime date)
        {
            var busyTrainerIds = _context.Appointments
                .Where(a => a.AppointmentDate.Date == date.Date && a.Status != "Cancelled")
                .Select(a => a.TrainerId)
                .Distinct()
                .ToList();

            var availableTrainers = _context.Trainers
                .Where(t => !busyTrainerIds.Contains(t.Id))
                .Include(t => t.Gym)
                .Select(t => new
                {
                    t.Id,
                    Name = t.FirstName + " " + t.LastName,
                    t.Specialization,
                    t.AvailableHours,
                    GymName = t.Gym.Name
                })
                .ToList();

            return Ok(availableTrainers);
        }

        // GET: api/Api/memberappointments?memberId=1
        [HttpGet("memberappointments")]
        public IActionResult GetMemberAppointments(int memberId)
        {
            var appointments = _context.Appointments
                .Where(a => a.MemberId == memberId)
                .Include(a => a.Trainer)
                .Include(a => a.Gym)
                .Select(a => new
                {
                    a.Id,
                    a.AppointmentDate,
                    a.ServiceType,
                    a.Duration,
                    a.Price,
                    a.Status,
                    TrainerName = a.Trainer.FirstName + " " + a.Trainer.LastName,
                    GymName = a.Gym.Name
                })
                .ToList();

            return Ok(appointments);
        }

        // GET: api/Api/appointmentsbydate?startDate=2024-01-01&endDate=2024-01-31
        [HttpGet("appointmentsbydate")]
        public IActionResult GetAppointmentsByDate(DateTime startDate, DateTime endDate)
        {
            var appointments = _context.Appointments
                .Where(a => a.AppointmentDate >= startDate && a.AppointmentDate <= endDate)
                .Include(a => a.Member)
                .Include(a => a.Trainer)
                .Include(a => a.Gym)
                .Select(a => new
                {
                    a.Id,
                    a.AppointmentDate,
                    a.ServiceType,
                    a.Status,
                    MemberName = a.Member.FirstName + " " + a.Member.LastName,
                    TrainerName = a.Trainer.FirstName + " " + a.Trainer.LastName,
                    GymName = a.Gym.Name
                })
                .ToList();

            return Ok(appointments);
        }
    }
}

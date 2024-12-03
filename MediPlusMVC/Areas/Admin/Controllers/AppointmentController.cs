using MediPlusMVC.DAL.Contexts;
using MediPlusMVC.DTO.AppointmentDTO;
using MediPlusMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MediPlusMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AppointmentController : Controller
    {
        readonly AppDBContext _context;
        public AppointmentController(AppDBContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Appointment> appointments = await _context.Appointments.ToListAsync();
            return View(appointments);
        }
        public IActionResult Create()
        {
            var activeDoctors = _context.Doctors
                .Where(d => d.isActive)
                .Select(d => new { d.Id, d.Username })
                .ToList();

            var activePatients = _context.Patients
                .Where(p => p.isActive)
                .Select(p => new { p.Id, p.Username })
                .ToList();

            ViewBag.Doctors = new SelectList(activeDoctors, "Id", "Username");
            ViewBag.Patients = new SelectList(activePatients, "Id", "Username");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AppointmentDTO appointmentDTO)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Inputs cannot be empty.");
                return View(appointmentDTO);
            }

            bool doctorExists = await _context.Doctors.AnyAsync(d => d.Id == appointmentDTO.DoctorId);
            bool patientExists = await _context.Patients.AnyAsync(p => p.Id == appointmentDTO.PatientId);
            if (!doctorExists || !patientExists)
            {
                ModelState.AddModelError("", "The specified doctor or patient does not exist.");
                return View(appointmentDTO);
            }
            
            var startOfHour = appointmentDTO.AppointmentDate.AddMinutes(-appointmentDTO.AppointmentDate.Minute)
            .AddSeconds(-appointmentDTO.AppointmentDate.Second)
            .AddMilliseconds(-appointmentDTO.AppointmentDate.Millisecond);

            var endOfHour = startOfHour.AddHours(1);

            bool doctorHasAppointment = await _context.Appointments.AnyAsync(a =>
                a.DoctorId == appointmentDTO.DoctorId &&
                a.AppointmentDate >= startOfHour &&
                a.AppointmentDate < endOfHour);

            if (doctorHasAppointment)
            {
                ModelState.AddModelError("", "The selected doctor already has an appointment during this time.");
                return View(appointmentDTO);
            }

            Appointment appointment = new()
            {
                DoctorId = appointmentDTO.DoctorId,
                PatientId = appointmentDTO.PatientId,
                AppointmentDate = appointmentDTO.AppointmentDate,
                CreatedAt = DateTime.Now
            };
            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Appointment");
        }

        public async Task<IActionResult> SoftDelete(int Id)
        {
            Appointment? appointment = await _context.Appointments.FirstOrDefaultAsync(d => d.Id == Id);
            if (appointment == null)
            {
                return NotFound("Appointment cannot be found.");
            }
            if (!appointment.isActive)
            {
                return BadRequest("Appointment has already been deleted");
            }
            appointment.isActive = false;
            appointment.DeletedAt = DateTime.Now;
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Appointment");
        }

        public async Task<IActionResult> RevertSoftDelete(int Id)
        {
            Appointment? appointment = await _context.Appointments.FirstOrDefaultAsync(d => d.Id == Id);
            if (appointment == null)
            {
                return NotFound("Appointment cannot be found.");
            }
            if (appointment.isActive)
            {
                return BadRequest("Appointment is already active.");
            }
            appointment.isActive = true;
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Appointment");
        }
        public async Task<IActionResult> HardDelete(int Id)
        {
            Appointment? appointment = await _context.Appointments.FirstOrDefaultAsync(d => d.Id == Id);
            if (appointment == null)
            {
                return NotFound("Appointment cannot be found.");
            }
            if (appointment.isActive)
            {
                return BadRequest("Appointment can not be deleted at the moment. Because it is active.");
            }
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Appointment");
        }


        public async Task<IActionResult> Update(int Id)
        {
            Appointment? appointment = await _context.Appointments.FirstOrDefaultAsync(d => d.Id == Id);
            if (appointment == null)
            {
                return BadRequest("Appointment can not be found.");
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Inputs can not be null.");
                return View(appointment);
            }
            AppointmentDTO appointmentDTO = new() { };
            appointmentDTO.Id = appointment.Id;
            appointmentDTO.DoctorId = appointment.DoctorId;
            appointmentDTO.PatientId = appointment.PatientId;
            appointmentDTO.AppointmentDate = appointment.AppointmentDate;
            appointmentDTO.isActive = appointment.isActive;
            return View(appointmentDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Update(AppointmentDTO updatedAppointment)
        {
            Appointment? appointment = await _context.Appointments.FirstOrDefaultAsync(d => d.Id == updatedAppointment.Id);
            if (appointment == null)
            {
                return BadRequest("Appointment can not be found.");
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Inputs cannot be null");
                return View(appointment);
            }
            appointment.DoctorId = updatedAppointment.DoctorId;
            appointment.PatientId = updatedAppointment.PatientId;
            appointment.AppointmentDate = updatedAppointment.AppointmentDate;
            appointment.UpdatedAt = DateTime.Now;
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Appointment");
        }
    }
}

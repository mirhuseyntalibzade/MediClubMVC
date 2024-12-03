using MediPlusMVC.DAL.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediPlusMVC.Models;
using MediPlusMVC.DTO.DoctorDTO;

namespace MediPlusMVC.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class DoctorController : Controller
    {
        readonly AppDBContext _context;
        public DoctorController(AppDBContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Doctor> doctors = await _context.Doctors.ToListAsync();
            return View(doctors);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DoctorDTO doctorDTO)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Inputs cannot be empty.");
                return View(doctorDTO);
            }
            Doctor doctor = new(){};
            doctor.FirstName = doctorDTO.FirstName;
            doctor.LastName = doctorDTO.LastName;
            doctor.PhoneNumber = doctorDTO.PhoneNumber;
            doctor.Email= doctorDTO.Email;
            doctor.Fincode = doctorDTO.Fincode;
            doctor.Username = $"{doctorDTO.FirstName}{doctorDTO.Fincode}";
            doctor.CreatedAt = DateTime.Now;
            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Doctor");
        }

        public async Task<IActionResult> SoftDelete(int Id)
        {
            Doctor? doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == Id);
            if (doctor == null)
            {
                return NotFound("Doctor cannot be found.");
            }
            if (!doctor.isActive)
            {
                return BadRequest("Doctor has already been deleted");
            }
            doctor.isActive = false;
            doctor.DeletedAt = DateTime.Now;
            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Doctor");
        }

        public async Task<IActionResult> RevertSoftDelete(int Id)
        {
            Doctor? doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == Id);
            if (doctor == null)
            {
                return NotFound("Doctor cannot be found.");
            }
            if (doctor.isActive)
            {
                return BadRequest("Doctor is already active.");
            }
            doctor.isActive = true;
            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Doctor");
        }
        public async Task<IActionResult> HardDelete(int Id)
        {
            Doctor? doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == Id);
            if (doctor == null)
            {
                return NotFound("Doctor cannot be found.");
            }
            if (doctor.isActive)
            {
                return BadRequest("Doctor can not be deleted at the moment. Because it is active.");
            }
            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Doctor");
        }


        public async Task<IActionResult> Update(int Id)
        {
            Doctor? doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == Id);
            if (doctor == null)
            {
                return BadRequest("Doctor can not be found.");
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Inputs can not be null.");
                return View(doctor);
            }
            DoctorDTO doctorDTO = new() { };
            doctorDTO.Id = doctor.Id;
            doctorDTO.FirstName = doctor.FirstName;
            doctorDTO.LastName = doctor.LastName;
            doctorDTO.PhoneNumber = doctor.PhoneNumber;
            doctorDTO.Fincode = doctor.Fincode;
            doctorDTO.Email = doctor.Email;
            doctorDTO.isActive = doctor.isActive;
            return View(doctorDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Update(DoctorDTO updatedDoctor)
        {
            Doctor? doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == updatedDoctor.Id);
            if (doctor == null)
            {
                return BadRequest("Doctor can not be found.");
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Inputs cannot be null");
                return View(doctor);
            }
            doctor.FirstName = updatedDoctor.FirstName;
            doctor.LastName = updatedDoctor.LastName;
            doctor.PhoneNumber = updatedDoctor.PhoneNumber;
            doctor.Email = updatedDoctor.Email;
            doctor.Fincode = updatedDoctor.Fincode;
            doctor.Username = $"{updatedDoctor.FirstName}{updatedDoctor.Fincode}";
            doctor.UpdatedAt = DateTime.Now;
            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Doctor");
        }
    }
}

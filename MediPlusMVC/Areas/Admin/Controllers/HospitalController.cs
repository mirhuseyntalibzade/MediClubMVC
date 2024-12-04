using MediPlusMVC.DAL.Contexts;
using MediPlusMVC.DTO.HospitalDTO;
using MediPlusMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MediPlusMVC.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class HospitalController : Controller
    {
        readonly AppDBContext _context;
        public HospitalController(AppDBContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Hospital> hospitals = await _context.Hospitals
        .Include(h => h.HospitalDoctors)
        .ThenInclude(hd => hd.Doctor)
        .ToListAsync();
            return View(hospitals);
        }
        public IActionResult Create()
        {
            ViewBag.Doctors = new SelectList(_context.Doctors, "Id", "Username");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(HospitalDTO hospitalDTO)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Doctors = new SelectList(_context.Doctors, "Id", "Username");
                ModelState.AddModelError("", "Inputs cannot be empty.");
                return View(hospitalDTO);
            }

            Hospital hospital = new() { };
            hospital.HospitalName = hospitalDTO.HospitalName;
            hospital.CreatedAt = DateTime.Now;
            hospital.HospitalDoctors = new List<HospitalDoctor>();
            if (hospitalDTO.DoctorIds != null && hospitalDTO.DoctorIds.Any())
            {
                foreach (var doctorId in hospitalDTO.DoctorIds)
                {
                    hospital.HospitalDoctors.Add(new HospitalDoctor
                    {
                        DoctorId = doctorId
                    });
                }
            }
            await _context.Hospitals.AddAsync(hospital);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Hospital");
        }

        public async Task<IActionResult> SoftDelete(int Id)
        {
            Hospital? hospital = await _context.Hospitals.FirstOrDefaultAsync(d => d.Id == Id);
            if (hospital == null)
            {
                return NotFound("Hospital cannot be found.");
            }
            if (!hospital.isActive)
            {
                return BadRequest("Hospital has already been deleted");
            }
            hospital.isActive = false;
            hospital.DeletedAt = DateTime.Now;
            _context.Hospitals.Update(hospital);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Hospital");
        }
        public async Task<IActionResult> RevertSoftDelete(int Id)
        {
            Hospital? hospital = await _context.Hospitals.FirstOrDefaultAsync(d => d.Id == Id);
            if (hospital == null)
            {
                return NotFound("Hospital cannot be found.");
            }
            if (hospital.isActive)
            {
                return BadRequest("Hospital is already active.");
            }
            hospital.isActive = true;
            _context.Hospitals.Update(hospital);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Hospital");
        }
        public async Task<IActionResult> HardDelete(int Id)
        {
            Hospital? hospital = await _context.Hospitals.FirstOrDefaultAsync(d => d.Id == Id);
            if (hospital == null)
            {
                return NotFound("Hospital cannot be found.");
            }
            if (hospital.isActive)
            {
                return BadRequest("Hospital can not be deleted at the moment. Because it is active.");
            }
            _context.Hospitals.Remove(hospital);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Hospital");
        }


        public async Task<IActionResult> Update(int Id)
        {
            Hospital? hospital = await _context.Hospitals.FirstOrDefaultAsync(d => d.Id == Id);
            if (hospital == null)
            {
                return BadRequest("Hospital can not be found.");
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Inputs can not be null.");
                return View(hospital);
            }
            ViewBag.Doctors = new SelectList(_context.Doctors, "Id", "Username");

            HospitalDTO hospitalDTO = new() { };
            hospitalDTO.Id = hospital.Id;
            hospitalDTO.HospitalName = hospital.HospitalName;
            hospitalDTO.isActive = hospital.isActive;
            return View(hospitalDTO);
        }
        [HttpPost]
        public async Task<IActionResult> Update(HospitalDTO updatedHospital)
        {
            Hospital? hospital = await _context.Hospitals
                .Include(h => h.HospitalDoctors)
                .FirstOrDefaultAsync(h => h.Id == updatedHospital.Id);

            if (hospital == null)
            {
                return NotFound("Hospital cannot be found.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Doctors = new SelectList(_context.Doctors, "Id", "Username");

                ModelState.AddModelError("", "Inputs cannot be null.");
                return View(updatedHospital);
            }

            hospital.HospitalName = updatedHospital.HospitalName;
            hospital.UpdatedAt = DateTime.Now;

            hospital.HospitalDoctors.Clear();

            if (updatedHospital.DoctorIds != null && updatedHospital.DoctorIds.Any())
            {
                foreach (var doctorId in updatedHospital.DoctorIds)
                {
                    hospital.HospitalDoctors.Add(new HospitalDoctor
                    {
                        DoctorId = doctorId,
                        HospitalId = hospital.Id
                    });
                }
            }

            _context.Hospitals.Update(hospital);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Hospital");
        }
    }
}

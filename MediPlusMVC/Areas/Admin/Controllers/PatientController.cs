using MediPlusMVC.DAL.Contexts;
using MediPlusMVC.DTO.PatientDTO;
using MediPlusMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MediPlusMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PatientController : Controller
    {
        readonly AppDBContext _context;
        public PatientController(AppDBContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Patient> patients = await _context.Patients.ToListAsync();
            return View(patients);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PatientDTO patientDTO)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Inputs cannot be empty.");
                return View(patientDTO);
            }
            Patient patient = new() { };
            patient.Name = patientDTO.FirstName;
            patient.Surname = patientDTO.LastName;
            patient.PhoneNumber = patientDTO.PhoneNumber;
            patient.Fincode = patientDTO.Fincode;
            patient.Username = $"{patientDTO.FirstName}{patientDTO.Fincode}";
            patient.CreatedAt = DateTime.Now;
            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Patient");
        }

        public async Task<IActionResult> SoftDelete(int Id)
        {
            Patient? patient = await _context.Patients.FirstOrDefaultAsync(d => d.Id == Id);
            if (patient == null)
            {
                return NotFound("Patient cannot be found.");
            }
            if (!patient.isActive)
            {
                return BadRequest("Patient has already been deleted");
            }
            patient.isActive = false;
            patient.DeletedAt = DateTime.Now;
            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Patient");
        }

        public async Task<IActionResult> RevertSoftDelete(int Id)
        {
            Patient? patient = await _context.Patients.FirstOrDefaultAsync(d => d.Id == Id);
            if (patient == null)
            {
                return NotFound("Patient cannot be found.");
            }
            if (patient.isActive)
            {
                return BadRequest("Patient is already active.");
            }
            patient.isActive = true;
            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Patient");
        }
        public async Task<IActionResult> HardDelete(int Id)
        {
            Patient? patient = await _context.Patients.FirstOrDefaultAsync(d => d.Id == Id);
            if (patient == null)
            {
                return NotFound("Patient cannot be found.");
            }
            if (patient.isActive)
            {
                return BadRequest("Patient can not be deleted at the moment. Because it is active.");
            }
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Patient");
        }


        public async Task<IActionResult> Update(int Id)
        {
            Patient? patient = await _context.Patients.FirstOrDefaultAsync(d => d.Id == Id);
            if (patient == null)
            {
                return BadRequest("Patient can not be found.");
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Inputs can not be null.");
                return View(patient);
            }
            PatientDTO patientDTO = new() { };
            patientDTO.Id = patient.Id;
            patientDTO.FirstName = patient.Name;
            patientDTO.LastName = patient.Surname;
            patientDTO.PhoneNumber = patient.PhoneNumber;
            patientDTO.Fincode = patient.Fincode;
            patientDTO.isActive = patient.isActive;
            return View(patientDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Update(PatientDTO updatedPatient)
        {
            Patient? patient = await _context.Patients.FirstOrDefaultAsync(d => d.Id == updatedPatient.Id);
            if (patient == null)
            {
                return BadRequest("Patient can not be found.");
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Inputs cannot be null");
                return View(patient);
            }
            patient.Name = updatedPatient.FirstName;
            patient.Surname = updatedPatient.LastName;
            patient.PhoneNumber = updatedPatient.PhoneNumber;
            patient.Fincode = updatedPatient.Fincode;
            patient.Username = $"{updatedPatient.FirstName}{updatedPatient.Fincode}";
            patient.UpdatedAt = DateTime.Now;
            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Patient");
        }
    }
}

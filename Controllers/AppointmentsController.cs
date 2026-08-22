using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly HospitalDbContext _context;

        public AppointmentsController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var appointments = await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .ToListAsync();

            return View(appointments);
        }
        // GET: Appointments/Create
        public IActionResult Create()
        {
            ViewBag.Patients = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(
                _context.Patients,
                "PatientId",
                "FullName");

            ViewBag.Doctors = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(
                _context.Doctors,
                "DoctorId",
                "FullName");

            return View();
        }

        // POST: Appointments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Appointments.Add(appointment);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Patients = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(
                _context.Patients,
                "PatientId",
                "FullName",
                appointment.PatientId);

            ViewBag.Doctors = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(
                _context.Doctors,
                "DoctorId",
                "FullName",
                appointment.DoctorId);

            return View(appointment);
        }
        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(a => a.AppointmentId == id);

            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }
        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments.FindAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            ViewBag.Patients = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(
                _context.Patients,
                "PatientId",
                "FullName",
                appointment.PatientId);

            ViewBag.Doctors = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(
                _context.Doctors,
                "DoctorId",
                "FullName",
                appointment.DoctorId);

            return View(appointment);
        }
        // POST: Appointments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            Appointment appointment)
        {
            if (id != appointment.AppointmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.AppointmentId))
                    {
                        return NotFound();
                    }

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Patients = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(
                _context.Patients,
                "PatientId",
                "FullName",
                appointment.PatientId);

            ViewBag.Doctors = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(
                _context.Doctors,
                "DoctorId",
                "FullName",
                appointment.DoctorId);

            return View(appointment);
        }
        private bool AppointmentExists(int id)
        {
            return _context.Appointments
                .Any(e => e.AppointmentId == id);
        }
        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(a => a.AppointmentId == id);

            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }
        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _context.Appointments
                .FindAsync(id);

            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
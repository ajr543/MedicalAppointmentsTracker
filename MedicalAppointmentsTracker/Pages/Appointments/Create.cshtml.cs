using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MedicalAppointmentsTracker.Data;
using MedicalAppointmentsTracker.Models;
using System.Text.Json;

namespace MedicalAppointmentsTracker.Pages.Appointments
{
    public class CreateModel : PageModel
    {
        private readonly MedicalAppointmentsTracker.Data.MedicalAppointmentsTrackerContext _context;

        public CreateModel(MedicalAppointmentsTracker.Data.MedicalAppointmentsTrackerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Appointment Appointment { get; set; } = default!;

        public IActionResult OnGet()
        {
            var userData = HttpContext.Session.GetString("UserData");

            if (userData == null)
            {
                return RedirectToPage("/Account/Login");
            }

            return Page();
        }

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userData = HttpContext.Session.GetString("UserData");

            if (userData == null)
            {
                return RedirectToPage("/Account/Login");
            }

            var userObj = JsonSerializer.Deserialize<UserSession>(userData);

            Appointment.UserId = userObj.UserId;

            _context.Appointment.Add(Appointment);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Appointments/Index");
        }
    }
}

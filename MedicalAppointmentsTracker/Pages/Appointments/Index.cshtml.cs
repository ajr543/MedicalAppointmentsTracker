using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MedicalAppointmentsTracker.Data;
using MedicalAppointmentsTracker.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;

namespace MedicalAppointmentsTracker.Pages.Appointments
{
    public class IndexModel : PageModel
    {
        private readonly MedicalAppointmentsTrackerContext _context;

        public IndexModel(MedicalAppointmentsTrackerContext context)
        {
            _context = context;
        }

        public IList<Appointment> Appointment { get; set; } = default!;

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        [DataType(DataType.Date)]
        [BindProperty(SupportsGet = true)]
        public DateTime? AppointmentStartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        [DataType(DataType.Date)]
        [BindProperty(SupportsGet = true)]
        public DateTime? AppointmentEndDate { get; set; }


        [BindProperty(SupportsGet = true)]
        public string? Status { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? CurrentSort { get; set; }


        public async Task<ActionResult> OnGetAsync(string sortOrder)
        {
            var userData = HttpContext.Session.GetString("UserData");

            if (userData == null)
            {
                return RedirectToPage("/Account/Login");
            }

            var userObj = JsonSerializer.Deserialize<UserSession>(userData);


            var appointments = from a in _context.Appointment
                               where a.UserId == userObj.UserId
                               select a;

            if (!string.IsNullOrEmpty(Status))
            {
                appointments = appointments.Where(x => x.Status == Status);
            }


            if (AppointmentStartDate.HasValue && AppointmentEndDate.HasValue)
            {
                appointments = appointments.Where(s => s.AppointmentDate.Date >= AppointmentStartDate && s.AppointmentDate.Date <= AppointmentEndDate);
            }
            else if (AppointmentStartDate.HasValue)
            {
                appointments = appointments.Where(s => s.AppointmentDate.Date >= AppointmentStartDate);
            }
            else if (AppointmentEndDate.HasValue)
            {
                appointments = appointments.Where(s => s.AppointmentDate.Date <= AppointmentEndDate);
            }

            // Determine sort direction
            switch (sortOrder)
            {
                case "date_desc":
                    appointments = appointments.OrderByDescending(u => u.AppointmentDate);
                    break;
                default:
                    appointments = appointments.OrderBy(u => u.AppointmentDate); // Default sorting
                    break;
            }

            CurrentSort = sortOrder;
           
            Appointment = await appointments.ToListAsync();

            return Page();
        }
    }
}

using MedicalAppointmentsTracker.Data;
using MedicalAppointmentsTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace MedicalAppointmentsTracker.Pages.Account
{
    public class LoginModel : PageModel
    {

        private readonly MedicalAppointmentsTrackerContext _context;

        public LoginModel(MedicalAppointmentsTrackerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public LoginUserModel Input { get; set; } = default!;

        public string SuccessMessage { get; set; }

        public void OnGet(string successMessage)
        {
            SuccessMessage = successMessage;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == Input.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User not found.");
                return Page();
            }

            // Check if the password is correct
            var isPasswordCorrect = PasswordHasher.Verify(Input.Password, user.PasswordHash);

            if (!isPasswordCorrect)
            {
                ModelState.AddModelError(string.Empty, "Invalid password.");
                return Page();
            }

            // Store object as JSON
            var userSession = new UserSession { Name = user.Name, UserId = user.Id, Email= user.Email };
            HttpContext.Session.SetString("UserData", JsonSerializer.Serialize(userSession));


            return RedirectToPage("/Appointments/Index");
        }

    }
}

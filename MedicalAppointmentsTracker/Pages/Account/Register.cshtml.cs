using MedicalAppointmentsTracker.Data;
using MedicalAppointmentsTracker.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedicalAppointmentsTracker.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly MedicalAppointmentsTrackerContext _context;

        public RegisterModel(MedicalAppointmentsTrackerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RegisterUserModel Input { get; set; } = default!;

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var hash = PasswordHasher.Hash(Input.Password);
            var user = new UserModel { Name = Input.Name, Email = Input.Email, PasswordHash = hash };
            await _context.Users.AddAsync(user);
            var res = await _context.SaveChangesAsync();

            if (res > 0)
            {
                return RedirectToPage("/Account/Login", new { successMessage = "User successfully created!" });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Registration failed.");
            }

            return Page();

        }
    }
}

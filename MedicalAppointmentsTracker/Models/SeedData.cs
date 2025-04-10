using MedicalAppointmentsTracker.Data;
using Microsoft.EntityFrameworkCore;

namespace MedicalAppointmentsTracker.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MedicalAppointmentsTrackerContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MedicalAppointmentsTrackerContext>>()))
            {
                if (context == null || context.Appointment == null)
                {
                    throw new ArgumentNullException("Null RazorPagesMovieContext");
                }

                if (context.Users.Any())
                {
                    return;   // DB has been seeded
                }

                var hash = PasswordHasher.Hash("john@123");
                var user1 = new UserModel
                {
                    Name = "John Doe",
                    Email = "John@gmail.com",
                    PasswordHash = hash,
                };
                context.Users.AddRange(user1);

                int res = context.SaveChanges();

                if (res > 0)
                {
                    // Look for any appointments.
                    if (context.Appointment.Any())
                    {
                        return;   // DB has been seeded
                    }

                    context.Appointment.AddRange(
                        new Appointment
                        {
                            AppointmentDate = DateTime.Now,
                            Provider = "Dr.Smith",
                            Reason = "Checkup",
                            Status = "Completed",
                            UserId = user1.Id
                        },

                        new Appointment
                        {
                            AppointmentDate = DateTime.Now.AddDays(1),
                            Provider = "Dr.Jones",
                            Reason = "Follow-up",
                            Status = "Upcoming",
                            UserId = user1.Id
                        },

                        new Appointment
                        {
                            AppointmentDate = DateTime.Now.AddDays(2),
                            Provider = "Dr.Brown",
                            Reason = "Consultation",
                            Status = "Cancelled",
                            UserId = user1.Id

                        },

                        new Appointment
                        {
                            AppointmentDate = DateTime.Now.AddDays(3),
                            Provider = "Dr.Wilson",
                            Reason = "Physical Therapy",
                            Status = "Completed",
                            UserId = user1.Id
                        }
                    );
                    context.SaveChanges();
                }
                
            }
        }
    }
}
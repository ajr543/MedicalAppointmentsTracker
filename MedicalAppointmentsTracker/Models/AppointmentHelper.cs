using Microsoft.AspNetCore.Mvc.Rendering;

namespace MedicalAppointmentsTracker.Models
{
    public static class AppointmentHelper
    {
        public static SelectList? StatusList = new SelectList(new List<string> { "Upcoming", "Completed", "Cancelled" });

    }
}

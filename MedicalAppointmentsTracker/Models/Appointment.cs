using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalAppointmentsTracker.Models
{
    public class Appointment
    {

        public int Id { get; set; }
        public string Provider { get; set; }
        [Display(Name = "Appointment Date")]
        [FutureDateRange] // Ensures only future dates (up to 1 year) are allowed
        [DataType(DataType.Date)]
        public DateTime AppointmentDate { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }

        public int UserId { get; set; }

        //public Appointment(int id, string provider, DateTime appointmentDate, string reason, string status)
        //{
        //    Id = id;
        //    Provider = provider;
        //    AppointmentDate = appointmentDate;
        //    Reason = reason;
        //    Status = status;
        //}
    }
}

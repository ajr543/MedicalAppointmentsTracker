using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MedicalAppointmentsTracker.Models;

namespace MedicalAppointmentsTracker.Data
{
    public class MedicalAppointmentsTrackerContext : DbContext
    {
        public MedicalAppointmentsTrackerContext (DbContextOptions<MedicalAppointmentsTrackerContext> options)
            : base(options)
        {
        }

        public DbSet<Appointment> Appointment { get; set; } = default!;
        public DbSet<UserModel> Users { get; set; } = default!;
    }
}

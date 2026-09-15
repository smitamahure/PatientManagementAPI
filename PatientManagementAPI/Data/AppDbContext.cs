using Microsoft.EntityFrameworkCore;
using PatientManagementAPI.Models;

namespace PatientManagementAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
       : base(options)
        {
        }

        public virtual DbSet<Patient> Patients { get; set; }

        public virtual DbSet<User> Users { get; set; }
    }
}

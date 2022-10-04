using DentaClinic.Models;
using Microsoft.EntityFrameworkCore;

namespace DentaClinic.Database
{
    public class WebDbContext : DbContext
    {
        public WebDbContext(DbContextOptions<WebDbContext> options)
            : base(options) { }

        public DbSet<PatientCard> PatientCards { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<VisitFeedback> VisitFeedbacks { get; set; }
    }
}

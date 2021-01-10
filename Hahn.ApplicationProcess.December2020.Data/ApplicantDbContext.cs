using Hahn.ApplicationProcess.December2020.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Hahn.ApplicationProcess.December2020.Data
{
    public class ApplicantDbContext:DbContext
    {
        private DbContextOptions _options; 
        public ApplicantDbContext(DbContextOptions<ApplicantDbContext> options) : base(options)
        {
            _options = options;
        }

        public DbSet<Applicant> Applicant { get; set; }
    }
}
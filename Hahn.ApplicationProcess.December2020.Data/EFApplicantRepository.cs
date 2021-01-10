using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hahn.ApplicationProcess.December2020.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hahn.ApplicationProcess.December2020.Data
{
    public class EFApplicantRepository: IApplicantRepository
    {
        private readonly ILogger<EFApplicantRepository> _logger;
        private readonly ApplicantDbContext _applicantDbContext;
        public EFApplicantRepository(ApplicantDbContext applicantDbContext, ILogger<EFApplicantRepository> logger)
        {
            _logger = logger;
            _applicantDbContext = applicantDbContext;
        }

        public async Task<int> Add(Applicant applicant)
        {
            try
            {
                await _applicantDbContext.Applicant.AddAsync(applicant);
                await _applicantDbContext.SaveChangesAsync();
                return applicant.Id;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception on adding new applicant to Database {@applicant}", applicant);
                throw;
            }
        }

        public async Task Update(Applicant applicant)
        {
            try
            {
                _applicantDbContext.Entry(applicant).State = EntityState.Modified;
                await _applicantDbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception on Updating new applicant to Database {@applicant}", applicant);
                throw;
            }
        }

        public async Task<Applicant> Get(int id)
        {
            try
            {
                return await _applicantDbContext.Applicant.Where(x => !x.IsDeleted && x.Id == id).AsNoTracking().FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception on get applicant from Database {id}", id);
                throw;
            }
        }

        public  IQueryable<Applicant> GetAll()
        {
            try
            {
                return  _applicantDbContext.Applicant.Where(x => !x.IsDeleted);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception on get all applicant from Database ");
                throw;
            }
        }
    }
}
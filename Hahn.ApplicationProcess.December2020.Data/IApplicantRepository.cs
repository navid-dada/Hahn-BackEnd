using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Hahn.ApplicationProcess.December2020.Domain.Model;

namespace Hahn.ApplicationProcess.December2020.Data
{
    public interface IApplicantRepository
    {
        Task<int> Add(Applicant applicant);
        Task Update(Applicant applicant);
        Task<Applicant> Get(int id);
        IQueryable<Applicant> GetAll();

    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using Hahn.ApplicationProcess.December2020.Domain.Model;
using Hahn.ApplicationProcess.December2020.Service.Command;

namespace Hahn.ApplicationProcess.December2020.Service
{
    public interface IApplicantService
    {
        
        Task<int> CreateApplicant(CreateApplicantCommand command);
        Task UpdateApplicant(UpdateApplicantCommand command);
        Task DeleteApplicant(int id);
        Task<Applicant> GetApplicant(int id);
        Task<IEnumerable<Applicant>> GetAllApplicants(); 

    }
}
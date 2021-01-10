using System.Collections.Generic;
using System.Threading.Tasks;
using Hahn.ApplicationProcess.December2020.Data;
using Hahn.ApplicationProcess.December2020.Domain;
using Hahn.ApplicationProcess.December2020.Domain.Helper;
using Hahn.ApplicationProcess.December2020.Domain.Model;
using Hahn.ApplicationProcess.December2020.Service.Command;
using Microsoft.EntityFrameworkCore;

namespace Hahn.ApplicationProcess.December2020.Service
{
    public class ApplicantService : IApplicantService
    {
        private readonly IApplicantRepository _applicantRepository;
        private readonly ICountryService _countryService;    
        public ApplicantService(IApplicantRepository applicantRepository, ICountryService countryService)
        {
            _applicantRepository=applicantRepository;
            _countryService = countryService;
        }

        public async Task<int> CreateApplicant(CreateApplicantCommand command)
        {
            var applicant = new Applicant.ApplicantBuilder(_countryService)
                .WithName(command.Name)
                .WithFamily(command.Family)
                .WithAddress(command.Address)
                .WithCountryOfOrigin(command.CountryOfOrigin)
                .WithEmailAddress(command.EmailAddress)
                .WithAge(command.Age)
                .WithHired(command.Hired)
                .Build();

            return await _applicantRepository.Add(applicant);

        }

        public async Task UpdateApplicant(UpdateApplicantCommand command)
        {
            var applicant = await _applicantRepository.Get(command.Id);
            var modifiedApplicant = Applicant.Instance(applicant, _countryService)
                .WithName(command.Name)
                .WithFamily(command.Family)
                .WithAddress(command.Address)
                .WithCountryOfOrigin(command.CountryOfOrigin)
                .WithEmailAddress(command.EmailAddress)
                .WithAge(command.Age)
                .WithHired(command.Hired)
                .Build();
            await _applicantRepository.Update(modifiedApplicant);
        }

        public async Task DeleteApplicant(int id)
        {
            var applicant = await _applicantRepository.Get(id);
            if (applicant == null) return;
            applicant.SetAsDeleted();
            await _applicantRepository.Update(applicant);
        }

        public async Task<Applicant> GetApplicant(int id)
        {
            return await _applicantRepository.Get(id);
        }

        public async Task<IEnumerable<Applicant>> GetAllApplicants()
        {
            return await _applicantRepository.GetAll().ToListAsync(); 
        }
    }
}
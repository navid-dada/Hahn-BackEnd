using Hahn.ApplicationProcess.December2020.Data;
using Hahn.ApplicationProcess.December2020.Domain.Model;

namespace Hahn.ApplicationProcess.December2020.Web.Model.ResultDto
{
    public class ApplicantDto
    {
        public int Id { get;  set; }
        public string Name { get;  set; }
        public string Family { get;  set; }
        public string Address { get;  set; }
        public string CountryOfOrigin { get;  set; }
        public string EmailAddress { get;  set; }
        public int Age { get; set; }
        public bool Hired { get;  set; }

        public static ApplicantDto FromApplicant(Applicant applicant)
        {
            var _applicant = new ApplicantDto();
            _applicant.Id = applicant.Id;
            _applicant.Name = applicant.Name;
            _applicant.Family = applicant.Family;
            _applicant.Address = applicant.Address;
            _applicant.CountryOfOrigin = applicant.CountryOfOrigin;
            _applicant.EmailAddress = applicant.EmailAddress;
            _applicant.Age = applicant.Age;
            _applicant.Hired = applicant.Hired;
            return _applicant; 

        }
    }
}
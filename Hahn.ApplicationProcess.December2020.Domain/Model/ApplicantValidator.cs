/*using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Hahn.ApplicationProcess.December2020.Domain.Helper;

namespace Hahn.ApplicationProcess.December2020.Domain.Model
{
    public class ApplicantValidator: AbstractValidator<Applicant>
    {
        private ICountryService _countryDataSet; 
        public ApplicantValidator(ICountryDataSet countryDataSet)
        {
            _countryDataSet = countryDataSet; 
            RuleFor(customer => customer.Name).NotNull().Length(5, 250);
            RuleFor(customer => customer.Family).NotNull().Length(2, 250);
            RuleFor(customer => customer.Address).NotNull().Length(10, 500);
            RuleFor(customer => customer.CountryOfOrigin).MustAsync(IsCorrectCountry).WithMessage("'{PropertyValue}' Is not a valid Country name.");
            RuleFor(customer => customer.EmailAddress).NotNull().Must(IsValidEmail).WithMessage("'{PropertyValue}' Is not a valid email address.");
            RuleFor(customer => customer.Age).InclusiveBetween(20, 60);
        }

        private  async Task<bool> IsCorrectCountry(string countryName, CancellationToken token)
        {
            return await _countryDataSet.IsCorrectCountry(countryName); 
        }

        private bool IsValidEmail(string email)
        {
            var regex = new Regex("^.+@.+\\.[a-zA-Z]{2,3}$");
            return regex.IsMatch(email); 
        }
    }
}*/
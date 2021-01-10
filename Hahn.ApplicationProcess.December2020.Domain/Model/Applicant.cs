using System;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Hahn.ApplicationProcess.December2020.Domain.Helper;

namespace Hahn.ApplicationProcess.December2020.Domain.Model
{
    public class Applicant
    {
        protected Applicant()
        {
        }

        private Applicant(Applicant applicant)
        {
            Id = applicant.Id;
            Name = applicant.Name;
            Family = applicant.Family;
            Address = applicant.Address;
            CountryOfOrigin = applicant.CountryOfOrigin;
            EmailAddress = applicant.EmailAddress;
            Address = applicant.Address;
            Age = applicant.Age;
            Hired = applicant.Hired;
        }

        private Applicant(string name, string family, string address, string countryOfOrigin, string emailAddress,
            int age, bool hired)
        {
            Name = name;
            Family = family;
            Address = address;
            CountryOfOrigin = countryOfOrigin;
            EmailAddress = emailAddress;
            Address = address;
            Age = age;
            Hired = hired;
        }

        public int Id { get;  private set; }
        public string Name { get; private  set; }
        public string Family { get;  private set; }
        public string Address { get;  private set; }
        public string CountryOfOrigin { get; private  set; }
        public string EmailAddress { get;  private  set; }
        public int Age { get; private set; }
        public bool Hired { get; private set; }
        public bool IsDeleted { get; private set; }

        private void Edit(string name, string family, string address, string countryOfOrigin, string emailAddress,
            int age, bool hired)
        {
            Name = name;
            Family = family;
            Address = address;
            CountryOfOrigin = countryOfOrigin;
            EmailAddress = emailAddress;
            Address = address;
            Age = age;
            Hired = hired;
        }

        public void SetAsDeleted()
        {
            IsDeleted = true;
        }
        public static ApplicantBuilder Instance(ICountryService countryService) => new ApplicantBuilder(countryService); 
        public static ApplicantBuilder Instance(Applicant applicant,ICountryService countryService) => new ApplicantBuilder(applicant, countryService);
        
        
        public class ApplicantBuilder : AbstractValidator<ApplicantBuilder>
        {
            private readonly ICountryService _countryService;
            private readonly Applicant _applicant;
            private string _name;
            private string _family;
            private string _address;
            private string _countryOfOrigin;
            private string _emailAddress;
            private int _age;
            private bool _hired;

            public ApplicantBuilder( ICountryService countryService)
            {
                _countryService = countryService;
                SetRules();
            }
            
            public ApplicantBuilder(Applicant applicant, ICountryService countryService)
            {
                _countryService = countryService;
                SetRules();
                _applicant = new Applicant(applicant);
                _name = _applicant.Name;
                _family = _applicant.Family;
                _address = _applicant.Address;
                _countryOfOrigin = _applicant.CountryOfOrigin;
                _emailAddress = _applicant.EmailAddress;
                _age = _applicant.Age;
                _hired = _applicant.Hired;
            }

            private void SetRules()
            {
                RuleFor(applicant => _name).NotNull().WithMessage("'Name' cannot be null.")
                    .Length(5, 250).WithMessage("'Name' must have 5 to 250 character.");
                RuleFor(applicant => _family).NotNull().WithMessage("'Family' cannot be null.")
                    .Length(2, 250).WithMessage("'Family' must have 5 to 250 character.");
                RuleFor(applicant => _address).NotNull().WithMessage("'Address' cannot be null.")
                    .Length(10, 500).WithMessage("'Address' must have 5 to 250 character.");
                RuleFor(applicant => _countryOfOrigin).MustAsync(IsCorrectCountry).WithMessage("'{PropertyValue}' Is not a valid Country name.");
                RuleFor(applicant => _emailAddress).NotNull().Must(IsValidEmail).WithMessage("'{PropertyValue}' Is not a valid email address.");
                RuleFor(applicant => _age).InclusiveBetween(20, 60).WithMessage("'Age' must be between 20 and 60.");
            }

            public ApplicantBuilder WithName(string name)
            {
                _name = name;
                return this;
            }
            public ApplicantBuilder WithFamily(string family)
            {
                _family = family; 
                return this;
            }
            public ApplicantBuilder WithCountryOfOrigin(string country)
            {
                _countryOfOrigin = country;
                return this;
            }
            public ApplicantBuilder WithEmailAddress(string email)
            {
                _emailAddress = email;
                return this;
            }
            
            public ApplicantBuilder WithAddress(string address)
            {
                _address = address;
                return this;
            }
            
            public ApplicantBuilder WithAge(int age)
            {
                _age = age;
                return this;
            }
            
            
            public ApplicantBuilder WithHired(bool hired)
            {
                _hired = hired;
                return this;
            }

            public Applicant Build()
            {
                var result = this.Validate(this);
                if (!result.IsValid)
                    throw new DomainException(result.Errors.Select(x=>x.ErrorMessage));
                
                if(_applicant == null)
                    return new Applicant(_name, _family,_address, _countryOfOrigin, _emailAddress, _age, _hired);
                
                _applicant.Edit(_name, _family,_address, _countryOfOrigin, _emailAddress, _age, _hired);
                return _applicant;
            }

            

            private  async Task<bool> IsCorrectCountry(string countryName, CancellationToken token)
            {
                return !string.IsNullOrEmpty(await _countryService.QueryWebService(countryName)); 
            }

            private bool IsValidEmail(string email)
            {
                var regex = new Regex("^.+@.+\\.[a-zA-Z]{2,3}$");
                return regex.IsMatch(email); 
            }
        }
    }
}
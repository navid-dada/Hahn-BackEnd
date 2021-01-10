using System;
using Hahn.ApplicationProcess.December2020.Domain;
using Hahn.ApplicationProcess.December2020.Domain.Helper;
using Hahn.ApplicationProcess.December2020.Domain.Model;
using NSubstitute;
using Xunit;

namespace Hahn.ApplicationProcess.December2020.Test.Domain
{
    public class ApplicantTest
    {
        
        
        public ApplicantTest()
        {
            
        }
        
        
        [Fact]
        public  void Create_Applicant_With_Valid_Data()
        {   
            //arrange
            var service = Substitute.For<ICountryService>();
            service.QueryWebService(Arg.Any<string>()).Returns(ApplicantTestData.CountryOfOrigin);
            
            //act
            Applicant applicant =  Applicant.Instance(service)
                .WithName(ApplicantTestData.Name)
                .WithFamily(ApplicantTestData.Family)
                .WithAddress(ApplicantTestData.Address)
                .WithCountryOfOrigin(ApplicantTestData.CountryOfOrigin)
                .WithEmailAddress(ApplicantTestData.EmailAddress)
                .WithAge(ApplicantTestData.Age)
                .WithHired(false).Build();

            //assert
            Assert.Equal(ApplicantTestData.Name, applicant.Name );
            Assert.Equal(ApplicantTestData.Family, applicant.Family);
            Assert.Equal(ApplicantTestData.Address, applicant.Address );
            Assert.Equal(ApplicantTestData.CountryOfOrigin, applicant.CountryOfOrigin );
            Assert.Equal(ApplicantTestData.EmailAddress, applicant.EmailAddress );
            Assert.Equal(ApplicantTestData.Age, applicant.Age );
            Assert.False(applicant.Hired );
        }

        [Fact]
        public void Create_Applicant_With_Invalid_Data()
        {
            //arrange
            var service = Substitute.For<ICountryService>();
            service.QueryWebService(Arg.Any<string>()).Returns(ApplicantTestData.EmptyInput);
            
            //act
            Action act  =()=>  Applicant.Instance(service)
                .WithName(ApplicantTestData.WrongName)
                .WithFamily(ApplicantTestData.WrongFamily)
                .WithAddress(ApplicantTestData.WrongAddress)
                .WithCountryOfOrigin(ApplicantTestData.WrongCountryOfOrigin)
                .WithEmailAddress(ApplicantTestData.WrongAddress)
                .WithAge(ApplicantTestData.WrongOverAge)
                .WithHired(false).Build();

            //assert
            var exception = Assert.Throws<DomainException>(act);
            Assert.Equal(6, exception.InnerExceptions.Count);
            
        }
        
        [Fact]
        public void Applicant_Set_Deleted()
        {
            //arrange
            var service = Substitute.For<ICountryService>();
            service.QueryWebService(Arg.Any<string>()).Returns(ApplicantTestData.CountryOfOrigin);
            
            var applicant =   Applicant.Instance(service)
                .WithName(ApplicantTestData.Name)
                .WithFamily(ApplicantTestData.Family)
                .WithAddress(ApplicantTestData.Address)
                .WithCountryOfOrigin(ApplicantTestData.CountryOfOrigin)
                .WithEmailAddress(ApplicantTestData.EmailAddress)
                .WithAge(ApplicantTestData.Age)
                .WithHired(false).Build();

            //act
            applicant.SetAsDeleted();
            
            //assert
            Assert.True(applicant.IsDeleted);
            
        }
    }
}
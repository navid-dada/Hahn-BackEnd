using System.Collections.Generic;

namespace Hahn.ApplicationProcess.December2020.Test.Domain
{
    public static class ApplicantTestData
    {
        public static string EmptyInput = string.Empty; 
        
        public static string Name = "Navid";
        public static string Family = "Shokri";
        public static string Address = "Tehran - Iran";
        public static string CountryOfOrigin = "Iran (Islamic Republic of)";
        public static string EmailAddress = "navid.pdp11@gmail.com";
        public static int Age = 32;
        
        public static string WrongName = "nav";
        public static string WrongFamily = "Sho";
        public static string WrongAddress = "Tehra";
        public static string WrongCountryOfOrigin = "Ira";
        public static string WrongEmailAddress = "navid.pdp";
        public static int WrongUnderAge = 13;
        public static int WrongOverAge = 13;

        public static List<string> PropertyNames  = new List<string>()
        {
            
        };


    }
}
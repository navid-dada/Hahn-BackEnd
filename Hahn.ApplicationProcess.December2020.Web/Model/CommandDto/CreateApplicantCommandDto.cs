using Hahn.ApplicationProcess.December2020.Service.Command;

namespace Hahn.ApplicationProcess.December2020.Web.Model.CommandDto
{
    public class CreateApplicantCommandDto
    { 
        /// <example>navid</example>
        public string Name { get;  set; }
        
        /// <example>shokri</example>
        public string Family { get;  set; }
        
        /// <example>Tehran - Iran</example>
        public string Address { get;  set; }
        
        /// <example>Iran (Islamic republic of)</example>
        public string CountryOfOrigin { get;  set; }
        
        /// <example>navid.pdp11@gmail.com</example>
        public string EmailAddress { get;  set; }
        
        /// <example>32</example>
        public int Age { get; set; }
        
        /// <example>false</example>
        public bool Hired { get;  set; }

        public CreateApplicantCommand ToCreateApplicantCommand()
        {
            var command = new CreateApplicantCommand();
            command.Name = Name; 
            command.Family = Family; 
            command.EmailAddress = EmailAddress; 
            command.CountryOfOrigin= CountryOfOrigin; 
            command.Age = Age; 
            command.Address = Address;
            command.Hired = Hired;
            return command;
        }
    }
    
    
}
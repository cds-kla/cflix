using System;

namespace CFlix.ActiveDirectory.NETStandard
{
    public class ActiveDirectoryUser
    {
        public string Name { get; set; } // name ou cn

        public string Email { get; set; } // userPrincipalName

        public string NameIdentifier { get; set; } // sAMAccountName
        
        public string EmployeeID { get; set; } // employeeID
    }
}

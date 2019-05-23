using System;

namespace trycatch
{
    class Contact
    {
        public Contact(string firstNameParam, string lastNameParam)
        {
            FirstName = firstNameParam;
            LastName = lastNameParam;
            FullName = $"{firstNameParam} {lastNameParam}";
        }
        public string FullName;
        public string FirstName;
        public string LastName;

        public string Email;
        public string Address;
    }
};
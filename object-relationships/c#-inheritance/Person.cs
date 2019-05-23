using System;

namespace c__inheritance
{
    abstract class Person
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }

        public Company Company { get; set; }

        public abstract void SubmitMaintenanceRequest(string problemToBeFixed);
    }
}
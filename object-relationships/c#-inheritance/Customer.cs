using System;

namespace c__inheritance{
    class Customer : Person
    {
        public override void SubmitMaintenanceRequest(string problemToBeFixed)
        {
            Console.WriteLine($"A customer submitted a maintenance request: {problemToBeFixed}");
        }

        public void sayHi(){
            Console.WriteLine("Hello!");
        }
    }

}
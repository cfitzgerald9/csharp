using System;

namespace c__inheritance{
    class Customer : Person
    {
        public override void SubmitMaintenanceRequest(Maintenance problemToBeFixed)
        {
            Console.WriteLine($"A customer submitted a maintenance request: {problemToBeFixed.description}");
        }

        public void sayHi(){
            Console.WriteLine("Hello!");
        }
    }

}
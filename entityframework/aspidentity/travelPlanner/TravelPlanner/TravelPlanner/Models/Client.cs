using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPlanner.Models
{
    public class Client
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        public List<Trip> Trips { get; set; } = new List<Trip>();
        public string ApplicationUserId { get; set; }
        public ApplicationUser User { get; set; }
        [NotMapped]
        public string FullName { get { return FirstName + " " + LastName; } }
    }
}

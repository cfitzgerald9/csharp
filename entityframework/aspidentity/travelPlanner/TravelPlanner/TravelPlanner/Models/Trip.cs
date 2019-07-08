using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPlanner.Models
{
    public class Trip : IValidatableObject
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Required]
        public string Location { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public ApplicationUser User { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if ((DateTime.Compare(EndDate, StartDate) < 0))
            {
                yield return new ValidationResult(
                    $"End date must be later than start date.",
                    new[] { "EndDate" });
            }
            if ((EndDate.Year > 2025))
            {
                yield return new ValidationResult(
                    $"End date cannot be after 2025",
                    new[] { "EndDate" });
            }
            if ((StartDate < DateTime.Now))
            {
                yield return new ValidationResult(
                    $"Start date must be later than current date.",
                    new[] { "StartDate" });
            }
        }
    }

}

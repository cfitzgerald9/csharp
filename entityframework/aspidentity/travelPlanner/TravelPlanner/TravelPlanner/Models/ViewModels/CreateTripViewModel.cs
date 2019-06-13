using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPlanner.Models.ViewModels
{
    public class CreateTripViewModel
    {
        public Trip trip { get; set; }

        public ApplicationUser user { get; set; }
        public List<SelectListItem> ClientOptions { get; set; }

    }
}
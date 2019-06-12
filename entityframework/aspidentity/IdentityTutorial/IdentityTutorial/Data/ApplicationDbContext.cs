﻿using System;
using System.Collections.Generic;
using System.Text;
using IdentityTutorial.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityTutorial.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    }
}

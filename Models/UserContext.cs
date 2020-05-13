﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.Models
{
    public class UserManagementContext : DbContext
    {
        public UserManagementContext(DbContextOptions options)
            : base(options)
        { 
        }
        public DbSet<Areas.Identity.Data.UserManagement> UserManagements { get; set; }
    }
}

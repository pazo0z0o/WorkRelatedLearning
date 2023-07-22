﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Data
{
    public class WorkDbContext : IdentityDbContext
    {
        public WorkDbContext(DbContextOptions<WorkDbContext> options) : base(options)
        {
                
        }
    }
}

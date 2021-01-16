﻿using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using WebMonitoringApi.Data.Models;

namespace WebMonitoringApi.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IOptions<OperationalStoreOptions> operationalOptions) : base(options, operationalOptions)
        { 
        }

        public DbSet<Log> Logs { get; set; }

        public DbSet<Url> Urls { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
        }
    }
}
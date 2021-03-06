﻿using Microsoft.Extensions.Options;
using WebMonitoringApi.Data.Models;
using Microsoft.EntityFrameworkCore;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;

namespace WebMonitoringApi.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IOptions<OperationalStoreOptions> operationalOptions
        ) : base(options, operationalOptions) { }

        public DbSet<Log> Logs { get; set; }

        public DbSet<Url> Urls { get; set; }

        public DbSet<ResponseHeader> ResponseHeaders { get; set; }
        public DbSet<RequestHeader> RequestHeaders { get; set; }
    }
}

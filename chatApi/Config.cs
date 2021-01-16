﻿using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace WebMonitoringApi
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            List<IdentityResource> identityResources = new List<IdentityResource>();
            identityResources.Add(new IdentityResources.OpenId());
            identityResources.Add(new IdentityResources.Profile());
            identityResources.Add(new IdentityResources.Email());
            identityResources.Add(new IdentityResources.Phone());

            return identityResources;
        }

        public static IEnumerable<ApiResource> GetAPIs()
        {
            List<ApiResource> apis = new List<ApiResource>();
            apis.Add(new ApiResource("WebMonitoringApi", "chat API")
            {
                Scopes = { "WebMonitoringApi" }
            });

            return apis;
        }

        public static IEnumerable<ApiScope> GetAPIScopes()
        {
            List<ApiScope> apiScopes = new List<ApiScope>
            {
                new ApiScope("WebMonitoringApi", "WebMonitoringApi")
            };

            return apiScopes;
        }

        public static IEnumerable<Client> GetClients()
        {
            List<Client> clients = new List<Client>();

            Client apiClient = new Client
            {
                ClientId = "WebMonitoringApi",
                RequireClientSecret = false,
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                AllowedScopes = new List<string>
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                IdentityServerConstants.StandardScopes.Email,
                "WebMonitoringApi"
            }
            };

            clients.Add(apiClient);

            return clients;
        }
    }
}
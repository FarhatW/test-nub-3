// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace jce.IdentityServer
{
    public class Config
    {
        // scopes define the resources in your system
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource("roles", "UserRoles", new[] { "role" })
                {
                    ShowInDiscoveryDocument = true,

                }

        };
    }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource
                {
                    Name = "jce",
                    Description = "Identity Server API",
                    DisplayName = "Identity Server API",
                    Enabled = true,
                    Scopes =
                    {
                        new Scope()
                        {
                            Name = "jce",
                            DisplayName = "API de joué club entreprise",
                            ShowInDiscoveryDocument = true,
                            UserClaims = {

                                IdentityServerConstants.StandardScopes.OpenId,
                                IdentityServerConstants.StandardScopes.Profile,
                                "role"
                            },
                        },
                    }
                },

            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            // client credentials client
            return new List<Client>
            {

                new Client
                {
                    ClientId = "jceFront",
                    ClientName = "joué club entreprise",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris =           { "http://localhost:5002/#/?" },
                    PostLogoutRedirectUris = { "http://localhost:50002/#/dashbord"},
                    AllowedCorsOrigins =     { "http://localhost:5002" },
                    

                    AllowedScopes =
                    {

                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "roles",
                        "jce"
                    }
                }
            };
        }
    }
}
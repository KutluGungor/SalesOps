using Duende.IdentityServer.Models;

namespace SalesOps.DuendeIdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
    [
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
        new IdentityResource("roles", "Roller", ["role"]),
        new IdentityResource("tenant", "Tenant Bilgisi", ["company_id", "branch_id"])
    ];

    public static IEnumerable<ApiScope> ApiScopes =>
    [
        new ApiScope("sales.read",    "Sales API - Okuma"),
        new ApiScope("sales.write",   "Sales API - Yazma"),
        new ApiScope("catalog.read",  "Catalog API - Okuma"),
        new ApiScope("catalog.write", "Catalog API - Yazma"),
        new ApiScope("employee.read", "Employee API - Okuma"),
        new ApiScope("employee.write","Employee API - Yazma"),
        new ApiScope("company.read",  "Company API - Okuma"),
        new ApiScope("company.write", "Company API - Yazma"),
    ];

    public static IEnumerable<ApiResource> ApiResources =>
    [
        new ApiResource("sales-api", "Sales API")
        {
            Scopes = { "sales.read", "sales.write" },
            UserClaims = { "role", "company_id", "branch_id" }
        },
        new ApiResource("catalog-api", "Catalog API")
        {
            Scopes = { "catalog.read", "catalog.write" },
            UserClaims = { "role", "company_id", "branch_id" }
        },
        new ApiResource("employee-api", "Employee API")
        {
            Scopes = { "employee.read", "employee.write" },
            UserClaims = { "role", "company_id", "branch_id" }
        },
        new ApiResource("company-api", "Company API")
        {
            Scopes = { "company.read", "company.write" },
            UserClaims = { "role", "company_id", "branch_id" }
        },
    ];

    public static IEnumerable<Client> Clients =>
    [
        // Resource Owner Password (test / postman için)
        new Client
        {
            ClientId = "salesops-ro",
            ClientName = "SalesOps Resource Owner Client",
            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
            ClientSecrets = { new Secret("salesops-secret".Sha256()) },
            AllowedScopes =
            {
                "openid", "profile", "roles", "tenant",
                "sales.read", "sales.write",
                "catalog.read", "catalog.write",
                "employee.read", "employee.write",
                "company.read", "company.write"
            },
            AccessTokenLifetime = 3600, // 1 saat
            AllowOfflineAccess = true   // refresh token
        },

        // Client Credentials (servis-servis iletişimi için)
        new Client
        {
            ClientId = "salesops-service",
            ClientName = "SalesOps Internal Service Client",
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            ClientSecrets = { new Secret("service-secret".Sha256()) },
            AllowedScopes =
            {
                "sales.read", "sales.write",
                "catalog.read", "catalog.write",
                "employee.read", "employee.write",
                "company.read", "company.write"
            }
        }
    ];
}

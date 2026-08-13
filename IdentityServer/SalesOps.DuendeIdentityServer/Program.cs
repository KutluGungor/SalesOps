using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SalesOps.DuendeIdentityServer;
using SalesOps.DuendeIdentityServer.Data;
using SalesOps.DuendeIdentityServer.Models;
using SalesOps.DuendeIdentityServer.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// EF Core - MSSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// ASP.NET Core Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Duende IdentityServer
builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;
    options.EmitStaticAudienceClaim = true;
})
.AddConfigurationStore(options =>
{
    options.ConfigureDbContext = b =>
        b.UseSqlServer(connectionString,
            sql => sql.MigrationsAssembly(typeof(Program).Assembly.GetName().Name));
})
.AddOperationalStore(options =>
{
    options.ConfigureDbContext = b =>
        b.UseSqlServer(connectionString,
            sql => sql.MigrationsAssembly(typeof(Program).Assembly.GetName().Name));
    options.EnableTokenCleanup = true;
})
.AddAspNetIdentity<ApplicationUser>()
.AddProfileService<CustomProfileService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseIdentityServer();
app.UseAuthorization();
app.MapControllers();

// Seed: Clients, Scopes, Resources, Roller ve Admin kullanıcısı
await SalesOps.DuendeIdentityServer.Data.DatabaseInitializer.SeedAsync(app);

app.Run();

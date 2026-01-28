using SalesOps.Company.Application.Mapping;
using SalesOps.Company.Application.Services.BranchService;
using SalesOps.Company.Application.Services.OrganizationService;
using SalesOps.Company.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddAutoMapper(typeof(GeneralMapping).Assembly);
builder.Services.AddScoped<IOrganizationService, OrganizationService>();
builder.Services.AddScoped<IBranchService, BranchService>();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
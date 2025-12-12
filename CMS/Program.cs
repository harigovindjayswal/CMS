
using Application.Core;
using AutoMapper;
using CMSAPI.Mappings;
using CMSApplication.Clients.Commands;
using CMSApplication.Clients.Queries;
using CMSApplication.Clients.Validatators;
using CMSDb.DbModels;
using CMSRep.IServices;
using CMSRep.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CmsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ICMSService, CMSService>();
builder.Services.AddSingleton(new MapperConfiguration(cfg =>
{
    cfg.AddProfile<MappingProfile>();
}).CreateMapper());
builder.Services.AddCors();
builder.Services.AddMediatR(x =>
{
    x.RegisterServicesFromAssemblyContaining<GetAllClients.Handler>();
    x.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssemblyContaining<CreateClientValidator>();
// builder.Services.AddValidatorsFromAssemblyContaining<EditClientValidator>();
builder.Services.AddTransient<ExceptionMiddleware>();
//builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);

var app = builder.Build();

// 1️⃣ Developer tools (Swagger)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 2️⃣ Redirection (only if HTTPS is configured properly)
app.UseHttpsRedirection();

// 3️⃣ Routing happens implicitly in ASP.NET Core 6+ templates
// app.UseRouting();  // Not required unless using endpoints manually

// 4️⃣ CORS 
app.UseCors(options => options
    .AllowAnyHeader()
    .AllowAnyMethod()
    .WithOrigins("http://localhost:3000", "https://localhost:3000")
);

// 5️⃣ Authentication / Authorization
app.UseAuthorization();

// 6️⃣ Your custom exception middleware (must wrap controllers!)
app.UseMiddleware<ExceptionMiddleware>();

// 7️⃣ Endpoint execution
app.MapControllers();

app.Run();

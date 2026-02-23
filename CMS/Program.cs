using Application.Clients.Queries;
using Application.Clients.Validatators;
using Application.Core;
using AutoMapper;
using CMSAPI;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.DependencyInjection;
using Persistence.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(opt => 
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    opt.Filters.Add(new AuthorizeFilter(policy));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddDbContext<CmsContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// builder.Services.AddDbContext<CmsIdentityContext>(options =>
// options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// builder.Services.AddScoped<ICMSService, CMSService>();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddPersistenceIdentity(builder.Configuration);
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

// builder.Services.AddIdentityApiEndpoints<User>(opt =>
// {
//     opt.User.RequireUniqueEmail = true;
// })
// .AddRoles<IdentityRole>()
// .AddEntityFrameworkStores<CmsIdentityContext>();

builder.Services
    .AddIdentity<User, IdentityRole>(opt =>
    {
        opt.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<CmsIdentityContext>()
    .AddDefaultTokenProviders();


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

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod()
    .AllowCredentials()
    .WithOrigins("http://localhost:3000", "https://localhost:3000"));

app.UseAuthentication();
app.UseAuthorization();

// 6️⃣ Your custom exception middleware (must wrap controllers!)
app.UseMiddleware<ExceptionMiddleware>();

// 7️⃣ Endpoint execution
app.MapControllers();
//app.MapGroup("api").MapIdentityApi<User>(); // api/login
async Task SeedRoles(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    string[] roles = { "Admin", "Lawyer","Staff", "Client" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedRoles(services);
}
app.Run();

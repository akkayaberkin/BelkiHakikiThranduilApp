using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using BelkiHakiki.API.Filters;
using BelkiHakiki.API.Helpers;
using BelkiHakiki.API.Middlewares;
using BelkiHakiki.API.Modules;
using BelkiHakiki.API.Tools;
using BelkiHakiki.Core.Repositories;
using BelkiHakiki.Core.Services;
using BelkiHakiki.Core.UnitOfWorks;
using BelkiHakiki.Repository;
using BelkiHakiki.Repository.Repositories;
using BelkiHakiki.Repository.UnitOfWorks;
using BelkiHakiki.Service.Mapping;
using BelkiHakiki.Service.Services;
using BelkiHakiki.Service.Validations;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
// Configuration
// Add services to the container.
builder.Services.AddCors();
builder.Services.AddControllers(options => options.Filters.Add(new ValidateFilterAttribute())).AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>());

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;

});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();

builder.Services.AddScoped(typeof(NotFoundFilter<>));
builder.Services.AddAutoMapper(typeof(MapProfile));

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });
});
var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);
var appSettings = appSettingsSection.Get<AppSettings>();
var key = Encoding.ASCII.GetBytes(appSettings.SecretKey);

builder.Services.AddCors(opt => opt.AddPolicy("GlobalCors", config =>
 {  //her metodu her istedği her header ı kabul et dedik.
     config.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
 }
    ));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.RequireHttpsMetadata = false;

    opt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidAudience = JwtTokenSettings.Audience,
        ValidIssuer = JwtTokenSettings.Issuer,
        ClockSkew = TimeSpan.Zero,
        ValidateLifetime = true, //token ın süresini doğrulasın.
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenSettings.Key)),
        ValidateIssuerSigningKey = true
    };
}

);
;
builder.Services.AddScoped<IUserService, UserService>();

builder.Host.UseServiceProviderFactory
    (new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new RepoServiceModule()));


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseCors("GlobalCors");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

using FutBinProject.Infrastructure.Context;
using FutBinProject.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace FutBin_Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionstring = builder.Configuration.GetConnectionString("OldDefaultConnection");

            // Add services to the container.
            builder.Services.AddDbContext<FutContext>(options =>
            {
                options.UseSqlServer(connectionstring);
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            // Add Swagger services
            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {{
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new string[]{}
                    }
                });
            });

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<FutContext>()
                .AddDefaultTokenProviders();
            builder.Services.AddAuthentication(option =>
            option.DefaultAuthenticateScheme = "myschema")
            .AddJwtBearer("myschema", option =>
                {
                    string key = "I hate My Brain Cuz it is so toxic and I like it";
                    var secertKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
                    option.TokenValidationParameters = new TokenValidationParameters
                            {
                                IssuerSigningKey = secertKey,
                                ValidateIssuer = false,
                                ValidateAudience = false,
                            };
                }
                    );
            builder.Services.AddScoped<FutContext>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    c.RoutePrefix = string.Empty; // This makes Swagger UI available at the root URL
                });
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

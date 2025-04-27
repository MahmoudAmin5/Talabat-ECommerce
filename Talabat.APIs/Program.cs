
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using StackExchange.Redis;
using System.Text;
using Talabat.APIs.DTO;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.APIs.Middleware;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Repository.Core;
using Talabat.Core.Services.Core;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;
using Talabat.Service;

namespace Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            #region Adding Services
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddDbContext<AppIdentityDbContext>(options => 
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            
            });
            builder.Services.AddScoped(typeof(IGenericRepository<>),typeof (GenericRepository<>));
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actioncontext) =>
                {
                    var errors = actioncontext.ModelState.Where(p => p.Value.Errors.Count() > 0)
                    .SelectMany(p => p.Value.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                    var Response = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(Response);
                };
            });
            builder.Services.AddSingleton<IConnectionMultiplexer>(options=>
            {
                var connection = builder.Configuration.GetConnectionString("RedisConnection");
                return ConnectionMultiplexer.Connect(connection);
            });
            builder.Services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
            builder.Services.AddScoped(typeof(ITokenService), typeof(TokenService));
            builder.Services.AddIdentity<AppUser, IdentityRole>()
                                   .AddEntityFrameworkStores<AppIdentityDbContext>();
            builder.Services.AddAuthentication(/*JwtBearerDefaults.AuthenticationScheme*/ Options =>
            {
                Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["JWT:ValidAudience"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
                        ValidateLifetime = true,
                    };
                });
            #endregion

            var app = builder.Build();

            #region Applying Migrations 
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var _dbcontext = services.GetRequiredService<StoreContext>();

            var LoggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                await _dbcontext.Database.MigrateAsync();
                var IdentityDbContext = services.GetRequiredService<AppIdentityDbContext>();
                await IdentityDbContext.Database.MigrateAsync();
                await StoreContextSeed.SeedAsync(_dbcontext); // Data Seeding
                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                await AppIdentityDbContextSeed.SeedUserAsync(userManager);
            }
            catch (Exception ex)
            {
                var logger = LoggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error has been happened during applying the migration");
            }
            #endregion

            // Configure the HTTP request pipeline.
            #region Kestrul Middelwares 
            app.UseMiddleware<ExceptionMiddleware>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStatusCodePagesWithReExecute("/error/{0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            

            app.MapControllers();

            #endregion

            app.Run();
        }
    }
}

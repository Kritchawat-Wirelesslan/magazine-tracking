using MagazineTracking.Core.Externals.Repositories;
using MagazineTracking.Core.Externals.Repositories.Interface;
using MagazineTracking.Infrastructure.EF.DatabaseContext;
using MagazineTracking.Infrastructure.Externals.Repositories;
using MagazineTracking.Infrastructure.Externals.Repositories.Interface.Authenticate;
using MagazineTracking.Infrastructure.Externals.Repositories.Interface.User;
using MagazineTracking.WebAPI.Authetication;
using MagazineTracking.WebAPI.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace MagazineTracking.WebAPI
{
	public class Startup
	{
		public IConfiguration configRoot { get; }
		public IApplicationBuilder application { get; }
		public IAppDBContext appDBContext { get; }
		public Startup(IConfiguration configuration)
		{
			configRoot = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDistributedMemoryCache();
			services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromSeconds(10);
				options.Cookie.HttpOnly = true;
				options.Cookie.IsEssential = true;
			});
			services.AddControllers();
			services.AddRazorPages();
			services.AddControllers();
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "MAGAZINE TRACKING API", Version = "v1" });
				c.AddSecurityDefinition("Bearer", //Name the security scheme
				   new OpenApiSecurityScheme
				   {
					   Description = "JWT Authorization header using the Bearer scheme.",
					   Type = SecuritySchemeType.Http,
					   Scheme = "bearer"
				   });

				c.AddSecurityRequirement(new OpenApiSecurityRequirement{
					{
						new OpenApiSecurityScheme{
							Reference = new OpenApiReference{
								Id = "Bearer", //The name of the previously defined security scheme.
                                Type = ReferenceType.SecurityScheme
							}
						},new List<string>()
					}
				});
			});

			services.AddDbContext<AppDbContext>(options =>
			{
				options.UseSqlServer(configRoot.GetConnectionString("DefaultConnection"));
			});

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(o =>
			{
				o.TokenValidationParameters = new TokenValidationParameters
				{
					ValidIssuer = configRoot["Jwt:Issuer"],
					ValidAudience = configRoot["Jwt:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configRoot["Jwt:Key"])),
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = false,
					ValidateIssuerSigningKey = true
				};
			});

			services.AddAuthorization(options =>
			{
				options.AddPolicy("FIREBASE",
				   policy => policy.RequireAssertion(x =>
				   {
					   return true;
				   })); ;
			});
			this.ConfigureIoC(services);
			services.Configure<AppSettings>(configRoot.GetSection("AppSettings"));
			services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configRoot.GetConnectionString("DefaultConnection")));
			services.AddControllersWithViews();
			services.AddControllers();
		}

		public virtual void ConfigureIoC(IServiceCollection services)
		{
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IAuthenticateMagazineRepository, AuthenticateMagazineRepository>();
			services.AddScoped<IApplicationContext, WebAPIApplicationContext>();
		}
		
		public void Configure(WebApplication app, IWebHostEnvironment env, AppDbContext dbContext)
		{
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}
			dbContext.Database.EnsureCreated();

			app.UseSwagger();
			app.UseSwaggerUI();

			app.UseCors(x => x
			.AllowAnyOrigin()
			.AllowAnyMethod()
			.AllowAnyHeader());

			app.UseSession();
			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();
			app.MapRazorPages();
			app.MapControllers();
			app.Run();
		}
	}
}

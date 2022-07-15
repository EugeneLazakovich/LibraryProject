using Lesson1_BL;
using Lesson1_BL.Auth;
using Lesson1_BL.Options;
using Lesson1_BL.Services.AuthService;
using Lesson1_BL.Services.BooksService;
using Lesson1_BL.Services.HashService;
using Lesson1_BL.Services.LibrariesService;
using Lesson1_BL.Services.RentBookService;
using Lesson1_BL.Services.UsersService;
using Lesson1_DAL;
using Lesson1_DAL.Interfaces;
using Lesson1_DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Lesson1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.Configure<AuthOptions>(options =>
                Configuration.GetSection(nameof(AuthOptions)).Bind(options));
            services.Configure<HashOptions>(options =>
                Configuration.GetSection(nameof(HashOptions)).Bind(options));

            var authOptions = Configuration.GetSection(nameof(AuthOptions)).Get<AuthOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = authOptions.Issuer,
                            ValidateAudience = true,
                            ValidAudience = authOptions.Audience,
                            ValidateLifetime = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authOptions.Key)),
                            ValidateIssuerSigningKey = true,
                        };
                    });
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IBooksRepository, BooksRepository>();
            services.AddScoped<IRentBookRepository, RentBookRepository>();
            services.AddScoped<ILibrariesRepository, LibrariesRepository>();
            services.AddScoped<IBooksService, BooksService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IBackgroundsService, BackgroundsService>();
            services.AddScoped<ICitiesService, CitiesService>();
            services.AddScoped<ILibrariesService, LibrariesService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRentBookService, RentBookService>();
            services.AddScoped<IHashService, HashService>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();

            services.AddDbContext<EFCoreDbContext>(options =>
               options.UseSqlServer(Configuration["ConnectionStrings:Default"], b => b.MigrationsAssembly("Lesson1_DAL")));
            services.AddControllersWithViews().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
        }
    }
}

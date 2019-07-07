using System.Linq;
using System.Text;
using DataService.Constants;
using DataService.Infrastructure;
using DataService.Models;
using DataService.Repositories;
using DataService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace PBSA_API
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
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            //Add CORS rule
            services.AddCors(options =>
            {
                options.AddPolicy("AllowMyOrigin",
                builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            //Add Firebase config
            services.Configure<FirebaseConfig>(Configuration.GetSection("FirebaseConfig"));

            //Add Swagger
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v4", new Info { Title = "My API", Version = "v4" });
            });

            //Add context and Transient for DI
            services.AddDbContext<BUSTAPContext>();

            //Repositories
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IWalletRepository, WalletRepository>();
            services.AddTransient<IIDPaperRepository, IDPaperRepository>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            services.AddTransient<IApplyPriceTypeRepository, ApplyPriceTypeRepository>();

            //Services
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IWalletService, WalletService>();
            services.AddTransient<IIDPaperService, IDPaperService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("../swagger/v4/swagger.json", "BUSTAPPING");
            });

            //Enable CORS
            app.UseCors("AllowMyOrigin");

            //Enable Identity
            app.UseAuthentication();

            //
            app.UseMvc();
        }
    }
}

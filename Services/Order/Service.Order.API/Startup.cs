using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Service.Order.Infrastructure;
using Shared.Service;
using System.IdentityModel.Tokens.Jwt;  

namespace Service.Order.API
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
            var requiredAuthirzePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
               options =>
               {
                   options.Authority = Configuration["IdentityServer"];
                   options.Audience = "resource_order";
                   options.RequireHttpsMetadata = false;
               }
           );

            services.AddDbContext<OrderDbContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), configure =>
                {
                    configure.MigrationsAssembly("Service.Order.Infrastructure");
                });

            });

            services.AddHttpContextAccessor();
            services.AddScoped<ISharedIdentityService, SharedIdentityService>();
            services.AddMediatR(typeof(Service.Order.Application.Commands.CreateOrderCommand).Assembly);

            services.AddMvc();
            services.AddControllers(opt =>
            {
                opt.Filters.Add(new AuthorizeFilter(requiredAuthirzePolicy));
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Service.Order.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Service.Order.API v1"));
            }

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

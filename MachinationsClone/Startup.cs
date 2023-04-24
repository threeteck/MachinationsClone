using System.Text;
using MachinationsClone.Models.Entities.Graph;
using JsonSubTypes;
using MachinationsClone.Models.DTOs.Response;
using MachinationsClone.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace MachinationsClone
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
            services.AddSwaggerGen();

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationContext>(option =>
                option.UseNpgsql(connectionString));
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = AuthOptions.ISSUER,
 
                        ValidateAudience = true,
                        ValidAudience = AuthOptions.AUDIENCE,
                        ValidateLifetime = true,
 
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true,
                    };
                });
            services.AddAuthorization();
            
            services.AddScoped<AuthenticationService>();
            services.AddHttpContextAccessor();
            
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                //Register the subtypes of the Device (Phone and Laptop)
                //and define the device Discriminator
                options.SerializerSettings.Converters.Add(
                    JsonSubtypesConverterBuilder
                        .Of(typeof(GraphElementDto), "ElementType")
                        .RegisterSubtype(typeof(GraphNodeDto), "node")
                        .RegisterSubtype(typeof(GraphConnectionDto), "connection")
                        .SerializeDiscriminatorProperty()
                        .Build()
                );

            });
            // In production, the React files will be served from this directory
            // services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/build"; });
            
            services.AddCors(options =>
            {
                options.AddPolicy(
                    "CorsPolicy",
                    policyBuilder => policyBuilder.WithOrigins("http://localhost:3000")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                    );
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.UseSwagger();
            app.UseSwaggerUI();

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseSpaStaticFiles();

            app.UseRouting();
            
            app.UseCors("CorsPolicy");
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "{controller}/{action=Index}/{id?}");
            });

            /*app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                // if (env.IsDevelopment()) spa.UseReactDevelopmentServer("start_webpack");
            });*/
        }
    }
}

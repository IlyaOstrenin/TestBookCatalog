using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.IO;
using System.Reflection;
using TestBookCatalog.Filters;
using TestBookCatalog.Models;
using TestBookCatalog.Repositories.Implementations;
using TestBookCatalog.Repositories.Interfaces;
using TestBookCatalog.Services.Implementations;
using TestBookCatalog.Services.Interfaces;

namespace TestBookCatalog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                .AddTransient<IBookService, BookService>()
                .AddTransient<IFavoriteService, FavoriteService>()
                .AddTransient<IUserService, UserService>()
                .AddTransient<IBookRepository, BookRepository>()
                .AddTransient<IFavoriteRepository, FavoriteRepository>()
                .AddTransient<IUserRepository, UserRepository>()
                .AddTransient<IMediaFileService, MediaFileService>()
                .AddTransient<IMediaFileRepository, MediaFileRepository>();

            services.AddControllers(o =>
            {
                o.Filters.Add(typeof(ExceptionFilters));
            });

            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT with Bearer",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });
                c.OperationFilter<SecurityRequirementsOperationFilter>();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TestBookCatalog", Version = "v1" });
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options => options.TokenValidationParameters = JwtOptions.BuildTokenParameters());

            services.AddAuthorization();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestBookCatalog v1"));
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

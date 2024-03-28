using Clarivate.Services;
using Microsoft.OpenApi.Models;

namespace Clarivate
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddHealthChecks();
            builder.Services.AddSingleton<IRandomUserService, RandomUserService>();
            builder.Services.AddHttpClient();

            builder.Services.AddHealthChecks()
                            .AddCheck<CustomHealthCheck>("custom_health_check");

            builder.Services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Clarivate Api", Version = "v1" });
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");

                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Clarivate Api");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Index}/{id?}");

            app.MapHealthChecks("/health");

            app.Run();
        }
    }
}

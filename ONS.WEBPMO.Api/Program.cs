using Microsoft.AspNetCore.Localization;
using ONS.WEBPMO.Application;
using ONS.WEBPMO.Domain.Resources;
using ONS.WEBPMO.Infrastructure;
using System.Globalization;

namespace ONS.WEBPMO.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var env = builder.Environment;

            ILogger<Program> logger = LoggerFactory.Create(logging =>
            {
                logging.AddConsole();
            }).CreateLogger<Program>();

            builder.Services.AddRazorPages();
            builder.Services.AddControllers();
            //.AddJsonOptions(x =>
            //x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve); 
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            string testConfigValue = string.Empty;

            if (env.IsDevelopment())
            {
                builder.Services.RegisterApplication(builder.Configuration)
                                .RegisterRepository(builder.Configuration.GetConnectionString("PMOConnectionStringDev"));
            }
            else
            {

                //var environmentVariables = Environment.GetEnvironmentVariables();

                //builder.Configuration.AddConfigITConfiguration(options =>
                //{
                //    options["-amb"] = environmentVariables["ConfigITamb"]?.ToString();
                //    options["-user"] = environmentVariables["ConfigITuser"]?.ToString();
                //    options["-password"] = environmentVariables["ConfigITpwd"]?.ToString();
                //    options["-r"] = environmentVariables["ConfigITr"]?.ToString();
                //});

                //var connectionString = builder.Configuration.GetConnectionString("PMOIntegracaoConnectionString");
                //builder.Configuration["TestConfigValue"] = connectionString;
                //builder.Services.RegisterApplication(builder.Configuration)
                //    .RegisterRepository(connectionString);

            }

            var supportedCultures = new[]
            {
                new CultureInfo("en-US"),
                new CultureInfo("pt-BR"),
            };

            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

            var app = builder.Build();
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();
            app.MapRazorPages();

            app.Run();
        }

    }
}

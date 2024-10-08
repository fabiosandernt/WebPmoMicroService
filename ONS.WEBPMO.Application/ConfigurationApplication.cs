using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ONS.WEBPMO.Application.Services.PMO.Implementation;
using ONS.WEBPMO.Application.Services.PMO.Implementation.Integrations;
using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using ONS.WEBPMO.Application.Services.PMO.Interfaces.Integrations;
using ONS.WEBPMO.Application.Services.PMO.Interfaces.OrigemColeta;
using ONS.WEBPMO.Domain.Presentations;
using ONS.WEBPMO.Domain.Presentations.Impl;
using ONS.WEBPMO.Servico.Usina;
using System.Text;
using System.Text.Json.Serialization;
namespace ONS.WEBPMO.Application
{
    public static class ConfigurationAplication
    {
        public static IServiceCollection RegisterApplication(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddAutoMapper(typeof(Application.ConfigurationAplication).Assembly);

            services.AddScoped<IInsumoService, InsumoService>();
            services.AddScoped<IOrigemColetaService, OrigemColetaService>();
            services.AddScoped<IReservatorioService, ReservatorioService>();
            services.AddScoped<ISubsistemaService, SubsistemaService>();
            services.AddScoped<IUnidadeGeradoraService, UnidadeGeradoraService>();
            services.AddScoped<IUsinaService, UsinaService>();
            services.AddScoped<IAgenteService, AgenteService>();
            services.AddScoped<IArquivoService, ArquivoService>();
            services.AddScoped<IColetaInsumoService, ColetaInsumoService>();
            services.AddScoped<IDadoColetaEstruturadoService, DadoColetaEstruturadoService>();
            services.AddScoped<IDadoColetaManutencaoService, DadoColetaManutencaoService>();
            services.AddScoped<IDadoColetaNaoEstruturadoService, DadoColetaNaoEstruturadoService>();
            services.AddScoped<IGabaritoPMOService, GabaritoPMOService>();
            services.AddScoped<IGeracaoBlocosService, GeracaoBlocosService>();
            services.AddScoped<IGrandezaService, GrandezaService>();
            services.AddScoped<IHistoricoService, HistoricoService>();
            services.AddScoped<ILogDadosInformadosService, LogDadosInformadosService>();
            services.AddScoped<ILogNotificacaoService, LogNotificacaoService>();
            services.AddScoped<INotificacaoService, NotificacaoService>();
            services.AddScoped<IParametroService, ParametroService>();
            services.AddScoped<IPMOService, PMOService>();
            services.AddScoped<ISemanaOperativaService, SemanaOperativaService>();
            services.AddScoped<ISGIService, SGIService>();
            services.AddScoped<ISharePointService, SharePointService>();
            services.AddScoped<IColetaInsumoPresentation, ColetaInsumoPresentation>();
            services.AddScoped<IGabaritoPresentation, GabaritoPresentation>();
            services.AddScoped<IInsumoPresentation, InsumoPresentation>();
            services.AddScoped<ILogNotificacaoPresentation, LogNotificacaoPresentation>();
            services.AddScoped<IBDTService, BDTService>();
            services.AddScoped<IGabaritoUsinaService, GabaritoUsinaService>();
            services.AddScoped<IPMOUsinaService, PMOUsinaService>();
            services.AddScoped<IBDTPMOService, BDTPMOService>();





            services.AddHttpClient();

            services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });


            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                );
            });

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JwtSecurity:PMOIntegracaoJwtSecurity"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true
                };
            });

            var info = new OpenApiInfo();
            info.Version = "V1";
            info.Title = "API ";

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", info);
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Insira o token JWT desta maneira : Bearer {seu token}",
                    Name = "Authorization",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"

                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In= ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });

            return services;

        }
    }
}

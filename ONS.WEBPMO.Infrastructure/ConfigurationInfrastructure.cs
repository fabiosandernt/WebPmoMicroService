using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ONS.WEBPMO.Domain.Repositories.Impl;
using ONS.WEBPMO.Domain.Repositories.Impl.Repositories;
using ONS.WEBPMO.Domain.Repositories.Impl.Repositories.BDT;
using ONS.WEBPMO.Domain.Repository;
using ONS.WEBPMO.Domain.Repository.BDT;
using ONS.WEBPMO.Infrastructure.Context;
using ONS.WEBPMO.Infrastructure.DataBase;


namespace ONS.WEBPMO.Infrastructure
{
    public static class ConfigurationInfrastructure
    {
        public static IServiceCollection RegisterRepository(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<WEBPMODbContext>(c =>
            {
                c.UseSqlServer(connectionString);
            });


            services.AddScoped(typeof(Repository<>));
            services.AddScoped<IInsumoRepository, InsumoRepository>();
            services.AddScoped<ICategoriaInsumoRepository, CategoriaInsumoRepository>();
            services.AddScoped<ITipoColetaRepository, TipoColetaRepository>();
            services.AddScoped<IGrandezaRepository, GrandezaRepository>();
            services.AddScoped<IParametroRepository, ParametroRepository>();
            services.AddScoped<ITipoDadoGrandezaRepository, TipoDadoGrandezaRepository>();
            services.AddScoped<IOrigemColetaRepository, OrigemColetaRepository>();

            services.AddScoped<IInstanteVolumeReservatorioRepository, InstanteVolumeReservatorioRepository>();
            services.AddScoped<IReservatorioEERepository, ReservatorioEERepository>();
            services.AddScoped<IReservatorioPMORepository, ReservatorioPMORepository>();
            services.AddScoped<ISubmercadoPMORepository, SubmercadoPMORepository>();
            services.AddScoped<ISubsistemaPMORepository, SubsistemaPMORepository>();
            services.AddScoped<IUnidadeGeradoraPMORepository, UnidadeGeradoraPMORepository>();
            services.AddScoped<IUsinaPEMRepository, UsinaPEMRepository>();
            services.AddScoped<IUsinaPMORepository, UsinaPMORepository>();
            services.AddScoped<IAgenteRepository, AgenteRepository>();
            services.AddScoped<IArquivoRepository, ArquivoRepository>();
            services.AddScoped<IArquivoSemanaOperativaRepository, ArquivoSemanaOperativaRepository>();
            services.AddScoped<IColetaInsumoRepository, ColetaInsumoRepository>();
            services.AddScoped<IDadoColetaRepository, DadoColetaRepository>();
            services.AddScoped<IDadoColetaEstruturadoRepository, DadoColetaEstruturadoRepository>();
            services.AddScoped<IDadoColetaManutencaoRepository, DadoColetaManutencaoRepository>();
            services.AddScoped<IDadoColetaNaoEstruturadoRepository, DadoColetaNaoEstruturadoRepository>();
            services.AddScoped<IGabaritoRepository, GabaritoRepository>();
            services.AddScoped<IHistoricoColetaInsumoRepository, HistoricoColetaInsumoRepository>();
            services.AddScoped<IHistoricoSemanaOperativaRepository, HistoricoSemanaOperativaRepository>();
            services.AddScoped<ILogDadosInformadosRepository, LogDadosInformadosRepository>();
            services.AddScoped<ILogNotificacaoRepository, LogNotificacaoRepository>();
            services.AddScoped<IPMORepository, PMORepository>();
            services.AddScoped<IReservatorioRepository, ReservatorioRepository>();
            services.AddScoped<ISemanaOperativaRepository, SemanaOperativaRepository>();
            services.AddScoped<ISituacaoColetaInsumoRepository, SituacaoColetaInsumoRepository>();
            services.AddScoped<ISituacaoSemanaOperativaRepository, SituacaoSemanaOperativaRepository>();
            services.AddScoped<ISubsistemaRepository, SubsistemaRepository>();
            services.AddScoped<ITipoLimiteRepository, TipoLimiteRepository>();
            services.AddScoped<ITipoPatamarRepository, TipoPatamarRepository>();
            services.AddScoped<IUnidadeGeradoraRepository, UnidadeGeradoraRepository>();
            services.AddScoped<IUsinaRepository, UsinaRepository>();



            return services;
        }
    }
}

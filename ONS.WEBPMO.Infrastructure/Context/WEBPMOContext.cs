using Microsoft.EntityFrameworkCore;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO;

namespace ONS.WEBPMO.Infrastructure.Context
{
    public class WEBPMODbContext : DbContext
    {
        public WEBPMODbContext(DbContextOptions<WEBPMODbContext> options) : base(options) { }

        public DbSet<Agente> Agentes { get; set; }
        public DbSet<CategoriaInsumo> CategoriaInsumos { get; set; }
        public DbSet<Arquivo> Arquivos { get; set; }
        public DbSet<ArquivoSemanaOperativa> ArquivoSemanaOperativas { get; set; }
        public DbSet<ColetaInsumo> ColetaInsumos { get; set; }
        public DbSet<DadoColetaEstruturado> DadoColetaEstruturados { get; set; }
        public DbSet<DadoColetaManutencao> DadoColetaManutencaos { get; set; }
        public DbSet<DadoColeta> DadoColetas { get; set; }
        public DbSet<DadoColetaNaoEstruturado> DadoColetaNaoEstruturados { get; set; }
        public DbSet<DadoConvergencia> DadoConvergencias { get; set; }
        public DbSet<Gabarito> Gabaritos { get; set; }
        public DbSet<Grandeza> Grandeza { get; set; }
        public DbSet<HistoricoColetaInsumo> HistoricoColetaInsumos { get; set; }
        public DbSet<HistoricoSemanaOperativa> HistoricoSemanaOperativas { get; set; }
        public DbSet<InsumoEstruturado> InsumoEstruturados { get; set; }
        public DbSet<Insumo> Insumos { get; set; }
        public DbSet<InsumoNaoEstruturado> InsumoNaoEstruturados { get; set; }
        public DbSet<LogDadosInformados> LogDadosInformadoss { get; set; }
        public DbSet<LogNotificacao> LogNotificacaos { get; set; }
        public DbSet<ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO.OrigemColeta> OrigemColetas { get; set; }
        public DbSet<Reservatorio> Reservatorios { get; set; }
        public DbSet<Subsistema> Subsistemas { get; set; }
        public DbSet<UnidadeGeradora> UnidadeGeradoras { get; set; }
        public DbSet<Usina> Usinas { get; set; }
        public DbSet<Parametro> Parametros { get; set; }
        public DbSet<PMO> PMOs { get; set; }
        public DbSet<SemanaOperativa> SemanaOperativas { get; set; }
        public DbSet<SituacaoColetaInsumo> SituacaoColetaInsumos { get; set; }
        public DbSet<SituacaoSemanaOperativa> SituacaoSemanaOperativas { get; set; }
        public DbSet<TipoColeta> TipoColetas { get; set; }
        public DbSet<TipoDadoGrandeza> TipoDadoGrandezas { get; set; }
        public DbSet<TipoLimite> TipoLimites { get; set; }
        public DbSet<TipoPatamar> TipoPatamars { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WEBPMODbContext).Assembly);
        }
    }
}

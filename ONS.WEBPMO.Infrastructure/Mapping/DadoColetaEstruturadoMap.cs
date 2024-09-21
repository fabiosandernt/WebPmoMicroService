namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    using System.Data.Entity.ModelConfiguration;

    using Entities;

    internal class DadoColetaEstruturadoMap : EntityTypeConfiguration<DadoColetaEstruturado>
    {
        public DadoColetaEstruturadoMap()
        {
            HasKey(t => t.Id);
            ToTable("tb_dadocoletaestruturado");
            Property(t => t.Id).HasColumnName("id_dadocoleta");
            Property(t => t.Valor).HasColumnName("val_dado").HasMaxLength(4000);
            Property(t => t.Estagio).HasColumnName("dsc_estagio").HasMaxLength(50);
            Property(t => t.TipoLimiteId).HasColumnName("id_tplimite");
            HasOptional(t => t.TipoLimite).WithMany().HasForeignKey(t => t.TipoLimiteId);
            Property(t => t.TipoPatamarId).HasColumnName("id_tppatamar");
            HasOptional(t => t.TipoPatamar).WithMany().HasForeignKey(t => t.TipoPatamarId);
            Property(t => t.DestacaModificacao).HasColumnName("flg_destacamodificacao");
        }
    }
}
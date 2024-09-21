namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    internal class InsumoMap : EntityTypeConfiguration<Insumo>
    {
        public InsumoMap()
        {
            HasKey(t => t.Id);
            ToTable("tb_insumopmo");
            Property(t => t.Id).HasColumnName("id_insumopmo");
            Property(t => t.Nome).HasColumnName("nom_insumopmo").IsRequired().HasMaxLength(150);
            Property(t => t.OrdemExibicao).HasColumnName("num_ordemexibicao");
            Property(t => t.PreAprovado).HasColumnName("flg_preaprovado");
            Property(t => t.Reservado).HasColumnName("flg_reservado");
            Property(t => t.DataUltimaAtualizacao).HasColumnName("din_ultimaalteracao");
            Property(t => t.Ativo).HasColumnName("flg_ativo");

            Property(t => t.ExportarInsumo).HasColumnName("flg_exportainsumo");
            Property(t => t.SiglaInsumo).HasColumnName("sgl_insumo").IsRequired().HasMaxLength(10);

            Property(t => t.Versao).HasColumnName("ver_controleconcorrencia")
                .IsConcurrencyToken()
                .IsRowVersion();

            Property(t => t.TipoInsumo).HasColumnName("tip_insumopmo")
                .IsRequired()
                .IsUnicode(false)
                .IsFixedLength()
                .HasMaxLength(1);
        }
    }
}
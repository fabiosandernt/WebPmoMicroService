using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class SemanaOperativaMap : IEntityTypeConfiguration<SemanaOperativa>
    {
        public void Configure(EntityTypeBuilder<SemanaOperativa> builder)
        {
            // Nome da tabela
            builder.ToTable("tb_semanaoperativa");

            // Chave primária
            builder.HasKey(t => t.Id);

            // Configuração das propriedades
            builder.Property(t => t.Id)
                   .HasColumnName("id_semanaoperativa");

            builder.Property(t => t.Nome)
                   .HasColumnName("nom_semanaoperativa")
                   .HasMaxLength(150);

            builder.Property(t => t.DataInicioSemana)
                   .HasColumnName("dat_iniciosemana");

            builder.Property(t => t.DataFimSemana)
                   .HasColumnName("dat_fimsemana");

            builder.Property(t => t.DataReuniao)
                   .HasColumnName("dat_reuniao");

            builder.Property(t => t.DataInicioManutencao)
                   .HasColumnName("dat_iniciomanutencao");

            builder.Property(t => t.DataFimManutencao)
                   .HasColumnName("dat_fimmanutencao");

            builder.Property(t => t.Revisao)
                   .HasColumnName("num_revisao")
                   .IsRequired();

            builder.Property(t => t.Versao)
                   .HasColumnName("ver_controleconcorrencia")
                   .IsConcurrencyToken()
                   .IsRowVersion();

            builder.Property(t => t.DataHoraAtualizacao)
                   .HasColumnName("din_ultimaalteracao");

            builder.Property(t => t.PMOId)
                   .HasColumnName("id_pmo");

            builder.Property(e => e.SituacaoId)
                   .HasColumnName("id_tpsituacaosemanaoper");

            // Relacionamento com Gabaritos
            builder.HasMany(e => e.Gabaritos)
                   .WithOne(e => e.SemanaOperativa)
                   .HasForeignKey(t => t.SemanaOperativaId)
                   .OnDelete(DeleteBehavior.Restrict); // Equivalente ao WithOptional e WillCascadeOnDelete(false)

            // Relacionamento com ColetasInsumos
            builder.HasMany(e => e.ColetasInsumos)
                   .WithOne(e => e.SemanaOperativa)
                   .HasForeignKey(t => t.SemanaOperativaId)
                   .OnDelete(DeleteBehavior.Restrict); // Equivalente ao WithRequired e WillCascadeOnDelete(false)

            // Relacionamento com Situacao
            builder.HasOne(e => e.Situacao)
                   .WithMany()
                   .HasForeignKey(e => e.SituacaoId)
                   .OnDelete(DeleteBehavior.Restrict); // Equivalente ao HasOptional e WillCascadeOnDelete(false)

            // Relacionamento com Arquivos
            builder.HasMany(t => t.Arquivos)
                   .WithOne(e => e.SemanaOperativa)
                   .HasForeignKey(t => t.SemanaOperativaId)
                   .OnDelete(DeleteBehavior.Cascade); // Equivalente ao WithRequired e WillCascadeOnDelete(true)

            // Relacionamento com DadoConvergencia
            builder.HasOne(t => t.DadoConvergencia)
                   .WithOne(t => t.SemanaOperativa)
                   .HasForeignKey<DadoConvergencia>(t => t.SemanaOperativaId) // Chave estrangeira explícita
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }

}

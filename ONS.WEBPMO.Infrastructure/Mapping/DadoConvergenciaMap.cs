using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using ONS.SGIPMO.Domain.Entities;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class DadoConvergenciaMap : EntityTypeConfiguration<DadoConvergencia>
    {
        public DadoConvergenciaMap()
        {
            ToTable("tb_dadosconvergencia");
            HasKey(t => t.Id);

            Property(t => t.Id).HasColumnName("id_dadosconvergencia").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.LoginConvergenciaRepresentanteCCEE).HasColumnName("lgn_representanteccee").HasMaxLength(60);
            Property(t => t.ObservacaoConvergenciaCCEE).HasColumnName("obs_ccee").HasMaxLength(1000);
        }
    }
}

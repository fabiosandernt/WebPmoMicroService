namespace ONS.WEBPMO.Application.Services.PMO.Implementation.GeracaoBlocos.Metadados
{
    using System.Collections.Generic;
    using Entities;
    using ONS.WEBPMO.Application.Services.PMO.Implementation.GeracaoBlocos.Blocos;

    public class BlocoTG : BlocoCadastroTermica
    {
        public BlocoTG(IList<DadoColeta> dadosColeta, IList<DadoColetaBloco> dadosColetaBloco, SemanaOperativa semanaOperativa)
            : base(TipoBlocoEnum.TG, dadosColeta, dadosColetaBloco, semanaOperativa)
        {

        }
    }
}

namespace ONS.WEBPMO.Application.Services.PMO.Implementation.GeracaoBlocos.Blocos
{
    using System.Collections.Generic;
    using Entities;

    public class BlocoHQ : BlocoRestricao
    {
        public BlocoHQ(IList<DadoColeta> dadosColeta, IList<DadoColetaBloco> dadosColetaBloco, SemanaOperativa semanaOperativa)
            : base(TipoBlocoEnum.HQ, dadosColeta, dadosColetaBloco, semanaOperativa)
        {
        }
    }
}

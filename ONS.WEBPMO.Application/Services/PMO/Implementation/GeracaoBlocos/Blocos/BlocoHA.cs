namespace ONS.WEBPMO.Application.Services.PMO.Implementation.GeracaoBlocos.Blocos
{
    using System.Collections.Generic;
    using Entities;

    public class BlocoHA : BlocoRestricao
    {
        public BlocoHA(IList<DadoColeta> dadosColeta, IList<DadoColetaBloco> dadosColetaBloco, SemanaOperativa semanaOperativa)
            : base(TipoBlocoEnum.HA, dadosColeta, dadosColetaBloco, semanaOperativa)
        {
        }
    }
}

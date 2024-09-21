using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Enumerations;


namespace ONS.WEBPMO.Application.Services.PMO.Implementation.GeracaoBlocos.Blocos
{
    public class BlocoHQ : BlocoRestricao
    {
        public BlocoHQ(IList<DadoColeta> dadosColeta, IList<DadoColetaBloco> dadosColetaBloco, SemanaOperativa semanaOperativa)
            : base(TipoBlocoEnum.HQ, dadosColeta, dadosColetaBloco, semanaOperativa)
        {
        }
    }
}

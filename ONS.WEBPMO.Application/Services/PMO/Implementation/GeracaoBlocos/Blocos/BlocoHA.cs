using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Enumerations;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation.GeracaoBlocos.Blocos
{
    public class BlocoHA : BlocoRestricao
    {
        public BlocoHA(IList<DadoColeta> dadosColeta, IList<DadoColetaBloco> dadosColetaBloco, SemanaOperativa semanaOperativa)
            : base(TipoBlocoEnum.HA, dadosColeta, dadosColetaBloco, semanaOperativa)
        {
        }
    }
}

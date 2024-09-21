using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Enumerations;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation.GeracaoBlocos.Blocos
{
    public class BlocoCT : BlocoCadastroTermica
    {
        public BlocoCT(IList<DadoColeta> dadosColeta, IList<DadoColetaBloco> dadosColetaBloco, SemanaOperativa semanaOperativa)
            : base(TipoBlocoEnum.CT, dadosColeta, dadosColetaBloco, semanaOperativa)
        {

        }
    }
}

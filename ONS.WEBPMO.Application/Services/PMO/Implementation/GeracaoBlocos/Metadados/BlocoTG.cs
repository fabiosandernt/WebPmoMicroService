using ONS.WEBPMO.Application.Services.PMO.Implementation.GeracaoBlocos.Blocos;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Enumerations;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation.GeracaoBlocos.Metadados
{
    public class BlocoTG : BlocoCadastroTermica
    {
        public BlocoTG(IList<DadoColeta> dadosColeta, IList<DadoColetaBloco> dadosColetaBloco, SemanaOperativa semanaOperativa)
            : base(TipoBlocoEnum.TG, dadosColeta, dadosColetaBloco, semanaOperativa)
        {

        }
    }
}

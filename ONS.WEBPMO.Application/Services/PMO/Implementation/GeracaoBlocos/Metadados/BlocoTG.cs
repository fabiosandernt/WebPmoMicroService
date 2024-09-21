namespace ONS.WEBPMO.Application.Services.PMO.Implementation.GeracaoBlocos.Metadados
{
    using ONS.WEBPMO.Application.Services.PMO.Implementation.GeracaoBlocos.Blocos;
    using System.Collections.Generic;

    public class BlocoTG : BlocoCadastroTermica
    {
        public BlocoTG(IList<DadoColeta> dadosColeta, IList<DadoColetaBloco> dadosColetaBloco, SemanaOperativa semanaOperativa)
            : base(TipoBlocoEnum.TG, dadosColeta, dadosColetaBloco, semanaOperativa)
        {

        }
    }
}

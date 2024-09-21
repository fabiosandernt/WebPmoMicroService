namespace ONS.WEBPMO.Application.Services.PMO.Implementation.GeracaoBlocos.Blocos
{
    using System.Collections.Generic;
    using Entities;

    public class BlocoCT : BlocoCadastroTermica
    {
        public BlocoCT(IList<DadoColeta> dadosColeta, IList<DadoColetaBloco> dadosColetaBloco, SemanaOperativa semanaOperativa)
            : base(TipoBlocoEnum.CT, dadosColeta, dadosColetaBloco, semanaOperativa)
        {

        }
    }
}

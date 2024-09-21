namespace ONS.WEBPMO.Application.Services.PMO.Implementation.GeracaoBlocos.Blocos
{
    using System.Collections.Generic;
    using System.Linq;
    using Common.IoC;
    using Common.Util;
    using Entities;
    using ONS.WEBPMO.Application.Services.PMO.Interfaces;

    public class BlocoRE : BlocoRestricao
    {
        private Parametro parametro;

        public BlocoRE(IList<DadoColeta> dadosColeta, IList<DadoColetaBloco> dadosColetaBloco, SemanaOperativa semanaOperativa)
            : base(TipoBlocoEnum.RE, dadosColeta, dadosColetaBloco, semanaOperativa)
        {
            IParametroService parametroService = ApplicationContext.Resolve<IParametroService>();
            parametro = parametroService.ObterParametro(ParametroEnum.AcrescimoRestricaoEletricaTermica);
        }

        protected override string ObterCodigoRestricao(IConjuntoGerador usinaReservatorio)
        {
            Usina usina = usinaReservatorio as Usina;

            if (usina != null && usina.TipoUsina == TipoUsinaEnum.Termica.ToDescription())
            {
                return string.Format("{0}{1}", usinaReservatorio.CodigoDPP, parametro.Valor);
            }

            return base.ObterCodigoRestricao(usinaReservatorio);
        }
    }
}

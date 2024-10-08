

using AutoMapper;
using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO;
using ONS.WEBPMO.Domain.Enumerations;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation.GeracaoBlocos.Blocos
{
    public class BlocoRE : BlocoRestricao
    {
        private readonly IParametroService _parametroService; 
        private readonly IMapper _mapper;

        public BlocoRE(IMapper mapper, IParametroService parametroService, IList<DadoColeta> dadosColeta, IList<DadoColetaBloco> dadosColetaBloco, SemanaOperativa semanaOperativa)
            : base(TipoBlocoEnum.RE, dadosColeta, dadosColetaBloco, semanaOperativa)
        {
            _parametroService = parametroService;
            _mapper = mapper;
        }
        protected override string ObterCodigoRestricao(IConjuntoGerador usinaReservatorio)
        {
            Usina usina = usinaReservatorio as Usina;
            
            var parametroDto = _parametroService.ObterParametro(ParametroEnum.AcrescimoRestricaoEletricaTermica);

            var parametro = _mapper.Map<Parametro>(parametroDto);

            if (usina != null && usina.TipoUsina == TipoUsinaEnum.Termica.ToDescription())
            {
                return string.Format("{0}{1}", usinaReservatorio.CodigoDPP, parametro.Valor);
            }

            return base.ObterCodigoRestricao(usinaReservatorio);
        }

        
    }
}

using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Enumerations;
using ONS.WEBPMO.Domain.Repository.PMO;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{
    public class ParametroService : IParametroService
    {
        private IParametroRepository parametroRepository { get; set; }

        public ParametroService(IParametroRepository ParametroRepository)
        {
            this.parametroRepository = ParametroRepository;
        }

        public Parametro ObterParametro(ParametroEnum paramentoEnum)
        {

            Parametro parametro = parametroRepository.ObterPorTipo(paramentoEnum);
            if (parametro == null || string.IsNullOrEmpty(parametro.Valor))
            {
                string mensagem = string.Format("Parâmetro {0} não cadastrado", paramentoEnum.GetDescription());
                throw new ArgumentException(mensagem);
            }
            return parametro;

        }
    }
}

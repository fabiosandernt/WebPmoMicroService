using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Enumerations;
using ONS.WEBPMO.Domain.Repository;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{
    public class ParametroService : IParametroService
    {
        private IParametroRepository ParametroRepository { get; set; }

        public ParametroService(IParametroRepository ParametroRepository)
        {
            this.ParametroRepository = ParametroRepository;
        }

        public Parametro ObterParametro(ParametroEnum parametroEnum)
        {
            Parametro parametro = ParametroRepository.ObterPorTipo(parametroEnum);
            if (parametro == null || string.IsNullOrEmpty(parametro.Valor))
            {
                string mensagem = string.Format("Parâmetro {0} não cadastrado", parametroEnum.ToDescription());

            }
            return parametro;
        }
    }
}

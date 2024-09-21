using ONS.Common.Exceptions;
using ONS.Common.Services.Impl;
using ONS.Common.Util;
using ONS.SGIPMO.Domain.Entities;
using ONS.SGIPMO.Domain.Repositories;
using ONS.WEBPMO.Application.Services.PMO.Interfaces;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{
    public class ParametroService : Service, IParametroService
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
                throw new ONSInfraException(mensagem);
            }
            return parametro;
        }
    }
}

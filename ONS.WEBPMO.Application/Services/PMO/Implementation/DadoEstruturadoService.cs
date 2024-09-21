using ONS.Common.Services.Impl;
using ONS.SGIPMO.Domain.Entities;
using ONS.SGIPMO.Domain.Repositories;
using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{
    public class DadoColetaEstruturadoService : Service, IDadoColetaEstruturadoService
    {
        private IDadoColetaEstruturadoRepository DadoColetaEstruturadoRepository { get; set; }

        public DadoColetaEstruturadoService(IDadoColetaEstruturadoRepository DadoColetaEstruturadoRepository)
        {
            this.DadoColetaEstruturadoRepository = DadoColetaEstruturadoRepository;
        }

        public DadoColetaEstruturado ObterPorChave(int chave)
        {
            return DadoColetaEstruturadoRepository.FindByKey(chave);
        }
    }
}

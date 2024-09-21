using System.Globalization;
using ONS.Common.Configuration;
using ONS.Common.Exceptions;
using ONS.Common.Seguranca;
using ONS.Common.Services.Impl;
using ONS.SGIPMO.Domain.Entities;
using ONS.SGIPMO.Domain.Entities.Enumerations;
using ONS.SGIPMO.Domain.Entities.Filters;
using ONS.SGIPMO.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using ONS.Common.Util.Pagination;
using ONS.WEBPMO.Application.Services.PMO.Interfaces;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{
    public class LogDadosInformadosService : Service, ILogDadosInformadosService
    {

        private readonly ILogDadosInformadosRepository logDadosInformadosRepository;
        public LogDadosInformadosService(ILogDadosInformadosRepository logDadosInformadosRepository)
        {
            this.logDadosInformadosRepository = logDadosInformadosRepository;
        }

        public PagedResult<LogDadosInformados> obterLogDadosInformados(LogDadosInformadosFilter filter)
        {
            PagedResult<LogDadosInformados> logsDadosInformados = new PagedResult<LogDadosInformados>(new List<LogDadosInformados>(), 0, 0, 0);

            logsDadosInformados = logDadosInformadosRepository.ConsultarPorFiltro(filter);

            return logsDadosInformados;
        }
    }
}

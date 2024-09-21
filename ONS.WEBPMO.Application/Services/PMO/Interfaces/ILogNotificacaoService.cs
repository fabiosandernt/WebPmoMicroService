using System.Collections.Generic;
using ONS.Common.Services;
using ONS.Common.Util.Files;
using ONS.Common.Util.Pagination;
using ONS.Common.Wcf;
using ONS.SGIPMO.Domain.Entities;
using System.ServiceModel;
using ONS.SGIPMO.Domain.Entities.DTO;
using ONS.SGIPMO.Domain.Entities.Filters;
using System;

namespace ONS.WEBPMO.Application.Services.PMO.Interfaces
{
    [ServiceContract]
    public interface ILogNotificacaoService : IService
    {
        [OperationContract]
        [UseNetDataContractSerializer("Agente", "SemanaOperativa")]
        PagedResult<LogNotificacao> ObterLogNotificacao(LogNotificacaoFilter filter);

        void LogarNotificacao(SemanaOperativa semanaOperativa, List<Agente> agentes, DateTime? dataHoraAcao, string nomeUsuario, string acao);

        [OperationContract]
        bool Apagar(List<int> idsLogs);
    }
}

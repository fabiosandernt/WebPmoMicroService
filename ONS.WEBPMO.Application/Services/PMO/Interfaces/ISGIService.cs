using System;
using System.Collections.Generic;
using System.ServiceModel;
using ONS.Common.Services;
using ONS.Common.Wcf;
using ONS.SGIPMO.Domain.Entities;

namespace ONS.WEBPMO.Application.Services.PMO.Interfaces
{
    [ServiceContract]
    public interface ISGIService : IService
    {
        [OperationContract]
        [UseNetDataContractSerializer]
        IList<DadoColetaManutencao> ObterManutencoesPorChaves(string[] chavesUnidadesGeradoras,
            DateTime dataInicio, DateTime dataFim);
    }
}

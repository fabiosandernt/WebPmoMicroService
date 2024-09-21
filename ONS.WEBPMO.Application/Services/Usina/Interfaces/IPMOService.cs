using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ONS.SGIPMO.Domain.Entities;

namespace ONS.WEBPMO.Servico.Usina
{
    [ServiceContract]
    [ServiceKnownType(typeof(List<CronogramaPMO>))]
    public interface IPMOService
    {
        /// <summary>
        /// Consulta PMO
        /// Parametros ano e mes, são obrigatórios para o retorno 
        /// </summary>
        /// <param name="ano">Ano para filtrar</param>
        /// <param name="mes">Mes para filtrar</param>
        /// <returns>Lista de CronogramaPMO</returns>
        [OperationContract]
        List<CronogramaPMO> ObterCronogramaPMO(int ano, int mes);
    }
}

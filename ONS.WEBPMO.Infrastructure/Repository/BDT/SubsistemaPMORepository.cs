using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ONS.Common.Repositories.Impl;
using ONS.SGIPMO.Domain.Entities.BDT;
using ONS.SGIPMO.Domain.Repositories.BDT;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Repositories.BDT
{
    using ONS.SGIPMO.Domain.Entities;

    [UseDbContext(ConnectionStringsNames.BDTModel)]
    public class SubsistemaPMORepository : Repository<SubsistemaPMO>, ISubsistemaPMORepository
    {
        /// <summary>
        /// Consulta todos os subsistemas na BDT
        /// </summary>
        /// <returns></returns>
        public IList<SubsistemaPMO> ConsultarTodos()
        {
            return EntitySet.ToList();
        }

        public IList<SubsistemaPMO> ConsultarAtivos()
        {
            return EntitySet.Where(s => s.DataDesativacao == null).ToList();
        }

        public IList<SubsistemaPMO> ConsultarOutros()
        {
            string sql = "SELECT "
            + "TRIM(Extent1.cod_ptointermedsubsistema) AS Id, "
            + "'' AS NomeLongo, "
            + "CURRENT As DataEntrada, "
            + "CURRENT As DataDesativacao, "
            + "Extent1.nom_ptointermedsubsistema AS NomeCurto, "
            + "Extent1.cod_subsistemamodenerg AS CodigoModeloEnergia "
            + "FROM informix.tb_ptointermedsubsistema AS Extent1 "
            + "INNER JOIN informix.tb_ptointersubsistestudo AS Extent2 ON Extent1.id_ptointermedsubsistema = Extent2.id_ptointermedsubsistema "
            + "WHERE Extent2.id_tpestudoeleene = 22 ";

            var retorno = EntitySet.SqlQuery(sql).ToList();

            retorno.ForEach(r => r.DataDesativacao = null);

            return retorno;
        }
    }
}

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
    public class SubmercadoPMORepository : Repository<SubmercadoPMO>, ISubmercadoPMORepository
    {
        /// <summary>
        /// Consulta todos os Submercados na BDT
        /// </summary>
        /// <returns></returns>
        public IList<SubmercadoPMO> ConsultarTodos()
        {
            return EntitySet.ToList();
        }
        
    }
}

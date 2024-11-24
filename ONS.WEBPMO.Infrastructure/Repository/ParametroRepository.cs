using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Enumerations;
using ONS.WEBPMO.Domain.Repository.PMO;
using ONS.WEBPMO.Infrastructure.Context;
using ONS.WEBPMO.Infrastructure.DataBase;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Repositories
{
    public class ParametroRepository : Repository<Parametro>, IParametroRepository
    {
        public ParametroRepository(WEBPMODbContext context) : base(context)
        {
        }


        public Parametro ObterPorTipo(ParametroEnum parametro)
        {
            string nomeParametro = parametro.GetDescription();
            var query = this.Query.FirstOrDefault(param => param.Nome == nomeParametro);
            return query;
        }
    }
}

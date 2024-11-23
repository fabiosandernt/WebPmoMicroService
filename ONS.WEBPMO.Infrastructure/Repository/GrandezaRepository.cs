using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository.PMO;
using ONS.WEBPMO.Infrastructure.Context;
using ONS.WEBPMO.Infrastructure.DataBase;

namespace ONS.WEBPMO.Domain.Repositories.Impl
{
    public class GrandezaRepository : Repository<Grandeza>, IGrandezaRepository
    {
        public GrandezaRepository(WEBPMODbContext context) : base(context)
        {
        }

        public IList<Grandeza> ConsultarPorFiltro(GrandezaFilter filter)
        {
            throw new NotImplementedException();
        }

        public IList<Grandeza> ConsultarPorInsumo(int idInsumo)
        {
            throw new NotImplementedException();
        }

        public Grandeza ConsultarPorNome(string nomeGrandeza)
        {
            throw new NotImplementedException();
        }

        public bool ExisteDadosColetaNaGrandeza(int idGrandeza)
        {
            throw new NotImplementedException();
        }

        public bool ExisteGrandezaPorEstagio(int idInsumo)
        {
            throw new NotImplementedException();
        }

        public bool ExisteGrandezaPorLimite(int idInsumo)
        {
            throw new NotImplementedException();
        }

        public bool ExisteGrandezaPorPatamar(int idInsumo)
        {
            throw new NotImplementedException();
        }

        public bool ExistePreAprovadoComAlteracao(int idInsumo)
        {
            throw new NotImplementedException();
        }
    }
}

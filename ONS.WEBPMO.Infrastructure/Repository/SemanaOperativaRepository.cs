using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository.PMO;
using ONS.WEBPMO.Infrastructure.Context;
using ONS.WEBPMO.Infrastructure.DataBase;

namespace ONS.WEBPMO.Domain.Repositories.Impl
{
    public class SemanaOperativaRepository : Repository<SemanaOperativa>, ISemanaOperativaRepository
    {
        public SemanaOperativaRepository(WEBPMODbContext context) : base(context)
        {
        }

        public IList<SemanaOperativa> ConsultarEstudoPorNome(string nomeEstudo, int quantidadeMaxima)
        {
            throw new NotImplementedException();
        }

        public IList<SemanaOperativa> ConsultarEstudoPorNomeEStatus(string nomeEstudo, int? idStatus, int quantidadeMaxima)
        {
            throw new NotImplementedException();
        }

        public IList<SemanaOperativa> ConsultarSemanasOperativasComGabarito()
        {
            throw new NotImplementedException();
        }
    }
}

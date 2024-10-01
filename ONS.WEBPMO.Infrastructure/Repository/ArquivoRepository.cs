using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository;
using ONS.WEBPMO.Infrastructure.Context;
using ONS.WEBPMO.Infrastructure.DataBase;

namespace ONS.WEBPMO.Domain.Repositories.Impl
{
    public class ArquivoRepository : Repository<Arquivo>, IArquivoRepository
    {
        public ArquivoRepository(WEBPMODbContext context) : base(context)
        {
        }

        public IList<Arquivo> ConsultarArquivosAssociadosGabaritos(IList<int> idsGabarito)
        {
            throw new NotImplementedException();
        }

        public void DeletarPorIdGabarito(IList<int> idsGabarito)
        {
            throw new NotImplementedException();
        }

        public byte[] GetDataContentFile(Arquivo arquivo)
        {
            throw new NotImplementedException();
        }

        public Arquivo SalvarArquivoContentFile(Arquivo instanciaArquivoAindaNaoSalvo)
        {
            throw new NotImplementedException();
        }
    }
}

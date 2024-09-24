
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository.Base;

namespace ONS.WEBPMO.Domain.Repository
{
    public interface IArquivoRepository : IRepository<Arquivo>
    {
        void DeletarPorIdGabarito(IList<int> idsGabarito);

        /// <summary>
        /// Método que obtem o byte[] de um arquivo. Foi colocado de forma separada porque internamento ele muda o 
        /// commandTimeout uma vez que arquivos de 200MB precisam ser suportados.
        /// </summary>
        /// <param name="arquivo"></param>
        /// <returns></returns>
        byte[] GetDataContentFile(Arquivo arquivo);

        /// <summary>
        /// Gravação de arquivo de forma isolada a fim de evitar sobrecarregar o SaveChanges().
        /// </summary>
        /// <param name="instanciaArquivoAindaNaoSalvo"></param>
        /// <returns></returns>
        Arquivo SalvarArquivoContentFile(Arquivo instanciaArquivoAindaNaoSalvo);

        /// <summary>
        /// Consulta os arquivos associados aos gabaritos por id.
        /// </summary>
        /// <param name="idsGabarito">Ids dos gabaritos</param>
        /// <returns></returns>
        IList<Arquivo> ConsultarArquivosAssociadosGabaritos(IList<int> idsGabarito);

    }
}

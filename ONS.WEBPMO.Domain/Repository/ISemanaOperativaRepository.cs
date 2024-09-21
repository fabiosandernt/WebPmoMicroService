

using ONS.WEBPMO.Domain.Repository.Base;

namespace ONS.WEBPMO.Domain.Repository
{
    public interface ISemanaOperativaRepository : IRepository<SemanaOperativa>
    {
        IList<SemanaOperativa> ConsultarSemanasOperativasComGabarito();

        IList<SemanaOperativa> ConsultarEstudoPorNome(string nomeEstudo, int quantidadeMaxima);

        IList<SemanaOperativa> ConsultarEstudoPorNomeEStatus(string nomeEstudo, int? idStatus, int quantidadeMaxima);
    }
}

using ONS.WEBPMO.Domain.Entities.BDT;
using ONS.WEBPMO.Domain.Repository.BDT;
using ONS.WEBPMO.Infrastructure.DataBase;
using System.Text;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Repositories.BDT
{
    //[UseDbContext(ConnectionStringsNames.BDTModel)]
    public class InstanteVolumeReservatorioRepository : Repository<InstanteVolumeReservatorio>, IInstanteVolumeReservatorioRepository
    {
        public IList<InstanteVolumeReservatorio> Consultar(string usinaId, DateTime dataInicio, DateTime dataFim)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(" SELECT");
            sql.Append(" gr.res_id as Id, gr.origem_id as OrigemId, gr.period_id as PeriodoId, gr.tpgrand_id as TipoGrandezaId, gr.instante, gr.valor");
            sql.Append(" FROM gr_hidr_res gr ");
            sql.Append(" INNER JOIN aprov a on a.res_id = gr.res_id");
            sql.Append(" WHERE gr.tpgrand_id = 'VUT' AND gr.period_id = 'DI' ");
            sql.Append($" AND a.usi_id = '{usinaId}'");
            sql.Append($" AND gr.instante between '{dataInicio.ToString("yyyy-MM-dd")} 00:00:00' AND '{dataFim.ToString("yyyy-MM-dd")} 23:59:59'");
            sql.Append(" ORDER BY gr.instante");

            var retorno = EntitySet.SqlQuery(sql.ToString()).ToList();
            retorno = retorno.ToList();

            return retorno;
        }
    }
}

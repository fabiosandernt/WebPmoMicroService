using System.Text;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Repositories.BDT
{
    [UseDbContext(ConnectionStringsNames.BDTModel)]
    public class ReservatorioPMORepository : Repository<ReservatorioPMO>, IReservatorioPMORepository
    {
        #region " SQL "
        private static readonly string sqlBase =
            @"select 
	res_pmo.usi_cod as Codigo,
	(TRIM(NVL(res_pmo.id_subsistema, '')) || ',' || res_pmo.cod_chave) as Id,
	res_pmo.nomelongo as NomeLongo,
	res_pmo.nomecurto as NomeCurto,
	(res_pmo.nomelongo || ' (' || res_pmo.usi_cod || ')') as NomeExibicao,
	res_pmo.id_subsistema as SiglaSubsistema,
	res_pmo.qtd_subsistema as QuantidadeSubsistema,
	CodUsiPlanejamentoJusante,
	Cod_reservatorioee,
	Cod_subsistemamodenerg,
	Nom_curto_reservatorioee
from
   (
	select 
		res.usi_cod,
		res.usi_cod || ',' || res.res_id as cod_chave,
		ag_t_groupconcatspace(distinct TRIM(res.nomelongo)) as nomelongo,
		ag_t_groupconcatspace(distinct TRIM(res.nomecurto)) as nomecurto,
		ag_t_groupconcatspace(distinct TRIM(nvl(res.id_subsistema, ''))) as id_subsistema,
		count(res.id_subsistema) as qtd_subsistema,
		GROUP_CONCAT(DISTINCT  TRIM( NVL(res.cod_usiplanejamento, '') ) ) as CodUsiPlanejamentoJusante,
		GROUP_CONCAT(DISTINCT  TRIM( NVL(res.cod_reservatorioee, '') ) ) as Cod_reservatorioee,
		res.cod_subsistemamodenerg as Cod_subsistemamodenerg,
		GROUP_CONCAT(DISTINCT  TRIM( NVL(Nom_curto_reservatorioee, '') ) ) as Nom_curto_reservatorioee

	from
		(
			select res.usi_cod, res.res_id, res.nomelongo, res.nomecurto, res.id_subsistema, 
				ree.cod_reservatorioee,
				tbSub.cod_subsistemamodenerg,
				uge.cod_usiplanejamento,
				ree.nom_curto as Nom_curto_reservatorioee
			from res
			left join
				res resjusante
			on 
				resjusante.res_id = res.usijusante
			left join
				aprov aprovjusante
			on 
				aprovjusante.res_id = resjusante.res_id
			left join
				uge
			on 
				aprovjusante.usi_id = uge.usi_id
			left join
				tb_reservatorioeevigenciares rev 
			on 
				res.res_id = rev.res_id  and rev.din_fimvigencia is NULL
			left join
				tb_reservatorioee ree  
			on 
				rev.id_reservatorioee = ree.id_reservatorioee
			left join
				tb_subsistema tbSub
			on 
				tbSub.id_subsistema = res.id_subsistema

			where 
				res.usi_cod is not null
				and res.usi_cod <> 0
				and res.dtdesativa is null	
			group by res.usi_cod, res.res_id, res.nomelongo, res.nomecurto, res.id_subsistema,ree.cod_reservatorioee, tbSub.cod_subsistemamodenerg, uge.cod_usiplanejamento, Nom_curto_reservatorioee
			order by res.usi_cod, res.res_id
		) res
	group by res.usi_cod, res.id_subsistema,res.id_subsistema, cod_chave, cod_subsistemamodenerg
   ) res_pmo

where 1 = 1 ";
        #endregion

        /// <summary>
        /// Consulta reservatorios do PMO
        /// Caso seja informado o parâmetro nomeExibicaoContem, será aplicado o filtro de LIKE (%nomeExibicaoContem%)
        /// </summary>
        /// <param name="nomeExibicaoContem">Nome de exibição para filtrar</param>
        /// <returns>Lista de Reservatorios do PMO</returns>
        public IList<ReservatorioPMO> ConsultarPorNomeExibicao(string nomeExibicaoContem)
        {
            StringBuilder sql = new StringBuilder();
            List<object> parametrosSql = new List<object>();

            sql.Append(sqlBase);

            if (!String.IsNullOrEmpty(nomeExibicaoContem))
            {
                sql.Append("and UPPER((res_pmo.nomelongo || ' (' || res_pmo.usi_cod || ')')) like UPPER(?) ");
                parametrosSql.Add(string.Format("%{0}%", nomeExibicaoContem));
            }

            sql.AppendLine("order by (res_pmo.nomelongo || ' (' || res_pmo.usi_cod || ')')");

            var retorno = EntitySet.SqlQuery(sql.ToString(), parametrosSql.ToArray()).ToList();

            return retorno;
        }

        /// <summary>
        /// Consulta reservatorios do PMO onde as chaves sejam iguais as passadas por parametro
        /// </summary>
        /// <param name="chaves">Nome de exibição para filtrar</param>
        /// <returns>Lista de Reservatorios do PMO</returns>
        public IList<ReservatorioPMO> ConsultarPorChaves(string[] chaves)
        {
            if (chaves == null || chaves.Length == 0)
            {
                throw new ArgumentNullException("O parâmetro chaves não pode ser nulo ou vazio.");
            }

            StringBuilder sql = new StringBuilder();
            List<object> parametrosSql = new List<object>();

            sql.Append(sqlBase);

            sql.AppendFormat("and (TRIM(NVL(res_pmo.id_subsistema, '')) || ',' || res_pmo.cod_chave) in ({0}) ", string.Join(",", chaves.Select(ch => "?")));
            sql.AppendLine("order by (res_pmo.nomelongo || ' (' || res_pmo.usi_cod || ')')");

            var retorno = EntitySet.SqlQuery(sql.ToString(), chaves).ToList();

            return retorno;
        }
    }
}

using System.Text;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Repositories.BDT
{
    [UseDbContext(ConnectionStringsNames.BDTModel)]
    public class UsinaPMORepository : Repository<UsinaPMO>, IUsinaPMORepository
    {
        #region " SQL "
        private static readonly string sqlBase = @"
                        select
	                        (NVL(usipmo.id_subsistema, '') || ',' || chave) as Id,
	                        usipmo.cod_usiplanejamento as CodUsinaPlanejamento,
	                        usipmo.nomelongo as NomeLongo,
	                        usipmo.nomecurto as NomeCurto,
	                        cod_tpgeracao as TipoGeracao,
	                        (nom_exibicao || ' (' || NVL(usipmo.id_subsistema, '') || ')') as NomeExibicao,
	                        usipmo.id_subsistema as SiglaSubsistema,
	                        qtd_subsistema as QuantidadeSubsistema,
	                        tbSub.cod_subsistemamodenerg as Cod_subsistemamodenerg,
	                        GROUP_CONCAT(DISTINCT  TRIM( NVL(ree.id_reservatorioee, '') ) ) as Id_reservatorioee,
	                        GROUP_CONCAT(DISTINCT  TRIM( NVL(ree.nom_curto, '') ) ) as Nom_curto_reservatorioee,
            				GROUP_CONCAT(DISTINCT  TRIM( NVL(ree.cod_reservatorioee, '') ) ) as Cod_reservatorioee,
				            --GROUP_CONCAT(DISTINCT TRIM( NVL(res.usijusante, '') ) ) as CodDPPUsiJusante,
				            uge.cod_usiplanejamento as CodUsiPlanejamentoJusante,
				            id_usina as IdUsina,
				            nomecurto_sme as NomeCurtoSubmercado,
				            id_sme as CodSubmercado,
			            	usipmo.id_tpusina as CodigoTipoGeracao
                        from 	(

	                        -- INICIO USI
	                        select 
		                        cast(usi_chave.chave as lvarchar(1091)) as chave,
		                        cast(usi_chave.cod_usiplanejamento as integer) as cod_usiplanejamento,
		                        cast(usi_nome.nomelongo as lvarchar) as nomelongo,
		                        cast(usi_nome.nomecurto as lvarchar) as nomecurto,
		                        cast(usi_chave.cod_tpgeracao as varchar(15)) as cod_tpgeracao,
		                        cast((usi_nome.nomelongo || ' (' || usi_nome.cod_usiplanejamento || ')') as lvarchar) as nom_exibicao,
		                        cast(usi_ss.id_subsistema as lvarchar) as id_subsistema,
		                        cast(usi_ss.qtd_subsistema as int) as qtd_subsistema,
					            cast(usi_nome.usi_id as lvarchar(100)) as id_usina,
					            cast(usi_nome.nomecurto_sme as lvarchar(1000)) as nomecurto_sme,
					            cast(usi_nome.sme_id as lvarchar(500)) as id_sme,
					            cast(usi_nome.tpusina_id as lvarchar(100)) as id_tpusina
	                        from 
		                        (
			                        -- subsistema
			                        select 
				                        usi_subsistema.id_tpgeracao,
				                        usi_subsistema.cod_usiplanejamento,
				                        ag_t_groupconcatspace(TRIM(NVL(usi_subsistema.id_subsistema, ''))) as id_subsistema,
				                        count(usi_subsistema.id_subsistema) as qtd_subsistema
			                        from
				                        (
					                        select  distinct
						                        tb_tpgeracao.id_tpgeracao,
						                        uge.cod_usiplanejamento,
						                        ins.id_subsistema		
					                        from 
						                        uge,
						                        eqp,
						                        usi,
						                        ins,
			                        tpusina,
			                        tb_tpgeracao
					                        where 	
						                        uge.eqp_id = eqp.eqp_id
						                        and uge.usi_id = usi.usi_id
						                        and usi.ins_id = ins.ins_id
						                        and usi.tpusina_id = tpusina.tpusina_id
						                        and tpusina.id_tpgeracao = tb_tpgeracao.id_tpgeracao
						                        and usi.dtdesativa is null
						                        and uge.cod_usiplanejamento is not null 
						                        and uge.cod_usiplanejamento <> 0	
						                        and eqp.dtdesativa is null
						                        and usi.dtdesativa is null
				                        ) as usi_subsistema
			                        group by 
				                        id_tpgeracao, 
				                        cod_usiplanejamento
		                        ) usi_ss,
		                        (
			                        -- nome
			                        select 
				                        usiuge_distinct.id_tpgeracao,
				                        usiuge_distinct.cod_usiplanejamento,
				                        ag_t_groupconcatspace(SUBSTRING(usiuge_distinct.nomelongo FROM 0 FOR 4) || ' ' || usiuge_distinct.nomecurto) as nomelongo,
				                        ag_t_groupconcatspace(usiuge_distinct.nomecurto) as nomecurto,
							            ag_t_groupconcatspace(usiuge_distinct.usi_id) as usi_id,
							            ag_t_groupconcatspace(usiuge_distinct.nomecurto_sme) as nomecurto_sme,
							            ag_t_groupconcatspace(usiuge_distinct.sme_id) as sme_id,
							            ag_t_groupconcatspace(usiuge_distinct.tpusina_id) as tpusina_id
			                        from
				                        (
					                        select distinct 
						                        tb_tpgeracao.id_tpgeracao,
						                        uge.cod_usiplanejamento,
						                        usi.nomelongo,
						                        usi.nomecurto,
						                        ins.id_subsistema,
									            usi.usi_id,
									            sme.nomecurto as nomecurto_sme,
									            sme.sme_id,
									            usi.tpusina_id
					                        from 
						                        uge,
						                        eqp,
						                        usi,
						                        ins,
			                        			tpusina,
			                        			tb_tpgeracao,
									sme
					                        where 
						                        uge.eqp_id = eqp.eqp_id
						                        and uge.usi_id = usi.usi_id
						                        and usi.ins_id = ins.ins_id
									            and ins.sme_id = sme.sme_id
						                        and usi.tpusina_id = tpusina.tpusina_id
						                        and tpusina.id_tpgeracao = tb_tpgeracao.id_tpgeracao
						                        and usi.dtdesativa is null
						                        and uge.cod_usiplanejamento is not null 
						                        and uge.cod_usiplanejamento <> 0
						                        and eqp.dtdesativa is null
						                        and usi.dtdesativa is null
				                        ) as usiuge_distinct
			                        group by
				                        usiuge_distinct.id_tpgeracao,
				                        usiuge_distinct.cod_usiplanejamento
		                        ) usi_nome,
		                        (
			                        -- chave
			                        select 
				                        usiuge.cod_usiplanejamento,
				                        usiuge.id_tpgeracao,
				                        usiuge.cod_usiplanejamento || ',' || case  
							                        when id_tpgeracao = 1 then 'H'
							                        when id_tpgeracao = 2 then 'T'
							                        else null
						                        end || ',' || ag_t_groupconcatpipe(distinct usiuge.usi_id) as chave,
				                        cod_tpgeracao
			                        from
				                        (
					                        select 
						                        tb_tpgeracao.id_tpgeracao,
						                        uge.cod_usiplanejamento,
						                        usi.usi_id,
						                        usi.nomelongo,
						                        usi.nomecurto,
						                        tb_tpgeracao.cod_tpgeracao,
						                        ins.id_subsistema,
						                        usi.usi_id
					                        from 
						                        uge,
						                        eqp,
						                        usi,
						                        ins,
						                        tpusina,
						                        tb_tpgeracao
					                        where 
						                        uge.eqp_id = eqp.eqp_id
						                        and uge.usi_id = usi.usi_id
						                        and usi.ins_id = ins.ins_id
						                        and usi.tpusina_id = tpusina.tpusina_id
						                        and tpusina.id_tpgeracao = tb_tpgeracao.id_tpgeracao
						                        and usi.dtdesativa is null
						                        and uge.cod_usiplanejamento is not null 
						                        and uge.cod_usiplanejamento <> 0
						                        and eqp.dtdesativa is null
						                        and usi.dtdesativa is null
					                        order by 
						                        tb_tpgeracao.id_tpgeracao, uge.cod_usiplanejamento
				                        ) as usiuge
			                        group by 
				                        usiuge.cod_usiplanejamento, 
				                        usiuge.id_tpgeracao,
				                        cod_tpgeracao
		                        ) usi_chave
	                        where
		                        usi_ss.cod_usiplanejamento = usi_nome.cod_usiplanejamento
		                        and usi_ss.id_tpgeracao = usi_nome.id_tpgeracao
		                        and usi_nome.cod_usiplanejamento = usi_chave.cod_usiplanejamento
		                        and usi_nome.id_tpgeracao = usi_chave.id_tpgeracao
		                        and not exists
			                        (
				                        select 1 
				                        from 
					                        pus,
					                        tpusina,
					                        tb_tpgeracao
				                        where 		
					                        pus.tpusina_id = tpusina.tpusina_id
					                        and tpusina.id_tpgeracao = tb_tpgeracao.id_tpgeracao
					                        and pus.cod_usiplanejamento = usi_ss.cod_usiplanejamento 
					                        and usi_ss.id_tpgeracao = tb_tpgeracao.id_tpgeracao					
					                        and pus.dtdesativa is null
			                        )
	                        -- TÉRMINO USI

	                        union all

	                        -- INICIO PUS
	                        select  
		                        cast((pus.cod_usiplanejamento || ',' || case  
							                        when tb_tpgeracao.id_tpgeracao = 1 then 'H'
							                        when tb_tpgeracao.id_tpgeracao = 2 then 'T'
							                        else null
						                        end || ',' || NVL(pus.usi_id, ' ') || ',' || pus.pus_id) as lvarchar(2091)) as chave,
		                        cast(pus.cod_usiplanejamento as integer) as cod_usiplanejamento,
		                        cast(SUBSTRING(pus.nomelongo FROM 0 FOR 4) || ' ' || pus.nomecurto as lvarchar) as nomelongo,
		                        cast(pus.nomecurto as lvarchar) as nomecurto,
		                        cast(tb_tpgeracao.cod_tpgeracao as varchar(15)) as cod_tpgeracao, 
		                        cast((SUBSTRING(pus.nomelongo FROM 0 FOR 4) || ' ' || pus.nomecurto || ' (' || pus.cod_usiplanejamento || ')') as lvarchar) as nom_exibicao,	
		                        cast(trim(pus.id_subsistema) as lvarchar) as id_subsistema,
		                        cast(1 as int) as qtd_subsistema,
					            cast(trim(usi.usi_id) as lvarchar(100)) as id_usina,
					            cast(sme.nomecurto as lvarchar(1091)) as nomecurto_sme,
					            cast(sme.sme_id as lvarchar(500)) as id_sme,
					            cast(pus.tpusina_id as lvarchar(100)) as id_tpusina
	                        from 
		                        pus,
		                        tpusina,
		                        tb_tpgeracao,
					            usi,
					            ins,
					            sme
	                        where 		
		                        pus.tpusina_id = tpusina.tpusina_id
		                        and tpusina.id_tpgeracao = tb_tpgeracao.id_tpgeracao
		                        and pus.usi_id = usi.usi_id
		                        and ins.ins_id = usi.ins_id
					            and sme.sme_id = ins.sme_id
		                        and pus.dtdesativa is null
		                        and pus.cod_usiplanejamento is not null 
		                        and pus.cod_usiplanejamento <> 0		
		                        and pus.dtdesativa is null
	                        -- TERMINO PUS
	                        ) usipmo 
	                        inner join
		                        tb_subsistema tbSub
	                        on 
		                        tbSub.id_subsistema = usipmo.id_subsistema
	                        left join
		                        aprov a 
	                        on 
		                        usipmo.chave like '%' || a.usi_id || '%'
	                        left join
		                        res res
	                        on 
		                        a.res_id = res.res_id
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
		                        a.res_id = rev.res_id  and rev.din_fimvigencia is NULL
	                        left join
		                        tb_reservatorioee ree  
	                        on 
		                        rev.id_reservatorioee = ree.id_reservatorioee
                        where  1=1
                        {filtro_usina}
                        group by  1, 2, 3, 4, 5, 6, 7, 8, 9, 13, 14, 15, 16, 17

                        order by (nom_exibicao || ' (' || NVL(usipmo.id_subsistema, '') || ')')

        ";


        #endregion

        /// <summary>
        /// Consulta usinas do PMO
        /// Caso seja informado o parâmetro nomeExibicaoContem, será aplicado o filtro de LIKE (%nomeExibicaoContem%)
        /// </summary>
        /// <param name="nomeExibicaoContem">Nome de exibição para filtrar</param>
        /// <returns>Lista de Usinas do PMO</returns>
        public IList<UsinaPMO> ConsultarPorNomeExibicao(string nomeExibicaoContem)
        {
            StringBuilder sql = new StringBuilder();
            List<object> parametrosSql = new List<object>();

            sql.Append(sqlBase);

            if (!String.IsNullOrEmpty(nomeExibicaoContem))
            {
                sql.Replace("{filtro_usina}", " and UPPER((nom_exibicao || ' (' || NVL(usipmo.id_subsistema, '') || ')')) like ? ");
                parametrosSql.Add(string.Format("%{0}%", nomeExibicaoContem.ToUpper()));
            }
            else
            {
                sql.Replace("{filtro_usina}", "");
            }

            var retorno = EntitySet.SqlQuery(sql.ToString(), parametrosSql.ToArray()).ToList();

            return retorno;
        }

        /// <summary>
        /// Consulta usinas do PMO onde as chaves sejam iguais as passadas por parametro
        /// </summary>
        /// <param name="chaves">Nome de exibição para filtrar</param>
        /// <returns>Lista de Usinas do PMO</returns>
        public IList<UsinaPMO> ConsultarPorChaves(string[] chaves)
        {
            if (chaves == null || chaves.Length == 0)
            {
                throw new ArgumentNullException("O parâmetro chaves não pode ser nulo ou vazio.");
            }

            StringBuilder sql = new StringBuilder();

            sql.Append(sqlBase);

            //sql.Replace("{filtro_usina}", string.Format(" and (NVL(usipmo.id_subsistema, '') || ',' || chave) in ({0}) ", string.Join(",", chaves.Select(ch => "?"))));
            sql.Replace("{filtro_usina}", "");

            var retorno = EntitySet.SqlQuery(sql.ToString(), chaves).ToList();
            retorno = retorno.Where(usi => chaves.Contains(usi.Id)).ToList();

            return retorno;
        }

        /// <summary>
        /// Consulta usinas do PMO
        /// </summary>
        /// <returns>Lista de Usinas do PMO</returns>
        public IList<UsinaPMO> Consultar()
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(sqlBase);

            sql.Replace("{filtro_usina}", "");

            var retorno = EntitySet.SqlQuery(sql.ToString()).ToList();
            retorno = retorno.ToList();

            return retorno;
        }

        public UsinaPMO Consultar(int CodUsinaPlanejamento, string codTipoGeracao)
        {
            StringBuilder sql = new StringBuilder();
            StringBuilder filtro = new StringBuilder();
            List<object> parametrosSql = new List<object>();

            sql.Append(sqlBase);

            filtro.Append(" and cod_usiplanejamento = ? ");
            parametrosSql.Add(CodUsinaPlanejamento);

            filtro.Append(" and cod_tpgeracao = ? ");
            parametrosSql.Add(codTipoGeracao);

            sql.Replace("{filtro_usina}", filtro.ToString());

            var retorno = EntitySet.SqlQuery(sql.ToString(), parametrosSql.ToArray()).FirstOrDefault();

            return retorno;
        }
    }
}

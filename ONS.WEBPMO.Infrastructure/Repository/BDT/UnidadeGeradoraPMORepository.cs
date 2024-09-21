using System.Text;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Repositories.BDT
{
    [UseDbContext(ConnectionStringsNames.BDTModel)]
    public class UnidadeGeradoraPMORepository : Repository<UnidadeGeradoraPMO>, IUnidadeGeradoraPMORepository
    {
        #region " SQL "
        private static readonly string sqlBase = @"
                            select distinct
		                            ugepmo.Id,
		                            ugepmo.usi_id,
		                            ugepmo.IdDpp,
		                            ugepmo.Nome,
		                            ugepmo.NumeroMaquina,
		                            ugepmo.NumeroConjunto,
		                            ugepmo.PotenciaNominal,
		                            ugepmo.CodUsinaPlanejamento,
		                            ugepmo.UsiBDTId,
		                            ugepmo.TipoGeracao,
		                            gruge.numgruge,
		                            ugepmo.gruge_id,
		                            tbSub.cod_subsistemamodenerg as Cod_subsistemamodenerg,
		                            tb_potcalcindisp.val_potcalcindisp,
		                            tb_tppotcalcindisp.cod_tppotcalcindisp,
		                            tb_potcalcindisp.din_fim,
		                            ugepmo.age_id_oper
	                            from (
	                               select distinct
		                              uge.uge_id as Id,
		                              usi.usi_id,
		                              uge.id_dpp as IdDpp,
		                              trim(uge.nomelongo) || ' (' || trunc(uge.potefe, 3) || ')' as Nome,
		                              substr(UGE.id_dpp, length(to_char(UGE.id_dpp)) - 2, 3) as NumeroMaquina,
		                              case
		                              when substr(UGE.id_dpp, 0, length(to_char(UGE.id_dpp)) - 3)  = to_char(uge.cod_usiplanejamento) then '1'
		                              else substr(UGE.id_dpp, 0, length(to_char(UGE.id_dpp)) - 3)
		                              end as NumeroConjunto,
		                              uge.potefe as PotenciaNominal,
		                              uge.cod_usiplanejamento as CodUsinaPlanejamento,
		                              usi.usi_id as UsiBDTId,
		                              tb_tpgeracao.cod_tpgeracao as TipoGeracao,
		                              uge.gruge_id,
		                              uge.age_id_oper
	                               from 
		                              uge,
		                              eqp,
		                              usi,						                        
		                              tpusina,
		                              tb_tpgeracao
	                               where
		                              uge.eqp_id = eqp.eqp_id
		                              and uge.tpeqp_id = eqp.tpeqp_id
		                              and uge.usi_id = usi.usi_id						                                
		                              and usi.tpusina_id = tpusina.tpusina_id
		                              and tpusina.id_tpgeracao = tb_tpgeracao.id_tpgeracao
		                              and eqp.dtdesativa is null
	                               and not exists 
	                               (
		                              select 1
		                              from 
			                             pus,
			                             tpusina tpusipus,
			                             tb_tpgeracao tpgerpus
		                              where 		
			                             pus.tpusina_id = tpusipus.tpusina_id
			                             and tpusipus.id_tpgeracao = tpgerpus.id_tpgeracao									    
			                             and pus.cod_usiplanejamento = uge.cod_usiplanejamento
			                             and tpusipus.tpusina_id = tpusina.tpusina_id
			                             and tpgerpus.id_tpgeracao = tb_tpgeracao.id_tpgeracao
			                             and pus.dtdesativa is null
	                               ) 

	                               union all

	                               select distinct
		                              uge.uge_id as Id,
		                              pus.usi_id,
		                              uge.id_dpp as IdDpp,
		                              trim(uge.nomelongo) || ' (' || trunc(uge.potefe, 3) || ')' as Nome,
		                              substr(UGE.id_dpp, length(to_char(UGE.id_dpp)) - 2, 3) as NumeroMaquina,
		                              case
		                              when substr(UGE.id_dpp, 0, length(to_char(UGE.id_dpp)) - 3)  = to_char(uge.cod_usiplanejamento) then '1'
		                              else substr(UGE.id_dpp, 0, length(to_char(UGE.id_dpp)) - 3)
		                              end as NumeroConjunto,
		                              uge.potefe as PotenciaNominal,
		                              pus.cod_usiplanejamento as CodUsinaPlanejamento,
		                              pus.usi_id as UsiBDTId,
		                              tb_tpgeracao.cod_tpgeracao as TipoGeracao,
		                              uge.gruge_id,
		                              uge.age_id_oper
	                               from 
		                              uge,
		                              eqp,
		                              pus,						                        
		                              tpusina,
		                              tb_tpgeracao
	                               where
		                              uge.eqp_id = eqp.eqp_id
		                              and uge.tpeqp_id = eqp.tpeqp_id
		                              and uge.usi_id = pus.usi_id						                                
		                              and pus.tpusina_id = tpusina.tpusina_id
		                              and tpusina.id_tpgeracao = tb_tpgeracao.id_tpgeracao
		                              and eqp.dtdesativa is null
		                              and pus.dtdesativa is null
	                            ) as ugepmo
	                            left join 
		                            gruge
	                            on 
		                            ugepmo.gruge_id = gruge.gruge_id
	
	                            left join
		                            pus pusSub
	                            on 
		                            pusSub.usi_id = ugepmo.usi_id
	
	                            left join
		                            tb_subsistema tbSub
	                            on 
		                            pusSub.id_subsistema = tbSub.id_subsistema
		
	                            left join 
		                            tb_potcalcindisp
	                            on 
		                            tb_potcalcindisp.uge_id = ugepmo.Id

	                            left join
		                            tb_tppotcalcindisp
	                            on 
		                            tb_tppotcalcindisp.id_tppotcalcindisp = tb_potcalcindisp.id_tppotcalcindisp
			
	                            where 
		                            CodUsinaPlanejamento is not null
		                            and ( tb_potcalcindisp.id_potcalcindisp in (
			                                    SELECT id_potcalcindisp FROM(
			                                       SELECT FIRST 1 aux.id_potcalcindisp
			                                       FROM tb_potcalcindisp aux
			                                       WHERE aux.uge_id = ugepmo.Id
			                                       ORDER BY aux.din_inicio DESC
			                                    )
                                           )
		                             )

     ";

        #endregion

        private IUsinaPMORepository usinaPMORepository;

        public UnidadeGeradoraPMORepository(IUsinaPMORepository usinaPMORepository)
        {
            this.usinaPMORepository = usinaPMORepository;
        }

        /// <summary>
        /// Consulta unidades geradoras do PMO sem parametro
        /// </summary>
        /// <returns>Lista de Unidades Geradoras do PMO</returns>
        public IList<UnidadeGeradoraPMO> Consultar()
        {
            var todasUges = EntitySet.SqlQuery(sqlBase, new object[] { }).AsNoTracking().ToList();

            var usinas = usinaPMORepository.ConsultarPorNomeExibicao(string.Empty);

            foreach (var uge in todasUges)
            {
                var usinaPmo = usinas.FirstOrDefault(usi => usi.CodUsinaPlanejamento == uge.CodUsinaPlanejamento && usi.TipoGeracao == uge.TipoGeracao);

                if (usinaPmo != null)
                {
                    uge.UsinaPMO = usinaPmo;
                    uge.Id = string.Format("{0}|{1}", uge.Id, usinaPmo.Id);
                }
            }

            return todasUges;
        }

        /// <summary>
        /// Consulta unidades geradoras do PMO onde as chaves sejam iguais as passadas por parametro
        /// </summary>
        /// <param name="chaves">Nome de exibição para filtrar</param>
        /// <returns>Lista de Unidades Geradoras do PMO</returns>
        public IList<UnidadeGeradoraPMO> ConsultarPorChaves(string[] chaves)
        {
            if (chaves == null || chaves.Length == 0)
            {
                throw new ArgumentNullException("chaves");
            }

            var chavesUges = chaves.Select(ch => new { UgeId = ch.Split('|')[0].Trim(), DppId = ch.Split('|')[1].Trim().Split(',')[1] }).ToArray();

            var todasUges = EntitySet.SqlQuery(sqlBase, new object[] { }).AsNoTracking().ToList();

            //filtrar as que estão no parametro chaves
            var retorno = todasUges.Where(bdtUge => chavesUges.Count(chave => chave.UgeId.Trim() == bdtUge.Id.Trim() && chave.DppId.Trim() == bdtUge.CodUsinaPlanejamento.ToString().Trim()) > 0).ToList();

            var usinas = usinaPMORepository.ConsultarPorNomeExibicao(string.Empty);

            foreach (var uge in retorno)
            {
                var usinaPmo = usinas.FirstOrDefault(usi => usi.CodUsinaPlanejamento == uge.CodUsinaPlanejamento && usi.TipoGeracao == uge.TipoGeracao);

                if (usinaPmo != null)
                {
                    uge.UsinaPMO = usinaPmo;
                    uge.Id = string.Format("{0}|{1}", uge.Id, usinaPmo.Id);
                }
            }

            return retorno;
        }

        /// <summary>
        /// Consulta unidades geradoras do PMO de uma Usina especifica
        /// </summary>
        /// <param name="chaveUsina">Nome de exibição para filtrar</param>
        /// <returns>Lista de Unidades Geradoras do PMO</returns>
        public IList<UnidadeGeradoraPMO> ConsultarPorUsina(string chaveUsina)
        {
            if (string.IsNullOrEmpty(chaveUsina))
            {
                throw new ArgumentNullException("chaveUsina");
            }

            var retorno = new List<UnidadeGeradoraPMO>();

            //desmembrar a chave da usina
            var codDpp = chaveUsina.Split(',')[1];
            var usiIds = chaveUsina.Split(',')[3].Split('|');

            //Buscar Usina e atribuir ao retornoH:\Projetos\WEBPMO\DEV_CRQ15889\Src\sgipmo\ONS.SGIPMO.Domain.Entities\Entities\OrigemColeta\Usina.cs
            var usinaPmo = usinaPMORepository.ConsultarPorChaves(new string[] { chaveUsina }).FirstOrDefault();

            if (usinaPmo != null)
            {
                var sql = new StringBuilder();

                sql.Append(sqlBase);

                sql.AppendFormat("and CodUsinaPlanejamento = ? and UsiBDTId in ({0}) ", string.Join(",", usiIds.Select(ch => "?")));
                sql.AppendLine("order by Nome");

                object[] parametros = { codDpp };

                parametros = parametros.Union(usiIds.Select(u => u.Trim()).AsEnumerable()).ToArray();
                retorno = EntitySet.SqlQuery(sql.ToString(), parametros).AsNoTracking().ToList();

                foreach (var uge in retorno)
                {
                    uge.UsinaPMO = usinaPmo;
                    uge.Id = string.Format("{0}|{1}", uge.Id, usinaPmo.Id);
                }
            }

            return retorno;
        }
    }
}

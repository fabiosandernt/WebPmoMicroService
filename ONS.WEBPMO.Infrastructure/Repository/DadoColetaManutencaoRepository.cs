namespace ONS.WEBPMO.Domain.Repositories.Impl
{
    public class DadoColetaManutencaoRepository : Repository<DadoColetaManutencao>, IDadoColetaManutencaoRepository
    {
        public PagedResult<DadoColetaManutencaoDTO> ConsultarPorColetaInsumo(DadoColetaInsumoFilter filter)
        {
            var query = EntitySet
                .Include(dado => dado.Gabarito.OrigemColeta)
                //  .Include(dado => dado.Periodicidade)
                .Where(dado => dado.ColetaInsumo.Id == filter.IdColetaInsumo);

            int resutadosPorPagina = filter.PageSize;
            int skip = (filter.PageIndex - 1) * resutadosPorPagina;
            int quantidadeTotal = query.Count();

            IList<DadoColetaManutencao> dadoColetaList = query
                .OrderBy(coletaInsumo => coletaInsumo.Gabarito.OrigemColeta.Nome)
                .Skip(skip)
                .Take(resutadosPorPagina)
                .ToList();

            IList<DadoColetaManutencaoDTO> dtos = new List<DadoColetaManutencaoDTO>();
            foreach (var coletaManutencao in dadoColetaList)
            {
                DadoColetaManutencaoDTO dto = ConvertParaDto(coletaManutencao);
                dtos.Add(dto);
                /*
                // periodiciadade diaria
                if (coletaManutencao.Periodicidade == "D")
                {
                    int diffDias = (coletaManutencao.DataFim - coletaManutencao.DataInicio).Days;
                    if (diffDias > 0)
                    {
                        DadoColetaManutencao confOriginal = coletaManutencao;
                        for (int dia = 0; dia < diffDias; dia++)
                        {
                            DadoColetaManutencao confPart = coletaManutencao;
                            DateTime dtIni = coletaManutencao.DataInicio.AddDays ( dia );
                            DateTime dtFim = coletaManutencao.DataInicio.AddDays ( dia + 1);

                            confPart.DataFim = new DateTime(dtIni.Year
                                    , dtIni.Month
                                    , dtIni.Day
                                    , 23, 59, 59 );
                            DadoColetaManutencaoDTO dto = ConvertParaDto(confPart);
                            dtos.Add(dto);

                            confPart.DataInicio = new DateTime(dtFim.Year
                                    , dtFim.Month
                                    , dtFim.Day
                                    , 0, 0, 0);

                            confPart.DataFim = dtFim;
                            dto = ConvertParaDto(confPart);
                            dtos.Add(dto);


                        }
                    }
                }
                else
                {
                    DadoColetaManutencaoDTO dto = ConvertParaDto(coletaManutencao);
                    dtos.Add(dto);
                }
                */
            }

            return new PagedResult<DadoColetaManutencaoDTO>(dtos, quantidadeTotal, filter.PageIndex, resutadosPorPagina);
        }

        private DadoColetaManutencaoDTO ConvertParaDto(DadoColetaManutencao from)
        {
            DadoColetaManutencaoDTO dto = new DadoColetaManutencaoDTO();
            dto.IdDadoColeta = from.Id;
            dto.Justificativa = from.Justificativa;
            dto.NomeUnidade = from.Gabarito.OrigemColeta.Nome;
            dto.Numero = from.Numero;
            dto.TempoRetorno = from.TempoRetorno;
            dto.DataFim = from.DataFim;
            dto.DataInicio = from.DataInicio;
            dto.SituacaoColetaInsumoDescricao = from.Situacao;
            dto.ClassificacaoPorTipoEquipamento = from.ClassificacaoPorTipoEquipamento;
            dto.Periodicidade = from.Periodicidade;

            if (from.Gabarito.OrigemColeta is UnidadeGeradora)
            {
                dto.NomeUsina = ((UnidadeGeradora)from.Gabarito.OrigemColeta).Usina.Nome;
            }

            return dto;
        }


        /* comentado devido a replicacao de codigo da branche sprint18_Web-PMO_Bug-76601 */
        public DadoColetaManutencao FindByKey(int chave)
        {
            try
            {
                var query = EntitySet
                .Include(dado => dado.Gabarito.OrigemColeta)
                .Where(dado => dado.Id == chave);
                return query.FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /* comentado devido a replicacao de codigo da branche sprint18_Web-PMO_Bug-76601 */
        public DadoColetaManutencao FindByColetaInsumoId(int coletaInsumoId)
        {
            var query = EntitySet
                .Include(dado => dado.ColetaInsumo.Agente)
                .Include(dado => dado.ColetaInsumo.Situacao)
                .Include(dado => dado.ColetaInsumo.Insumo)
                .Where(dado => dado.ColetaInsumoId == coletaInsumoId);
            return query.FirstOrDefault();
        }


        public bool Any(int idColetaInsumo, string idOrigemColeta, DateTime dataInicio, DateTime dataFim, int? idDadoColetaDiferente = null)
        {
            var query = EntitySet
                .Include(dadoColeta => dadoColeta.Gabarito.OrigemColeta)
                .Include(dadoColeta => dadoColeta.ColetaInsumo)
                .Where(dadoColeta => dadoColeta.ColetaInsumo.Id == idColetaInsumo
                                     && EntityFunctions.TruncateTime(dadoColeta.DataInicio) == dataInicio.Date
                                     && EntityFunctions.TruncateTime(dadoColeta.DataFim) == dataFim.Date
                                     && dadoColeta.Gabarito.OrigemColeta.Id == idOrigemColeta);

            if (idDadoColetaDiferente.HasValue)
            {
                query = query.Where(dadoColeta => dadoColeta.Id != idDadoColetaDiferente);
            }

            return query.Any();
        }

        public bool AnyWithTime(int idColetaInsumo, string idOrigemColeta, DateTime dataInicio, DateTime dataFim, int? idDadoColetaDiferente = null)
        {
            var query = EntitySet
                .Include(dadoColeta => dadoColeta.Gabarito.OrigemColeta)
                .Include(dadoColeta => dadoColeta.ColetaInsumo)
                .Where(dadoColeta => dadoColeta.ColetaInsumo.Id == idColetaInsumo
                                     && dadoColeta.DataInicio == dataInicio
                                     && dadoColeta.DataFim == dataFim
                                     && dadoColeta.Gabarito.OrigemColeta.Id == idOrigemColeta);

            if (idDadoColetaDiferente.HasValue)
            {
                query = query.Where(dadoColeta => dadoColeta.Id != idDadoColetaDiferente);
            }

            return query.Any();
        }


        public void DeletarPorIdGabarito(IList<int> idsGabarito)
        {
            string sql = @"DELETE [dcm] from [dbo].[tb_dadocoletamanutencao] as [dcm]
                           inner join [dbo].[tb_dadocoleta] as [dc]
                            on [dc].[id_dadocoleta] = [dcm].[id_dadocoleta]
                            where [dc].[id_gabarito] in ({0})";

            Context.Database.ExecuteSqlCommand(string.Format(sql, String.Join(",", idsGabarito)));
        }

        public IList<DadoColetaManutencao> ConsultarDadosComInsumoParticipaBlocoMP(int idSemanaOperativa)
        {
            string tipoBloco = TipoBlocoEnum.MP.ToString();
            var query = from dado in EntitySet
                        join ci in Context.Set<ColetaInsumo>() on dado.ColetaInsumo.Id equals ci.Id
                        join ie in Context.Set<InsumoEstruturado>() on ci.Insumo.Id equals ie.Id
                        where
                            ci.SemanaOperativa.Id == idSemanaOperativa
                            && ie.TipoBloco == tipoBloco
                        select dado;

            return query
                .Include(dado => dado.ColetaInsumo.Insumo)
                .Include(dado => dado.ColetaInsumo.SemanaOperativa)
                .Include(dado => dado.ColetaInsumo.Agente)
                .Include(dado => dado.UnidadeGeradora.Usina)
                .ToList();
        }
    }
}

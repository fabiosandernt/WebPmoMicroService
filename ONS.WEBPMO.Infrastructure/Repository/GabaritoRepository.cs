namespace ONS.WEBPMO.Domain.Repositories.Impl
{
    public class GabaritoRepository : Repository<Gabarito>, IGabaritoRepository
    {
        public DbContext _Context2;

        public GabaritoRepository()
        {
            _Context2 = ApplicationContext.Resolve<DbContext>("SGIPMOModel");
        }

        private class GroupKeyGabaritoByOrigemColeta
        {
            public int IdAgente { get; set; }
            public string IdOrigemColeta { get; set; }
            public string NomeOrigemColeta { get; set; }
            public string NomeAgente { get; set; }
            public string CodigoPerfilONS { get; set; }
        }

        public IEnumerable<Gabarito> ConsultarParaRemover(IList<int> ids)
        {
            var query = EntitySet.Include(gabarito => gabarito.DadosColeta);
            return query.Where(g => ids.Contains(g.Id));
        }

        public PagedResult<GabaritoAgrupadoAgenteOrigemColetaDTO> ConsultarAgrupadoPorAgenteTipoOrigemPaginado(GabaritoOrigemColetaFilter filtro)
        {
            var query = EntitySet.Include(gabarito => gabarito.Agente)
                                 .Include(gabarito => gabarito.OrigemColeta)
                                 .Include(gabarito => gabarito.Insumo)
                                 .AsQueryable();

            query = query.Where(gabarito => gabarito.IsPadrao == filtro.IsPadrao);

            if (filtro.IdAgente.HasValue)
            {
                query = query.Where(e => e.Agente.Id == filtro.IdAgente);
            }

            if (filtro.IdInsumo.HasValue)
            {
                query = query.Where(e => e.Insumo.Id == filtro.IdInsumo);
            }
            if (filtro.IdSemanaOperativa.HasValue)
            {
                query = query.Where(e => e.SemanaOperativa.Id == filtro.IdSemanaOperativa);
            }

            IQueryable<IGrouping<GroupKeyGabaritoByOrigemColeta, Gabarito>> gabaritoGroups;

            if (filtro.TipoInsumo == TipoInsumoEnum.NaoEstruturado)
            {
                query = query.Where(e => e.Insumo is InsumoNaoEstruturado);

                gabaritoGroups = query.GroupBy(e => new GroupKeyGabaritoByOrigemColeta
                {
                    IdAgente = e.Agente.Id,
                    NomeAgente = e.Agente.Nome,
                    CodigoPerfilONS = e.CodigoPerfilONS
                });
            }
            else
            {
                query = query.Where(e => e.Insumo is InsumoEstruturado);
                if (filtro.TipoOrigemColeta == TipoOrigemColetaEnum.UnidadeGeradora)
                {
                    gabaritoGroups = from g in query
                                     join ug in Context.Set<UnidadeGeradora>() on g.OrigemColeta.Id equals ug.Id
                                     group g by new GroupKeyGabaritoByOrigemColeta
                                     {
                                         IdOrigemColeta = ug.Usina.Id,
                                         NomeOrigemColeta = ug.Usina.Nome,
                                         IdAgente = g.Agente.Id,
                                         NomeAgente = g.Agente.Nome,
                                         CodigoPerfilONS = g.CodigoPerfilONS
                                     }
                        into gp
                                     select gp;
                }
                else
                {
                    query = query.WhereIsOfType(filtro.TipoOrigemColeta.Value);
                    gabaritoGroups = query.GroupBy(e => new GroupKeyGabaritoByOrigemColeta
                    {
                        IdAgente = e.Agente.Id,
                        NomeAgente = e.Agente.Nome,
                        IdOrigemColeta = e.OrigemColeta.Id,
                        NomeOrigemColeta = e.OrigemColeta.Nome,
                        CodigoPerfilONS = e.CodigoPerfilONS
                    });
                }
            }

            int resutadosPorPagina = filtro.PageSize;

            int skip = (filtro.PageIndex - 1) * resutadosPorPagina;

            int quantidadeTotal = gabaritoGroups.Count();

            var gabaritoGroup = gabaritoGroups
               .OrderBy(e => e.Key.NomeAgente)
               .Skip(skip)
               .Take(resutadosPorPagina).ToList();


            IList<int> gabaritosIds = new List<int>();
            gabaritoGroup.ForEach(group => group.ToList().ForEach(gabarito => gabaritosIds.Add(gabarito.Id)));

            IList<Insumo> insumosTodos = (from insumo in Context.Set<Insumo>()
                                          from gabarito in insumo.Gabaritos
                                          where gabaritosIds.Contains(gabarito.Id)
                                          select insumo).ToList();

            IList<GabaritoAgrupadoAgenteOrigemColetaDTO> gabaritoOrigemColetaDtos = new List<GabaritoAgrupadoAgenteOrigemColetaDTO>();
            foreach (var group in gabaritoGroup)
            {
                GabaritoAgrupadoAgenteOrigemColetaDTO gabaritoAgrupadoAgenteOrigemColetaDto = new GabaritoAgrupadoAgenteOrigemColetaDTO();
                gabaritoAgrupadoAgenteOrigemColetaDto.Agente = new ChaveDescricaoDTO<int>(group.Key.IdAgente, group.Key.NomeAgente);
                gabaritoAgrupadoAgenteOrigemColetaDto.OrigemColeta = new ChaveDescricaoDTO<string>(group.Key.IdOrigemColeta, group.Key.NomeOrigemColeta);
                gabaritoAgrupadoAgenteOrigemColetaDto.CodigoPerfilONS = group.Key.CodigoPerfilONS;

                var insumos =
                    insumosTodos.Where(insumo => group.Any(gabarito => gabarito.Insumo.Id == insumo.Id))
                        .Distinct()
                        .OrderBy(insumo => insumo.OrdemExibicao)
                        .ThenBy(insumo => insumo.Nome)
                        .Select(insumo => new ChaveDescricaoDTO<int>(insumo.Id, insumo.Nome));

                gabaritoAgrupadoAgenteOrigemColetaDto.Insumos.AddRange(insumos);

                gabaritoOrigemColetaDtos.Add(gabaritoAgrupadoAgenteOrigemColetaDto);
            }



            return new PagedResult<GabaritoAgrupadoAgenteOrigemColetaDTO>(gabaritoOrigemColetaDtos, quantidadeTotal, filtro.PageIndex, resutadosPorPagina);
        }

        public IList<Gabarito> ConsultarPorGabaritoFilter(GabaritoConfiguracaoFilter filter)
        {
            return CriarQueryConsultaGabaritoFilter(filter).ToList();
        }

        public PagedResult<Gabarito> ConsultarPorGabaritoFilterPaginado(GabaritoConfiguracaoFilter filter)
        {
            return FindPaged(CriarQueryConsultaGabaritoFilter(filter), filter.PageIndex, filter.PageSize,
                gab => gab.OrderBy(g => g.OrigemColeta.Nome));
        }

        private IQueryable<Gabarito> CriarQueryConsultaGabaritoFilter(GabaritoConfiguracaoFilter filter)
        {
            var query = EntitySet.AsQueryable();

            query = query.Where(gabarito => gabarito.IsPadrao == filter.IsPadrao);

            if (filter.IdAgente.HasValue)
            {
                query = query.Where(gabarito => gabarito.Agente.Id == filter.IdAgente);
            }

            if (filter.IdSemanaOperativa.HasValue)
            {
                query = query.Where(e => e.SemanaOperativa.Id == filter.IdSemanaOperativa);
            }

            if (filter.IdsInsumo.Count > 0)
            {
                query = query.Where(e => filter.IdsInsumo.Any(insumo => insumo == e.Insumo.Id));
            }

            if (filter.IdInsumo.HasValue)
            {
                query = query.Where(e => e.Insumo.Id == filter.IdInsumo);
            }

            if (!string.IsNullOrWhiteSpace(filter.TipoInsumo))
            {
                query = query.Where(e => e.Insumo.TipoInsumo == filter.TipoInsumo);
            }

            if (filter.IdsOrigemColeta.Count > 0)
            {
                query = query.Where(
                    e => filter.IdsOrigemColeta.Any(idOrigemColeta => idOrigemColeta == e.OrigemColeta.Id));
            }
            else
            {
                if (filter.IsOrigemColetaNull)
                {
                    query = query.Where(e => e.OrigemColeta == null);
                }
            }

            if (!string.IsNullOrEmpty(filter.IdOrigemColetaPai))
            {
                query = from g in query
                        join ug in Context.Set<UnidadeGeradora>()
                            on g.OrigemColeta.Id equals ug.Id
                        where ug.Usina.Id == filter.IdOrigemColetaPai
                        select g;
            }

            if (filter.IsNullCodigoPerfilONS)
            {
                query = query.Where(e => string.IsNullOrEmpty(e.CodigoPerfilONS));
            }
            else if (!string.IsNullOrWhiteSpace(filter.CodigoPerfilONS))
            {
                query = query.Where(e => e.CodigoPerfilONS == filter.CodigoPerfilONS);
            }

            return query;
        }

        public Gabarito ObterPorColetaInumoOrigemColeta(int idColetaInsumo, string idOrigemColeta)
        {
            return Context.Set<ColetaInsumo>()
                .Where(coletaInsumo => coletaInsumo.Id == idColetaInsumo)
                .Join(EntitySet,
                    coletaInsumo => new { idInsumo = coletaInsumo.Insumo.Id, idAgente = coletaInsumo.Agente.Id, codigoPerfilONS = coletaInsumo.CodigoPerfilONS, idSemanaOperativa = coletaInsumo.SemanaOperativaId },
                    gabarito => new { idInsumo = gabarito.Insumo.Id, idAgente = gabarito.Agente.Id, codigoPerfilONS = gabarito.CodigoPerfilONS, idSemanaOperativa = gabarito.SemanaOperativaId ?? 0 },
                    (coletaInsumo, gabarito) => gabarito)
                .FirstOrDefault(gabarito => gabarito.OrigemColeta.Id == idOrigemColeta);
        }

        public void DeletarPorIdSemanaOperativa(int idSemanaOperativa)
        {
            var query = from g in EntitySet
                        join so in Context.Set<SemanaOperativa>() on g.SemanaOperativa.Id equals so.Id
                        where so.Id == idSemanaOperativa
                        select g;

            Delete(query);
        }

        public IList<Gabarito> ConsultarGabaritoParticipaBloco(int idSemanaOperativa)
        {
            var query = from g in EntitySet
                        join ie in Context.Set<InsumoEstruturado>() on g.InsumoId equals ie.Id
                        where g.SemanaOperativaId == idSemanaOperativa
                              && !string.IsNullOrEmpty(ie.TipoBloco)
                        select g;

            return query.ToList();
        }

        public IList<GabaritoParticipantesBaseDTO<ReservatorioParticipanteGabaritoDTO>> ConsultarReservatoriosParticipantesGabarito(bool isPadrao, string nomeRevisao = "")
        {
            return this.ConsultarOrigemColetaParticipantesPorGabarito<ReservatorioParticipanteGabaritoDTO, Reservatorio>(isPadrao, nomeRevisao);
        }

        public IList<GabaritoParticipantesBaseDTO<UsinaParticipanteGabaritoDTO>> ConsultarUsinasParticipantesGabarito(bool isPadrao, string nomeRevisao = "")
        {
            return this.ConsultarOrigemColetaParticipantesPorGabarito<UsinaParticipanteGabaritoDTO, Usina>(isPadrao, nomeRevisao);
        }

        public IList<GabaritoParticipantesBaseDTO<TParticipanteGabarito>> ConsultarOrigemColetaParticipantesPorGabarito<TParticipanteGabarito, TOrigemColeta>(bool isPadrao, string nomeRevisao = "")
            where TParticipanteGabarito : ParticipanteGabaritoDTOBase, new()
            where TOrigemColeta : OrigemColeta
        {
            if (!isPadrao && string.IsNullOrEmpty(nomeRevisao))
            {
                return null;
            }

            if (isPadrao)
            {
                List<GabaritoParticipantesBaseDTO<TParticipanteGabarito>> gabaritoPadraoList = new List<GabaritoParticipantesBaseDTO<TParticipanteGabarito>>();
                gabaritoPadraoList.Add(new GabaritoParticipantesBaseDTO<TParticipanteGabarito>()
                {
                    NomeRevisao = "Gabarito Padrão",
                    ParticipantesDTOList =
                                (from o in _Context2.Set<TOrigemColeta>()

                                 where o.Gabaritos.Any(g => g.IsPadrao == true)

                                 select new TParticipanteGabarito()
                                 {
                                     Nome = o.Nome
                                 }).Distinct()
                });

                return gabaritoPadraoList;
            }
            else
            {
                return (from gab in _Context2.Set<Gabarito>()
                        from sem in _Context2.Set<SemanaOperativa>().Where(s => gab.SemanaOperativaId == s.Id).DefaultIfEmpty()
                        where sem.Nome.Contains(nomeRevisao)
                        group sem.Nome by sem into groupGab
                        select new GabaritoParticipantesBaseDTO<TParticipanteGabarito>()
                        {
                            NomeRevisao = groupGab.Key.Nome,
                            ParticipantesDTOList =
                                (from o in _Context2.Set<TOrigemColeta>()

                                 where o.Gabaritos.Any(g => g.SemanaOperativa != null && g.SemanaOperativa.Nome.Contains(groupGab.Key.Nome) == true)
                                 where o.Gabaritos.Any(g => g.SemanaOperativa.Nome == groupGab.Key.Nome)

                                 select new TParticipanteGabarito()
                                 {
                                     Nome = o.Nome
                                 }).Distinct()

                        }).ToList();
            }
        }

        public IList<GabaritoParticipantesBaseDTO<AgenteParticipanteGabaritoDTO>> ConsultarAgentesParticipantesGabarito(bool isPadrao, string nomeRevisao = "")
        {
            if (!isPadrao && string.IsNullOrEmpty(nomeRevisao))
            {
                return null;
            }

            if (isPadrao)
            {
                List<GabaritoParticipantesBaseDTO<AgenteParticipanteGabaritoDTO>> gabaritoPadraoList = new List<GabaritoParticipantesBaseDTO<AgenteParticipanteGabaritoDTO>>();
                gabaritoPadraoList.Add(new GabaritoParticipantesBaseDTO<AgenteParticipanteGabaritoDTO>()
                {
                    NomeRevisao = "Gabarito Padrão",
                    ParticipantesDTOList =
                                (from o in _Context2.Set<Agente>()

                                 where o.Gabaritos.Any(g => g.IsPadrao == true)

                                 select new AgenteParticipanteGabaritoDTO()
                                 {
                                     Nome = o.Nome
                                 }).Distinct()
                });

                return gabaritoPadraoList;
            }
            else
            {
                return (from gab in _Context2.Set<Gabarito>()
                        from sem in _Context2.Set<SemanaOperativa>().Where(s => gab.SemanaOperativaId == s.Id).DefaultIfEmpty()
                        where sem.Nome.Contains(nomeRevisao)
                        group sem.Nome by sem into groupGab
                        select new GabaritoParticipantesBaseDTO<AgenteParticipanteGabaritoDTO>()
                        {
                            NomeRevisao = groupGab.Key.Nome,
                            ParticipantesDTOList =
                                (from a in _Context2.Set<Agente>()

                                 where a.Gabaritos.Any(g => g.SemanaOperativa.Nome == groupGab.Key.Nome)

                                 select new AgenteParticipanteGabaritoDTO()
                                 {
                                     Nome = a.Nome
                                 }).Distinct()

                        }).ToList();
            }
        }

        public IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoUsinaDTO>> ConsultarUsinaPorGabarito(bool isPadrao, string nomeRevisao = "")
        {
            if (!isPadrao && string.IsNullOrEmpty(nomeRevisao))
            {
                return null;
            }

            if (isPadrao)
            {
                IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoUsinaDTO>> gabaritoPadraoList = new List<GabaritoConfiguracaoBaseDTO<ConfiguracaoUsinaDTO>>();
                gabaritoPadraoList.Add(new GabaritoConfiguracaoBaseDTO<ConfiguracaoUsinaDTO>()
                {
                    NomeRevisao = "Gabarito Padrão",
                    ConfiguracaoDTOList =
                                (from g in _Context2.Set<Gabarito>()
                                 join a in _Context2.Set<Agente>() on g.AgenteId equals a.Id
                                 join u in _Context2.Set<Usina>() on g.OrigemColetaId equals u.Id

                                 where g.IsPadrao == true

                                 select new ConfiguracaoUsinaDTO()
                                 {
                                     AgenteRazaoSocial = a.NomeLongo,
                                     Nome = u.NomeLongo,
                                     CodDPP = SqlFunctions.StringConvert((double)u.CodigoDPP),
                                     TipoGeracao = u.TipoUsina
                                 }).Distinct()
                });

                return gabaritoPadraoList;
            }
            else
            {
                return (from gab in _Context2.Set<Gabarito>()
                        from sem in _Context2.Set<SemanaOperativa>().Where(s => gab.SemanaOperativaId == s.Id).DefaultIfEmpty()
                        where sem.Nome.Contains(nomeRevisao)
                        group sem.Nome by sem into groupGab
                        select new GabaritoConfiguracaoBaseDTO<ConfiguracaoUsinaDTO>()
                        {
                            NomeRevisao = groupGab.Key.Nome,
                            ConfiguracaoDTOList =
                                (from g in groupGab.Key.Gabaritos
                                 join a in _Context2.Set<Agente>() on g.AgenteId equals a.Id
                                 from s in _Context2.Set<SemanaOperativa>().Where(s => g.SemanaOperativaId == s.Id).DefaultIfEmpty()
                                 join u in _Context2.Set<Usina>() on g.OrigemColetaId equals u.Id

                                 where s.Nome == groupGab.Key.Nome

                                 select new ConfiguracaoUsinaDTO()
                                 {
                                     AgenteRazaoSocial = a.NomeLongo,
                                     Nome = u.NomeLongo,
                                     CodDPP = SqlFunctions.StringConvert((double)u.CodigoDPP),
                                     TipoGeracao = u.TipoUsina
                                 }).Distinct()

                        }).ToList();
            }
        }

        public IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoUGEDTO>> ConsultarUGEPorGabarito(bool isPadrao, string nomeRevisao = "")
        {
            if (!isPadrao && string.IsNullOrEmpty(nomeRevisao))
            {
                return null;
            }

            if (isPadrao)
            {
                IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoUGEDTO>> gabaritoPadraoList = new List<GabaritoConfiguracaoBaseDTO<ConfiguracaoUGEDTO>>();
                gabaritoPadraoList.Add(new GabaritoConfiguracaoBaseDTO<ConfiguracaoUGEDTO>()
                {
                    NomeRevisao = "Gabarito Padrão",
                    ConfiguracaoDTOList =
                                (from g in _Context2.Set<Gabarito>()
                                 join a in _Context2.Set<Agente>() on g.AgenteId equals a.Id
                                 join u in _Context2.Set<UnidadeGeradora>() on g.OrigemColetaId equals u.Id

                                 where g.IsPadrao == true

                                 select new ConfiguracaoUGEDTO()
                                 {
                                     AgenteRazaoSocial = a.NomeLongo,
                                     Nome = u.Nome,
                                     CodDPP = SqlFunctions.StringConvert((double)u.CodigoDPP),
                                     TipoGeracao = u.Usina.TipoUsina
                                 }).Distinct()
                });

                return gabaritoPadraoList;
            }
            else
            {
                return (from gab in _Context2.Set<Gabarito>()
                        from sem in _Context2.Set<SemanaOperativa>().Where(s => gab.SemanaOperativaId == s.Id).DefaultIfEmpty()
                        where sem.Nome.Contains(nomeRevisao)
                        group sem.Nome by sem into groupGab
                        select new GabaritoConfiguracaoBaseDTO<ConfiguracaoUGEDTO>()
                        {
                            NomeRevisao = groupGab.Key.Nome,
                            ConfiguracaoDTOList =
                                (from g in groupGab.Key.Gabaritos
                                 join a in _Context2.Set<Agente>() on g.AgenteId equals a.Id
                                 from s in _Context2.Set<SemanaOperativa>().Where(s => g.SemanaOperativaId == s.Id).DefaultIfEmpty()
                                 join u in _Context2.Set<UnidadeGeradora>() on g.OrigemColetaId equals u.Id

                                 where s.Nome == groupGab.Key.Nome

                                 select new ConfiguracaoUGEDTO()
                                 {
                                     AgenteRazaoSocial = a.NomeLongo,
                                     Nome = u.Nome,
                                     CodDPP = SqlFunctions.StringConvert((double)u.CodigoDPP),
                                     TipoGeracao = u.Usina.TipoUsina
                                 }).Distinct()

                        }).ToList();
            }
        }

        public IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoReservatorioDTO>> ConsultarReservatorioPorGabarito(bool isPadrao, string nomeRevisao = "")
        {
            return this.ConsultarGenericamentePorGabarito<GabaritoConfiguracaoBaseDTO<ConfiguracaoReservatorioDTO>, ConfiguracaoReservatorioDTO, Reservatorio>(isPadrao, nomeRevisao);
        }

        public IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoSubsistemaDTO>> ConsultarSubsistemaPorGabarito(bool isPadrao, string nomeRevisao = "")
        {
            return this.ConsultarGenericamentePorGabarito<GabaritoConfiguracaoBaseDTO<ConfiguracaoSubsistemaDTO>, ConfiguracaoSubsistemaDTO, Subsistema>(isPadrao, nomeRevisao);
        }

        public IList<TGabaritoConfiguracao> ConsultarGenericamentePorGabarito<TGabaritoConfiguracao, TOrigemColetaConfiguracao, TOrigemColeta>(bool isPadrao, string nomeRevisao = "")
            where TGabaritoConfiguracao : GabaritoConfiguracaoBaseDTO<TOrigemColetaConfiguracao>, new()
            where TOrigemColetaConfiguracao : ConfiguracaoDTOBase, new()
            where TOrigemColeta : OrigemColeta
        {
            if (!isPadrao && string.IsNullOrEmpty(nomeRevisao))
            {
                return null;
            }

            if (isPadrao)
            {
                IList<TGabaritoConfiguracao> gabaritoPadraoList = new List<TGabaritoConfiguracao>();
                gabaritoPadraoList.Add(new TGabaritoConfiguracao()
                {
                    NomeRevisao = "Gabarito Padrão",
                    ConfiguracaoDTOList =
                                (from g in _Context2.Set<Gabarito>()
                                 join a in _Context2.Set<Agente>() on g.AgenteId equals a.Id
                                 join u in _Context2.Set<TOrigemColeta>() on g.OrigemColetaId equals u.Id

                                 where g.IsPadrao == true

                                 select new TOrigemColetaConfiguracao()
                                 {
                                     AgenteRazaoSocial = a.NomeLongo,
                                     Nome = u.Nome
                                 }).Distinct()
                });

                return gabaritoPadraoList;
            }
            else
            {
                return (from gab in _Context2.Set<Gabarito>()
                        from sem in _Context2.Set<SemanaOperativa>().Where(s => gab.SemanaOperativaId == s.Id).DefaultIfEmpty()
                        where sem.Nome.Contains(nomeRevisao)
                        group sem.Nome by sem into groupGab
                        select new TGabaritoConfiguracao()
                        {
                            NomeRevisao = groupGab.Key.Nome,
                            ConfiguracaoDTOList =
                                (from g in groupGab.Key.Gabaritos
                                 join a in _Context2.Set<Agente>() on g.AgenteId equals a.Id
                                 from s in _Context2.Set<SemanaOperativa>().Where(s => g.SemanaOperativaId == s.Id).DefaultIfEmpty()
                                 join u in _Context2.Set<TOrigemColeta>() on g.OrigemColetaId equals u.Id

                                 where s.Nome == groupGab.Key.Nome

                                 select new TOrigemColetaConfiguracao()
                                 {
                                     AgenteRazaoSocial = a.NomeLongo,
                                     Nome = u.Nome
                                 }).Distinct()

                        }).ToList();
            }
        }

        public IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoInsumoNaoEstruturadoDTO>> ConsultarAgentesPodemEnviarDadosNaoEstruturadosPorGabarito(bool isPadrao, string nomeRevisao = "")
        {
            if (!isPadrao && string.IsNullOrEmpty(nomeRevisao))
            {
                return null;
            }

            if (isPadrao)
            {
                List<GabaritoConfiguracaoBaseDTO<ConfiguracaoInsumoNaoEstruturadoDTO>> gabaritoPadraoList = new List<GabaritoConfiguracaoBaseDTO<ConfiguracaoInsumoNaoEstruturadoDTO>>();
                gabaritoPadraoList.Add(new GabaritoConfiguracaoBaseDTO<ConfiguracaoInsumoNaoEstruturadoDTO>()
                {
                    NomeRevisao = "Gabarito Padrão",
                    ConfiguracaoDTOList =
                                (from g in _Context2.Set<Gabarito>()
                                 join i in _Context2.Set<InsumoNaoEstruturado>() on g.InsumoId equals i.Id
                                 join a in _Context2.Set<Agente>() on g.AgenteId equals a.Id

                                 where g.IsPadrao == true

                                 select new ConfiguracaoInsumoNaoEstruturadoDTO()
                                 {
                                     AgenteRazaoSocial = a.Nome
                                 }).Distinct()
                });

                return gabaritoPadraoList;
            }
            else
            {
                return (from gab in _Context2.Set<Gabarito>()
                        from sem in _Context2.Set<SemanaOperativa>().Where(s => gab.SemanaOperativaId == s.Id).DefaultIfEmpty()
                        where sem.Nome.Contains(nomeRevisao)
                        group sem.Nome by sem into groupGab
                        select new GabaritoConfiguracaoBaseDTO<ConfiguracaoInsumoNaoEstruturadoDTO>()
                        {
                            NomeRevisao = groupGab.Key.Nome,
                            ConfiguracaoDTOList =
                                (from g in _Context2.Set<Gabarito>()
                                 join s in _Context2.Set<SemanaOperativa>() on g.SemanaOperativaId equals s.Id
                                 join i in _Context2.Set<InsumoNaoEstruturado>() on g.InsumoId equals i.Id
                                 join a in _Context2.Set<Agente>() on g.AgenteId equals a.Id

                                 where s.Nome == groupGab.Key.Nome

                                 select new ConfiguracaoInsumoNaoEstruturadoDTO()
                                 {
                                     Nome = a.Nome
                                 }).Distinct()

                        }).ToList();
            }
        }

        public IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoGeracaoComplementar>> ConsultarAgentesComGeracaoComplementar(bool isPadrao, string nomeRevisao = "")
        {
            if (!isPadrao && string.IsNullOrEmpty(nomeRevisao))
            {
                return null;
            }

            if (isPadrao)
            {
                List<GabaritoConfiguracaoBaseDTO<ConfiguracaoGeracaoComplementar>> gabaritoPadraoList = new List<GabaritoConfiguracaoBaseDTO<ConfiguracaoGeracaoComplementar>>();
                gabaritoPadraoList.Add(new GabaritoConfiguracaoBaseDTO<ConfiguracaoGeracaoComplementar>()
                {
                    NomeRevisao = "Gabarito Padrão",
                    ConfiguracaoDTOList =
                                (from g in _Context2.Set<Gabarito>()
                                 join a in _Context2.Set<Agente>() on g.AgenteId equals a.Id

                                 where g.IsPadrao == true && g.OrigemColetaId == null

                                 select new ConfiguracaoGeracaoComplementar()
                                 {
                                     AgenteRazaoSocial = a.Nome
                                 }).Distinct()
                });

                return gabaritoPadraoList;
            }
            else
            {
                return (from gab in _Context2.Set<Gabarito>()
                        from sem in _Context2.Set<SemanaOperativa>().Where(s => gab.SemanaOperativaId == s.Id).DefaultIfEmpty()
                        where gab.OrigemColetaId == null && sem.Nome.Contains(nomeRevisao)
                        group sem.Nome by sem into groupGab
                        select new GabaritoConfiguracaoBaseDTO<ConfiguracaoGeracaoComplementar>()
                        {
                            NomeRevisao = groupGab.Key.Nome,
                            ConfiguracaoDTOList =
                                (from g in _Context2.Set<Gabarito>()
                                 join s in _Context2.Set<SemanaOperativa>() on g.SemanaOperativaId equals s.Id
                                 join a in _Context2.Set<Agente>() on g.AgenteId equals a.Id

                                 where s.Nome == groupGab.Key.Nome

                                 select new ConfiguracaoGeracaoComplementar()
                                 {
                                     Nome = a.Nome
                                 }).Distinct()

                        }).ToList();
            }
        }


    }
}

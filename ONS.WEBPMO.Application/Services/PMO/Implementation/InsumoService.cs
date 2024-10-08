using AutoMapper;
using ONS.Common.Exceptions;
using ONS.Common.Util.Pagination;
using ONS.WEBPMO.Application.DTO;
using ONS.WEBPMO.Application.Models.Insumo;
using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using ONS.WEBPMO.Application.Services.PMO.Interfaces.OrigemColeta;
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO;
using ONS.WEBPMO.Domain.Entities.Resources;
using ONS.WEBPMO.Domain.Enumerations;
using ONS.WEBPMO.Domain.Repository;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{
    public class InsumoService : IInsumoService
    {
        private readonly IInsumoRepository insumoRepository;
        private readonly IOrigemColetaService origemColetaService;
        private readonly ICategoriaInsumoRepository categoriaInsumoRepository;
        private readonly ITipoColetaRepository tipoColetaRepository;
        private readonly IGrandezaRepository grandezaRepository;
        private readonly IParametroRepository parametroRepository;
        private readonly ITipoDadoGrandezaRepository tipoDadoGrandezaRepository;
        private readonly IMapper _mapper;

        public InsumoService(
            IInsumoRepository insumoRepository,
            IOrigemColetaService origemColetaService,
            ICategoriaInsumoRepository categoriaInsumoRepository,
            ITipoColetaRepository tipoColetaRepository,
            IGrandezaRepository grandezaRepository,
            IParametroRepository parametroRepository,
            ITipoDadoGrandezaRepository tipoDadoGrandezaRepository,
            IMapper mapper)
        {
            this.insumoRepository = insumoRepository;
            this.origemColetaService = origemColetaService;
            this.categoriaInsumoRepository = categoriaInsumoRepository;
            this.tipoColetaRepository = tipoColetaRepository;
            this.grandezaRepository = grandezaRepository;
            this.parametroRepository = parametroRepository;
            this.tipoDadoGrandezaRepository = tipoDadoGrandezaRepository;
            _mapper = mapper;
        }

        #region Consultas
        public async Task<IList<VisualizarInsumoModel>> ConsultarTodosInsumos()
        {
            var query = await insumoRepository.GetAllAsync();

            var insumosDto = new List<VisualizarInsumoModel>();

            foreach (var insumo in query)
            {
                var insumoDto = new VisualizarInsumoModel
                {
                    Id = insumo.Id,
                    Nome = insumo.Nome,
                    OrdemExibicao = insumo.OrdemExibicao,
                    PreAprovado = insumo.PreAprovado.ToString(),
                    Reservado = insumo.Reservado.ToString(),
                    TipoInsumo = insumo.TipoInsumo,                   
                    SiglaInsumo = insumo.SiglaInsumo,
                    ExportarInsumo = insumo.ExportarInsumo.ToString(),
                    Ativo = insumo.Ativo.ToString()                    
    };

                insumosDto.Add(insumoDto);
            }

            return insumosDto;
        }
        public IList<Insumo> ConsultarInsumosNaoEstruturadoEEstruturadoComGrandezaAtiva()
        {
            return insumoRepository.ConsultarInsumosNaoEstruturadoEEstruturadoComGrandezaAtiva();
        }

        public IList<InsumoEstruturado> ConsultarInsumoPorTipoOrigemColetaCategoria(
            TipoOrigemColetaEnum tipoOrigemColeta, CategoriaInsumoEnum? categoria = null)
        {
            TipoColetaEnum tipoColeta = new TipoColetaEnum();

            switch (tipoOrigemColeta)
            {
                case TipoOrigemColetaEnum.Subsistema:
                    tipoColeta = TipoColetaEnum.Subsistema;
                    break;
                case TipoOrigemColetaEnum.UnidadeGeradora:
                    tipoColeta = TipoColetaEnum.UnidadeGeradora;
                    break;
                case TipoOrigemColetaEnum.Reservatorio:
                case TipoOrigemColetaEnum.Usina:
                    tipoColeta = TipoColetaEnum.UsinaReservatorio;
                    break;
                case TipoOrigemColetaEnum.GeracaoComplementar:
                    tipoColeta = TipoColetaEnum.GeracaoComplementar;
                    break;
            }

            return insumoRepository.ConsultarInsumoEstruturadoComGrandezaAtiva(tipoColeta, categoria);
        }

        public IList<InsumoEstruturado> ConsultarInsumoEstruturadosPorUsina(string idUsina)
        {
            Usina usina = origemColetaService.ObterOrigemColetaPorChaveOnline<Usina>(idUsina);

            CategoriaInsumoEnum categoria = usina.TipoUsina.Equals(TipoUsinaEnum.Hidraulica.ToDescription())
                ? CategoriaInsumoEnum.Hidreletrico
                : CategoriaInsumoEnum.Termico;

            return ConsultarInsumoPorTipoOrigemColetaCategoria(TipoOrigemColetaEnum.Usina, categoria);
        }

        public IList<Insumo> ConsultarInsumosPorNome(string nomeInsumo)
        {
            return insumoRepository.ConsultarPorNomeLike(nomeInsumo);
        }

        public IList<InsumoNaoEstruturado> ConsultarInsumoNaoEstruturado()
        {
            return insumoRepository.ConsultarInsumoNaoEstruturado();
        }

        

        public PagedResult<Insumo> ConsultarInsumosPorFiltro(InsumoFiltro filtro)
        {
            return insumoRepository.ConsultarPorInsumoFiltroPaginado(filtro);
        }

        public InsumoNaoEstruturado ObterInsumoNaoEstruturadoPorChave(int id)
        {
            return (InsumoNaoEstruturado)insumoRepository.FindByKey(id);
        }

        public InsumoEstruturado ObterInsumoEstruturadoPorChave(int id)
        {
            return (InsumoEstruturado)insumoRepository.FindByKey(id);
        }

        public Insumo ConsultarInsumo(int id)
        {
            return insumoRepository.GetAll().Where(insumo => insumo.Id == id).First();
        }

        #endregion

        #region Incluir Insumo

        public int InserirInsumoNaoEstruturado(InsumoNaoEstruturado insumo)
        {
            IList<string> mensagens = new List<string>();
            ValidarInclusaoInsumo(insumo.Nome, mensagens);
            ValidarInclusaoSiglaInsumo(insumo.SiglaInsumo, mensagens);
            VerificarONSBusinessException(mensagens);

            insumoRepository.Add(insumo);
            return insumo.Id;
        }

        public int InserirInsumoEstruturado(DadosInclusaoInsumoEstruturadoDTO dadosInsumoEstruturado)
        {
            IList<Grandeza> grandezas = new List<Grandeza>();

            IList<TipoDadoGrandeza> tiposDados = ObterTiposDadoGrandeza();

            foreach (ManutencaoGrandezaDTO grandezaDto in dadosInsumoEstruturado.Grandezas)
            {
                Grandeza grandeza = _mapper.Map<Grandeza>(grandezaDto);
                grandeza.TipoDadoGrandeza = tiposDados.Where(tipo => tipo.Id == grandezaDto.TipoDadoGrandezaId).First();
                grandezas.Add(grandeza);
            }

            IList<string> mensagens = new List<string>();
            ValidarInclusaoInsumo(dadosInsumoEstruturado.Nome, mensagens);
            ValidarInclusaoSiglaInsumo(dadosInsumoEstruturado.SiglaInsumo, mensagens);
            ValidarIncluirAlterarGrandezas(grandezas, mensagens);
            VerificarONSBusinessException(mensagens);

            InsumoEstruturado insumo = new InsumoEstruturado
            {
                CategoriaInsumo = categoriaInsumoRepository.FindByKey(dadosInsumoEstruturado.CategoriaId),
                Nome = dadosInsumoEstruturado.Nome,
                OrdemExibicao = dadosInsumoEstruturado.OrdemExibicao.Value,
                PreAprovado = dadosInsumoEstruturado.IsPreAprovado,
                Reservado = false,
                TipoColeta = tipoColetaRepository.FindByKey(dadosInsumoEstruturado.TipoColetaId),
                ExportarInsumo = dadosInsumoEstruturado.ExportarInsumo,
                SiglaInsumo = dadosInsumoEstruturado.SiglaInsumo
            };

            foreach (Grandeza grandeza in grandezas)
            {
                insumo.Grandezas.Add(grandeza);
            }

            insumoRepository.Add(insumo);

            return insumo.Id;
        }

        #endregion

        #region Alterar Insumo

        public void AlterarInsumoNaoEstruturado(InsumoNaoEstruturado insumo, byte[] versao)
        {
            IList<string> mensagens = new List<string>();
            ValidarAlteracaoInsumo(insumo.Id, insumo.Nome, mensagens);
            ValidarAlteracaoInsumoSigla(insumo.Id, insumo.SiglaInsumo, mensagens);
            VerificarONSBusinessException(mensagens);

            InsumoNaoEstruturado insumoNaoEstruturado = (InsumoNaoEstruturado)insumoRepository.FindByKeyConcurrencyValidate(insumo.Id, versao, true);
            if (insumoNaoEstruturado != null)
            {
                insumoNaoEstruturado.IsUtilizadoConvergencia = insumo.IsUtilizadoConvergencia;
                insumoNaoEstruturado.IsUtilizadoDECOMP = insumo.IsUtilizadoDECOMP;
                insumoNaoEstruturado.IsUtilizadoProcessamento = insumo.IsUtilizadoProcessamento;
                insumoNaoEstruturado.IsUtilizadoPublicacao = insumo.IsUtilizadoPublicacao;
                insumoNaoEstruturado.Nome = insumo.Nome;
                insumoNaoEstruturado.OrdemExibicao = insumo.OrdemExibicao;
                insumoNaoEstruturado.PreAprovado = insumo.PreAprovado;
                insumoNaoEstruturado.ExportarInsumo = insumo.ExportarInsumo;
                insumoNaoEstruturado.SiglaInsumo = insumo.SiglaInsumo;
                insumoNaoEstruturado.Ativo = insumo.Ativo;
            }
        }

        public void AlterarInsumoEstruturado(DadosInclusaoInsumoEstruturadoDTO dadosInsumoEstruturado)
        {
            byte[] versaoInsumo = Convert.FromBase64String(dadosInsumoEstruturado.VersaoStringInsumo);
            var insumo = (InsumoEstruturado)insumoRepository.FindByKeyConcurrencyValidate(
                dadosInsumoEstruturado.Id, versaoInsumo);

            IList<Grandeza> grandezas = new List<Grandeza>();

            IList<TipoDadoGrandeza> tiposDados = ObterTiposDadoGrandeza();

            foreach (ManutencaoGrandezaDTO grandezaDto in dadosInsumoEstruturado.Grandezas)
            {
                Grandeza grandeza = _mapper.Map<Grandeza>(grandezaDto);
                grandeza.TipoDadoGrandeza = tiposDados.First(tipo => tipo.Id == grandezaDto.TipoDadoGrandezaId);
                grandezas.Add(grandeza);
            }

            IList<string> mensagens = new List<string>();
            ValidarAlteracaoInsumo(dadosInsumoEstruturado.Id, dadosInsumoEstruturado.Nome, mensagens);
            ValidarAlteracaoInsumoSigla(dadosInsumoEstruturado.Id, dadosInsumoEstruturado.SiglaInsumo, mensagens);
            ValidarInsumoAssociadoGabaritoSemGrandezaAtiva(insumo, grandezas, mensagens);
            ValidarIncluirAlterarGrandezas(grandezas, mensagens);

            VerificarONSBusinessException(mensagens);

            insumo.CategoriaInsumo = categoriaInsumoRepository.FindByKey(dadosInsumoEstruturado.CategoriaId);
            insumo.Nome = dadosInsumoEstruturado.Nome;
            insumo.OrdemExibicao = dadosInsumoEstruturado.OrdemExibicao.Value;
            insumo.PreAprovado = dadosInsumoEstruturado.IsPreAprovado;
            insumo.Reservado = false;
            insumo.TipoColeta = tipoColetaRepository.FindByKey(dadosInsumoEstruturado.TipoColetaId);
            insumo.DataUltimaAtualizacao = DateTime.Now;
            insumo.ExportarInsumo = dadosInsumoEstruturado.ExportarInsumo;
            insumo.SiglaInsumo = dadosInsumoEstruturado.SiglaInsumo;
            insumo.Ativo = dadosInsumoEstruturado.Ativo;

            var idsGrandezas = grandezas.Where(g => g.Id > 0).Select(g => g.Id);
            var grandezasExcluir = insumo.Grandezas.Where(g => !idsGrandezas.Contains(g.Id)).ToList();

            grandezaRepository.Delete(grandezasExcluir);

            foreach (Grandeza grandeza in grandezas)
            {
                if (grandeza.Id > 0)
                {
                    Grandeza grandezaExistente = grandezaRepository.FindByKey(grandeza.Id);
                    if (grandezaExistente != null)
                    {
                        grandezaExistente.IsColetaPorEstagio = grandeza.IsColetaPorEstagio;
                        grandezaExistente.IsPreAprovadoComAlteracao = grandeza.IsPreAprovadoComAlteracao;
                        grandezaExistente.IsColetaPorLimite = grandeza.IsColetaPorLimite;
                        grandezaExistente.IsColetaPorPatamar = grandeza.IsColetaPorPatamar;
                        grandezaExistente.Nome = grandeza.Nome;
                        grandezaExistente.OrdemExibicao = grandeza.OrdemExibicao;
                        grandezaExistente.PodeRecuperarValor = grandeza.PodeRecuperarValor;
                        grandezaExistente.DestacaDiferenca = grandeza.DestacaDiferenca;
                        grandezaExistente.IsObrigatorio = grandeza.IsObrigatorio;
                        grandezaExistente.AceitaValorNegativo = grandeza.AceitaValorNegativo;
                        grandezaExistente.QuantidadeCasasDecimais = grandeza.QuantidadeCasasDecimais;
                        grandezaExistente.QuantidadeCasasInteira = grandeza.QuantidadeCasasInteira;
                        grandezaExistente.TipoDadoGrandeza = grandeza.TipoDadoGrandeza;
                        grandezaExistente.Ativo = grandeza.Ativo;
                    }
                }
                else
                {
                    insumo.Grandezas.Add(grandeza);
                }
            }
        }

        public bool InsumoBloqueadosAlteracao(int id)
        {
            InsumoEstruturado insumo = ObterInsumoEstruturadoPorChave(id);

            if (insumo.Gabaritos.Any())
            {
                return true;
            }
            return false;
        }

        #endregion

        #region Excluir Insumo

        public void ExcluirInsumoPorChave(int idInsumo, byte[] versao)
        {
            Insumo insumo = insumoRepository.FindByKeyConcurrencyValidate(idInsumo, versao, true);
            if (insumo != null)
            {
                IList<Grandeza> grandezas = grandezaRepository.ConsultarPorInsumo(idInsumo);

                IList<string> mensagens = new List<string>();
                ValidarAlterarExcluirInsumoReservado(insumo, mensagens);
                ValidarExclusaoInsumo(insumo, mensagens);

                foreach (Grandeza grandeza in grandezas)
                {
                    ValidarExclusaoGrandeza(grandeza, mensagens);
                }

                VerificarONSBusinessException(mensagens);

                foreach (Grandeza grandeza in grandezas)
                {
                    grandezaRepository.Delete(grandeza);
                }

                insumoRepository.Delete(insumo);
            }
        }

        #endregion

        #region Grandeza

        public IList<Grandeza> ObterGrandezasPorInsumo(int idInsumo)
        {
            GrandezaFilter filtro = new GrandezaFilter
            {
                IdInsumo = idInsumo,
                IsOrdenacaoPadrao = true
            };
            return grandezaRepository.ConsultarPorFiltro(filtro);
        }

        public IList<Insumo> ConsultarInsumosPorSemanaOperativaAgentes(int idSemanaOperativa, params int[] idsAgente)
        {
            return insumoRepository.ConsultarInsumosPorSemanaOperativaAgentes(idSemanaOperativa, idsAgente);
        }

        public bool PermitirAlteracaoGrandeza(int idGrandeza)
        {
            bool resultado = true;

            if (idGrandeza > 0)
            {
                //Grandeza grandeza = grandezaRepository.FindByKey(idGrandeza);
                //resultado = !grandeza.DadosColeta.Any();
                resultado = !grandezaRepository.ExisteDadosColetaNaGrandeza(idGrandeza);
            }

            return resultado;
        }

        public Grandeza ObterGrandezaPorId(int idGrandeza)
        {
            return grandezaRepository.FindByKey(idGrandeza);
        }

        public IList<TipoDadoGrandeza> ObterTiposDadoGrandeza()
        {
            return tipoDadoGrandezaRepository.All(r => r.OrderBy(t => t.Descricao));
        }

        #endregion

        #region Validações Insumo

        public void VerificarInsumoReservado(int id)
        {
            Insumo insumo = insumoRepository.FindByKey(id);

            IList<string> mensagens = new List<string>();
            ValidarAlterarExcluirInsumoReservado(insumo, mensagens);
            VerificarONSBusinessException(mensagens);
        }

        private void ValidarExclusaoInsumo(Insumo insumo, IList<string> mensagens)
        {
            if (insumo.Gabaritos.Any())
            {
                mensagens.Add(string.Format(SGIPMOMessages.MS036, insumo.Nome));
            }
        }

        private void ValidarAlterarExcluirInsumoReservado(Insumo insumo, IList<string> mensagens)
        {
            if (insumo.Reservado)
            {
                mensagens.Add(string.Format(SGIPMOMessages.MS042));
            }
        }

        private void ValidarInclusaoInsumo(string nome, IList<string> mensagens)
        {
            Insumo insumo = insumoRepository.ConsultarPorNome(nome);

            if (insumo != null)
            {
                mensagens.Add(string.Format(SGIPMOMessages.MS038));
            }
        }

        private void ValidarInclusaoSiglaInsumo(string sigla, IList<string> mensagens)
        {
            Insumo insumo = insumoRepository.ConsultarPorSigla(sigla);

            if (insumo != null)
            {
                mensagens.Add(string.Format(SGIPMOMessages.MS070));
            }
        }

        private void ValidarAlteracaoInsumo(int id, string nome, IList<string> mensagens)
        {
            Insumo insumo = insumoRepository.ConsultarPorNome(nome);

            if (insumo != null && insumo.Id != id)
            {
                mensagens.Add(string.Format(SGIPMOMessages.MS038));
            }
        }

        private void ValidarAlteracaoInsumoSigla(int id, string sigla, IList<string> mensagens)
        {
            Insumo insumo = insumoRepository.ConsultarPorSigla(sigla);

            if (insumo != null && insumo.Id != id)
            {
                mensagens.Add(string.Format(SGIPMOMessages.MS070));
            }
        }

        public void ValidarIncluirAlterarGrandeza(Grandeza grandeza)
        {
            IList<string> mensagens = new List<string>();
            ValidarIncluirAlterarGrandeza(grandeza, mensagens);
            VerificarONSBusinessException(mensagens);
        }

        private void ValidarIncluirAlterarGrandezas(IList<Grandeza> grandezas, IList<string> mensagens)
        {
            foreach (Grandeza grandeza in grandezas)
            {
                Grandeza grandezaPorNome = grandezas.FirstOrDefault(
                    g => g.Nome.ToLower().Equals(grandeza.Nome.ToLower())
                    && g.Id != grandeza.Id);
                if (grandezaPorNome != null)
                {
                    mensagens.Add(string.Format(SGIPMOMessages.MS054));
                }
                ValidarIncluirAlterarGrandeza(grandeza, mensagens);
            }
        }

        private void ValidarIncluirAlterarGrandeza(Grandeza grandeza, IList<string> mensagens)
        {
            if (grandeza.TipoDadoGrandeza.Id == (int)TipoDadoGrandezaEnum.Numerico)
            {
                Parametro parametroQtdMaxDigitosGrandeza = parametroRepository.ObterPorTipo(ParametroEnum.QuatidadeMaximaDigitosGrandeza);
                Parametro parametroQtdMaxDecimaisGrandeza = parametroRepository.ObterPorTipo(ParametroEnum.QuantidadeMaximaDecimaisGrandeza);

                if (grandeza.QuantidadeCasasDecimais > int.Parse(parametroQtdMaxDecimaisGrandeza.Valor))
                {
                    mensagens.Add(string.Format(SGIPMOMessages.MS056, int.Parse(parametroQtdMaxDecimaisGrandeza.Valor)));
                }

                if (grandeza.QuantidadeCasasInteira > int.Parse(parametroQtdMaxDigitosGrandeza.Valor))
                {
                    mensagens.Add(string.Format(SGIPMOMessages.MS055, int.Parse(parametroQtdMaxDigitosGrandeza.Valor)));
                }
            }
            else if (grandeza.AceitaValorNegativo)
            {
                mensagens.Add(string.Format(SGIPMOMessages.MS039));
            }
        }

        public void ValidarExclusaoGrandeza(int idGrandeza)
        {
            Grandeza grandeza = grandezaRepository.FindByKey(idGrandeza);
            IList<string> mensagens = new List<string>();
            ValidarExclusaoGrandeza(grandeza, mensagens);
            VerificarONSBusinessException(mensagens);
        }

        private void ValidarExclusaoGrandeza(Grandeza grandeza, IList<string> mensagens)
        {
            if (grandeza.DadosColeta.Any())
            {
                mensagens.Add(string.Format(SGIPMOMessages.MS037, grandeza.Nome));
            }
        }

        private void VerificarONSBusinessException(IList<string> mensagens)
        {
            if (mensagens.Any())
            {
                throw new ONSBusinessException(mensagens);
            }
        }

        /// <summary>
        /// Caso o insumo já esteje associado a algum gabarito.
        /// Verificar se existe ao menos uma grandeza ativa.
        /// </summary>
        /// <param name="insumo">Insumo com os gabaritos a ser validado</param>
        /// <param name="grandezas">Lista de grandezas do insumo</param>
        /// <param name="mensagens">Lista de mensagens de erros levantadas</param>
        private void ValidarInsumoAssociadoGabaritoSemGrandezaAtiva(InsumoEstruturado insumo,
            IList<Grandeza> grandezas, IList<string> mensagens)
        {
            if (insumo.Gabaritos.Any() && !grandezas.Any(g => g.Ativo))
            {
                mensagens.Add(SGIPMOMessages.MS064);
            }
        }

        #endregion
    }
}

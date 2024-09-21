using System.Globalization;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation.LogAuditoria
{
    using System;

    public class MapaLogAuditoria : MapaLogAuditoriaBase
    {
        private readonly Func<ExibicaoCalculadaArgument, object> exibicaoCalculadaBoolParaSimNao = b =>
        {
            object retorno = string.Empty;
            if (b != null)
            {
                retorno = (bool)b.ValorCorrente ? "Sim" : "Não";
            }
            return retorno;
        };

        private readonly Func<ExibicaoCalculadaArgument, object> formatadorData = d => string.Format("{0:dd/MM/yyyy}", d.ValorCorrente);

        public MapaLogAuditoria(string connectionStringName)
            : base(connectionStringName)
        {

        }

        public override void PreencherMapa()
        {
            MapearArquivo();

            MapearGabarito();

            MapearInsumoEstruturado();
            MapearInsumoNaoEstruturado();
            MapearGrandeza();

            MapearPMO();
            MapearSemanaOperativa();

            MapearColetaInsumo();
            MapearDadoColetaEstruturado();
            MapearDadoColetaNaoEstruturado();
            MapearDadoColetaManutencao();

            MapearSubsistema();
            MapearReservatorio();
            MapearUsina();
            MapearUnidadeGeradora();

            MapearDadoConvergencia();
        }

        #region Grandeza

        private void MapearGrandeza()
        {
            var mapaGrandeza = this.Mapa.RecuperarClasse<Grandeza>();

            mapaGrandeza.AdicionarCaminhoExibicao(g => g.Nome)
                        .AdicionarCaminhoExibicao(insumo => insumo.Insumo.Nome);
            mapaGrandeza.NomeExterno = "Grandeza";

            mapaGrandeza.RecuperarPropriedade(g => g.Nome)
                .ConfigProps(propriedade =>
                {
                    propriedade.NomeExterno = "Nome da Grandeza";
                });

            mapaGrandeza.RecuperarPropriedade(g => g.AceitaValorNegativo)
                .ConfigProps(propriedade =>
                {
                    propriedade.NomeExterno = "Aceita Valor Negativo?";
                    propriedade.AdicionarExibicaoCalculada(exibicaoCalculadaBoolParaSimNao);
                });

            mapaGrandeza.RecuperarPropriedade(g => g.IsObrigatorio)
                .ConfigProps(propriedade =>
                {
                    propriedade.NomeExterno = "Comportamento Obrigatório?";
                    propriedade.AdicionarExibicaoCalculada(exibicaoCalculadaBoolParaSimNao);
                });

            mapaGrandeza.RecuperarPropriedade(g => g.Ativo)
                .ConfigProps(propriedade =>
                {
                    propriedade.NomeExterno = "Ativa?";
                    propriedade.AdicionarExibicaoCalculada(exibicaoCalculadaBoolParaSimNao);

                });

            mapaGrandeza.RecuperarPropriedade(g => g.DestacaDiferenca)
                .ConfigProps(propriedade =>
                {
                    propriedade.NomeExterno = "Destaca Diferença?";
                    propriedade.AdicionarExibicaoCalculada(exibicaoCalculadaBoolParaSimNao);

                });

            mapaGrandeza.RecuperarPropriedade(g => g.IsColetaPorEstagio)
                .ConfigProps(propriedade =>
                {
                    propriedade.NomeExterno = "Coletada por Estágio?";
                    propriedade.AdicionarExibicaoCalculada(exibicaoCalculadaBoolParaSimNao);
                });

            mapaGrandeza.RecuperarPropriedade(g => g.IsPreAprovadoComAlteracao)
                .ConfigProps(propriedade =>
                {
                    propriedade.NomeExterno = "Pré-aprovado \"não estágio\" com alteração?";
                    propriedade.AdicionarExibicaoCalculada(exibicaoCalculadaBoolParaSimNao);
                });

            mapaGrandeza.RecuperarPropriedade(g => g.IsColetaPorPatamar)
                .ConfigProps(propriedade =>
                {
                    propriedade.NomeExterno = "Coletada por Patamar?";
                    propriedade.AdicionarExibicaoCalculada(exibicaoCalculadaBoolParaSimNao);

                });

            mapaGrandeza.RecuperarPropriedade(g => g.IsColetaPorLimite)
            .ConfigProps(propriedade =>
            {
                propriedade.NomeExterno = "Coletada por Limite?";
                propriedade.AdicionarExibicaoCalculada(exibicaoCalculadaBoolParaSimNao);

            });

            mapaGrandeza.RecuperarPropriedade(g => g.PodeRecuperarValor)
                .ConfigProps(propriedade =>
                {
                    propriedade.NomeExterno = "Pode Recuperar Valor?";
                    propriedade.AdicionarExibicaoCalculada(exibicaoCalculadaBoolParaSimNao);

                });

            mapaGrandeza.RecuperarPropriedade(g => g.OrdemExibicao)
                .ConfigProps(propriedade =>
                {
                    propriedade.NomeExterno = "Ordem de Exibição";

                });

            mapaGrandeza.RecuperarPropriedade(g => g.QuantidadeCasasDecimais)
                .ConfigProps(propriedade =>
                {
                    propriedade.NomeExterno = "Qtd. Casas Decimais";

                });

            mapaGrandeza.RecuperarPropriedade(g => g.QuantidadeCasasInteira)
                .ConfigProps(propriedade =>
                {
                    propriedade.NomeExterno = "Qtd. Casas Inteiras";

                });



            mapaGrandeza.RecuperarPropriedade(g => g.TipoDadoGrandezaId)
                .AdicionarCaminhoExibicao(g => g.TipoDadoGrandeza.Descricao)
                .ConfigProps(propriedade =>
                {
                    propriedade.NomeExterno = "Tipo de Dado";

                });

        }

        #endregion

        #region Mapa das Origens das Coletas

        private void MapearOrigemColeta<T>(Classe<T> mapaOrigemColeta) where T : OrigemColeta
        {
            mapaOrigemColeta.AdicionarCaminhoExibicao(ent => ent.Nome)
                .AdicionarCaminhoExibicao(
                    ent => ent.TipoOrigemColeta,
                    args => ((TipoOrigemColetaEnum)args.ValorCorrente).ToDescription());

            mapaOrigemColeta.RecuperarPropriedade(prop => prop.Nome).NomeExterno = "Nome";
            mapaOrigemColeta.RecuperarPropriedade(prop => prop.TipoOrigemColetaDescricao)
                .AdicionarExibicaoCalculada(args => ((TipoOrigemColetaEnum)args.ValorCorrente.ToString()[0]).ToDescription())
                .NomeExterno = "Tipo";

        }

        private void MapearSubsistema()
        {
            var mapaSubsistema = this.Mapa.RecuperarClasse<Subsistema>();
            mapaSubsistema.NomeExterno = "Subsistema";

            this.MapearOrigemColeta(mapaSubsistema);
            mapaSubsistema.RecuperarPropriedade(prop => prop.Codigo).NomeExterno = "Código";
        }

        private void MapearReservatorio()
        {
            var mapaReservatorio = this.Mapa.RecuperarClasse<Reservatorio>();
            mapaReservatorio.NomeExterno = "Reservatório";

            this.MapearOrigemColeta(mapaReservatorio);

            mapaReservatorio.RecuperarPropriedade(prop => prop.NomeCurto).NomeExterno = "Nome Curto";
            mapaReservatorio.RecuperarPropriedade(prop => prop.NomeLongo).NomeExterno = "Nome Longo";
            mapaReservatorio.RecuperarPropriedade(prop => prop.CodigoDPP).NomeExterno = "Código DPP";
            mapaReservatorio.RecuperarPropriedade(prop => prop.IdSubsistema)
                .AdicionarCaminhoExibicao(prop => prop.Subsistema.Nome).NomeExterno = "Subsistema";
        }

        private void MapearUsina()
        {
            var mapaUsina = this.Mapa.RecuperarClasse<Usina>();
            mapaUsina.NomeExterno = "Usina";

            this.MapearOrigemColeta(mapaUsina);

            mapaUsina.RecuperarPropriedade(prop => prop.NomeCurto).NomeExterno = "Nome Curto";
            mapaUsina.RecuperarPropriedade(prop => prop.NomeLongo).NomeExterno = "Nome Longo";
            mapaUsina.RecuperarPropriedade(prop => prop.CodigoDPP).NomeExterno = "Código DPP";
            mapaUsina.RecuperarPropriedade(prop => prop.IdSubsistema)
                .AdicionarCaminhoExibicao(prop => prop.Subsistema.Nome).NomeExterno = "Subsistema";
            mapaUsina.RecuperarPropriedade(prop => prop.TipoUsina).NomeExterno = "Tipo de Usina";
        }

        private void MapearUnidadeGeradora()
        {
            var mapaUnidadeGeradora = this.Mapa.RecuperarClasse<UnidadeGeradora>();
            mapaUnidadeGeradora.NomeExterno = "Usina";

            this.MapearOrigemColeta(mapaUnidadeGeradora);

            mapaUnidadeGeradora.RecuperarPropriedade(prop => prop.CodigoDPP).NomeExterno = "Código DPP";
            mapaUnidadeGeradora.RecuperarPropriedade(prop => prop.NumeroConjunto).NomeExterno = "Núm. do Conjunto";
            mapaUnidadeGeradora.RecuperarPropriedade(prop => prop.NumeroMaquina).NomeExterno = "Núm. da Máquina";
            mapaUnidadeGeradora.RecuperarPropriedade(prop => prop.PotenciaNominal).NomeExterno = "Potência Nominal";
            mapaUnidadeGeradora.RecuperarPropriedade(prop => prop.UsinaId)
                .AdicionarCaminhoExibicao(prop => prop.Usina.Nome)
                .NomeExterno = "Usina";
        }

        #endregion

        #region Gabarito

        private void MapearGabarito()
        {
            var mapaGabarito = this.Mapa.RecuperarClasse<Gabarito>();

            mapaGabarito.ConfigProps(classe =>
            {
                classe.NomeExterno = "Gabarito";
            });


            mapaGabarito.AdicionarCaminhoExibicao(gabarito => gabarito.SemanaOperativa.Nome,
                args =>
                {
                    Gabarito gabarito = (Gabarito)args.Entidade;
                    return gabarito.SemanaOperativa == null ? "Gabarito Padrão" : args.ValorCorrente;
                });

            mapaGabarito.RecuperarPropriedade(gabarito => gabarito.InsumoId)
                        .AdicionarCaminhoExibicao(gabarito => gabarito.Insumo.Nome).NomeExterno = "Insumo";

            mapaGabarito.RecuperarPropriedade(gabarito => gabarito.AgenteId)
                        .ConfigProps(prop =>
                        {
                            prop.NomeExterno = "Agente";
                            prop.AdicionarExibicaoCalculada(argument =>
                            {
                                Gabarito gabarito = argument.Entidade as Gabarito;

                                if (gabarito != null && gabarito.Agente != null)
                                {
                                    Agente agente = gabarito.Agente;
                                    if (agente != null)
                                    {
                                        if (string.IsNullOrWhiteSpace(gabarito.CodigoPerfilONS))
                                        {
                                            return agente.Nome;
                                        }
                                        return string.Format("{0}/{1}", agente.Nome, gabarito.CodigoPerfilONS);
                                    }
                                }
                                return string.Empty;
                            });
                        });

            mapaGabarito.RecuperarPropriedade(gabarito => gabarito.SemanaOperativaId)
                        .AdicionarCaminhoExibicao(gabarito => gabarito.SemanaOperativa.Nome)
                        .ConfigProps(prop =>
                        {
                            prop.NomeExterno = "Estudo";
                        });

            mapaGabarito.RecuperarPropriedade(gabarito => gabarito.OrigemColetaId)
                        .AdicionarExibicaoCalculada(args =>
                        {
                            Gabarito gabarito = (Gabarito)args.Entidade;
                            OrigemColeta origemColeta = gabarito.OrigemColeta;
                            if (origemColeta != null)
                            {
                                return origemColeta.TipoOrigemColeta.ToDescription() + ": " + origemColeta.Nome;
                            }
                            return TipoOrigemColetaEnum.GeracaoComplementar.ToDescription();
                        }).NomeExterno = "Origem da Coleta";

            mapaGabarito.RecuperarPropriedade(gabarito => gabarito.Id).Visivel = false;
        }

        #endregion

        #region PMO

        private void MapearPMO()
        {
            var mapaPMO = this.Mapa.RecuperarClasse<PMO>();

            mapaPMO.AdicionarCaminhoExibicao(pmo => pmo.AnoReferencia, arg =>
            {
                PMO pmo = arg.Entidade as PMO;
                if (pmo != null)
                {
                    CultureInfo cultura = CultureInfo.CurrentCulture;
                    string nomeMes = cultura.TextInfo.ToTitleCase(
                            cultura.DateTimeFormat.GetMonthName(pmo.MesReferencia));
                    return string.Format("PMO {0} {1}", nomeMes, pmo.AnoReferencia);
                }
                return string.Empty;
            });

            mapaPMO.NomeExterno = "PMO";
            mapaPMO.RecuperarPropriedade(prop => prop.AnoReferencia).NomeExterno = "Ano Referência";
            mapaPMO.RecuperarPropriedade(prop => prop.MesReferencia)
                .AdicionarExibicaoCalculada(
                prop =>
                {
                    int valorMes;
                    if (int.TryParse(Convert.ToString(prop.ValorCorrente), out valorMes))
                    {
                        CultureInfo cultura = CultureInfo.CurrentCulture;
                        string nomeMes = cultura.TextInfo.ToTitleCase(
                            cultura.DateTimeFormat.GetMonthName(valorMes));
                        return nomeMes;
                    }
                    return prop.ValorCorrente;

                }).NomeExterno = "Mês Referência";
            mapaPMO.RecuperarPropriedade(prop => prop.QuantidadeMesesAdiante).NomeExterno = "Quantidade de meses a frente";
        }

        #endregion

        #region Semana Operativa

        private void MapearSemanaOperativa()
        {
            var mapaSemanaOperativa = this.Mapa.RecuperarClasse<SemanaOperativa>();

            mapaSemanaOperativa.AdicionarCaminhoExibicao(s => s.Nome);

            mapaSemanaOperativa.NomeExterno = "Semana Operativa";

            mapaSemanaOperativa.RecuperarPropriedade(prop => prop.DataInicioSemana)
                               .AdicionarExibicaoCalculada(formatadorData).NomeExterno = "Data Início Semana";
            mapaSemanaOperativa.RecuperarPropriedade(prop => prop.DataFimSemana)
                               .AdicionarExibicaoCalculada(formatadorData).NomeExterno = "Data Fim Semana";
            mapaSemanaOperativa.RecuperarPropriedade(prop => prop.DataReuniao)
                               .AdicionarExibicaoCalculada(formatadorData).NomeExterno = "Data Reunião";
            mapaSemanaOperativa.RecuperarPropriedade(prop => prop.DataInicioManutencao)
                               .AdicionarExibicaoCalculada(formatadorData).NomeExterno = "Data Início Manutenção";
            mapaSemanaOperativa.RecuperarPropriedade(prop => prop.DataFimManutencao)
                               .AdicionarExibicaoCalculada(formatadorData).NomeExterno = "Data Fim Manutenção";
            mapaSemanaOperativa.RecuperarPropriedade(prop => prop.Revisao).NomeExterno = "Revisão";
            mapaSemanaOperativa.RecuperarPropriedade(prop => prop.SituacaoId)
                .AdicionarCaminhoExibicao(prop => prop.Situacao.Descricao)
                .NomeExterno = "Estado";
            mapaSemanaOperativa.RecuperarPropriedade(prop => prop.DataHoraAtualizacao).NomeExterno = "Data Ultima Alteração";
        }

        #endregion

        #region Coleta Insumo

        private void MapearColetaInsumo()
        {
            var mapaColetaInsumo = this.Mapa.RecuperarClasse<ColetaInsumo>();

            mapaColetaInsumo.AdicionarCaminhoExibicao(prop => prop.Insumo.Nome)
                .AdicionarCaminhoExibicao(prop => prop.Agente.Nome);

            mapaColetaInsumo.NomeExterno = "Coleta Insumo";

            mapaColetaInsumo.RecuperarPropriedade(prop => prop.InsumoId)
                        .AdicionarCaminhoExibicao(prop => prop.Insumo.Nome).NomeExterno = "Insumo";

            mapaColetaInsumo.RecuperarPropriedade(prop => prop.AgenteId).ConfigProps(
                prop =>
                {
                    prop.NomeExterno = "Agente";
                    prop.AdicionarExibicaoCalculada(args =>
                    {
                        ColetaInsumo coletaInsumo = args.Entidade as ColetaInsumo;
                        if (coletaInsumo != null && coletaInsumo.Agente != null)
                        {
                            if (string.IsNullOrWhiteSpace(coletaInsumo.CodigoPerfilONS))
                            {
                                return coletaInsumo.Agente.Nome;
                            }
                            return string.Format("{0}/{1}", coletaInsumo.Agente.Nome, coletaInsumo.CodigoPerfilONS);
                        }
                        return string.Empty;
                    });
                });


            mapaColetaInsumo.RecuperarPropriedade(prop => prop.SituacaoId)
                            .AdicionarCaminhoExibicao(prop => prop.Situacao.Descricao).NomeExterno = "Situação Coleta";

            mapaColetaInsumo.RecuperarPropriedade(prop => prop.MotivoRejeicaoONS).NomeExterno = "Motivo Rejeição ONS";
            mapaColetaInsumo.RecuperarPropriedade(prop => prop.MotivoAlteracaoONS).NomeExterno = "Motivo Alteração ONS";
            mapaColetaInsumo.RecuperarPropriedade(prop => prop.DataHoraAtualizacao).NomeExterno = "Data Ultima Alteração";
            mapaColetaInsumo.RecuperarPropriedade(prop => prop.CodigoPerfilONS).NomeExterno = "Perfil ONS";
            mapaColetaInsumo.RecuperarPropriedade(prop => prop.LoginAgenteAlteracao).NomeExterno = "Executor Ultima Alteração";
        }

        #endregion

        #region Dado Coleta

        private void MapearDadoColeta<T>(Classe<T> mapaDadoColeta) where T : DadoColeta
        {
            mapaDadoColeta.AdicionarCaminhoExibicao(prop => prop.Gabarito.OrigemColeta.Nome);

            mapaDadoColeta.NomeExterno = "Dado Coleta";

            mapaDadoColeta.RecuperarPropriedade(prop => prop.ColetaInsumo.AgenteId)
                          .AdicionarCaminhoExibicao(prop => prop.ColetaInsumo.Agente.Nome).NomeExterno = "Agente";
            mapaDadoColeta.RecuperarPropriedade(prop => prop.ColetaInsumo.InsumoId)
                          .AdicionarCaminhoExibicao(prop => prop.ColetaInsumo.Insumo.Nome).NomeExterno = "Insumo";
            mapaDadoColeta.RecuperarPropriedade(prop => prop.TipoDadoColeta)
                .ConfigProps(prop =>
                {
                    prop.NomeExterno = "Tipo";
                    prop.AdicionarExibicaoCalculada(
                        o => o.ValorCorrente as string == "E" ? "Estruturado" :
                            o.ValorCorrente as string == "L" ? "Não Estruturado" : "Manutenção");
                });
        }

        private void MapearDadoColetaEstruturado()
        {
            var mapaDadoColetaEstruturado = this.Mapa.RecuperarClasse<DadoColetaEstruturado>();

            MapearDadoColeta(mapaDadoColetaEstruturado);
            mapaDadoColetaEstruturado
                .AdicionarCaminhoExibicao(prop => prop.Estagio)
                .AdicionarCaminhoExibicao(prop => prop.Grandeza.Nome)
                .AdicionarCaminhoExibicao(prop => prop.TipoPatamar.Descricao)
                .AdicionarCaminhoExibicao(prop => prop.TipoLimite.Descricao);

            mapaDadoColetaEstruturado.NomeExterno = "Dado Coleta Estruturado";

            mapaDadoColetaEstruturado.RecuperarPropriedade(prop => prop.GrandezaId)
                          .AdicionarCaminhoExibicao(prop => prop.Grandeza.Nome).NomeExterno = "Grandeza";

            mapaDadoColetaEstruturado.RecuperarPropriedade(prop => prop.Valor).NomeExterno = "Valor";
            mapaDadoColetaEstruturado.RecuperarPropriedade(prop => prop.Estagio).NomeExterno = "Estágio";

            mapaDadoColetaEstruturado.RecuperarPropriedade(prop => prop.GabaritoId)
                        .AdicionarExibicaoCalculada(args =>
                        {
                            DadoColetaEstruturado dadoColetaEstruturado = (DadoColetaEstruturado)args.Entidade;
                            if (dadoColetaEstruturado != null)
                            {
                                if (dadoColetaEstruturado.Gabarito != null
                                    && dadoColetaEstruturado.Gabarito.OrigemColeta != null)
                                {
                                    OrigemColeta origemColeta = dadoColetaEstruturado.Gabarito.OrigemColeta;
                                    if (origemColeta != null)
                                    {
                                        return origemColeta.TipoOrigemColeta.ToDescription() + ": " + origemColeta.Nome;
                                    }
                                }
                            }
                            return TipoOrigemColetaEnum.GeracaoComplementar.ToDescription();
                        }).NomeExterno = "Origem da Coleta";
        }

        private void MapearDadoColetaNaoEstruturado()
        {
            var mapaDadoColetaNaoEstruturado = this.Mapa.RecuperarClasse<DadoColetaNaoEstruturado>();

            MapearDadoColeta(mapaDadoColetaNaoEstruturado);

            mapaDadoColetaNaoEstruturado.NomeExterno = "Dado Coleta Não Estruturado";
            mapaDadoColetaNaoEstruturado.RecuperarPropriedade(prop => prop.Observacao).NomeExterno = "Observação";
        }

        private void MapearDadoColetaManutencao()
        {
            var mapaDadoColetaManutencao = this.Mapa.RecuperarClasse<DadoColetaManutencao>();

            MapearDadoColeta(mapaDadoColetaManutencao);

            mapaDadoColetaManutencao.NomeExterno = "Dado Coleta Manutenção";
            mapaDadoColetaManutencao.RecuperarPropriedade(prop => prop.UnidadeGeradora.Id)
                .AdicionarCaminhoExibicao(prop => prop.UnidadeGeradora.Nome).NomeExterno = "Unidade Geradora";

            mapaDadoColetaManutencao.RecuperarPropriedade(prop => prop.UnidadeGeradora.UsinaId)
                .AdicionarCaminhoExibicao(prop => prop.UnidadeGeradora.Usina.Nome).NomeExterno = "Usina";

            mapaDadoColetaManutencao.RecuperarPropriedade(prop => prop.DataInicio)
                               .AdicionarExibicaoCalculada(formatadorData).NomeExterno = "Data Início Manutenção";
            mapaDadoColetaManutencao.RecuperarPropriedade(prop => prop.DataFim)
                               .AdicionarExibicaoCalculada(formatadorData).NomeExterno = "Data Fim Manutenção";
            mapaDadoColetaManutencao.RecuperarPropriedade(prop => prop.Justificativa).NomeExterno = "Justificativa";
            mapaDadoColetaManutencao.RecuperarPropriedade(prop => prop.Numero).NomeExterno = "Número";
            mapaDadoColetaManutencao.RecuperarPropriedade(prop => prop.TempoRetorno).NomeExterno = "Tempo Retorno";
        }

        #endregion

        #region Arquivo

        private void MapearArquivo()
        {
            var mapaArquivo = this.Mapa.RecuperarClasse<Arquivo>();

            mapaArquivo.NomeExterno = "Arquivo";
            mapaArquivo.AdicionarCaminhoExibicao(s => s.Nome);

            mapaArquivo.RecuperarPropriedade(prop => prop.Nome).AdicionarExibicaoCalculada(argument =>
            {
                string retorno = argument.ValorCorrente.ToString();
                var instanciaArquivo = argument.Entidade as Arquivo;
                if (instanciaArquivo != null)
                {
                    retorno = string.Format("<a href='[URL_ONS_APPLICATION]/PublicResource/DownloadFileCompressed?targetName={0}'>{1}</a>", instanciaArquivo.Id, argument.ValorCorrente);
                }
                return retorno;
            }).NomeExterno = "Nome";
        }

        #endregion

        #region Mapeamento de Insumo

        private void MapearInsumo<T>(Classe<T> mapaInsumo) where T : Insumo
        {
            mapaInsumo.AdicionarCaminhoExibicao(insumo => insumo.Nome);

            mapaInsumo.RecuperarPropriedade(prop => prop.Nome).NomeExterno = "Nome do Insumo";

            mapaInsumo.RecuperarPropriedade(estruturado => estruturado.DataUltimaAtualizacao)
             .ConfigProps(
                 propriedade =>
                 {
                     propriedade.NomeExterno = "Data Ultima Alteração";
                 });

            mapaInsumo.RecuperarPropriedade(estruturado => estruturado.PreAprovado)
               .ConfigProps(
                   propriedade =>
                   {
                       propriedade.NomeExterno = "Pré-Aprovado?";
                       propriedade.AdicionarExibicaoCalculada(exibicaoCalculadaBoolParaSimNao);

                   });
            mapaInsumo.RecuperarPropriedade(estruturado => estruturado.TipoInsumo)
                .ConfigProps(
                    propriedade =>
                    {
                        propriedade.NomeExterno = "Tipo";
                        propriedade.AdicionarExibicaoCalculada(
                            o => o.ValorCorrente as string == "E" ? "Estruturado" : "Não Estruturado");

                    });
            mapaInsumo.RecuperarPropriedade(estruturado => estruturado.Reservado)
                .ConfigProps(
                    propriedade =>
                    {
                        propriedade.NomeExterno = "Reservado?";
                        propriedade.AdicionarExibicaoCalculada(exibicaoCalculadaBoolParaSimNao);

                    });
            mapaInsumo.RecuperarPropriedade(estruturado => estruturado.OrdemExibicao)
          .ConfigProps(
              propriedade =>
              {
                  propriedade.NomeExterno = "Ordem de Exibição";

              });
        }

        private void MapearInsumoEstruturado()
        {
            var mapaInsumoEstruturado = this.Mapa.RecuperarClasse<InsumoEstruturado>();

            this.MapearInsumo(mapaInsumoEstruturado);

            mapaInsumoEstruturado.NomeExterno = "Insumo Estruturado";

            mapaInsumoEstruturado.RecuperarPropriedade(estruturado => estruturado.QuantidadeMesesAdiante)
                .ConfigProps(
                    propriedade =>
                    {
                        propriedade.NomeExterno = "Qtd. Meses a Frente";

                    });
            mapaInsumoEstruturado.RecuperarPropriedade(estruturado => estruturado.TipoInsumo)
                .ConfigProps(
                    propriedade =>
                    {
                        propriedade.NomeExterno = "Tipo";
                        propriedade.AdicionarExibicaoCalculada(
                            o => o.ValorCorrente as string == "E" ? "Estruturado" : "Não Estruturado");

                    });

            mapaInsumoEstruturado.RecuperarPropriedade(estruturado => estruturado.CategoriaInsumoId)
                .AdicionarCaminhoExibicao(prop => prop.CategoriaInsumo.Descricao)
                        .NomeExterno = "Categoria";


            mapaInsumoEstruturado.RecuperarPropriedade(estruturado => estruturado.TipoColetaId)
                .AdicionarCaminhoExibicao(prop => prop.TipoColeta.Descricao)
                .NomeExterno = "Tipo de Coleta";


        }

        private void MapearInsumoNaoEstruturado()
        {
            var mapaInsumoNaoEstruturado = this.Mapa.RecuperarClasse<InsumoNaoEstruturado>();

            this.MapearInsumo(mapaInsumoNaoEstruturado);

            mapaInsumoNaoEstruturado.NomeExterno = "Insumo Não Estruturado";

            mapaInsumoNaoEstruturado.RecuperarPropriedade(estruturado => estruturado.IsUtilizadoConvergencia)
                .ConfigProps(
                    propriedade =>
                    {
                        propriedade.NomeExterno = "Utilizado na Convergência?";
                        propriedade.AdicionarExibicaoCalculada(exibicaoCalculadaBoolParaSimNao);
                    });
            mapaInsumoNaoEstruturado.RecuperarPropriedade(estruturado => estruturado.IsUtilizadoDECOMP)
              .ConfigProps(
                  propriedade =>
                  {
                      propriedade.NomeExterno = "Bloco Montador";
                      propriedade.AdicionarExibicaoCalculada(exibicaoCalculadaBoolParaSimNao);

                  });

            mapaInsumoNaoEstruturado.RecuperarPropriedade(estruturado => estruturado.IsUtilizadoProcessamento)
             .ConfigProps(
                 propriedade =>
                 {
                     propriedade.NomeExterno = "Utilizado no Processamento?";
                     propriedade.AdicionarExibicaoCalculada(exibicaoCalculadaBoolParaSimNao);

                 });
            mapaInsumoNaoEstruturado.RecuperarPropriedade(estruturado => estruturado.IsUtilizadoPublicacao)
          .ConfigProps(
              propriedade =>
              {
                  propriedade.NomeExterno = "Utilizado na Publicação?";
                  propriedade.AdicionarExibicaoCalculada(exibicaoCalculadaBoolParaSimNao);

              });


        }

        #endregion

        #region DadoConvergencia

        private void MapearDadoConvergencia()
        {
            var mapaDadoConvergencia = this.Mapa.RecuperarClasse<DadoConvergencia>();

            mapaDadoConvergencia.NomeExterno = "Dado Convergência";
            mapaDadoConvergencia.AdicionarCaminhoExibicao(d => d.SemanaOperativa.Nome);
            mapaDadoConvergencia.RecuperarPropriedade(prop => prop.ObservacaoConvergenciaCCEE).NomeExterno = "Observação";
        }

        #endregion
    }
}

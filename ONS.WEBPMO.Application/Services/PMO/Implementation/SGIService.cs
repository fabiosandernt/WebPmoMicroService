using ONS.WEBPMO.Application.Services.PMO.Interfaces;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{
    public class SGIService : Service, ISGIService
    {
        private readonly static ILog log = LogManager.GetLogger(typeof(ISGIService));

        private readonly IUnidadeGeradoraRepository unidadeGeradoraRepository;
        private readonly SGIExternalServices.interfaceSGIPMO servicoSGI;

        public SGIService(IUnidadeGeradoraRepository unidadeGeradoraRepository, interfaceSGIPMO servicoSGI)
        {
            this.servicoSGI = servicoSGI;
            this.unidadeGeradoraRepository = unidadeGeradoraRepository;
        }

        public IList<DadoColetaManutencao> ObterManutencoesPorChaves(string[] chavesUnidadesGeradoras,
            DateTime dataInicio, DateTime dataFim)
        {
            IList<DadoColetaManutencao> dadoColetaManutencaoList = new List<DadoColetaManutencao>();

            DadosManutencao[] manutencoes = ObterManutencoes(chavesUnidadesGeradoras, dataInicio, dataFim);
            /*
            string manut = Newtonsoft.Json.JsonConvert.SerializeObject(manutencoes);

            List<string> idManut = manutencoes.Select(p => p.NumeroManutencao).ToList();
            string x = Newtonsoft.Json.JsonConvert.SerializeObject(idManut);
            */

            IList<UnidadeGeradora> unidadesGeradoras = unidadeGeradoraRepository.FindByKeys(chavesUnidadesGeradoras);

            foreach (DadosManutencao manutencao in manutencoes)
            {
                /* Manutenção que durem mais de dois dias atingem o horário de pico e não precisam ser verificadas */
                if (manutencao.DataTermino.Subtract(manutencao.DataInicio).TotalDays < 3)
                {
                    /* Manutenção em que pelo menos um dia ocorra no domimgo */
                    if (manutencao.DataInicio.DayOfWeek == DayOfWeek.Sunday ||
                        manutencao.DataTermino.DayOfWeek == DayOfWeek.Sunday)
                    {
                        /* Manutenção que ocorre no domingo mais não ocorre o dia inteiro */
                        //if (manutencao.DataInicio.DayOfWeek == DayOfWeek.Sunday &&
                        //    manutencao.DataTermino.DayOfWeek == DayOfWeek.Sunday &&
                        //    (manutencao.DataInicio.TimeOfDay != horaPicoDomingoInicio ||
                        //     manutencao.DataTermino.TimeOfDay != horaPicoDomingoTermino))
                        //{
                        //    continue;
                        //}

                        /* Manutenção que comece depois do horário de pico do sábado e não duram o domingo inteiro */
                        //if (manutencao.DataInicio.DayOfWeek == DayOfWeek.Saturday &&
                        //    manutencao.DataInicio.TimeOfDay > horaPicoTermino &&
                        //    manutencao.DataTermino.TimeOfDay != horaPicoDomingoTermino)
                        //{
                        //    continue;
                        //}

                        /* Manutenção que começa depois das zero horas do domingo e não atingem o horário de pico da segunda */
                        //if (manutencao.DataTermino.DayOfWeek == DayOfWeek.Monday &&
                        //    manutencao.DataTermino.TimeOfDay < horaPicoInicio &&
                        //    manutencao.DataInicio.TimeOfDay != horaPicoDomingoInicio)
                        //{
                        //    continue;
                        //}
                    }
                    else
                    {
                        /* Manutenção que ocorra em dias diferentes, mas com intervalo menor que 20h (intervalor entre 22 - 18h do dia seguinte) */
                        if (manutencao.DataInicio.Date != manutencao.DataTermino.Date &&
                            manutencao.DataTermino.Subtract(manutencao.DataInicio).TotalMinutes < 1200)
                        {
                            continue;
                        }
                    }
                }

                if (manutencao.TipoEquipamentoSGI == "18")//Se for usina, replica manutenção para as UGE's, conforme TFS 6409
                {
                    string[] unidadesGeradorasDaUsina = ObterUnidadesGeradorasPorUsina(manutencao.IdUnidadeGeradora.Trim(), chavesUnidadesGeradoras);

                    foreach (string idUnidade in unidadesGeradorasDaUsina)
                    {
                        manutencao.IdUnidadeGeradora = DecomporChavePseudoUsina(idUnidade);

                        ((List<DadoColetaManutencao>)dadoColetaManutencaoList).AddRange(ObterDadoColetaDeManutencao(manutencao, unidadesGeradoras));
                    }
                }
                else
                {
                    //dadoColetaManutencaoList.Add(ObterDadoColetaDeManutencao(manutencao, unidadesGeradoras));
                    ((List<DadoColetaManutencao>)dadoColetaManutencaoList).AddRange(ObterDadoColetaDeManutencao(manutencao, unidadesGeradoras));
                }

            }

            return dadoColetaManutencaoList;
        }

        private string[] ObterUnidadesGeradorasPorUsina(string IdUnidadeGeradora, string[] chavesUnidadesGeradoras)
        {
            return chavesUnidadesGeradoras.Where(x => x.Contains(IdUnidadeGeradora)).ToArray();
        }

        private List<DadoColetaManutencao> ObterDadoColetaDeManutencao(DadosManutencao manutencao, IList<UnidadeGeradora> unidadesGeradoras)
        {
            List<DadoColetaManutencao> retList = new List<DadoColetaManutencao>();
            string tempoRetorno = manutencao.TempoRetorno;
            if (!string.IsNullOrEmpty(tempoRetorno) && tempoRetorno.Length > 5)
            {
                tempoRetorno = tempoRetorno.Substring(2);
            }

            DadoColetaManutencao dadoColeta = ConvertFromService(manutencao, unidadesGeradoras, tempoRetorno);
            retList.Add(dadoColeta);
            /*
            if (manutencao.Periodicidade == "D")
            {
                int diffDias = (manutencao.DataTermino - manutencao.DataInicio).Days;
                if (diffDias > 0)
                {
                    DadosManutencao confOriginal = manutencao;


                    DateTime instanteInicial = new DateTime(manutencao.DataInicio.Year, manutencao.DataInicio.Month, manutencao.DataInicio.Day
                        , manutencao.DataInicio.Hour, manutencao.DataInicio.Minute, 0);
                    DateTime instanteFinal = new DateTime(manutencao.DataInicio.Year, manutencao.DataInicio.Month, manutencao.DataInicio.Day
                        , manutencao.DataTermino.Hour, manutencao.DataTermino.Minute, 0);

                    if (instanteFinal <= instanteInicial)
                    {
                        // exemplo
                        // DataInicio 24/07/2024 22:00:00  - dataFim 25/07/2024 10:00:00
                        // instanteInicial = 24/07/2024 22:00:00  - instante final = 24/07/2024 10:00:00


                        DateTime diaInicial = manutencao.DataInicio;
                        int horaInicio = manutencao.DataInicio.Hour;
                        int MinutoInicio = manutencao.DataInicio.Minute;
                        int horaFim = manutencao.DataTermino.Hour;
                        int MinutoFim = manutencao.DataTermino.Minute;

                        for (int dia = 0; dia <= diffDias; dia++)
                        {

                            DadosManutencao confPartMesmoDia = manutencao;

                            // inicio ate 23:59 do mesmo dia 
                            DateTime dtIniAux = diaInicial.AddDays(dia);
                            DateTime dtIni = new DateTime(dtIniAux.Year, dtIniAux.Month, dtIniAux.Day
                                , horaInicio, MinutoInicio, 0);
                            confPartMesmoDia.DataInicio = dtIni;

                            confPartMesmoDia.DataTermino = new DateTime(dtIni.Year
                                    , dtIni.Month
                                    , dtIni.Day
                                    , 23, 59, 59);
                            DadoColetaManutencao dto = ConvertFromService(confPartMesmoDia, unidadesGeradoras, tempoRetorno);
                            retList.Add(dto);


                            // 00:00 do dia seguinte ate o horario final do dia seguinte
                            DadosManutencao confPartDiaSeguinte = manutencao;

                            DateTime dtFimAux = dtIni.AddDays(1);
                            DateTime dtFim = new DateTime(dtFimAux.Year, dtFimAux.Month, dtFimAux.Day
                                , horaFim, MinutoFim, 0);

                            confPartDiaSeguinte.DataInicio = new DateTime(dtFim.Year
                                    , dtFim.Month
                                    , dtFim.Day
                                    , 0, 0, 0);

                            confPartDiaSeguinte.DataTermino = dtFim;

                            dto = ConvertFromService(confPartDiaSeguinte, unidadesGeradoras, tempoRetorno);
                            retList.Add(dto);
                        }
                    }
                    else
                    {
                        // exemplo
                        // DataInicio 24/07/2024 08:00:00  - dataFim 25/07/2024 17:00:00
                        // instanteInicial = 24/07/2024 08:00:00  - instante final = 24/07/2024 17:00:00
                        DateTime diaInicial = manutencao.DataInicio;
                        int horaInicio = manutencao.DataInicio.Hour;
                        int MinutoInicio = manutencao.DataInicio.Minute;
                        int horaFim = manutencao.DataTermino.Hour;
                        int MinutoFim = manutencao.DataTermino.Minute;

                        for (int dia = 0; dia <= diffDias; dia++)
                        {

                            DadosManutencao confPartMesmoDia = manutencao;
                          
                            DateTime dtIniAux = diaInicial.AddDays(dia);
                            DateTime dtIni = new DateTime(dtIniAux.Year, dtIniAux.Month, dtIniAux.Day
                                , horaInicio, MinutoInicio, 0);
                            confPartMesmoDia.DataInicio = dtIni;

                            confPartMesmoDia.DataTermino = new DateTime(dtIni.Year
                                    , dtIni.Month
                                    , dtIni.Day
                                    , horaFim, MinutoFim, 0);
                            DadoColetaManutencao dto = ConvertFromService(confPartMesmoDia, unidadesGeradoras, tempoRetorno);
                            retList.Add(dto);

                        }
                    }

                }
            }
            else
            {
                DadoColetaManutencao dadoColeta = ConvertFromService(manutencao, unidadesGeradoras, tempoRetorno);
                retList.Add(dadoColeta);

            }
            */
            return retList;
        }

        private DadoColetaManutencao ConvertFromService(DadosManutencao from, IList<UnidadeGeradora> unidadesGeradoras, string tempoRetorno)
        {
            DadoColetaManutencao dadoColeta = new DadoColetaManutencao();
            dadoColeta.DataInicio = from.DataInicio;
            dadoColeta.DataFim = from.DataTermino;
            dadoColeta.TempoRetorno = tempoRetorno;
            dadoColeta.Numero = from.NumeroManutencao;
            dadoColeta.Justificativa = from.Justificativa;

            dadoColeta.Periodicidade = from.Periodicidade;
            dadoColeta.Situacao = from.Situacao;
            dadoColeta.ClassificacaoPorTipoEquipamento = from.ClassificacaoPorTipoEquipamento;

            dadoColeta.UnidadeGeradora = unidadesGeradoras
                .FirstOrDefault(ug =>
                    DecomporChavePseudoUsina(ug.Id).Trim() == from.IdUnidadeGeradora.Trim());

            return dadoColeta;
        }


        private DadosManutencao[] ObterManutencoes(string[] chavesUnidadesGeradoras,
            DateTime dataInicio, DateTime dataFim)
        {
            DadosManutencao[] manutencoes = null;
            List<string> idsUnidadesGeradoras = chavesUnidadesGeradoras
                .Select(DecomporChavePseudoUsina).ToList();

            List<string> idsUsinas = chavesUnidadesGeradoras
                .Select(DecomporChaveUsina).ToList();

            idsUnidadesGeradoras.AddRange(idsUsinas.Distinct());

            TimeElapsedCounter timer;
            using (timer = TimeElapsedCounter.New)
            {
                servicoSGI.Timeout = 600000;
                servicoSGI.RequestEncoding = System.Text.Encoding.UTF8;
                manutencoes = servicoSGI.ObterManutencoesPorChaves(idsUnidadesGeradoras.ToArray(), dataInicio, dataFim);
            }
            log.DebugFormat("Serviço SGI Executado. tempo[{0}]", timer.ElapsedMilliseconds);

            return manutencoes;
        }

        private string DecomporChavePseudoUsina(string idUnidadeGeradora)
        {
            return idUnidadeGeradora.Split('|')[0].Trim();
        }

        private string DecomporChaveUsina(string idUnidadeGeradora)
        {
            string[] retorno = idUnidadeGeradora.Split(',');
            return retorno[retorno.Length - 1].Trim();
        }
    }
}

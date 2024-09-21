using System;
using ONS.Common.Exceptions;
using ONS.Common.Seguranca;
using ONS.Common.Services.Impl;
using ONS.Common.Util.Pagination;
using ONS.SGIPMO.Domain.Entities;
using ONS.SGIPMO.Domain.Entities.Filters;
using ONS.SGIPMO.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using ONS.WEBPMO.Application.Services.PMO.Interfaces;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{
    public class DadoColetaManutencaoService : Service, IDadoColetaManutencaoService
    {
        private readonly IDadoColetaManutencaoRepository dadoColetaManutencaoRepository;

        public DadoColetaManutencaoService(IDadoColetaManutencaoRepository dadoColetaEstruturadoRepository)
        {
            dadoColetaManutencaoRepository = dadoColetaEstruturadoRepository;
        }

        public DadoColetaManutencao ObterPorChave(int chave)
        {
            return dadoColetaManutencaoRepository.FindByKey(chave);
        }

        public PagedResult<DadoColetaManutencaoDTO> ConsultarDadoColetaManutencaoPorColetaInsumo(
            DadoColetaInsumoFilter filter)
        {
            return dadoColetaManutencaoRepository.ConsultarPorColetaInsumo(filter);
        }

        public DadoColetaManutencao ObterPorColetaInsumoId(int idColetaInsumo)
        {
            return dadoColetaManutencaoRepository.FindByColetaInsumoId(idColetaInsumo);
        }

        public void IncluirDadoColeta(DadoColetaManutencao dadoColeta)
        {
            IList<string> mensagens = new List<string>();
            ValidarExisteDadoColetaManutencaoComMesmaData(dadoColeta, mensagens);
            ValidarPeriodoManutencao(dadoColeta, mensagens);
            VerificarONSBusinessException(mensagens);

            AtualizarUsuarioDataHoraColetaInsumo(dadoColeta.ColetaInsumo);
            dadoColetaManutencaoRepository.Add(dadoColeta);
        }

        public void IncluirDadoColetaSeNaoExiste(IList<DadoColetaManutencao> dadoColetaList)
        {
            foreach (DadoColetaManutencao dadoColeta in dadoColetaList)
            {
                AtualizarUsuarioDataHoraColetaInsumo(dadoColeta.ColetaInsumo);
                dadoColetaManutencaoRepository.Add(dadoColeta);
            }
        }

        public void Excluir(DadoColetaManutencao dadoColeta)
        {
            AtualizarUsuarioDataHoraColetaInsumo(dadoColeta.ColetaInsumo);
            dadoColetaManutencaoRepository.Delete(dadoColeta);
        }

        public void AlterarDadoColeta(DadoColetaManutencao dadoColeta)
        {
            IList<string> mensagens = new List<string>();
            ValidarExisteDadoColetaManutencaoComMesmaData(dadoColeta, mensagens);
            ValidarPeriodoManutencao(dadoColeta, mensagens);
            VerificarONSBusinessException(mensagens);

            AtualizarUsuarioDataHoraColetaInsumo(dadoColeta.ColetaInsumo);
        }

        private void AtualizarUsuarioDataHoraColetaInsumo(ColetaInsumo coletaInsumo)
        {
            coletaInsumo.DataHoraAtualizacao = DateTime.Now;
            coletaInsumo.LoginAgenteAlteracao = UserInfo.UserName;
        }

        #region Validação
        private void VerificarONSBusinessException(IList<string> mensagens)
        {
            if (mensagens.Any())
            {
                throw new ONSBusinessException(mensagens);
            }
        }

        //[MS034] O sistema não deve permitir que se informe mais de uma manutenção 
        //para uma unidade geradora de usina em um mesmo período (data de início e término).
        private void ValidarExisteDadoColetaManutencaoComMesmaData(DadoColetaManutencao dadoColeta, IList<string> mensagens)
        {
            bool existeDadoColeta = dadoColeta.Id > 0
                ? dadoColetaManutencaoRepository.Any(dadoColeta.ColetaInsumo.Id,
                    dadoColeta.Gabarito.OrigemColeta.Id, dadoColeta.DataInicio, dadoColeta.DataFim, dadoColeta.Id)
                : dadoColetaManutencaoRepository.Any(dadoColeta.ColetaInsumo.Id,
                    dadoColeta.Gabarito.OrigemColeta.Id, dadoColeta.DataInicio, dadoColeta.DataFim);

            if (existeDadoColeta)
            {
                mensagens.Add(SGIPMOMessages.MS034);
            }
        }

        /// <summary>
        /// Verifica se a data de manutenção está contida dentro do período de manutenção da semana operativa.
        /// </summary>
        /// <param name="dado">Dado coleta de manutenção com peíodo a ser validado</param>
        /// <param name="mensagens">Linta contendo as mensagens de erro levantadas</param>
        private void ValidarPeriodoManutencao(DadoColetaManutencao dado, IList<string> mensagens)
        {
            DateTime dataInicio = dado.ColetaInsumo.SemanaOperativa.DataInicioManutencao;
            DateTime dataFim = dado.ColetaInsumo.SemanaOperativa.DataFimManutencao;

            if (!(dado.DataInicio < dataFim && dado.DataFim > dataInicio))
            {
                mensagens.Add(string.Format(SGIPMOMessages.MS063,
                    dataInicio.ToString("dd/MM/yyyy"), dataFim.ToString("dd/MM/yyyy")));
            }
        }
        #endregion
    }
}

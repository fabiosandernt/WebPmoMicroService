﻿using System.Collections.Generic;
using System.ServiceModel;
using ONS.Common.Wcf;
using ONS.SGIPMO.Domain.Entities;

namespace ONS.WEBPMO.Application.Services.PMO.Interfaces.Integrations
{
    using Common.Services;
    using ONS.SGIPMO.Domain.Entities.BDT;

    [ServiceContract]
    public interface IBDTService : IService
    {
        /// <summary>
        /// Consulta UGEs sem parametro
        /// </summary>
        /// <returns>Lista de UGEs</returns>
        [OperationContract]
        [UseNetDataContractSerializer]
        IList<UnidadeGeradora> ConsultarUnidadesGeradoras();

        /// <summary>
        /// Consulta de reservatorios
        /// Caso seja informado o parâmetro nomeExibicaoContem, será aplicado o filtro de LIKE (%nomeExibicaoContem%);
        /// Caso seja informado o parâmetro quantidadeMaxima, será retornado apenas a quantidade de registros informado;
        /// </summary>
        /// <param name="nomeExibicaoContem">Nome de exibição para filtrar</param>
        /// <returns>Lista de Reservatorios</returns>
        [OperationContract]
        [UseNetDataContractSerializer]
        IList<Reservatorio> ConsultarReservatorioPorNomeExibicao(string nomeExibicaoContem = "");

        /// <summary>
        /// Consulta reservatorios onde as chaves sejam iguais as passadas por parametro
        /// </summary>
        /// <param name="chaves">Nome de exibição para filtrar</param>
        /// <returns>Lista de Reservatorios</returns>
        [OperationContract]
        [UseNetDataContractSerializer]
        IList<Reservatorio> ConsultarReservatorioPorChaves(params string[] chaves);

        /// <summary>
        /// Consulta usinas
        /// Caso seja informado o parâmetro nomeExibicaoContem, será aplicado o filtro de LIKE (%nomeExibicaoContem%)
        /// </summary>
        /// <param name="nomeExibicaoContem">Nome de exibição para filtrar</param>
        /// <returns>Lista de Usinas</returns>
        [OperationContract]
        [UseNetDataContractSerializer]
        IList<Usina> ConsultarUsinaPorNomeExibicao(string nomeExibicaoContem = "");

        /// <summary>
        /// Consulta usinas onde as chaves sejam iguais as passadas por parametro
        /// </summary>
        /// <param name="chaves">Nome de exibição para filtrar</param>
        /// <returns>Lista de Usinas</returns>
        [OperationContract]
        [UseNetDataContractSerializer]
        IList<Usina> ConsultarUsinaPorChaves(params string[] chaves);

        /// <summary>
        /// Consulta usinas sem parametro
        /// </summary>
        /// <returns>Lista de Usinas</returns>
        [OperationContract]
        [UseNetDataContractSerializer]
        IList<Usina> ConsultarUsinas();

        /// <summary>
        /// Consulta unidades geradoras onde as chaves sejam iguais as passadas por parametro
        /// </summary>
        /// <param name="chaves">Nome de exibição para filtrar</param>
        /// <returns>Lista de Unidades Geradoras</returns>
        [OperationContract]
        [UseNetDataContractSerializer]
        IList<UnidadeGeradora> ConsultarUnidadeGeradoraPorChaves(params string[] chaves);

        /// <summary>
        /// Consulta unidades geradoras de uma Usina especifica
        /// </summary>
        /// <param name="chaveUsina">Nome de exibição para filtrar</param>
        /// <returns>Lista de Unidades Geradoras</returns>
        [OperationContract]
        [UseNetDataContractSerializer]
        IList<UnidadeGeradora> ConsultarUnidadeGeradoraPorUsina(string chaveUsina);

        /// <summary>
        /// Consulta todos os subsistemas na BDT
        /// </summary>
        /// <returns>Lista de subsistemas</returns>
        [OperationContract]
        [UseNetDataContractSerializer]
        IList<Subsistema> ConsultarTodosSubsistemas();

        /// <summary>
        /// Consulta todos os subsistemas na BDT
        /// </summary>
        /// <returns>Lista de subsistemas</returns>
        [OperationContract]
        [UseNetDataContractSerializer]
        IList<Subsistema> ConsultarSubsistemasAtivos();

        /// <summary>
        /// Consulta todos agentes que contém valor parcial (like) passado pelo parametro <paramref name="nomeParcial"/>
        /// </summary>
        /// <param name="nomeParcial">Valor parcial do nome do agente</param>
        /// <returns>Lista de Agentes</returns>
        [OperationContract]
        [UseNetDataContractSerializer]
        IList<Agente> ConsultarAgentesPorNome(string nomeParcial, int top = int.MaxValue);


        /// <summary>
        /// Consulta todos agentes com as chaves informadas
        /// </summary>
        /// <param name="chaves">Chaves dos Agentes</param>
        /// <returns>Lista de Agentes</returns>
        [OperationContract]
        [UseNetDataContractSerializer]
        IList<Agente> ConsultarAgentesPorChaves(params int[] chaves);

        /// <summary>
        /// Consulta todos os submercados
        /// </summary>
        /// <returns>Lista de Submercados</returns>
        [OperationContract]
        [UseNetDataContractSerializer]
        IList<SubmercadoPMO> ConsultarSubmercados();

        /// <summary>
        /// Consulta todos os submercados
        /// </summary>
        /// <returns>Lista de Submercados</returns>
        [OperationContract]
        [UseNetDataContractSerializer]
        IList<UsinaPEM> ConsultarDadosUsinasVisaoPEM();

        /// <summary>
        /// Consulta dados de todos os REEs ativos
        /// </summary>
        /// <returns>Lista de todos os REEs ativos</returns>
        [OperationContract]
        [UseNetDataContractSerializer]
        IList<ReservatorioEE> ConsultarReservatoriosEquivalentesDeEnergiaAtivos();

    }
}
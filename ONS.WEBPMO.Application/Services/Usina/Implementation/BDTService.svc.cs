    using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ONS.Common.IoC;
using AutoMapper;
using ONS.SGIPMO.Domain.Entities.BDT;

namespace ONS.WEBPMO.Servico.Usina
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class BDTService : IBDTService
    {

        #region ReservatorioEE
        /// <summary>
        /// Consulta dados de todos os REEs ativos
        /// </summary>
        /// <returns>Lista de todos os REEs ativos</returns>
        /// 
        public IList<ReservatorioEE> ConsultarReservatoriosEquivalentesDeEnergiaAtivos()
        {
            var service = ApplicationContext.Resolve<ONS.SGIPMO.Domain.Services.Integrations.IBDTService>();
            var lista = service.ConsultarReservatoriosEquivalentesDeEnergiaAtivos();
           
            return lista;
        }
        #endregion

        #region Reservatorio

        /// <summary>
        /// Consulta de reservatorios
        /// Caso seja informado o parâmetro nomeExibicaoContem, será aplicado o filtro de LIKE (%nomeExibicaoContem%);
        /// Caso seja informado o parâmetro quantidadeMaxima, será retornado apenas a quantidade de registros informado;
        /// </summary>
        /// <param name="nomeExibicaoContem">Nome de exibição para filtrar</param>
        /// <returns>Lista de Reservatorios</returns>
        public IList<Reservatorio> ConsultarReservatorioPorNomeExibicao(string nome = "")
        {
            var service = ApplicationContext.Resolve<ONS.SGIPMO.Domain.Services.Integrations.IBDTService>();
            var lista = service.ConsultarReservatorioPorNomeExibicao(nome);
            var result = Mapper.Map<IList<Reservatorio>>(lista);
            return result;
        }


        /// <summary>
        /// Consulta reservatorios onde as chaves sejam iguais as passadas por parametro
        /// </summary>
        /// <param name="chaves">Nome de exibição para filtrar</param>
        /// <returns>Lista de Reservatorios</returns>
        public IList<Reservatorio> ConsultarReservatorioPorChaves(params string[] chaves)
        {
            var service = ApplicationContext.Resolve<ONS.SGIPMO.Domain.Services.Integrations.IBDTService>();
            var lista = service.ConsultarReservatorioPorChaves(chaves);
            var result = Mapper.Map<IList<Reservatorio>>(lista);
            return result;
        }

        #endregion

        #region Usina

        /// <summary>
        /// Consulta usinas
        /// Caso seja informado o parâmetro nomeExibicaoContem, será aplicado o filtro de LIKE (%nomeExibicaoContem%)
        /// </summary>
        /// <param name="nomeExibicaoContem">Nome de exibição para filtrar</param>
        /// <returns>Lista de Usinas</returns>
        public IList<Usina> ConsultarUsinaPorNomeExibicao(string nome = "")
        {
            var service = ApplicationContext.Resolve<ONS.SGIPMO.Domain.Services.Integrations.IBDTService>();
            var lista = service.ConsultarUsinaPorNomeExibicao(nome);
            var result = Mapper.Map<IList<Usina>>(lista);
            return result;
        }

        /// <summary>
        /// Consulta usinas onde as chaves sejam iguais as passadas por parametro
        /// </summary>
        /// <param name="chaves">Nome de exibição para filtrar</param>
        /// <returns>Lista de Usinas</returns>
        public IList<Usina> ConsultarUsinaPorChaves(params string[] chaves)
        {
            var service = ApplicationContext.Resolve<ONS.SGIPMO.Domain.Services.Integrations.IBDTService>();
            var lista = service.ConsultarUsinaPorChaves(chaves);
            //this.PreencherGabarito<ONS.SGIPMO.Domain.Entities.Usina>(lista);
            var result = Mapper.Map<IList<Usina>>(lista);
            return result;
        }

        /// <summary>
        /// Consulta usinas sem parametro
        /// </summary>
        /// <returns>Lista de Usinas</returns>
        public IList<Usina> ConsultarUsinas()
        {
            var service = ApplicationContext.Resolve<ONS.SGIPMO.Domain.Services.Integrations.IBDTService>();
            var lista = service.ConsultarUsinas();
            //this.PreencherGabarito<ONS.SGIPMO.Domain.Entities.Usina>(lista);
            var result = Mapper.Map<IList<Usina>>(lista);
            return result;
        }

        public IList<UsinaPEM> ConsultarDadosUsinasVisaoPEM()
        {
            var service = ApplicationContext.Resolve<ONS.SGIPMO.Domain.Services.Integrations.IBDTService>();
            var lista = service.ConsultarDadosUsinasVisaoPEM();

            return lista;
        }

        #endregion

        #region Unidade Geradora

        /// <summary>
        /// Consulta unidades geradoras onde as chaves sejam iguais as passadas por parametro
        /// </summary>
        /// <param name="chaves">Nome de exibição para filtrar</param>
        /// <returns>Lista de Unidades Geradoras</returns>
        public IList<UnidadeGeradora> ConsultarUnidadeGeradoraPorChaves(params string[] chaves)
        {
            var service = ApplicationContext.Resolve<ONS.SGIPMO.Domain.Services.Integrations.IBDTService>();
            var lista = service.ConsultarUnidadeGeradoraPorChaves(chaves);
            var result = Mapper.Map<IList<UnidadeGeradora>>(lista);
            return result;
        }

        /// <summary>
        /// Consulta unidades geradoras sem parametro
        /// </summary>
        /// <returns>Lista de Unidades Geradoras</returns>
        public IList<UnidadeGeradora> ConsultarUnidadesGeradoras()
        {
            var service = ApplicationContext.Resolve<ONS.SGIPMO.Domain.Services.Integrations.IBDTService>();
            var lista = service.ConsultarUnidadesGeradoras();
            var result = Mapper.Map<IList<UnidadeGeradora>>(lista);
            return result;
        }

        /// <summary>
        /// Consulta unidades geradoras de uma Usina especifica
        /// </summary>
        /// <param name="chaveUsina">Nome de exibição para filtrar</param>
        /// <returns>Lista de Unidades Geradoras</returns>
        public IList<UnidadeGeradora> ConsultarUnidadeGeradoraPorUsina(string chave)
        {
            var service = ApplicationContext.Resolve<ONS.SGIPMO.Domain.Services.Integrations.IBDTService>();
            var lista = service.ConsultarUnidadeGeradoraPorUsina(chave);
            var result = Mapper.Map<IList<UnidadeGeradora>>(lista);
            return result;
        }

        #endregion


        #region Subsistemas

        /// <summary>
        /// Consulta todos os subsistemas na BDT
        /// </summary>
        /// <returns>Lista de subsistemas</returns>
        public IList<Subsistema> ConsultarTodosSubsistemas()
        {
            var service = ApplicationContext.Resolve<ONS.SGIPMO.Domain.Services.Integrations.IBDTService>();
            var lista = service.ConsultarTodosSubsistemas();
            var result = Mapper.Map<IList<Subsistema>>(lista);            
            return result;
        }

        /// <summary>
        /// Consulta todos os subsistemas na BDT
        /// </summary>
        /// <returns>Lista de subsistemas</returns>
        public IList<Subsistema> ConsultarSubsistemasAtivos()
        {
            var service = ApplicationContext.Resolve<ONS.SGIPMO.Domain.Services.Integrations.IBDTService>();
            var lista = service.ConsultarSubsistemasAtivos();
            var result = Mapper.Map<IList<Subsistema>>(lista);
            return result;
        }

        #endregion

        #region Agentes

        /// <summary>
        /// Consulta todos agentes que contém valor parcial (like) passado pelo parametro <paramref name="nomeParcial"/>
        /// </summary>
        /// <param name="nomeParcial">Valor parcial do nome do agente</param>
        /// <returns>Lista de Agentes</returns>
        public IList<Agente> ConsultarAgentesPorNome(string nome, int top = int.MaxValue)
        {
            var service = ApplicationContext.Resolve<ONS.SGIPMO.Domain.Services.Integrations.IBDTService>();
            var lista = service.ConsultarAgentesPorNome(nome, top);
            var result = Mapper.Map<IList<Agente>>(lista);
            return result;
        }


        /// <summary>
        /// Consulta todos agentes com as chaves informadas
        /// </summary>
        /// <param name="chaves">Chaves dos Agentes</param>
        /// <returns>Lista de Agentes</returns>        
        public IList<Agente> ConsultarAgentesPorChaves(params string[] chaves)
        {
            int[] chavesInt = null;
            if (chaves != null)
                chavesInt = chaves.Select(c => {
                    var valor = -1;
                    int.TryParse(c, out valor);
                    return valor;
                }).ToArray();

            var service = ApplicationContext.Resolve<ONS.SGIPMO.Domain.Services.Integrations.IBDTService>();
            var lista = service.ConsultarAgentesPorChaves(chavesInt);
            var result = Mapper.Map<IList<Agente>>(lista);
            return result;
        }

        #endregion

        #region Outros

        private void PreencherGabarito<T>(IList<T> origens)
            where T : SGIPMO.Domain.Entities.OrigemColeta
        {

            if (origens != null)
            {
                var service = ApplicationContext.Resolve<ONS.SGIPMO.Domain.Services.IOrigemColetaService>();
                origens.ToList().ForEach(o => {
                    T aux = service.ObterOrigemColetaPorChave<T>(o.Id);
                    if (aux != null)
                    {
                        o.Gabaritos = new HashSet<SGIPMO.Domain.Entities.Gabarito>( aux.Gabaritos.Take(1) );
                    }
                });


            }
        }

        #endregion

        #region Submercados

        /// <summary>
        /// Consulta Submercados na BDT
        /// </summary>
        /// <returns>Lista de Submercados</returns>
        public IList<SubmercadoPMO> ConsultarSubmercados()
        {
            var service = ApplicationContext.Resolve<ONS.SGIPMO.Domain.Services.Integrations.IBDTService>();
            var lista = service.ConsultarSubmercados();

            return lista;
        }

        #endregion

    }

}

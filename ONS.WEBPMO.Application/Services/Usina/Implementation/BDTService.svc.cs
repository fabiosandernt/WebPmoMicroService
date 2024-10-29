using ONS.WEBPMO.Domain.Entities.BDT;
using ONS.WEBPMO.Domain.Entities.Usina;
using ONS.WEBPMO.Domain.Entities.Usina.OrigemColetaUsina;

namespace ONS.WEBPMO.Servico.Usina
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class BDTService : IBDTService
    {
        public IList<Agente> ConsultarAgentesPorChaves(params string[] chaves)
        {
            throw new NotImplementedException();
        }

        public IList<Agente> ConsultarAgentesPorNome(string nome, int top = int.MaxValue)
        {
            throw new NotImplementedException();
        }

        public IList<UsinaPEM> ConsultarDadosUsinasVisaoPEM()
        {
            throw new NotImplementedException();
        }

        public IList<Reservatorio> ConsultarReservatorioPorChaves(params string[] chaves)
        {
            throw new NotImplementedException();
        }

        public IList<Reservatorio> ConsultarReservatorioPorNomeExibicao(string nome = "")
        {
            throw new NotImplementedException();
        }

        public IList<ReservatorioEE> ConsultarReservatoriosEquivalentesDeEnergiaAtivos()
        {
            throw new NotImplementedException();
        }

        public IList<SubmercadoPMO> ConsultarSubmercados()
        {
            throw new NotImplementedException();
        }

        public IList<Subsistema> ConsultarSubsistemasAtivos()
        {
            throw new NotImplementedException();
        }

        public IList<Subsistema> ConsultarTodosSubsistemas()
        {
            throw new NotImplementedException();
        }

        public IList<UnidadeGeradora> ConsultarUnidadeGeradoraPorChaves(params string[] chaves)
        {
            throw new NotImplementedException();
        }

        public IList<UnidadeGeradora> ConsultarUnidadeGeradoraPorUsina(string chave)
        {
            throw new NotImplementedException();
        }

        public IList<UnidadeGeradora> ConsultarUnidadesGeradoras()
        {
            throw new NotImplementedException();
        }

        public IList<Domain.Entities.Usina.OrigemColetaUsina.Usina> ConsultarUsinaPorChaves(params string[] chaves)
        {
            throw new NotImplementedException();
        }

        public IList<Domain.Entities.Usina.OrigemColetaUsina.Usina> ConsultarUsinaPorNomeExibicao(string nome = "")
        {
            throw new NotImplementedException();
        }

        public IList<Domain.Entities.Usina.OrigemColetaUsina.Usina> ConsultarUsinas()
        {
            throw new NotImplementedException();
        }
    }

}

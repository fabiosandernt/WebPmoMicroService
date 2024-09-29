namespace ONS.WEBPMO.Application.Services.PMO.Interfaces.OrigemColeta
{
    //[ServiceContract]
    public interface IOrigemColetaService 
    {
        
        
        PagedResult<OrigemColeta> ConsultarOrigemColetasGabaritoPaginado(GabaritoParticipantesFilter filter);

        T ObterOrigemColetaPorChaveOnline<T>(string id) where T : OrigemColeta;

        T ObterOrigemColetaPorChave<T>(string id) where T : OrigemColeta;

        
        
        OrigemColeta ObterOuCriarOrigemColetaPorId(string idOrigemColeta, TipoOrigemColetaEnum tipoOrigemColeta);

        
        
        IList<OrigemColeta> ConsultarOuCriarOrigemColetaPorIds(IList<string> idsOrigemColeta, TipoOrigemColetaEnum tipoOrigemColeta);

        
        
        IList<OrigemColeta> ConsultarOrigemColetaPorTipoNomeOnline(TipoOrigemColetaEnum tipoOrigemColeta, string nome);

        
        
        IList<UnidadeGeradora> ConsultarUnidadeGeradoraPorUsinaOnline(string idOrigemColeta);

        
        
        IList<Usina> ConsultarUsinaParticipanteGabaritoPorColetaInsumo(int idColetaInsumo);

        
        
        IList<UnidadeGeradora> ConsultarUnidadeGeradoParticipanteGabaritoPorColetaInsumoUsina(
            int idColetaInsumo, string idUsina);

        
        
        IList<UnidadeGeradora> ConsultarUnidadeGeradoParticipanteGabaritoPorColetaInsumo(int idColetaInsumo);

        
        
        IList<UnidadeGeradoraManutencaoSGIDTO> ConsultarUnidadeGeradoParticipanteGabaritoPorColetaInsumos(List<int> idColetaInsumos);

        
        void SincronizarOrigensColetaComBDT();
    }
}

namespace ONS.WEBPMO.Servico.Usina
{
    //[ServiceContract]
    [ServiceKnownType(typeof(List<CronogramaPMO>))]
    public interface IPMOService
    {
        /// <summary>
        /// Consulta PMO
        /// Parametros ano e mes, são obrigatórios para o retorno 
        /// </summary>
        /// <param name="ano">Ano para filtrar</param>
        /// <param name="mes">Mes para filtrar</param>
        /// <returns>Lista de CronogramaPMO</returns>
        
        List<CronogramaPMO> ObterCronogramaPMO(int ano, int mes);
    }
}

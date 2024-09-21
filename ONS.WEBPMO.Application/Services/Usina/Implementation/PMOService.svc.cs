namespace ONS.WEBPMO.Servico.Usina
{
    public class PMOService : IPMOService
    {
        #region Usina

        /// <summary>
        /// Consulta PMO
        /// Parametros ano e mes, são obrigatórios para o retorno 
        /// </summary>
        /// <param name="ano">Ano para filtrar</param>
        /// <param name="mes">Mes para filtrar</param>
        /// <returns>Lista de CronogramaPMO</returns>
        public List<CronogramaPMO> ObterCronogramaPMO(int ano, int mes)
        {
            var service = ApplicationContext.Resolve<ONS.SGIPMO.Domain.Services.IPMOService>();
            List<CronogramaPMO> result = new List<CronogramaPMO>();
            var filtro = new PMOFilter() { Ano = ano, Mes = mes };

            SGIPMO.Domain.Entities.PMO pmo = service.ObterPMOPorFiltroExterno(filtro);

            if (pmo != null && pmo.SemanasOperativas != null && pmo.SemanasOperativas.Count > 0)
            {
                foreach (SGIPMO.Domain.Entities.SemanaOperativa cron in pmo.SemanasOperativas)
                {
                    result.Add(new CronogramaPMO
                    {
                        DataReuniao = cron.DataReuniao.Date,
                        DataInicioSemana = cron.DataInicioSemana.Date,
                        DataFimSemana = cron.DataFimSemana.Date,
                        DataInicioManutencao = cron.DataInicioManutencao.Date,
                        DataFimManutencao = cron.DataFimManutencao.Date,
                        Situacao = cron.Situacao?.Descricao,
                        Revisao = cron.Revisao
                    });
                }
            }
            //a.    Data da Reunião: Date
            //b.    Início Semana: Date
            //c.	Término Semana: Date
            //d.	Início Manutenção: Date
            //e.	Término Manutenção: Date
            //f.	Estado / Processo Estudo: String
            //g.	Revisão: Int

            return result;
        }

        #endregion

    }

}

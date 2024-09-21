namespace ONS.WEBPMO.Domain.Entities.Usina
{
    public class CronogramaPMO : BaseObject
    {
        public CronogramaPMO()
        {
        }

        public DateTime DataReuniao { get; set; }
        public DateTime DataInicioSemana { get; set; }
        public DateTime DataFimSemana { get; set; }
        public DateTime DataInicioManutencao { get; set; }
        public DateTime DataFimManutencao { get; set; }
        public string Situacao { get; set; }
        public int Revisao { get; set; }

    }
}

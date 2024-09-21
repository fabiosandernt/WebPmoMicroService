using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ONS.WEBPMO.Application.DTO
{
    public class DadosAlteracaoSemanaOperativaDTO
    {
        public int Id { get; set; }
        public DateTime DataReuniao { get; set; }
        public DateTime DataInicioManutencao { get; set; }
        public DateTime DataFimManutencao { get; set; }
    }
}

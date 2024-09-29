

namespace ONS.WEBPMO.Domain.DTO
{
    public class ConvergirPLDDTO
    {
        public int IdSemanaOperativa { get; set; }
        public bool Convergir { get; set; }
        public byte[] VersaoSemanaOperativa { get; set; }
        public string ObservacoesConvergenciaPld { get; set; }

    }
}



namespace ONS.WEBPMO.Domain.DTO
{
    public class AberturaEstudoDTO
    {

        public int IdSemanaOperativa { get; set; }
        public int? IdEstudo { get; set; }
        public bool IsPadrao { get; set; }
        public byte[] VersaoPMO { get; set; }
        public byte[] VersaoSemanaOperativa { get; set; }

    }
}

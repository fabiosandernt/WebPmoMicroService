namespace ONS.WEBPMO.Application.DTO
{
    public class GabaritoParticipantesBaseDTO<TParticipantes>
    {
        public GabaritoParticipantesBaseDTO()
        {
            ParticipantesDTOList = new List<TParticipantes>();
        }

        public string NomeRevisao { get; set; }

        public IEnumerable<TParticipantes> ParticipantesDTOList { get; set;}
    }
}

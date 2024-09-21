namespace ONS.WEBPMO.Application.DTO
{
    using System;
    using System.Collections.Generic;

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

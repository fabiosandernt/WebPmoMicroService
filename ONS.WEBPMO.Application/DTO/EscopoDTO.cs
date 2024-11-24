namespace ONS.WEBPMO.Application.DTO
{
    public class EscopoDTO
    {
        /// <summary>
        /// Identificador único do escopo.
        /// </summary>
        public string IdEscopo { get; set; }

        /// <summary>
        /// Nome do escopo.
        /// </summary>
        public string NomeEscopo { get; set; }

        /// <summary>
        /// Tipo de escopo, caso seja necessário distinguir diferentes tipos.
        /// </summary>
        public string TipoEscopo { get; set; }

        /// <summary>
        /// Descrição adicional do escopo.
        /// </summary>
        public string Descricao { get; set; }
    }
}

namespace ONS.WEBPMO.Domain.Entities.PMO
{
    public class CategoriaInsumo : IdDescricaoBaseEntity
    {
        public bool UsoPmo { get; set; }
        public bool UsoMontador { get; set; }
        //public string TipoUsina { get; set; } Retirado, conforme solicitação de e-mail enviado em 29/08/2018 10:40h

        public CategoriaInsumo() { }

    }
}

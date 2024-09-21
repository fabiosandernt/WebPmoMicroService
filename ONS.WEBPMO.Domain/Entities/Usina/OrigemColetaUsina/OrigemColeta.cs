namespace ONS.WEBPMO.Domain.Entities.Usina.OrigemColetaUsina
{
    public abstract class OrigemColeta : BaseObject
    {
        public string Nome { get; set; }

        public string TipoOrigemColeta { get; set; }

        public virtual IList<Gabarito> Gabaritos { get; set; }

    }
}

namespace ONS.WEBPMO.Domain.Entities.PMO
{   
    public class DadoColetaNaoEstruturado : DadoColeta
    {

        public DadoColetaNaoEstruturado()
        {
            Arquivos = new List<Arquivo>();
        }

        public string Observacao { get; set; }

        public virtual IList<Arquivo> Arquivos { get; set; }

    }
}

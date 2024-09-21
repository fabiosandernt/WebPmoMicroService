namespace ONS.WEBPMO.Application.Services.PMO.Implementation.GeracaoBlocos.Metadados
{
    using Enums;
    using ONS.WEBPMO.Application.Services.PMO.Implementation.GeracaoBlocos.Enums;

    public class CampoFixo : Campo
    {
        public object Valor { get; private set; }
        public bool RepetirConteudo { get; private set; }

        public CampoFixo(
            int tamanho,
            object valor,
            TipoDadoRegistro tipoDado = TipoDadoRegistro.Texto,
            Alinhamento alinhamento = Alinhamento.Esquerda,
            bool repetirConteudo = false,
            bool adicaoEspaco = true)
            : base(tamanho, tipoDado, alinhamento, " ", adicaoEspaco)
        {
            Valor = valor;
            RepetirConteudo = repetirConteudo;
        }


    }
}
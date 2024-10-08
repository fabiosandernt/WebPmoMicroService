using ONS.WEBPMO.Application.Services.PMO.Implementation.GeracaoBlocos.Enums;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation.GeracaoBlocos.Metadados
{
    
    public class Campo
    {
        public int Tamanho { get; private set; }
        public TipoDadoRegistro TipoDado { get; private set; }
        public Alinhamento Alinhamento { get; private set; }
        public string SubstitutoValoresNulos { get; private set; }
        public bool AdicaoEspaco { get; private set; }

        public Campo(
            int tamanho,
            TipoDadoRegistro tipoDado = TipoDadoRegistro.Texto,
            Alinhamento alinhamento = Alinhamento.Esquerda,
            string substitutoValorNulo = " ",
            bool adicaoEspaco = true)
        {
            Tamanho = tamanho;
            TipoDado = tipoDado;
            Alinhamento = alinhamento;
            SubstitutoValoresNulos = substitutoValorNulo ?? string.Empty;
            AdicaoEspaco = adicaoEspaco;
        }
    }
}
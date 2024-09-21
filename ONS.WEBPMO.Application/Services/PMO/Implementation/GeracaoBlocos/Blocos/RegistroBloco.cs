namespace ONS.WEBPMO.Application.Services.PMO.Implementation.GeracaoBlocos.Blocos
{
    using System;
    using System.Text;

    using Enums;
    using Metadados;

    public class RegistroBloco
    {
        public string Chave { get; private set; }
        public RegistroConfiguracao Configuracoes { get; private set; }
        public object[] Dados { get; private set; }

        public RegistroBloco(string chaveRegistro, RegistroConfiguracao configuracoes, object[] dados = null)
        {
            Chave = chaveRegistro;
            Configuracoes = configuracoes;
            Dados = dados;
        }

        public void ToString(StringBuilder sbToAppend)
        {
            for (int i = 0, j = 0; i < Configuracoes.Length; i++)
            {
                Campo campo = Configuracoes[i];
                CampoFixo campoFixo = campo as CampoFixo;

                string valor = string.Empty;
                object dado = campoFixo == null ? Dados[j++] : campoFixo.Valor;

                if (dado == null)
                {
                    valor = campo.SubstitutoValoresNulos;
                }
                else
                {
                    switch (campo.TipoDado)
                    {
                        case TipoDadoRegistro.Numero:
                            valor = ReplaceNewLine(
                                dado.ToString().Replace(".", string.Empty).Replace(",", ".").Replace("+", string.Empty));
                            break;
                        case TipoDadoRegistro.Texto:
                            valor = ReplaceNewLine(dado.ToString(), " ");
                            break;
                        case TipoDadoRegistro.Data:
                            valor = dado is DateTime
                                ? ((DateTime)dado).ToString("dd/MM/yyyy")
                                : ReplaceNewLine(dado.ToString());
                            break;
                    }
                }

                if (valor.Length > campo.Tamanho)
                {
                    valor = valor.Substring(0, campo.Tamanho);
                }
                else
                {
                    bool repeteConteudo = campoFixo != null && campoFixo.RepetirConteudo;

                    switch (campo.Alinhamento)
                    {
                        case Alinhamento.Direita:
                            valor = valor.PadLeft(campo.Tamanho, repeteConteudo ? valor[0] : ' ');
                            break;
                        case Alinhamento.Esquerda:
                            valor = valor.PadRight(campo.Tamanho, repeteConteudo ? valor[0] : ' ');
                            break;
                    }
                }

                sbToAppend.Append(valor);
                sbToAppend.Append(campo.AdicaoEspaco ? " " : string.Empty);
            }

            sbToAppend.AppendLine();
        }

        private string ReplaceNewLine(string valor, string novoValor = "")
        {
            return valor
                .Replace(Environment.NewLine, novoValor)
                .Replace("\n", novoValor)
                .Replace("\r", novoValor)
                .Replace("\t", novoValor)
                .Trim();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            ToString(sb);
            return sb.ToString();
        }
    }
}
using System;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation.GeracaoBlocos.Metadados
{
    using System.Collections.Generic;
    using System.Linq;
    using Enums;
    using ONS.WEBPMO.Application.Services.PMO.Implementation.GeracaoBlocos.Enums;

    public class RegistroConfiguracao
    {
        private readonly List<Campo> campos;

        public RegistroConfiguracao()
        {
            campos = new List<Campo>();
        }

        public RegistroConfiguracao ConfigurarCampoFixo(
            int tamanho,
            object valor,
            TipoDadoRegistro tipoDado = TipoDadoRegistro.Texto,
            Alinhamento alinhamento = Alinhamento.Esquerda,
            bool conteudoRepetido = false,
            bool adicaoEspaco = true)
        {
            campos.Add(new CampoFixo(tamanho, valor, tipoDado, alinhamento, conteudoRepetido, adicaoEspaco));
            return this;
        }

        public RegistroConfiguracao ConfigurarCampo(
            int tamanho,
            TipoDadoRegistro tipoDado = TipoDadoRegistro.Texto,
            Alinhamento alinhamento = Alinhamento.Esquerda,
            string substitutoValorNulo = " ",
            bool adicaoEspaco = true)
        {
            campos.Add(new Campo(tamanho, tipoDado, alinhamento, substitutoValorNulo, adicaoEspaco));
            return this;
        }

        public RegistroConfiguracao ConfigurarSeparador(int tamanho = 1, char separador = ' ')
        {
            campos.Add(new CampoFixo(tamanho, separador, adicaoEspaco: false));
            return this;
        }

        public Campo this[int index] { get { return campos[index]; } }

        public int Length { get { return campos.Count; } }

        public int Count(Func<Campo, bool> predicate)
        {
            return campos.Count(predicate);
        }
    }
}
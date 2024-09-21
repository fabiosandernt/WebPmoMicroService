using ONS.WEBPMO.Application.Services.PMO.Implementation.GeracaoBlocos.Metadados;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Enumerations;
using System.Text;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation.GeracaoBlocos.Blocos
{  
    public abstract class BlocoMontador
    {
        private readonly Dictionary<string, RegistroConfiguracao> mapaRegistros;
        private readonly IList<RegistroBloco> registros;

        public IList<DadoColeta> DadosColeta { get; set; }
        public IList<DadoColetaBloco> DadosColetaBloco { get; set; }
        public SemanaOperativa SemanaOperativa { get; set; }

        protected abstract void ConfigurarMapeamento();
        protected abstract void ProcessarDadosBloco();

        public BlocoMontador(IList<DadoColeta> dadosColeta, IList<DadoColetaBloco> dadosColetaBloco, SemanaOperativa semanaOperativa)
        {
            DadosColeta = dadosColeta;
            DadosColetaBloco = dadosColetaBloco;
            SemanaOperativa = semanaOperativa;

            mapaRegistros = new Dictionary<string, RegistroConfiguracao>();
            registros = new List<RegistroBloco>();
        }

        protected RegistroConfiguracao ConfigurarRegistro(string chaveRegistro)
        {
            return ConfigurarRegistro(chaveRegistro, new RegistroConfiguracao());
        }

        protected RegistroConfiguracao ConfigurarRegistro(string chaveRegistro, RegistroConfiguracao configuracoes)
        {
            ValidarChaveRegistro(chaveRegistro);
            mapaRegistros.Add(chaveRegistro, configuracoes);
            return configuracoes;
        }

        protected BlocoMontador AdicionarRegistro(string chaveRegistro, params object[] dados)
        {
            ValidarConfiguracaoDados(chaveRegistro, dados);
            RegistroConfiguracao configuracoes = mapaRegistros[chaveRegistro];
            registros.Add(new RegistroBloco(chaveRegistro, configuracoes, dados));
            return this;
        }

        #region Validações
        private void ValidarConfiguracaoDados(string chaveRegistro, object[] dados)
        {
            if (mapaRegistros.ContainsKey(chaveRegistro))
            {
                RegistroConfiguracao configuracao = mapaRegistros[chaveRegistro];
                int quantidadeCampos = configuracao.Count(c => !(c is CampoFixo));
                int quantidadeDados = dados == null ? default : dados.Length;
                if (quantidadeCampos > quantidadeDados)
                {
                    throw new ArgumentOutOfRangeException(
                        string.Format(
                            @"O registro '{0}' do bloco '{1}' possui {2} campos configurados, mas foram adicionados {3} dados. Verifique a quantidade de dados informados.",
                            chaveRegistro, GetType().Name, quantidadeCampos, quantidadeDados));
                }
            }
            else
            {
                throw new ArgumentException(
                    string.Format("O registro '{0}' do bloco '{1}' não foi configurado.",
                                  chaveRegistro, GetType().Name));
            }
        }

        private void ValidarChaveRegistro(string chaveRegistro)
        {
            if (mapaRegistros.ContainsKey(chaveRegistro))
            {
                throw new ArgumentException(
                    string.Format("O registro '{0}' do bloco '{1}' já foi configurado.",
                                  chaveRegistro, GetType().Name));
            }
        }
        #endregion

        #region Métodos Auxiliares - Obtenção de dados
        protected object ObterValorGrandeza(IEnumerable<DadoColetaEstruturado> dados,
            int? posicaoGrandeza = null, int? posicaoInsumo = null, string valorDefault = "-",
            TipoPatamarEnum? patamar = null, TipoLimiteEnum? limite = null)
        {
            var dadosGrandeza = dados.AsQueryable();

            if (posicaoGrandeza != null)
            {
                dadosGrandeza = dadosGrandeza.Where(d => d.Grandeza.OrdemBlocoMontador == posicaoGrandeza);
            }

            if (posicaoInsumo != null)
            {
                dadosGrandeza = dadosGrandeza.Where(d => d.Grandeza.Insumo.OrdemBlocoMontador == posicaoInsumo);
            }

            if (patamar != null)
            {
                dadosGrandeza = dadosGrandeza.Where(d => d.TipoPatamar != null && d.TipoPatamar.Id == (int)patamar);
            }

            if (limite != null)
            {
                dadosGrandeza = dadosGrandeza.Where(d => d.TipoLimite != null && d.TipoLimite.Id == (int)limite);
            }

            DadoColetaEstruturado dado = dadosGrandeza.FirstOrDefault();

            return ObterValorGrandeza(dado, valorDefault);
        }

        protected object ObterValorGrandeza(DadoColetaEstruturado dado, string valorDefault = "-")
        {
            return dado == null || string.IsNullOrEmpty(dado.Valor) ? valorDefault : dado.Valor;
        }

        #endregion

        public void ToString(StringBuilder sbOutput)
        {
            ConfigurarMapeamento();
            ProcessarDadosBloco();

            foreach (RegistroBloco registro in registros)
            {
                ValidarConfiguracaoDados(registro.Chave, registro.Dados);
                registro.ToString(sbOutput);
            }
        }
    }
}
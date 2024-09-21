
using System.Text;

namespace ONS.WEBPMO.Application.Models.LogAuditoria
{  
    //[ModelBinder(typeof(EnumerableValueBinder))]
    public class ExtracaoLogAuditoriaModel
    {
        public string Id { get; set; }
        public string Estudo { get; set; }
        public string Agente { get; set; }
        public string Insumo { get; set; }
        public string Executor { get; set; }
        public string OrigemColeta { get; set; }
        public DateTime DataExecucao { get; set; }
        public bool Exportar { get; set; }

        public bool Reservado { get { return Exportar; } }

        public string IdLogico {
            get { 
                var info = new StringBuilder();
                info.AppendLine(this.Id);
                info.AppendLine(this.Estudo);
                info.AppendLine(this.Agente);
                info.AppendLine(this.Insumo);
                info.AppendLine(this.Executor);
                info.AppendLine(this.OrigemColeta);
                info.AppendLine(this.DataExecucao.ToString("dd/MM/yyyy HH:mm:ss"));
                return info.ToString();
            }
        }
    }
}
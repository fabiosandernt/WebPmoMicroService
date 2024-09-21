using System.Data.Entity;
using System.Data.SqlClient;
using System.Security.Cryptography;
using ONS.Common.Repositories.Impl;
using ONS.SGIPMO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ONS.Common.Util.Pagination;

namespace ONS.WEBPMO.Domain.Repositories.Impl
{
    public class LogNotificacaoRepository : Repository<LogNotificacao>, ILogNotificacaoRepository
    {
        public PagedResult<LogNotificacao> ObterLogNotificacao(LogNotificacaoFilter filter)
        {
            var query = EntitySet
                .Include(logNotificacao => logNotificacao.Agente)
                .AsQueryable();

            query = query.Where(logNotificacao => logNotificacao.SemanaOperativa.Id == filter.IdSemanaOperativa);

            if (filter.IdsAgentes.Any())
            {
                query = query.Where(logNotificacao => filter.IdsAgentes.Contains(logNotificacao.Agente.Id));
            }

            List<string> acoes = new List<string>();

            if (filter.Abertura)
            {
                acoes.Add("Abertura");
            }

            if (filter.Reabertura)
            {
                acoes.Add("Reabertura");
            }

            if (filter.Rejeicao)
            {
                acoes.Add("Rejeição");
            }

            if (acoes.Count() > 0)
                query = query.Where(logNotificacao => acoes.Contains(logNotificacao.Acao));

            int resutadosPorPagina = filter.PageSize;
            int skip = (filter.PageIndex - 1) * resutadosPorPagina;
            int quantidadeTotal = query.Count();

            IList<LogNotificacao> logNotificacaoResultado = query
               .OrderBy(logNotificacao => logNotificacao.DataEnvioNotificacao)
               .ThenBy(logNotificacao => logNotificacao.Agente.Nome)
               .Skip(skip)
               .Take(resutadosPorPagina)
               .ToList();

            return new PagedResult<LogNotificacao>(logNotificacaoResultado, quantidadeTotal, filter.PageIndex, resutadosPorPagina);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using ONS.Common.Repositories.Impl;
using ONS.Common.Util.Files;
using ONS.SGIPMO.Domain.Entities;

namespace ONS.WEBPMO.Domain.Repositories.Impl
{
    public class ArquivoRepository : Repository<Arquivo>, IArquivoRepository
    {
        public void DeletarPorIdGabarito(IList<int> idsGabarito)
        {
            var query = from g in Context.Set<Gabarito>()
                join dc in Context.Set<DadoColetaNaoEstruturado>() on g.Id equals dc.Gabarito.Id
                where idsGabarito.Contains(g.Id)
                select dc;

            var arquivos = query.SelectMany(d => d.Arquivos);

            Delete(arquivos);
        }

        /// <summary>
        /// Método que obtem o byte[] de um arquivo. Foi colocado de forma separada porque internamento ele muda o 
        /// commandTimeout uma vez que arquivos de 200MB precisam ser suportados.
        /// </summary>
        /// <param name="arquivo"></param>
        /// <returns></returns>
        public byte[] GetDataContentFile(Arquivo arquivo)
        {
            int? commandTimeoutAnterior = (Context as IObjectContextAdapter).ObjectContext.CommandTimeout;
            (Context as IObjectContextAdapter).ObjectContext.CommandTimeout = 999999999;
            byte[] retorno = arquivo.Content.Data;
            (Context as IObjectContextAdapter).ObjectContext.CommandTimeout = commandTimeoutAnterior; // Atribuindo o timeout que estava antes
            
            return retorno;
        }

        /// <summary>
        /// Gravação de arquivo de forma isolada a fim de evitar sobrecarregar o SaveChanges().
        /// </summary>
        /// <param name="instanciaArquivoAindaNaoSalvo"></param>
        /// <returns></returns>
        public Arquivo SalvarArquivoContentFile(Arquivo instanciaArquivoAindaNaoSalvo)
        {
            // Inserindo o arquivo no banco de dados utilizando a infraestrtura do entity. O tamanho máximo de arquivo 
            // inserido foi 160MB estando o banco de dados na mesma rede que o IIS.
            int? commandTimeoutAnterior = (Context as IObjectContextAdapter).ObjectContext.CommandTimeout;
            (Context as IObjectContextAdapter).ObjectContext.CommandTimeout = int.MaxValue;

            try
            {
                Add(instanciaArquivoAindaNaoSalvo);
            }
            finally
            {
                (Context as IObjectContextAdapter).ObjectContext.CommandTimeout = commandTimeoutAnterior; // Atribuindo o timeout que estava antes
            }

            return instanciaArquivoAindaNaoSalvo;
        }

        public IList<Arquivo> ConsultarArquivosAssociadosGabaritos(IList<int> idsGabarito)
        {
            var query = from g in Context.Set<Gabarito>()
                join sem in Context.Set<SemanaOperativa>() on g.SemanaOperativa.Id equals sem.Id
                join arqSem in Context.Set<ArquivoSemanaOperativa>() on sem.Id equals arqSem.SemanaOperativa.Id
                join arq in Context.Set<Arquivo>() on arqSem.Arquivo.Id equals arq.Id
                where idsGabarito.Contains(g.Id)
                select arq;

            return query.ToList();
        }

        public override void Delete(Arquivo arquivo)
        {   
            arquivo.Deleted = true;
            Context.SaveChanges();
        }

        public override void Delete(IEnumerable<Arquivo> entities)
        {
            foreach (Arquivo arquivo in entities)
            {
                arquivo.Deleted = true;
            }
            this.Context.SaveChanges();
        }

        public override void Add(Arquivo entity)
        {
            if (entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
                entity.Content.Id = entity.Id;
            }
            this.EntitySet.Add(entity);
            this.Context.SaveChanges();
        }

        /*public List<int> FindIdsByHashCodes(IList<UniqueIdentificationFileDownloadRequest> hashCodes)
        {
            List<int> idsRetorno = new List<int>();
            foreach (var hashCode in hashCodes)
            {
                var query = from g in Context.Set<Arquivo>()
                            where g.HashVerificacao.Equals(hashCode.HashCode) && g.Guid.Equals(hashCode.Guid)
                            select g.Id;
                if (query.Any())
                {
                    idsRetorno.AddRange(query.ToList());
                }    
            }
            return idsRetorno;
        }*/

    }
}

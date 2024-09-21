using System;
using System.Globalization;
using ONS.Common.Exceptions;
using ONS.Common.Seguranca;
using ONS.Common.Services.Impl;
using ONS.Common.Temp;
using ONS.Common.Util;
using ONS.Common.Util.Files;
using ONS.Common.Util.Pagination;

using ONS.SGIPMO.Domain.Entities;
using ONS.SGIPMO.Domain.Entities.DTO;
using ONS.SGIPMO.Domain.Entities.Enumerations;
using ONS.SGIPMO.Domain.Entities.Filters;
using ONS.SGIPMO.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using ONS.WEBPMO.Application.Services.PMO.Interfaces;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{
    public class ArquivoService : Service, IArquivoService
    {

        private readonly ISemanaOperativaService semanaOperativaService;
        private readonly IColetaInsumoService coletaInsumoService;
        private readonly IArquivoRepository arquivoRepository;

        public ArquivoService(IArquivoRepository arquivoRepository,
            IColetaInsumoService coletaInsumoService,
            ISemanaOperativaService semanaOperativaService)
        {
            this.arquivoRepository = arquivoRepository;
            this.coletaInsumoService = coletaInsumoService;
            this.semanaOperativaService = semanaOperativaService;
        }

        public Arquivo ObterArquivoDadoNaoEstruturado(Guid idArquivo)
        {
            return arquivoRepository.FindByKey(idArquivo);
        }

        public byte[] ObterArquivoDadoNaoEstruturadoEmBytes(Guid idArquivo)
        {
            Arquivo arquivo = ObterArquivoDadoNaoEstruturado(idArquivo);
            if (arquivo != null)
            {
                return arquivo.Content.Data;
            }
            return new byte[0];
        }

        public ResponseDownload ObterArquivosCompactados(RequestDownload request)
        {
            ResponseDownload responseDownload = new ResponseDownloadMemory();

            if (request.Ids.Any())
            {
                if (request.Ids.Count > 1)
                {
                    CompressedFileMemory compressedFile = Compression.CompactarArquivosEmMemoria(ObterArquivos, request.Ids,
                        request.SuggestedFilename);
                    responseDownload.GenerateReponseDownload(compressedFile.Content, compressedFile.Filename);
                }
                else
                {
                    Arquivo arquivo = ObterArquivo(request.Ids.First());
                    if (arquivo != null)
                    {
                        if (arquivo.Id == Guid.Empty)
                        {
                            responseDownload.GenerateReponseDownload(arquivo.Content.Data, request.SuggestedFilename);
                        }
                        else
                        {
                            responseDownload.GenerateReponseDownload(arquivoRepository.GetDataContentFile(arquivo), arquivo.Nome);
                        }
                    }
                }
            }
            return responseDownload;
        }

        public IList<CompressableFile> ObterArquivos(IList<string> list)
        {
            var compressableFiles = new List<CompressableFile>();

            foreach (string idArquivo in list)
            {
                Arquivo arquivo = ObterArquivo(idArquivo);
                //if (arquivo != null && arquivo.Id > 0)
                //if (arquivo != null && !string.IsNullOrWhiteSpace(arquivo.Id))
                if (arquivo != null && arquivo.Id != Guid.Empty)
                {
                    compressableFiles.Add(new CompressableFile()
                    {
                        Content = arquivoRepository.GetDataContentFile(arquivo),
                        Filename = arquivo.Nome
                    });
                }
            }
            return compressableFiles;
        }

        public Arquivo ObterArquivo(string idArquivo)
        {
            Arquivo arquivo = null;

            Guid idGuid = Guid.Empty;
            try
            {
                idGuid = Guid.Parse(idArquivo);
            }
            catch (Exception ex) { }

            if (idGuid != Guid.Empty)
            {
                arquivo = ObterArquivoDadoNaoEstruturado(idGuid);
            }
            else
            {
                try
                {
                    arquivo = new Arquivo();
                    arquivo.Nome = "ArquivoTemporario";
                    arquivo.Content = new BinaryData { Data = FileTemp.GetFileTemp(idArquivo) };
                }
                catch (Exception ex) { }
            }
            return arquivo;
        }

        public void LimparArquivosTemporariosUpload(ISet<ArquivoDadoNaoEstruturadoDTO> arquivos)
        {
            foreach (var arquivo in arquivos)
            {
                try
                {
                    if (arquivo.Id == Guid.Empty && !string.IsNullOrEmpty(arquivo.IdArquivoTemporario) && !arquivo.IdArquivoTemporario.StartsWith(UploadFileModel.PrefixDatabase))
                    {
                        FileTemp.DeleteFileTemp(arquivo.IdArquivoTemporario);
                    }
                }
                catch (Exception ex) { }
            }
        }

        public void SalvarDadoColetaNaoEstruturada(DadosGravacaoDadoColetaInsumoNaoEstruturadoDTO dto,
            DadosMonitoramentoColetaInsumoDTO dtoDadosAnalise, ColetaInsumo coletaInusmo = null)
        {
            coletaInsumoService.SalvarDadoColetaNaoEstruturada(dto, dtoDadosAnalise, coletaInusmo);
        }

        public void AprovarColetaDadosNaoEstruturados(DadosMonitoramentoColetaInsumoDTO dtoColeta,
            DadosGravacaoDadoColetaInsumoNaoEstruturadoDTO dtoDados)
        {
            coletaInsumoService.AprovarColetaDadosNaoEstruturados(dtoColeta, dtoDados);
        }

        public void RejeitarColetaDadosNaoEstruturados(DadosMonitoramentoColetaInsumoDTO dtoColeta,
            DadosGravacaoDadoColetaInsumoNaoEstruturadoDTO dtoDados)
        {
            coletaInsumoService.RejeitarColetaDadosNaoEstruturados(dtoColeta, dtoDados);
        }

        public void IniciarConvergenciaCCEE(InicializacaoConvergenciaCceeDTO dto)
        {
            semanaOperativaService.IniciarConvergenciaCCEE(dto);
        }

        public void PublicarResultados(PublicacaoResultadosDTO dto)
        {
            semanaOperativaService.PublicarResultados(dto);
        }

    }
}

﻿using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{
    public class DadoColetaNaoEstruturadoService : IDadoColetaNaoEstruturadoService
    {
        private IDadoColetaNaoEstruturadoRepository DadoColetaNaoEstruturadoRepository { get; set; }
        private ISemanaOperativaRepository SemanaOperativaRepository { get; set; }
        private IColetaInsumoRepository ColetaInsumoRepository { get; set; }
        private IGabaritoRepository GabaritoRepository { get; set; }
        private ISituacaoColetaInsumoRepository SituacaoColetaInsumoRepository { get; set; }
        private IArquivoRepository ArquivoRepository { get; set; }

        public DadoColetaNaoEstruturadoService(
            IDadoColetaNaoEstruturadoRepository dadoColetaNaoEstruturadoRepository,
            ISemanaOperativaRepository semanaOperativaRepository,
            IColetaInsumoRepository coletaInsumoRepository,
            IGabaritoRepository gabaritoRepository,
            ISituacaoColetaInsumoRepository situacaoColetaInsumoRepository,
            IArquivoRepository arquivoRepository)
        {
            DadoColetaNaoEstruturadoRepository = dadoColetaNaoEstruturadoRepository;
            SemanaOperativaRepository = semanaOperativaRepository;
            ColetaInsumoRepository = coletaInsumoRepository;
            GabaritoRepository = gabaritoRepository;
            SituacaoColetaInsumoRepository = situacaoColetaInsumoRepository;
            ArquivoRepository = arquivoRepository;
        }

        public DadoColetaNaoEstruturado ObterPorChave(int chave)
        {
            throw new NotImplementedException();
        }
    }
}

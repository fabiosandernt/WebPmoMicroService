using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pitang.ONS.Common.Services.Impl;
using Pitang.ONS.Gerlim.Domain.Entities;
using Pitang.ONS.Common.Services;
using Pitang.ONS.Gerlim.Domain.Repositories;
using Pitang.ONS.Gerlim.Domain.Services.Resources;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{
    public class LimiteEquipamentoService : Service<LimiteEquipamento, int>, ILimiteEquipamentoService
    {

        public LimiteEquipamentoService(ILimiteEquipamentoRepository limiteEquipamentoRepository)
            : base(limiteEquipamentoRepository)
        {
        }

        public void InserirLimiteEquipamento(LimiteEquipamento limiteEquipamento)
        {
            var dicionarioPropriedades = new Dictionary<string, object>();
            var listaErro = new List<string>();

            //[RG005]: Obrigatoriedade de escolha de um item de tipo de equipamento para relacionamento. [MS001]
            //[RG006]: Obrigatoriedade de escolha de um item de equipamento para relacionamento. [MS002]
            //[RG007]: Obrigatoriedade de escolha de um item de estação para relacionamento. [MS003]
            //[RG008]: Obrigatoriedade de escolha de um item de tipo de limite de equipamento para relacionamento. [MS004]
            //[RG009]: Obrigatoriedade de escolha de um item de período do dia para relacionamento. [MS005]
            //[RG010]: Obrigatoriedade de preenchimento do campo Valor do Limite quando o motivo do não preenchimento estiver vazio [MS006]
            dicionarioPropriedades.Add("Tipo de equipamento", limiteEquipamento.TipoEquipamento);
            dicionarioPropriedades.Add("Equipamento", limiteEquipamento.Equipamento);
            dicionarioPropriedades.Add("Estação", limiteEquipamento.EstacaoTensao);
            dicionarioPropriedades.Add("Tipo de limite de equipamento", limiteEquipamento.TipoLimiteEquipamento);
            dicionarioPropriedades.Add("Período do dia", limiteEquipamento.PeriodoDia);
            dicionarioPropriedades.Add("Valor do Limite", limiteEquipamento.ValorLimite);

            //[RG011]: Obrigatoriedade de preenchimento do campo Duração quando o motivo do não preenchimento for diferente de “NP” [MS007]
            //TODO: Não foi identificado o campo motivo.

            base.Repository.Add(limiteEquipamento);
        }


        #region "Funcionalidade para validação de regra de negocio"




        #endregion
    }
}

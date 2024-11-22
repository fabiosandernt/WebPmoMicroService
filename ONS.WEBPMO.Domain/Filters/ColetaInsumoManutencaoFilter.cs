﻿using AspNetCore.IQueryable.Extensions;
using AspNetCore.IQueryable.Extensions.Pagination;
using AspNetCore.IQueryable.Extensions.Sort;

namespace ONS.WEBPMO.Domain.Entities.Filters
{
    public class ColetaInsumoManutencaoFilter : ICustomQueryable, IQueryPaging, IQuerySort
    {
        public int IdColetaInsumo { get; set; }
        public byte[] Versao { get; set; }
        public string MotivoAlteracaoONS { get; set; }
        public string MotivoRejeicaoONS { get; set; }
        public int? Limit { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int? Offset { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Sort { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}

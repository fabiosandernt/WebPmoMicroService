
using ONS.WEBPMO.Domain.Entities.Base;
using ONS.WEBPMO.Domain.Enumerations;

namespace ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO
{
    public abstract class OrigemColeta : BaseEntity<string>
    {  
        public OrigemColeta()
        {   
            Gabaritos = new HashSet<Gabarito>();
        }

        public string Nome { get; set; }

        public string TipoOrigemColetaDescricao {
            get
            {
                return TipoOrigemColeta.ToString();
                //return TipoOrigemColeta.ToChar();
            }
            //Necessário para mapeamento no entity framework
            set {  }
        }

        public abstract TipoOrigemColetaEnum TipoOrigemColeta { get; }
        public virtual ISet<Gabarito> Gabaritos { get; set; }

        public override string ToString() {
            return Nome;
        }
    }

    public class OrigemColetaComparer : IEqualityComparer<OrigemColeta>
    {
        public bool Equals(OrigemColeta origemColetaX, OrigemColeta origemColetaY)
        {
            return origemColetaX.Id == origemColetaY.Id;
        }

        public int GetHashCode(OrigemColeta origemColeta)
        {
            return origemColeta.Id.GetHashCode();
        }
    }

    public static class QueryableExtensions
    {
        public static IQueryable<Gabarito> WhereIsOfType(this IQueryable<Gabarito> query, TipoOrigemColetaEnum tipoOrigemColeta)
        {
            switch (tipoOrigemColeta)
            {
                case TipoOrigemColetaEnum.Usina:
                    return query.Where(gabarito => gabarito.OrigemColeta is Usina);
                case TipoOrigemColetaEnum.Subsistema:
                    return query.Where(gabarito => gabarito.OrigemColeta is Subsistema);
                case TipoOrigemColetaEnum.UnidadeGeradora:
                    return query.Where(gabarito => gabarito.OrigemColeta is UnidadeGeradora);
                case TipoOrigemColetaEnum.Reservatorio:
                    return query.Where(gabarito => gabarito.OrigemColeta is Reservatorio);
                case TipoOrigemColetaEnum.GeracaoComplementar:
                    return query.Where(gabarito => gabarito.OrigemColeta == null);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        public static IQueryable<OrigemColeta> WhereIsOfType(this IQueryable<OrigemColeta> query, TipoOrigemColetaEnum tipoOrigemColeta)
        {
            switch (tipoOrigemColeta)
            {
                case TipoOrigemColetaEnum.Usina:
                    return query.Where(origemColeta => origemColeta is Usina);
                case TipoOrigemColetaEnum.Subsistema:
                    return query.Where(origemColeta => origemColeta is Subsistema);
                case TipoOrigemColetaEnum.UnidadeGeradora:
                    return query.Where(origemColeta => origemColeta is UnidadeGeradora);
                case TipoOrigemColetaEnum.Reservatorio:
                    return query.Where(origemColeta => origemColeta is Reservatorio);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}

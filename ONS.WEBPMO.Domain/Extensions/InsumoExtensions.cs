namespace ONS.WEBPMO.Domain.Entities.Extensions
{
    public static class InsumoExtensions
    {

        public static bool EhVolumeInicias(this Insumo insumo)
        {
            return insumo.Nome.Trim().ToLower().Equals("volumes iniciais");
        }
    }
}

namespace ONS.WEBPMO.Domain.Repositories.Impl.Repositories.BDT
{
    [UseDbContext(ConnectionStringsNames.BDTModel)]
    public class SubmercadoPMORepository : Repository<SubmercadoPMO>, ISubmercadoPMORepository
    {
        /// <summary>
        /// Consulta todos os Submercados na BDT
        /// </summary>
        /// <returns></returns>
        public IList<SubmercadoPMO> ConsultarTodos()
        {
            return EntitySet.ToList();
        }

    }
}

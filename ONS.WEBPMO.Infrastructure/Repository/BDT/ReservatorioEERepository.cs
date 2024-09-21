using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ONS.Common.Repositories.Impl;
using ONS.SGIPMO.Domain.Repositories.BDT;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Repositories.BDT
{
    using ONS.SGIPMO.Domain.Entities;
    using ONS.SGIPMO.Domain.Entities.BDT;

    [UseDbContext(ConnectionStringsNames.BDTModel)]
    public class ReservatorioEERepository : Repository<ReservatorioEE>, IReservatorioEERepository
    {
        public IList<ReservatorioEE> ConsultarReservatoriosEquivalentesDeEnergiaAtivos()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select "
                + "    id_reservatorioee as Id_reservatorioee,"
                + "    nom_curto as Nom_curto_reservatorioee,"
                + "    cod_reservatorioee as Cod_reservatorioee,"
                + "    '" + TipoOrigemColetaEnum.Reservatorio + "' as TipoOrigemColeta"//Setado diretamente
                + " from tb_reservatorioee"
                + " where dat_desativacao is null");

            var retorno = EntitySet.SqlQuery(sql.ToString()).ToList();

            return retorno;
        }

    }
}

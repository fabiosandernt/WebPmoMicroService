namespace ONS.WEBPMO.Domain.Repositories.Impl.Repositories.BDT
{
    [UseDbContext(ConnectionStringsNames.BDTModel)]
    public class UsinaPEMRepository : Repository<UsinaPEM>, IUsinaPEMRepository
    {
        /// <summary>
        /// Consulta usinas do PEM
        /// </summary>
        /// <returns>Lista de Usinas do PEM</returns>
        public IList<UsinaPEM> ConsultarDadosUsinasVisaoPEM()
        {
            string sql =
                "SELECT "
                //--############################################################
                //--Dados de USINA
                //--############################################################
                + " usi.nomecurto, --usina\n"
                + " usi.nomelongo,\n"
                + " usi.ido_usi idousina, --usina\n"
                + " usi.usi_id idusina, --usina\n"
                + " usi.tpusina_id tipousina, --Tipo usina\n"
                + " potencia.pot potenciainstalada, --potencia\n"
                + " ins.id_subsistema idsubsistema, --subsistema\n"
                + " usi.cod_aneel codaneel, --ceg\n"
                + " tb_modalidadeoperusiconj.cod_modalidadeoperusiconj codmodalidadeoperusiconj, --modalidade\n"
                + " usi.dtentrada dataentradausina, --usina\n"
                + " usi.dtprevista dataprevistausina, --usina\n"
                + " usi.dtdesativa datadesativacaousina, --usina\n"
                + " usi.dtentcom dataentradacom, --usina\n"
                + " usi.val_potmin valorpotenciaminima, --usina\n"
                //--############################################################
                //--Dados do Ponto de Conexão GENÉRICO
                //--############################################################
                + " case tb_niveltensao.id_niveltensao --ponto de conexão id\n"
                + "     when null then ntc.id_niveltensao\n"
                + " 	else tb_niveltensao.id_niveltensao\n"
                + " end idpontodeconexao , \n"
                + " case tb_niveltensao.ido_ons--ponto de conexão ido\n"
                + "     when null then ntc.ido_ons\n"
                + " 	else tb_niveltensao.ido_ons\n"
                + " end idoonspontodeconexao, \n"
                + " case tb_niveltensao.nom_curto--ponto de conexão nom_curto\n"
                + "     when null then ntc.nom_curto\n"
                + " 	else tb_niveltensao.nom_curto\n"
                + " end nomecurtopontodeconexao,\n"
                //--############################################################
                //--Dados INSTALAÇÃO
                //--############################################################
                + " ins.val_latitude valorlatitude,--latitude\n"
                + " ins.val_longitude valorlongitude,--longitude\n"
                + " ins.estad_id idestado, --UF\n"
                + " ins.sme_id idsme, --sme_id\n"
                + " ins.cos_id idcos, --cos_id\n"
                + " tb_conjuntousina.cod_conjuntousina conjuntousina --conjunto\n"
                + " FROM usi\n"
                + " LEFT JOIN(\n"
                + "            SELECT usi1.usi_id usi_id1,\n"
                + "              sum(potefe) pot\n"
                + "         FROM uge uge1 INNER JOIN usi usi1 ON Usi1.usi_id = uge1.usi_id\n"

                + "     GROUP BY usi1.usi_id) potencia ON potencia.usi_id1 = usi.usi_id\n"
                + " LEFT JOIN  tb_usiniveltensao ON tb_usiniveltensao.usi_id = usi.usi_id\n"
                + " LEFT JOIN  tb_niveltensao ON tb_niveltensao.id_niveltensao = tb_usiniveltensao.id_niveltensao\n"
                + " LEFT JOIN ins ON ins.ins_id = usi.ins_id\n"
                + " LEFT JOIN tb_modalidadeoperusiconj ON tb_modalidadeoperusiconj.id_modalidadeoperusiconj = usi.id_modalidadeoperusiconj\n"
                + " LEFT JOIN tb_conjuntousina ON tb_conjuntousina.id_conjuntousina = usi.id_conjuntousina\n"
                + " LEFT JOIN tb_conjusinaniveltensao ON tb_conjusinaniveltensao.id_conjuntousina = tb_conjuntousina.id_conjuntousina\n"
                + " LEFT JOIN tb_niveltensao ntc ON ntc.id_niveltensao = tb_conjusinaniveltensao.id_niveltensao\n";

            var retorno = EntitySet.SqlQuery(sql).ToList();
            retorno = retorno.ToList();

            return retorno;
        }

    }
}

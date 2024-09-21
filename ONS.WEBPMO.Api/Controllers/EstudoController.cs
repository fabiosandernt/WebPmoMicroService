using Microsoft.AspNetCore.Mvc;

namespace ONS.WEBPMO.Api.Controllers
{
    public class EstudoController : ControllerBase
    {
        private readonly ISemanaOperativaService semanaOperativaService;

        public EstudoController(ISemanaOperativaService semanaOperativaService)
        {
            this.semanaOperativaService = semanaOperativaService;
        }

        public ActionResult ConsultarEstudo(string term)
        {
            var semanasOperativas = semanaOperativaService.ConsultarEstudoPorNome(term);
            return Json(semanasOperativas.Select(s => new { Descricao = s.Nome, Chave = s.Id }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConsultarEstudos(AutoCompleteModel model)
        {
            var semanasOperativas = semanaOperativaService.ConsultarEstudoPorNome(model.Term);
            ClearResults(model, semanasOperativas);
            return Json(semanasOperativas.Select(s => new { Descricao = s.Nome, Chave = s.Id }), JsonRequestBehavior.AllowGet);
        }

        private static void ClearResults(AutoCompleteModel model, IList<SemanaOperativa> semanasOperativas)
        {
            IList<int> idsExcludedItems = model.RemovableKeysList;
            if (idsExcludedItems.Any())
            {
                var itemsToRemove = semanasOperativas.Where(linha => idsExcludedItems.Contains(linha.Id)).ToList();
                foreach (SemanaOperativa semanaOperativa in itemsToRemove) semanasOperativas.Remove(semanaOperativa);
            }
        }
    }
}
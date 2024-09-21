using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ONS.WEBPMO.Application.Models
{
    public class AutoCompleteModel
    {
        public string Term { get; set; }
        public string RemovableKeys { get; set; }

        public IList<int> RemovableKeysList
        {
            get
            {
                IList<int> retorno = new List<int>();
                if (!string.IsNullOrEmpty(RemovableKeys))
                {
                    string[] splittedIds = RemovableKeys.Split('|');
                    foreach (string splittedId in splittedIds)
                    {
                        try { retorno.Add(int.Parse(splittedId)); } catch (Exception ex) { }
                    }
                }
                return retorno;
            }
        }

    }
}
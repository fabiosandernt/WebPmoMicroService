using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONS.WEBPMO.Application.DTO
{
    public class RequestDownload
    {
        public RequestDownload()
        {
            Ids = new List<string>();
        }

        public IList<string> Ids { get; set; }
        public string SuggestedFilename { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONS.WEBPMO.Application.DTO
{

    public class ResponseDownloadMemory : ResponseDownload
    {
        public byte[] Content { get; set; }

        public override void GenerateReponseDownload(byte[] content, string filename)
        {
            Content = content;
            base.Filename = filename;
        }
    }
}

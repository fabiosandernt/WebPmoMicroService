using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONS.WEBPMO.Domain.Entities.Base
{
    public interface ILogicalDeletion
    {
        bool Deleted { get; set; }

    }
}

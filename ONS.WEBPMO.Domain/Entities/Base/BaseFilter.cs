using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ONS.WEBPMO.Domain.Entities.Base.BaseFilter;

namespace ONS.WEBPMO.Domain.Entities.Base
{
    public abstract class BaseFilter : IBaseFilter
    {
        public virtual int? Limit { get; set; } = 10;
        public virtual int? Offset { get; set; }
        public virtual string? Sort { get; set; }
    }

}

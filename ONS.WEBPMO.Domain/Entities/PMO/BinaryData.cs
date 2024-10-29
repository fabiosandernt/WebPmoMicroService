
using System.ComponentModel.DataAnnotations.Schema;

namespace ONS.WEBPMO.Domain.Entities.PMO
{
    [NotMapped]
    public class BinaryData
    {

        public Guid Id { get; set; }

        public byte[] Data { get; set; }
    }
}

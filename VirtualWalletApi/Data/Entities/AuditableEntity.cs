using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualWalletApi.Data.Entities
{
    public class AuditableEntity
    {
        public DateTime CreatedAt { set; get; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { set; get; }
    }
}

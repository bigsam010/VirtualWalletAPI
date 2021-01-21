using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualWalletApi.Data.Enums
{
    public enum ECustomerStatus
    {

        [Description("ACTIVE")]
        ACTIVE = 1,

        [Description("SUSPENDED")]
        SUSPENDED = 2
    }
}

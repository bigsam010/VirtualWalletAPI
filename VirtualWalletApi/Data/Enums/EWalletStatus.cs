using System;
using System.ComponentModel;

namespace VirtualWalletApi.Data.Enums
{
    public enum EWalletStatus
    {
        [Description("ACTIVE")]
        ACTIVE = 1,

        [Description("BLOCKED")]
        BLOCKED = 2
    }
}

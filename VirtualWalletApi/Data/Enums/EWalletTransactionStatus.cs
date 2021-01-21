using System;
using System.ComponentModel;

namespace VirtualWalletApi.Data.Enums
{
    public enum EWalletTransactionStatus
    {
        [Description("APPROVED")]
        APPROVED = 1,
        [Description("PENDING")]
        PENDING = 2,
        [Description("DECLINED")]
        DECLINED = 3

    }
}

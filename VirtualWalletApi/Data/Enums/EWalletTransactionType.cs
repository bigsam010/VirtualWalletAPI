using System;
using System.ComponentModel;

namespace VirtualWalletApi.Data.Enums
{
    public enum EWalletTransactionType
    {
        [Description("DEPOSIT")]
        DEPOSIT = 1,

        [Description("WITHDRAW")]
        WITHDRAW = 2

    }
}

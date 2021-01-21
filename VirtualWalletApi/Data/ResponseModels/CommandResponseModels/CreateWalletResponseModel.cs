using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualWalletApi.Data.ResponseModels.CommandResponseModels
{
    public class CreateWalletResponseModel
    {
        public Guid Id { set; get; }
        public string Currency { set; get; }
        public string AccountNumber { set; get; }
        public string Status { set; get; }
        public decimal Balance { set; get; }
        public string Label { set; get; }
    }
}

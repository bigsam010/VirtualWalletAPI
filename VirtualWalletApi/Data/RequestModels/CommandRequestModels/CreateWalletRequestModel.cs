using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualWalletApi.Data.ResponseModels.CommandResponseModels;

namespace VirtualWalletApi.Data.RequestModels.CommandRequestModels
{
    public class CreateWalletRequestModel : IRequest<CreateWalletResponseModel>
    {
        public string Label { set; get; }
    }
}

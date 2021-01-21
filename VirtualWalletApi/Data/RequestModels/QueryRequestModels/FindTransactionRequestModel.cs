using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualWalletApi.Data.ResponseModels.QueryResponseModels;

namespace VirtualWalletApi.Data.RequestModels.QueryRequestModels
{
    public class FindTransactionRequestModel : IRequest<FindTransactionResponseModel>
    {
        public Guid TransId { set; get; }
    }
}

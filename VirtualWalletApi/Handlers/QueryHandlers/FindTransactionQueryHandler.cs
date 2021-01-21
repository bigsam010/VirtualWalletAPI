using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VirtualWalletApi.Data.Entities;
using VirtualWalletApi.Data.Persistence;
using VirtualWalletApi.Data.RequestModels.QueryRequestModels;
using VirtualWalletApi.Data.ResponseModels.QueryResponseModels;
using VirtualWalletApi.Utilities;

namespace VirtualWalletApi.Handlers.QueryHandlers
{
    public class FindTransactionQueryHandler : IRequestHandler<FindTransactionRequestModel, FindTransactionResponseModel>
    {
        private readonly IRepository<WalletTransaction> _walletTransRepo;
        private readonly IMapper _mapper;

        public FindTransactionQueryHandler(IRepository<WalletTransaction> walletTransRepo, IMapper mapper)
        {
            _walletTransRepo = walletTransRepo;
            _mapper = mapper;

        }

        public async Task<FindTransactionResponseModel> Handle(FindTransactionRequestModel request, CancellationToken cancellationToken)
        {
            var trans = _walletTransRepo.SingleOrDefault(t => t.Id == request.TransId&&t.CustomerId== WebHelper.CurrentCustomerId);
            if (trans == null)
            {
                throw new ArgumentException("Transaction not found");
            }
            return _mapper.Map<FindTransactionResponseModel>(trans);
        }
    }
}

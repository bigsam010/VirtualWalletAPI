using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VirtualWalletApi.Data.Entities;
using VirtualWalletApi.Data.Persistence;
using VirtualWalletApi.Data.RequestModels.CommandRequestModels;
using VirtualWalletApi.Data.ResponseModels.CommandResponseModels;
using VirtualWalletApi.Utilities;

namespace VirtualWalletApi.Handlers.CommandHandlers
{
    public class CreateWalletCommandHandler : IRequestHandler<CreateWalletRequestModel, CreateWalletResponseModel>
    {

        private readonly IRepository<Wallet> _walletRepo;
        private readonly IMapper _mapper;

        public CreateWalletCommandHandler( IRepository<Wallet> walletRepo, IMapper mapper)
        {
            _walletRepo = walletRepo;
            _mapper = mapper;
        }
        public async Task<CreateWalletResponseModel> Handle(CreateWalletRequestModel request, CancellationToken cancellationToken)
        {
            var walletLabel = request.Label;
            if (!String.IsNullOrEmpty(request.Label))
            {
                if (_walletRepo.SingleOrDefault(w => w.CustomerId == WebHelper.CurrentCustomerId && w.Label == request.Label) != null)
                {
                    throw new ArgumentException("Wallet with this label already exist");
                }
            }

            else
            {
                walletLabel = Util.NewWalletLabel();
                //ensure wallet label is unique for this customer
                while (_walletRepo.SingleOrDefault(w => w.CustomerId == WebHelper.CurrentCustomerId && w.Label == walletLabel) != null)
                {
                    walletLabel = Util.NewWalletLabel();
                }
            }

            var walletAccountNumber = Util.NewWalletAccountNumber();
            //ensure wallet accountnumber is unique
            while (_walletRepo.SingleOrDefault(w => w.AccountNumber == walletAccountNumber) != null)
            {
                walletAccountNumber = Util.NewWalletAccountNumber();
            }
            var wallet = new Wallet
            {
                AccountNumber = walletAccountNumber,
                CustomerId = WebHelper.CurrentCustomerId,
                Id = Guid.NewGuid(),
                Label = walletLabel

            };
            await _walletRepo.AddAsync(wallet);
            return _mapper.Map<CreateWalletResponseModel>(wallet);

        }
    }
}

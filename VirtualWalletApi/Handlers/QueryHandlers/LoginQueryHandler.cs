using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VirtualWalletApi.Data.Entities;
using VirtualWalletApi.Data.Enums;
using VirtualWalletApi.Data.Persistence;
using VirtualWalletApi.Data.RequestModels.QueryRequestModels;
using VirtualWalletApi.Data.ResponseModels.QueryResponseModels;
using VirtualWalletApi.Utilities;

namespace VirtualWalletApi.Handlers.QueryHandlers
{
    public class LoginQueryHandler : IRequestHandler<LoginRequestModel, LoginResponseModel>
    {
        private readonly IRepository<Customer> _customerRepo;
        private readonly IMapper _mapper;

        public LoginQueryHandler(IRepository<Customer> customerRepo, IMapper mapper)
        {
            _customerRepo = customerRepo;
            _mapper = mapper;

        }
        public async Task<LoginResponseModel> Handle(LoginRequestModel request, CancellationToken cancellationToken)
        {
            var customer = _customerRepo.SingleOrDefault(c => c.Email == request.Email);
            if (customer == null)
            {
                throw new ArgumentException("Invalid login credentials");
            }
            var passwordHasher = new PasswordHasher<Customer>();
            var isPasswordValid = passwordHasher.VerifyHashedPassword(customer, customer.Password, request.Password);

            if (isPasswordValid == PasswordVerificationResult.Failed)
            {
                throw new ArgumentException("Invalid login credentials");
            }
            else if (customer.Status != ECustomerStatus.ACTIVE)
            {
                throw new ArgumentException("Customer account has been suspended");
            }

            var response = _mapper.Map<LoginResponseModel>(customer);
            response.Token = JWTHelper.GetJWTToken(customer);
            return response;

        }
    }
}

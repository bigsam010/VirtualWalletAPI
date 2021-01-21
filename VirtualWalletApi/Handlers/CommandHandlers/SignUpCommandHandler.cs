using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
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
    public class SignUpCommandHandler : IRequestHandler<SignUpRequestModel, SignUpResponseModel>
    {
        private readonly IRepository<Customer> _customerRepo;
        private readonly IMapper _mapper;
        public SignUpCommandHandler(IRepository<Customer> customerRepo, IMapper mapper)
        {
            _customerRepo = customerRepo;
            _mapper = mapper;
        }
        public async Task<SignUpResponseModel> Handle(SignUpRequestModel request, CancellationToken cancellationToken)
        {
            //check if email already exists
            if (_customerRepo.SingleOrDefault(c => c.Email == request.Email) != null)
            {
                throw new ArgumentException("Email already exist");
            }
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                Firstname = request.Firstname,
                Lastname = request.Lastname

            };
            customer.Password = new PasswordHasher<Customer>().HashPassword(customer, request.Password);
            await _customerRepo.AddAsync(customer);

            customer = _customerRepo.SingleOrDefault(c => c.Email == request.Email);
            var response = _mapper.Map<SignUpResponseModel>(customer);
            response.Token = JWTHelper.GetJWTToken(customer);
            return response;

        }
    }
}

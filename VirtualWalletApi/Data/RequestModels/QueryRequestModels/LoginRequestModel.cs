using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using VirtualWalletApi.Data.ResponseModels.QueryResponseModels;

namespace VirtualWalletApi.Data.RequestModels.QueryRequestModels
{
    public class LoginRequestModel : IRequest<LoginResponseModel>
    {
        [Required]
        [EmailAddress]
        public string Email { set; get; }
        [Required]
        public string Password { set; get; }
    }
}

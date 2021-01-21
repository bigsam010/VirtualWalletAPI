using MediatR;
using System.ComponentModel.DataAnnotations;
using VirtualWalletApi.Data.ResponseModels.CommandResponseModels;

namespace VirtualWalletApi.Data.RequestModels.CommandRequestModels
{
    public class SignUpRequestModel : IRequest<SignUpResponseModel>
    {

        [EmailAddress]
        public string Email { set; get; }
        [Required]
        public string Firstname { set; get; }
        [Required]
        public string Lastname { set; get; }
        public string Password { set; get; }
    }
}

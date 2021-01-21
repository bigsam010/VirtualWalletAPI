using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualWalletApi.Data.ResponseModels.CommandResponseModels
{
    public class SignUpResponseModel
    {
        public Guid Id { set; get; }
       public string Email { set; get; }
        public string Firstname { set; get; }
        public string Lastname { set; get; }
        public string Status { set; get; }
        public string Token { set; get; }
        public string CreatedAt { set; get; }

    }
}

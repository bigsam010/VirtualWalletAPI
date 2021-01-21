using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualWalletApi.Utilities
{
    public class WebHelper
    {
        private static IHttpContextAccessor _httpContextAccessor;
        public static Guid CurrentCustomerId
        {
            get
            {
                return Guid.Parse(_httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == "Id").FirstOrDefault()?.Value ?? "");

            }
        }
        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
    }
}

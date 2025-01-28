﻿using System.Security.Claims;

namespace MultiShop.Basket.LoginServices
{
    public class LoginService : ILoginService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public LoginService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string GetUserId => _contextAccessor.HttpContext?.User?.FindFirst("sub")?.Value
                               ?? _contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                               ?? string.Empty;
    }
}

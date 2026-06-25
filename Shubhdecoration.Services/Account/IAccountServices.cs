using Shubhdecoration.Data.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shubhdecoration.Services.Account
{
    public interface IAccountServices
    {
        Task<UserProfile> Login(LoginModel model);
    }
}

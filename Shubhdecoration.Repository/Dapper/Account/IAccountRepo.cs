using Shubhdecoration.Data.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shubhdecoration.Repository.Dapper.Account
{
    public interface IAccountRepo
    {
        Task<UserProfile> Login(LoginModel model);
        Task<bool> Registration(SignupModel model); 
        Task<List<UserListModel>> UserList();

    }
}

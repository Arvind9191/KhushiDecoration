using Microsoft.EntityFrameworkCore;
using Shubhdecoration.Data.Account;  
namespace Shubhdecoration.Services.Account
{
    public class AccountServices : IAccountServices
    {  
        public async Task<UserProfile> Login(LoginModel model)
        {
            UserProfile UserPro=new UserProfile();  
            object[] parameter = { model.UserName, model.Password };
            //UserProfile UserPro = (await _dbContext.Userprofile.FromSqlRaw("EXEC [SP_Login] {0},{1}", parameter).ToListAsync()).First();
            return UserPro;
        }
    }
}

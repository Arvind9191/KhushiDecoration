using Dapper;
using Shubhdecoration.Data.Account;
using Shubhdecoration.DataAccess.Dapper;
using System;
using System.Data;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Shubhdecoration.Repository.Dapper.Account
{
    public class AccountRepo : IAccountRepo
    {
        private readonly IConnecctions _connecctions;
        public AccountRepo(IConnecctions connecctions)
        {
            _connecctions = connecctions;
        }
        public async Task<UserProfile> Login(LoginModel model)
        {
            const string checkUserSql = @"SELECT COUNT(1) FROM UserApplication WHERE UserName = @UserName OR EmailId = @UserName;";
            const string loginSql = @"SELECT UserId, FName, LName, UserName, EmailId, MobileNo, UserRole 
                                      FROM UserApplication WHERE (username = @UserName OR EmailId = @UserName) 
                                      AND password = @Password;";
            try
            {
                using (IDbConnection connection = _connecctions.GetConnection())
                {
                    int userCount = await connection.ExecuteScalarAsync<int>(checkUserSql, new { UserName = model.UserName });
                    if (userCount == 0)
                    {
                        throw new Exception("Username or Email and password not exists.");
                    }
                    UserProfile? user = await connection.QueryFirstOrDefaultAsync<UserProfile>(loginSql, new
                    {
                        UserName = model.UserName,
                        Password = model.Password
                    });
                    if (user == null)
                    {
                        throw new Exception("Username or Email and password not exists.");
                    }
                    return user;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<bool> Registration(SignupModel model)
        {
            bool IsSuccess = false;
            const string checkquery = @"SELECT COUNT(1) FROM UserApplication  WHERE emailid = @EmailId OR mobileno = @MobileNo;";
            const string query = @" INSERT INTO UserApplication 
            (fname, lname, mobileno, emailid, username, password, userrole, isactive) 
            VALUES 
            (@FName, @LName, @MobileNo, @EmailId, @UserName, @Password, @UserRole, @IsActive);";

            string[] nameParts = model.Name.Trim().Split(' ', 2);
            string firstName = nameParts[0];
            string lastName = nameParts.Length > 1 ? nameParts[1] : string.Empty;
            try
            {
                using (IDbConnection connection = _connecctions.GetConnection())
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();
                    int existingUserCount = await connection.ExecuteScalarAsync<int>(checkquery, new
                    {
                        EmailId = model.Email,
                        MobileNo = model.Phone
                    });
                    if (existingUserCount > 0)
                    {
                        throw new Exception("Email or Mobile number already registered.");
                    }
                    var parameters = new
                    {
                        FName = firstName,
                        LName = lastName,
                        MobileNo = model.Phone,
                        EmailId = model.Email,
                        UserName = model.Email,
                        Password = model.Password,
                        UserRole = model.UserRole == 0 ? 2 : model.UserRole,
                        IsActive = true
                    };
                    int result = await connection.ExecuteAsync(query, parameters);
                    if (result > 0)
                    {
                        IsSuccess = true;
                    }
                    return IsSuccess;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        } 
        public async Task<List<UserListModel>> UserList()
        {
            const string checkUserSql = @"SELECT UserId, FName, LName, UserName, EmailId, MobileNo, UserRole, IsActive FROM UserApplication;";
            try
            {
                using (IDbConnection connection = _connecctions.GetConnection())
                { 
                    var users = await connection.QueryAsync<UserListModel>(checkUserSql); 
                    var userList = users.ToList(); 
                    if (userList.Count == 0)
                    {
                        throw new Exception("No user profiles exist in the system.");
                    } 
                    return userList;
                }
            }
            catch (Exception ex)
            { 
                throw;
            }
        }
    }
}
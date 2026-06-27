using Dapper; 
using Shubhdecoration.Data.Common;
using Shubhdecoration.DataAccess.Dapper; 
using System.Data; 
namespace Shubhdecoration.Repository.Dapper
{
    public class UnitOfWorks : IUnitOfWorks
    {
        private readonly IConnecctions _connecctions;
        ResponseModel response = new ResponseModel();
        public UnitOfWorks(IConnecctions connecctions)
        {
            _connecctions = connecctions;
        } 
        public async Task<ResponseModel> ExecuteAsync(string quiry, object parameter)
        {
            using (IDbConnection connection = _connecctions.GetConnection())
            {
                int rowsAffected = await connection.ExecuteAsync(quiry, parameter);
                response.Id = rowsAffected;
                response.IsSuccess = true;
            }
            return response;
        }
        public async Task<ResponseModel> ExecuteScalarAsync(string quiry, object parameter)
        {
            using (IDbConnection connection = _connecctions.GetConnection())
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                int existingUserCount = await connection.ExecuteScalarAsync<int>(quiry, parameter); 
                if (existingUserCount > 0)
                {
                    throw new Exception("Email or Mobile number already registered.");
                }
                int result = await connection.ExecuteAsync(quiry, parameter);
                if (result > 0)
                {
                    response.Id = result;
                    response.IsSuccess = true;
                }
                return response;
            }
        }
    }
}

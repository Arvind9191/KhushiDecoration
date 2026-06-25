using Dapper;
using Shubhdecoration.Data.Decoration;
using Shubhdecoration.DataAccess.Dapper;
using Shubhdecoration.Repository.Dapper.Common; 
namespace Shubhdecoration.Repository.Master
{
    public class MasterRepository : IMasterRepository
    {
        private readonly IConnecctions _connecction;
        private readonly ICommonRepository _common;
        public MasterRepository(IConnecctions connecction, ICommonRepository common)
        {
            _connecction = connecction;
            _common = common;
        }
        #region=============== Master Repository =============== 
        public async Task<bool> InsertCategoryAsync(Category model)
        {
            int insertedId = 0;
            bool Isuccess = false;
            try
            {
                string query = @"INSERT INTO categories (CategoryName, Description, IsActive, CreatedAt) 
                 VALUES (@CategoryName, @Description, @IsActive, @CreatedAt) 
                 RETURNING categoryid";
                var parameters = new
                {
                    CategoryName = model.CategoryName,
                    Description = model.Description,
                    IsActive = model.IsActive,
                    CreatedAt = model.CreatedAt
                };
                using (var connection = _connecction.GetConnection())
                {
                    insertedId = await connection.ExecuteScalarAsync<int>(query, parameters);
                }
                if (insertedId > 0)
                {
                    string fname = $"{insertedId}_category";
                    string imageUrl = await _common.UploadImageAsync(model.ImgFile, fname);

                    string upquery = @"UPDATE categories 
                       SET imageurl = @imageurl 
                       WHERE categoryid = @categoryid";
                    var parameter = new
                    {
                        categoryid = insertedId,
                        imageurl = imageUrl
                    };
                    using (var connection = _connecction.GetConnection())
                    {
                        int rowsAffected = await connection.ExecuteAsync(upquery, parameter);
                        if (rowsAffected > 0)
                        {
                            Isuccess = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return Isuccess;
        }
        #endregion=============== Master Repository ============
    }
}

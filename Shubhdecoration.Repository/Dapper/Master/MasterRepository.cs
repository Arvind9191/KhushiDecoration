using Dapper;
using Shubhdecoration.Data.Common;
using Shubhdecoration.Data.Decoration;
using Shubhdecoration.DataAccess.Dapper;
using Shubhdecoration.Repository.Dapper.Common;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
namespace Shubhdecoration.Repository.Dapper.Master
{
    public class MasterRepository : IMasterRepository
    {
        private readonly IConnecctions _connection;
        private readonly ICommonRepository _common;
        private readonly IUnitOfWorks _unitOfWorks;
        ResponseModel response = new ResponseModel();
        public MasterRepository(IConnecctions connection, ICommonRepository common, IUnitOfWorks unitOfWorks)
        {
            _connection = connection;
            _common = common;
            _unitOfWorks = unitOfWorks;
        }
        #region=============== Master Repository =============== 
        public async Task<bool> InsertCategoryAsync(Category model)
        {
            int insertedId = 0;
            bool Isuccess = false; 
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
                response = await _unitOfWorks.ExecuteScalarAsync(query, parameters);
                if (response.Id > 0)
                {
                    insertedId = response.Id;
                    string fname = $"{insertedId}_category";
                    string imageUrl = await _common.MegaUploadImage(model.ImgFile, fname);
                    string upquery = @"UPDATE categories 
                       SET imageurl = @imageurl 
                       WHERE categoryid = @categoryid";
                    var parameter = new
                    {
                        categoryid = insertedId,
                        imageurl = imageUrl
                    };
                    using (var connection = _connection.GetConnection())
                    {
                        response = await _unitOfWorks.ExecuteAsync(upquery, parameter);
                        if (response.Id > 0)
                        {
                            response.IsSuccess = true;
                            response.Id = insertedId;
                            Isuccess = true;
                        }
                    }
                }              
            return Isuccess;
        }


        public async Task<List<Category>> CategoryListOrSingle(int cId)
        { 
            string sql = @"
        SELECT 
            categoryid as CategoryId, 
            categoryname as CategoryName, 
            description as Description, 
            isactive as IsActive, 
            createdat as CreatedAt
             FROM categories 
             WHERE IsActive=true"; 
            var parameters = new DynamicParameters(); 
            if (cId > 0)
            {
                sql += " AND categoryid = @CategoryId";
                parameters.Add("CategoryId", cId);
            } 
            var categoryDictionary = new Dictionary<int, Category>();

            using (IDbConnection connection = _connection.GetConnection())
            { 
                var result = await connection.QueryAsync<Category>(sql,param: parameters); 
                return result.ToList();
            }
        }
        #endregion=============== Master Repository ============
    }
}

using Dapper;
using Shubhdecoration.Data.Decoration;
using Shubhdecoration.DataAccess.Dapper;
using Microsoft.Extensions.Logging;
using System.Data;

namespace Shubhdecoration.Repository.Dapper.Decoration
{
    public class DecorationRespository : IDecorationRepository
    {
        private readonly IConnecctions _connecctions;
        public DecorationRespository(IConnecctions connecctions)
        {
            _connecctions = connecctions;
        }
        public async Task<bool> CreateDecoCard(CreateCardModel model)
        {
            const string query = @"
                INSERT INTO DecoType (CategoryId, DecoName, Description, ImageUrl, Price, IsActive, CreatedAt) 
                VALUES (@CategoryId, @DecoName, @Description, @ImageUrl, @Price, @IsActive, @CreatedAt);";

            try
            {
                using (IDbConnection connection = _connecctions.GetConnection())
                {
                    int rowsAffected = await connection.ExecuteAsync(query, new
                    {
                        CategoryId = model.CategoryId,
                        DecoName = model.DecoName,
                        Description = model.Description,
                        ImageUrl = model.ImageUrl,
                        Price = model.Price,
                        IsActive = model.IsActive,
                    });
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database Error: {ex.Message}");
                return false;
            }
        }  
        public async Task<List<DecorationModel>> GetAllDecoration(int catId, int decoId)
        { 
            string sql = @"
        SELECT 
            c.categoryid, c.categoryname, c.description, c.imageurl, 
            d.decotypeid, d.deconame, d.description, d.imageurl, d.price 
        FROM Categories c 
        INNER JOIN DecoType d ON c.categoryid = d.categoryid
        WHERE c.isactive = true AND d.isactive = true";

            var parameters = new DynamicParameters();

            // 1. Agar CategoryId aati hai toh filter lagayein
            if (catId > 0)
            {
                sql += " AND c.categoryid = @CategoryId";
                parameters.Add("CategoryId", catId);
            }

            // 2. Agar DecoId aati hai toh additional filter lagayein
            if (decoId > 0)
            {
                sql += " AND d.decotypeid = @DecoId";
                parameters.Add("DecoId", decoId);
            }

            var decorationDictionary = new Dictionary<int, DecorationModel>();

            try
            {
                // '_connecctions' spelling thik kar lein agar aapke paas actual class mein alag hai
                using (IDbConnection connection = _connecctions.GetConnection())
                {
                    await connection.QueryAsync<DecorationModel, CardViewModel, DecorationModel>(
                        sql,
                        (category, card) =>
                        {
                            // Dictionary mein check karein ki category pehle se hai ya nahi
                            if (!decorationDictionary.TryGetValue(category.CategoryId, out var currentCategory))
                            {
                                currentCategory = category;
                                currentCategory.CardList = new List<CardViewModel>();
                                decorationDictionary.Add(currentCategory.CategoryId, currentCategory);
                            }

                            // Card data map karein (splitOn column ke baad ka data)
                            if (card != null)
                            {
                                card.Title = card.DecoName; // ViewModel mapping
                                currentCategory.CardList.Add(card);
                            }

                            return currentCategory;
                        },
                        param: parameters,
                        splitOn: "decotypeid" // Yeh split string bilkul sahi hai
                    );
                } 
                return decorationDictionary.Values.ToList();
            }
            catch (Exception ex)
            {
                // Asal project mein throw ke sath log karna zaroori hai (e.g., _logger.LogError(ex, ...))
                throw;
            }
        }
    }
}
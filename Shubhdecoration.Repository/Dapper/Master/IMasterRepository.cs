using Shubhdecoration.Data.Decoration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shubhdecoration.Repository.Dapper.Master
{
    public interface IMasterRepository
    {
        #region=============== Master Repository ===============
        Task<List<Category>> CategoryListOrSingle(int cId);
        Task<bool> InsertCategoryAsync(Category model);
        #endregion=============== Master Repository ============
    }
}

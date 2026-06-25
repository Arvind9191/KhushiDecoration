using Shubhdecoration.Data.Decoration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shubhdecoration.Repository.Master
{
    public interface IMasterRepository
    {
        #region=============== Master Repository ===============
        Task<bool> InsertCategoryAsync(Category model);
        #endregion=============== Master Repository ============
    }
}

using Shubhdecoration.Data.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shubhdecoration.Repository.Dapper
{
    public interface IUnitOfWorks
    {
        Task<ResponseModel> ExecuteAsync(string quiry,object parameter);
        Task<ResponseModel> ExecuteScalarAsync(string quiry,object parameter);
    }
}

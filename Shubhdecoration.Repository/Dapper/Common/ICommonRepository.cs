using Microsoft.AspNetCore.Http;
using Shubhdecoration.Data.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shubhdecoration.Repository.Dapper.Common
{
    public interface ICommonRepository
    {
        List<Dropdown> DdlCotegoties();
        //Task<string> ImgbbUploadImage(IFormFile file,string fname);
        Task<string> MegaUploadImage(IFormFile file, string fileName);
    }
} 

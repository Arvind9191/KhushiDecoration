using CG.Web.MegaApiClient;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Shubhdecoration.Data.Account;
using Shubhdecoration.Data.Common;
using Shubhdecoration.DataAccess.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Shubhdecoration.Repository.Dapper.Common
{
    public class CommonRepository : ICommonRepository
    {
        private readonly HttpClient _httpClient;
        //private readonly string _apiKey;
        //private readonly string _apiUrl;
        List<Dropdown> dropdowns = new List<Dropdown>();
        private readonly IConnecctions _connecctions;
        private static IConfiguration _configuration;
        public CommonRepository(IConnecctions connecctions, HttpClient httpClient, IConfiguration configuration)
        {
            _connecctions = connecctions;
            _httpClient = httpClient;
            //_apiKey = configuration["ImgBB:ApiKey"];
            //_apiUrl = configuration["ImgBB:ApiUrl"];
            _configuration = configuration;
        }
        public List<Dropdown> DdlCotegoties()
        {
            const string categorySql = @"SELECT categoryid AS Value, categoryname AS Text FROM Categories;";
            try
            {
                using (IDbConnection connection = _connecctions.GetConnection())
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();
                    var result = connection.Query<Dropdown>(categorySql);
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #region=============== IMGBB UPLOAD API CODE ==================== 
        //public async Task<string> ImgbbUploadImage(IFormFile file, string fname)
        //{
        //    string imageUrl = string.Empty;
        //    try
        //    {
        //        if (file == null || file.Length == 0)
        //            throw new ArgumentException("Kripya ek valid image file select karein.");
        //        using var content = new MultipartFormDataContent();
        //        using var ms = new MemoryStream();
        //        await file.CopyToAsync(ms);
        //        var fileBytes = ms.ToArray();
        //        var fileContent = new ByteArrayContent(fileBytes);
        //        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
        //        content.Add(fileContent, "image", fname);
        //        var requestUrl = $"{_apiUrl}?key={_apiKey}";
        //        var response = await _httpClient.PostAsync(requestUrl, content);
        //        if (!response.IsSuccessStatusCode)
        //        {
        //            var errorContent = await response.Content.ReadAsStringAsync();
        //            throw new Exception($"ImgBB API Error: {errorContent}");
        //        }
        //        var responseString = await response.Content.ReadAsStringAsync();
        //        using var jsonDoc = JsonDocument.Parse(responseString);
        //        imageUrl = jsonDoc.RootElement
        //                            .GetProperty("data")
        //                            .GetProperty("url")
        //                            .GetString();

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return imageUrl;
        //}
        #endregion=============== IMGBB UPLOAD API CODE ===================
        #region=============== MEGA.IO UPLOAD API CODE ==================== 
        public async Task<string> MegaUploadImage(IFormFile file, string customName = null)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File is empty or null.");
            }
            string getext = Path.GetExtension(file.FileName).ToLower();
            string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".webp", ".gif" };

            if (!allowedExtensions.Contains(getext))
            {
                throw new ArgumentException("Only image files are allowed!");
            }
            string fileName = !string.IsNullOrEmpty(customName) ? $"{customName}{getext}" : Path.GetFileName(file.FileName);
            string base64Result = string.Empty;
            var email = _configuration["MegaSettings:Email"];
            var password = _configuration["MegaSettings:Password"];
            var client = new MegaApiClient();
            try
            {
                await client.LoginAsync(email, password);
                var nodes = await client.GetNodesAsync();
                var rootFolder = nodes.Single(n => n.Type == NodeType.Root);
                INode uploadedFileNode;
                using (var stream = file.OpenReadStream())
                {
                    uploadedFileNode = await client.UploadAsync(stream, fileName, rootFolder);
                }
                Uri downloadUrl = await client.GetDownloadLinkAsync(uploadedFileNode);
                if (downloadUrl != null)
                {
                    using (Stream downloadStream = await client.DownloadAsync(downloadUrl))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await downloadStream.CopyToAsync(memoryStream);
                            byte[] imageBytes = memoryStream.ToArray();

                            string mimeType = $"image/{getext.Replace(".", "")}";
                            base64Result = $"data:{mimeType};base64,{Convert.ToBase64String(imageBytes)}";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (client.IsLoggedIn)
                {
                    await client.LogoutAsync();
                }
            }
            return base64Result;
        }
        #endregion=============== MEGA.IO UPLOAD API CODE ==================
    }
}

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
        private readonly string _apiKey;
        private readonly string _apiUrl;
        List<Dropdown> dropdowns = new List<Dropdown>();
        private readonly IConnecctions _connecctions;
        public CommonRepository(IConnecctions connecctions, HttpClient httpClient, IConfiguration configuration)
        {
            _connecctions = connecctions;
            _httpClient = httpClient;
            _apiKey = configuration["ImgBB:ApiKey"];
            _apiUrl = configuration["ImgBB:ApiUrl"];
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
        public async Task<string> UploadImageAsync(IFormFile file, string fname)
        {
            string imageUrl = string.Empty;
            try
            {

                if (file == null || file.Length == 0)
                    throw new ArgumentException("Kripya ek valid image file select karein.");
                using var content = new MultipartFormDataContent();
                using var ms = new MemoryStream();
                await file.CopyToAsync(ms);
                var fileBytes = ms.ToArray();
                var fileContent = new ByteArrayContent(fileBytes);
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
                content.Add(fileContent, "image", fname);
                var requestUrl = $"{_apiUrl}?key={_apiKey}";
                var response = await _httpClient.PostAsync(requestUrl, content);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"ImgBB API Error: {errorContent}");
                }
                var responseString = await response.Content.ReadAsStringAsync();
                using var jsonDoc = JsonDocument.Parse(responseString);
                imageUrl = jsonDoc.RootElement
                                    .GetProperty("data")
                                    .GetProperty("url")
                                    .GetString();

            }
            catch (Exception ex)
            {

            }
            return imageUrl;
        }
    }
}

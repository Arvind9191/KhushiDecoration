using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Shubhdecoration.Data.Common
{
    public class CommonModel
    {

    }
    public class FileDetails
    {
        public string FileName { get; set; } = string.Empty;
        public string Extension { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
        public string FileUrl { get; set; } = string.Empty;
        public bool IsSuccess { get; set; }
    }
    public class ResponseModel 
    {
        public int Id { get; set; }  
        public int InfoType { get; set; }
        public bool IsSuccess { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;  
        public string UserName { get; set; } = string.Empty; 
        public string Password { get; set; } = string.Empty; 
    } 
    public class DdlCotegoties
    {
        public int Value { get; set; }
        public string Text { get; set; } = string.Empty;
    }

    public class Dropdown
    {
        public int Value { get; set; }
        public string Text { get; set; } = string.Empty;
    } 
    public class EmailModel
    {
        public string ToEmail { get; set; } = string.Empty;
        public string CC { get; set; } = string.Empty;
        public string BCC { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string HtmlContent { get; set; } = string.Empty;
        public Attachment? Attachment { get; set; }

    }
}

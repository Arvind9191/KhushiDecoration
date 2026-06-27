using Newtonsoft.Json;
using Shubhdecoration.Data.Common;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace ShubhDecoration.Helper
{
    public static class DataHelper
    {
        private static IConfiguration _config;
        public static void Initialize(IConfiguration configuration)
        {
            _config = configuration;
        }
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        public static T? GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        } 
        public static int GenerateOTP()
        {
            var random = new Random();
            int otp = random.Next(1000, 9999);
            return otp;
        }
        public static bool IsValidEmail(string strEmail)
        {
            bool msg = false;
            try
            {
                string email = strEmail;
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(email);
                if (match.Success)
                {
                    msg = true;
                }
                else
                {
                    msg = false;
                }
            }
            catch (Exception ex)
            {
                msg = false;
            }
            return msg;
        }
        public static bool SendEmailDopmain(EmailModel obj)
        {
            bool isEmailSend = false;
            try
            {
                var fromEmail = EmailSettings.SenderEmail;
                var password = EmailSettings.Password;
                var port = EmailSettings.Port;
                var host = EmailSettings.Host;
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(fromEmail);
                if (!string.IsNullOrEmpty(obj.ToEmail))
                {
                    List<string> toEmails = new List<string>();
                    toEmails = obj.ToEmail.Split(',').ToList();
                    if (toEmails != null && toEmails.Count > 0)
                    {
                        for (int i = 0; i < toEmails.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(toEmails[i]) && IsValidEmail(toEmails[i]))
                            {
                                mail.To.Add(new MailAddress(toEmails[i]));

                            }
                        }
                    }
                }
                //////  Send bcc emnail
                if (!string.IsNullOrEmpty(obj.BCC))
                {
                    List<string> bccEmails = new List<string>();
                    bccEmails = obj.BCC.Split(",").ToList();
                    if (bccEmails != null && bccEmails.Count > 0)
                    {
                        for (int j = 0; j < bccEmails.Count(); j++)
                        {
                            if (!string.IsNullOrEmpty(bccEmails[j]) && IsValidEmail(bccEmails[j]))
                            {
                                mail.Bcc.Add(new MailAddress(bccEmails[j]));
                            }
                        }
                    }
                }
                ////// Send cc email
                if (!string.IsNullOrEmpty(obj.CC))
                {
                    List<string> ccEmails = new List<string>();
                    ccEmails = obj.CC.Split(",").ToList();
                    if (ccEmails != null && ccEmails.Count > 0)
                    {
                        for (int k = 0; k < ccEmails.Count(); k++)
                        {
                            if (!string.IsNullOrEmpty(ccEmails[k]) && IsValidEmail(ccEmails[k]))
                            {
                                mail.CC.Add(new MailAddress(ccEmails[k]));
                            }
                        }
                    }
                }
                ////// Send and attachment file 
                if (obj.Attachment != null)
                {
                    mail.Attachments.Add(obj.Attachment);
                }

                mail.Subject = obj.Subject;
                mail.Body = obj.Body;
                mail.IsBodyHtml = true;
                SmtpClient smtpClient = new SmtpClient(host);
                smtpClient.Host = host;
                smtpClient.Port = Convert.ToInt32(port);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(fromEmail, password);
                smtpClient.EnableSsl = true;
                try
                {
                    //Send email
                    smtpClient.Send(mail);
                    isEmailSend = true;
                    ///WriteErrorLog("Mail Sent for" + obj.Subject);
                }
                catch (Exception ex)
                {
                    isEmailSend = false;
                }
            }
            catch (Exception ex)
            {
                isEmailSend = false;
            }
            return isEmailSend;
        }
        public static string CurrentDateTime()
        {
            string currentDateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            return currentDateTime;
        }
        public static DateTime CurrentDate(string formate)
        { 
            //string dateString = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);             
            string dateString = DateTime.Now.ToString(formate, System.Globalization.CultureInfo.InvariantCulture);             
            DateTime currentDateTime = DateTime.ParseExact(dateString, formate, System.Globalization.CultureInfo.InvariantCulture);

            return currentDateTime;
        } 
        public static async Task<FileDetails> SaveFileAsync(IFormFile file, string rootPath, string folderName)
        {
            FileDetails fileDetails = new FileDetails();
            string fileName = string.Empty;
            if (file != null || file.Length > 0)
            {
                string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".pdf", ".docx" };
                string fileExtension = Path.GetExtension(file.FileName).ToLower();
                if (allowedExtensions.Contains(fileExtension))
                {
                    string[] allowedMimeTypes = { "image/jpeg", "image/png", "application/pdf", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" };
                    if (allowedMimeTypes.Contains(file.ContentType))
                    {
                        string uploadPath = Path.Combine(rootPath, folderName);
                        if (!Directory.Exists(uploadPath))
                        {
                            Directory.CreateDirectory(uploadPath);
                        }
                        fileName = Guid.NewGuid().ToString() + fileExtension;
                        string filePath = Path.Combine(uploadPath, fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            fileDetails.IsSuccess = true;
                            fileDetails.FileName = fileName;
                            fileDetails.Extension = fileExtension;
                            await file.CopyToAsync(stream);
                        }
                    }
                    else
                    {
                        fileDetails.ErrorMessage = "Invalid file content";
                    }
                }
                else
                {
                    fileDetails.ErrorMessage = "Invalid file type";
                }
            }
            else
            {
                fileDetails.ErrorMessage = "No file selected";
            }
            return fileDetails;
        }
    }
    public static class EmailSettings
    {
        public static string SenderEmail { get; set; } = string.Empty;
        public static string Password { get; set; } = string.Empty;
        public static string Port { get; set; } = string.Empty;
        public static string Host { get; set; } = string.Empty;
    } 
}

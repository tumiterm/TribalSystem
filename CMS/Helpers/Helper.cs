using CMS.App_Start;
using CMS.Controllers;
using CMS.Mail_Config;

using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;



namespace CMS.Helpers
{
    public static class Helper
    {
        private static Random _random = new Random();

        private static string _API_KEY = "2C9DAABE-20FE-4BC1-9BF3-276D9BBC9699";
        public static int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }
        public static string EncryptEntry(string ToEncrypt)
        {
            return Convert.ToBase64String(Encoding.ASCII.GetBytes(ToEncrypt));
        }
        public static string DecryptInput(string cypherString)
        {
            return Encoding.ASCII.GetString(Convert.FromBase64String(cypherString));
        }

        public static void SendEmailNotification(string reciever, string subject, string message)
        {
            var senderMail = new MailAddress(EmailInfo.Username, "Tribal System");
            var recieverMail = new MailAddress(reciever, "Valued Client");
            var password = EmailInfo.Password;
            var sub = subject;
            var body = message;
            var smtp = new SmtpClient
            {
                Host = EmailInfo.Smtp_Host,
                Port = Convert.ToInt32(EmailInfo.Smtp_Port),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials= false,
                Credentials = new NetworkCredential(senderMail.Address,password)
            };

            using (var mess = new MailMessage(senderMail, recieverMail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            {
                smtp.Send(mess);
            }

        }

        private static Random random = new Random();
        public static string RandomStringGenerator(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string EncryptInput(string input)
        {
            return Convert.ToBase64String(
               System.Security.Cryptography.SHA256.Create()
               .ComputeHash(Encoding.UTF8.GetBytes(input))
               );
        }

        public static bool  IsMailValid(string email)
        {
            using (CMSEntities db = new CMSEntities())
            {
                var getMail =  db.Users.Where(a => a.Email == email).FirstOrDefault();
                return getMail != null;
            }
        }

        //public static bool DoesIdExist(string id)
        //{
        //    using (CMSEntities db = new CMSEntities())
        //    {
        //        var getUserId = db.Users.Where(a => a.ID_Number == id).FirstOrDefault();
        //        return getUserId != null;
        //    }
        //}

        public static SelectList ToSelectList<TEnum>(this TEnum obj)
        where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            return new SelectList(Enum.GetValues(typeof(TEnum))
            .OfType<Enum>()
            .Select(x => new SelectListItem
            {
                Text = Enum.GetName(typeof(TEnum), x),
                Value = (Convert.ToInt32(x))
                .ToString()
            }), "Value", "Text");
        }

        public static Guid GenerateGuid()
        {
            return Guid.NewGuid(); 
        }

        public static void OnSendSMSNotification()
        {
            //Configuration.Default.ApiKey.Add("AUTHORIZATION", _API_KEY);

            //var apiInstance = new SmsApi();
            //var messageStatusRequest = new array[Integer]();

            //try
            //{
            //    // Get SMS delivery statuses
            //    messageStatusResponse result = apiInstance.smsStatus(messageStatusRequest);
            //    Debug.WriteLine(result);
            //}
            //catch (Exception e)
            //{
            //    Debug.Print("Exception when calling SmsApi.smsStatus: " + e.Message);
            //}
        }

        public static string OnConvertEnumToString(this eExpertise ex)
        {
            switch (ex)
            {
                case eExpertise.Chemical_Skills:
                    return "Chemical Skills";
                case eExpertise.Fire_Fighter:
                    return "Fire Fighter";
                case eExpertise.HealthCareProfessional:
                    return "Health Care Professional";
                case eExpertise.Life_Guard:
                    return "Life Guard";
                default:
                    return "None Selected";
            }
        }


    }
}
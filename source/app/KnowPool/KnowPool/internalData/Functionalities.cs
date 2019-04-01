using System;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace KnowPool.internalData
{
    public class Functionalities
    {
        private static readonly string sendgrid_key = "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";
        private const string accountSid = "AC02c1c2b84adbeee7efd56db0a0ee9d19";
        private const string authToken = "03b5d61d630e878b6f74cc5158a175d4";
        public enum EventType {
            verification,
            login
        }
        public static string OTPGenerator()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[6];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new String(stringChars);
        }

        public static async Task<bool> EmailSender(string url_otp, string email, string userName, EventType eventType)
        {
            try
            {
                var client = new SendGridClient(sendgrid_key);
                var from = new EmailAddress("admin@knowpool.com", "KnowPool");
                var to = new EmailAddress(email, userName);
                var subject = "";
                var plainTextContent = "";
                var htmlContent = "";

                if (eventType.Equals(EventType.verification))
                {
                    subject = "KnowPool | Verification Email";
                    plainTextContent = "Verification URL for KnowPool : " + url_otp;
                    htmlContent = "Verification URL for KnowPool : " + url_otp;
                }
                else if (eventType.Equals(EventType.login))
                {
                    subject = "KnowPool | Login OTP";
                    plainTextContent = "Your OTP for KnowPool is : " + url_otp;
                    htmlContent = "Your OTP for KnowPool is : " + url_otp;
                }
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool SMSSender(string phoneNumber, string url_otp, EventType eventType)
        {
            //phoneNumber = "+919717155636";
            try
            {
                TwilioClient.Init(accountSid, authToken);
                

                if (eventType.Equals(EventType.verification))
                {
                    var message = MessageResource.Create(
                    body: "Verification URL for KnowPool : " + url_otp,
                    from: new Twilio.Types.PhoneNumber("+12028834807"),
                    to: new Twilio.Types.PhoneNumber(phoneNumber)
                );
                }
                else if (eventType.Equals(EventType.login))
                {
                    var message = MessageResource.Create(
                    body: "Your OTP for KnowPool is : " + url_otp,
                    from: new Twilio.Types.PhoneNumber("+12028834807"),
                    to: new Twilio.Types.PhoneNumber(phoneNumber)
                );
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

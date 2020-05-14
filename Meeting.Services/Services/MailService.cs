using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using NLog;
using System.Text;
using System;
using System.IO;
using Meeting.Entities.Models;
using System.Collections.Generic;
using Meeting.Services.Services.Interface;

namespace Meeting.Services.Services
{
    public class MailService: IMailService
    {
        public static bool IsTest = false;
        public static Logger logger = LogManager.GetCurrentClassLogger();
    

        #region  Список захардкодженых email 

        public static MailAddress mailbot = new MailAddress("mailbot@specialist.ru");
        public static MailAddress info = new MailAddress("info@specialist.ru");
        public static MailAddress errorMail = new MailAddress("errormeetinginfo@specialist.ru");
        public static MailAddress developer = new MailAddress("tibragimov@specialist.ru");
        public static MailAddress dinzis = new MailAddress("dinzis@specialist.ru");
        public static MailAddress webinarmanager = new MailAddress("webinarmanager@specialist.ru");
        public static MailAddress emailMarketing = new MailAddress("vakhmadullina@specialist.ru");

        #endregion




        private const string MailMaster = "<html><body>{0}</body></thml>";
        public void SendErrorDeveloper(string body, string subject, string bcc = "",
           params MailAddress[] cc)
        {
            Send(errorMail, developer, body, subject, null, null, 0, bcc, cc);
        }
        public void Send(MailAddress from, MailAddress to, string body, string subject, string bcc = "",
            params MailAddress[] cc)
        {
            Send(from, to, body, subject, null, null, 0, bcc, cc);
        }

        public void SendSync(MailAddress from, MailAddress to, string body, string subject, string bcc = "",
            params MailAddress[] cc)
        {
            SendSync(from, to, body, subject, null, null, 0, bcc, cc);
        }

        public void SendWithReplyLimit(MailAddress from, MailAddress to, string body, string subject,
            int replayMinutes, string bcc = "", params MailAddress[] cc)
        {
            Send(from, to, body, subject, null, null, replayMinutes, bcc, cc);
        }

      
        public void Send(MailAddress from, MailAddress to, string body, string subject,
            List<UploadFile> uploadFiles, string fileName = null, int replyMinutes = 0, string bcc = "",
            params MailAddress[] cc)
        {
            //try
            //{
            if (IsTest)
            {
                to = developer;
                cc = new MailAddress[] { developer };
                bcc = developer.Address;
            }
            SendSync(@from, to, body, subject, uploadFiles, fileName, replyMinutes, bcc, cc);
            logger.Info($"Пиьсмо отправлено на почтовый ящик {to}");
            //}
            //catch (Exception e)
            //{
            //    if (to != null && to.Address != null) logger.Error(e, "email " + to.Address);
            //    else {
            //        logger.Info("to == null || to.Address == null");
            //    }
            //    //Logger.Exception(e, this + " " + to.Address +
            //    //    " " + subject);
            //};

            //ThreadPool.QueueUserWorkItem(o => {
            //    try
            //    {
            //       // SendSync(@from, to, body, subject, uploadFiles, fileName, replyMinutes, cc);

            //    }
            //    catch (Exception e)
            //    {
            //        //Logger.Exception(e, this + " " + to.Address +
            //        //    " " + subject);
            //    }
            //});


        }

        public void SendSync(MailAddress from, MailAddress to, string body, string subject, List<UploadFile> uploadFiles,
            string fileName = null, int replyMinutes = 0, string bcc = "", params MailAddress[] cc)
        {
            using (var client = new SmtpClient())
            {


                var htmlBody = body.Contains("<html>") ? body : string.Format(MailMaster, body);
                using (var message =
                    new MailMessage(from, to)
                    {
                        IsBodyHtml = true,
                        BodyEncoding = Encoding.UTF8,
                        Subject = subject,
                        Body = htmlBody,
                    })
                {
                    if (replyMinutes > 0)
                    {
                        var replyDate = DateTime.Now.AddMinutes(replyMinutes);
                        message.Headers.Add("X-Message-Flag", "Follow up");
                        message.Headers.Add("Reply-By", replyDate.ToString("ddd, dd MMM yyyy HH:mm:ss zz"));
                    }

                    if (uploadFiles != null)
                    {
                        foreach (var uploadFile in uploadFiles)
                        {
                            if (uploadFile != null && uploadFile.ContentLength > 0)
                            {
                                message.Attachments.Add(new Attachment(uploadFile.Stream, uploadFile.Name));
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(fileName))
                    {
                        var attachment = new Attachment(fileName);
                        attachment.Name = Path.GetFileName(fileName);
                        message.Attachments.Add(attachment);
                    }
                    if (cc != null)
                        foreach (var addresse in cc)
                        {
                            if (addresse != null)
                            {
                                message.CC.Add(addresse);
                            }
                        }

                    if (!string.IsNullOrEmpty(bcc))
                    {
                        foreach (var item in bcc.Split(';'))
                        {
                            if (!string.IsNullOrEmpty(item))
                            {
                                MailAddress addressBCC = new MailAddress(item);
                                message.Bcc.Add(addressBCC);
                            }
                        }
                    }

                    client.Send(message);
                }
            }
        }

        private MailAddress MailAddress(string Email, string FullName)
        {
            try
            {
                return new MailAddress(Email, FullName);

            }
            catch (Exception e)
            {
                logger.Fatal(e, this + " wrong email " + Email);
            }
            return null;
        }

       
      
    }
}

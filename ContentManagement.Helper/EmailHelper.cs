using DocumentFormat.OpenXml.Vml;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace ContentManagement.Helper
{
    public class EmailHelper
    {
        static List<MemoryStream> attachments = new List<MemoryStream>();
        public static void SendEmail(SendEmailSpecification sendEmailSpecification)
        {
            MailMessage message = new MailMessage()
            {
                Sender = new MailAddress(sendEmailSpecification.FromAddress),
                From = new MailAddress(sendEmailSpecification.FromAddress),
                Subject = sendEmailSpecification.Subject,
                IsBodyHtml = true,
                Body = sendEmailSpecification.Body,
            };

            if (sendEmailSpecification.Attechments.Count > 0)
            {
                Attachment attach;
                foreach (var file in sendEmailSpecification.Attechments)
                {

                    string fileData = file.Src.Split(',').LastOrDefault();
                    byte[] bytes = Convert.FromBase64String(fileData);
                    var ms = new MemoryStream(bytes);
                    attach = new Attachment(ms, file.Name, file.FileType);
                    attachments.Add(ms);
                    message.Attachments.Add(attach);
                }
            }
            if (sendEmailSpecification.AttachmentFileURL != null)
            {
                foreach (var item in sendEmailSpecification.AttachmentFileURL)
                {
                    message.Attachments.Add(new Attachment(sendEmailSpecification.AttachmentRootPath + "/" + item));
                }
            }
            sendEmailSpecification.ToAddress.Split(",").ToList().ForEach(toAddress =>
            {
                message.To.Add(new MailAddress(toAddress));
            });
            if (!string.IsNullOrEmpty(sendEmailSpecification.CCAddress))
            {
                sendEmailSpecification.CCAddress.Split(",").ToList().ForEach(ccAddress =>
                {
                    message.CC.Add(new MailAddress(ccAddress));
                });
            }

            SmtpClient smtp = new SmtpClient()
            {
                Port = 465,
                Host = "smtps.aruba.it",
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("noreply@ditib.it", "DitibItalia@2024"),
                DeliveryMethod = SmtpDeliveryMethod.Network,
            };

            try
            {
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            if (attachments.Count() > 0)
            {
                foreach (var attachment in attachments)
                {
                    try
                    {
                        attachment.Dispose();
                    }
                    catch
                    {
                    }
                }
            }

        }

        private static Attachment ConvertStringToStream(FileInfo fileInfo)
        {
            string fileData = fileInfo.Src.Split(',').LastOrDefault();
            byte[] bytes = Convert.FromBase64String(fileData);
            System.Net.Mail.Attachment attach;
            using (MemoryStream ms = new MemoryStream(bytes))
            {

                attach = new System.Net.Mail.Attachment(ms, fileInfo.Name, fileInfo.FileType);
                // I guess you know how to send email with an attachment
                // after sending email
                //ms.Close();
                attachments.Add(ms);
            }
            return attach;
        }

        public static MailResponse SendMailByMailKit(string message, string subject, string mail, string ccAddress)
        {
            /*
             https://copyprogramming.com/howto/send-mailkit-email-with-an-attachment-from-memorystream
             https://www.taithienbo.com/send-email-with-attachments-using-mailkit-for-net-core/
             https://jasonwatmore.com/post/2022/03/11/net-6-send-an-email-via-smtp-with-mailkit
             */
            var response = new MailResponse();
            try
            {
                MimeMessage emailMessage = new MimeMessage();
                emailMessage.From.Add(MailboxAddress.Parse("noreply@ditib.it"));
                emailMessage.To.Add(MailboxAddress.Parse(mail));
                emailMessage.Subject = subject;
                emailMessage.Cc.Add(MailboxAddress.Parse(ccAddress));

                BodyBuilder emailBodyBuilder = new BodyBuilder();
                emailBodyBuilder.TextBody = message;
                #region HTML
                emailMessage.Body = new TextPart(TextFormat.Html) { Text = "<table class='body-wrap' style='box-sizing: border-box; font-size: 14px; width: 100%; background-color: transparent; margin: 0;'><tr style ='box-sizing: border-box; font-size: 14px; margin: 0;'>        <td style ='box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0;' valign='top'></td><td class='container' width='600' style=' box-sizing: border-box; font-size: 14px; vertical-align: top; display: block !important; max-width: 600px !important; clear: both !important; margin: 0 auto;' valign='top'> <div class='content' style=' box-sizing: border-box; font-size: 14px; max-width: 600px; display: block; margin: 0 auto; padding: 20px;'> <table class='main' width='100%' cellpadding='0' cellspacing='0' itemprop='action' itemscope itemtype ='http://schema.org/ConfirmAction' style='box-sizing: border-box; font-size: 14px; border-radius: 3px; margin: 0; border: none;'>                    <tr style = 'box-sizing: border-box; font-size: 14px; margin: 0;'> <td class='content-wrap' style='box-sizing: border-box; color: #495057; font-size: 14px; vertical-align: top; margin: 0;padding: 30px; box-shadow: 0 0.75rem 1.5rem rgba(18,38,63,.03); ;border-radius: 7px; background-color: #fff;' valign='top'><meta itemprop = 'name' content='Confirm Email' style='box-sizing: border-box; font-size: 14px; margin:0;' /><table width = '100%' cellpadding='0' cellspacing='0' style='box-sizing: border-box; font-size: 14px; margin: 0;'> <tr style ='box-sizing: border-box; font-size: 14px; margin: 0;'><td class='content-block' style='box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 0 0 20px;' valign='top'>" + message + @"</td> </tr>
                                <tr style = 'box-sizing:
                                    border-box; font-size: 14px; margin: 0;'>
                                    <td class='content-block' style='box-sizing: border-box; font-size: 14px;
                                        vertical-align: top; margin: 0; padding: 0 0 20px;' valign='top'>
                                        <b>DITIB ITALIA</b>
                                    </td>
                                </tr>
                                <tr style='box-sizing:
                                    border-box; font-size: 14px; margin: 0;'>
                                    <td class='content-block' style='text-align: center;box-sizing: border-box; font-size: 14px;
                                        vertical-align: top; margin: 0; padding: 0;' valign='top'><img style='max-height:100px;'
                                            src='https://ditib.it/assets/italya/ItalyaLogo.png' /></td>
                                </tr>
                                <tr style = 'box-sizing:
                                    border-box; font-size: 14px; margin: 0;'>
                                    <td class='content-block' style='text-align: center;box-sizing: border-box; font-size: 14px;
                                        vertical-align: top; margin: 0; padding: 0;' valign='top'>© 2024 <a
                                            href='https://ditib.it/'>DITIB ITALIA </a></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>" };
                #endregion
                // SMTP Bilgileri => aruba.it => https://webmail.aruba.it/smart/#settings/managemobile/ios
                var smtp = new MailKit.Net.Smtp.SmtpClient();
                smtp.Connect("smtps.aruba.it", 465, true);
                smtp.Authenticate("noreply@ditib.it", "DitibItalia@2024");
                smtp.Send(emailMessage);
                smtp.Disconnect(true);
                response.Message = "E-Posta gönderme işlemi başarılı!";
                response.Result = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "E-Posta Gönderme işlemi sırasında bir hata meydana geldi; " + ex.Message;
                response.Result = false;
                return response;
            }

        }

        public static MailResponse SendMailByMailKitWithDocument(string message, string subject, string mail, List<byte[]> files, string fileName)
        {
            /*
             https://copyprogramming.com/howto/send-mailkit-email-with-an-attachment-from-memorystream
             https://www.taithienbo.com/send-email-with-attachments-using-mailkit-for-net-core/
             https://jasonwatmore.com/post/2022/03/11/net-6-send-an-email-via-smtp-with-mailkit
             */
            var response = new MailResponse();
            try
            {
                MimeMessage emailMessage = new MimeMessage();
                emailMessage.From.Add(MailboxAddress.Parse("hafiz@musdav.org.tr"));
                emailMessage.To.Add(MailboxAddress.Parse(mail));
                emailMessage.Subject = subject;

                Multipart multipart = new Multipart("mixed");

                BodyBuilder emailBodyBuilder = new BodyBuilder();
                emailBodyBuilder.TextBody = message;

                #region HTML
                var htmlPart = new TextPart(TextFormat.Html)
                {
                    Text = "<table class='body-wrap' style='box-sizing: border-box; font-size: 14px; width: 100%; background-color: transparent; margin: 0;'><tr style ='box-sizing: border-box; font-size: 14px; margin: 0;'>        <td style ='box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0;' valign='top'></td><td class='container' width='600' style=' box-sizing: border-box; font-size: 14px; vertical-align: top; display: block !important; max-width: 600px !important; clear: both !important; margin: 0 auto;' valign='top'> <div class='content' style=' box-sizing: border-box; font-size: 14px; max-width: 600px; display: block; margin: 0 auto; padding: 20px;'> <table class='main' width='100%' cellpadding='0' cellspacing='0' itemprop='action' itemscope itemtype ='http://schema.org/ConfirmAction' style='box-sizing: border-box; font-size: 14px; border-radius: 3px; margin: 0; border: none;'>                    <tr style = 'box-sizing: border-box; font-size: 14px; margin: 0;'> <td class='content-wrap' style='box-sizing: border-box; color: #495057; font-size: 14px; vertical-align: top; margin: 0;padding: 30px; box-shadow: 0 0.75rem 1.5rem rgba(18,38,63,.03); ;border-radius: 7px; background-color: #fff;' valign='top'><meta itemprop = 'name' content='Confirm Email' style='box-sizing: border-box; font-size: 14px; margin:0;' /><table width = '100%' cellpadding='0' cellspacing='0' style='box-sizing: border-box; font-size: 14px; margin: 0;'> <tr style ='box-sizing: border-box; font-size: 14px; margin: 0;'><td class='content-block' style='box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 0 0 20px;' valign='top'>" + message + @"</td> </tr>
                                        <tr style = 'box-sizing:
                                            border-box; font-size: 14px; margin: 0;'>
                                            <td class='content-block' style='box-sizing: border-box; font-size: 14px;
                                                vertical-align: top; margin: 0; padding: 0 0 20px;' valign='top'>
                                                <b>DITIB ITALIA</b>
                                            </td>
                                        </tr>
                                        <tr style='box-sizing:
                                            border-box; font-size: 14px; margin: 0;'>
                                            <td class='content-block' style='text-align: center;box-sizing: border-box; font-size: 14px;
                                                vertical-align: top; margin: 0; padding: 0;' valign='top'><img style='max-height:100px;'
                                                    src='https://ditib.it/assets/italya/ItalyaLogo.png' /></td>
                                        </tr>
                                        <tr style = 'box-sizing:
                                            border-box; font-size: 14px; margin: 0;'>
                                            <td class='content-block' style='text-align: center;box-sizing: border-box; font-size: 14px;
                                                vertical-align: top; margin: 0; padding: 0;' valign='top'>© 2024 <a
                                                    href='https://ditib.it/'>DITIB ITALIA </a></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>"
                };
                #endregion

                var multipart2 = new Multipart("related");
                multipart2.Add(htmlPart);
                multipart2.Add(emailBodyBuilder.ToMessageBody());

                foreach (var file in files)
                {
                    var attachment = new MimePart("application", "pdf")
                    {
                        Content = new MimeContent(new MemoryStream(file), ContentEncoding.Default),
                        ContentDisposition = new MimeKit.ContentDisposition(MimeKit.ContentDisposition.Attachment),
                        ContentTransferEncoding = ContentEncoding.Base64,
                        FileName = fileName
                    };
                    multipart2.Add(attachment);
                }

                multipart.Add(multipart2);

                emailMessage.Body = multipart;

                var smtp = new MailKit.Net.Smtp.SmtpClient();
                smtp.Connect("smtps.aruba.it", 465, true);
                smtp.Authenticate("noreply@ditib.it", "DitibItalia@2024");
                smtp.Send(emailMessage);
                smtp.Disconnect(true);
                response.Message = "E-posta gönderme işlemi başarılı!";
                response.Result = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "E-Posta Gönderme işlemi sırasında bir hata meydana geldi; " + ex.Message;
                response.Result = false;
                return response;
            }

        }
    }


}

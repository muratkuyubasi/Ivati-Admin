using ContentManagement.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace Hafiz.Core.Utilities.Mail
{
    public class MailHelper
    {
        public static MailResponse SendMail(string message, string subject, string mail)
        {
            var response = new MailResponse();
            try
            {
                mail = mail.ToLower();
                var receiverMail = new MailAddress("noreply@ditib.it", "İtalya Diyanet Vakfı");
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("noreply@ditib.it", "DitibItalia@2024")
                };
                using (var mess = new MailMessage()
                {
                    From = receiverMail,
                    Subject = subject,
                    IsBodyHtml = true,
                    Body = @"<table class='body-wrap' style=' box-sizing: border-box;
    font-size: 14px; width: 100%; background-color: transparent; margin: 0;'>
    <tr style=' box-sizing: border-box; font-size: 14px;margin:
        0;'>
        <td style=' box-sizing: border-box; font-size: 14px;
            vertical-align: top; margin: 0;' valign='top'></td>
        <td class='container' width='600' style=' box-sizing:
            border-box; font-size: 14px; vertical-align: top; display: block !important; max-width: 600px !important;
            clear: both !important; margin: 0 auto;' valign='top'>
            <div class='content' style=' box-sizing:
                border-box; font-size: 14px; max-width: 600px; display: block; margin: 0 auto; padding: 20px;'>
                <table class='main' width='100%' cellpadding='0' cellspacing='0' itemprop='action' itemscope
                    itemtype='http://schema.org/ConfirmAction' style='box-sizing: border-box; font-size: 14px; border-radius: 3px; margin: 0;
                    border: none;'>
                    <tr style=' box-sizing: border-box;
                        font-size: 14px; margin: 0;'>
                        <td class='content-wrap' style='
                            box-sizing: border-box; color: #495057; font-size: 14px; vertical-align: top; margin:
                            0;padding: 30px; box-shadow: 0 0.75rem 1.5rem rgba(18,38,63,.03); ;border-radius: 7px;
                            background-color: #fff;' valign='top'>
                            <meta itemprop='name' content='Confirm Email' style='box-sizing: border-box; font-size: 14px; margin:
                                0;' />
                            <table width='100%' cellpadding='0' cellspacing='0'
                                style='box-sizing: border-box; font-size: 14px; margin: 0;'>
                                <tr style=' box-sizing:
                                    border-box; font-size: 14px; margin: 0;'>
                                    <td class='content-block' style='box-sizing: border-box; font-size: 14px;
                                        vertical-align: top; margin: 0; padding: 0 0 20px;' valign='top'>" + message + @"</td>
                                </tr>

                               <tr style='box-sizing:
                                    border-box; font-size: 14px; margin: 0;'>
                                    <td class='content-block' style='text-align: center;box-sizing: border-box; font-size: 14px;
                                        vertical-align: top; margin: 0; padding: 0;' valign='top'><img style='max-height:100px;'
                                            src='https://ditib.it/assets/italya/ItalyaLogo.png' /></td>
                                </tr>
                                <tr style='box-sizing:
                                    border-box; font-size: 14px; margin: 0;'>
                                    <td class='content-block' style='text-align: center;box-sizing: border-box; font-size: 14px;
                                        vertical-align: top; margin: 0; padding: 0;' valign='top'>© 2024 <a
                                            href='https://ditib.it/'>İtalya Diyanet Vakfı </a></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>"
                })
                {
                    mess.To.Add(mail);
                    smtp.Send(mess);
                }
                response.Message = "Başarılı";
                response.Result = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Result = false;
                return response;
            }
        }

        public static MailResponse SendMail2(string message, string subject, string mail, Attachment attachments)
        {
            var response = new MailResponse();
            try
            {
                mail = mail.ToLower();
                var receiverMail = new MailAddress("noreply@ditib.it", "İtalya Diyanet Vakfı");
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("noreply@ditib.it", "DitibItalia@2024")
                };

                using (var mailMessage = new MailMessage
                {
                    From = receiverMail,
                    Subject = subject,
                    IsBodyHtml = true,
                    Body = @"<table class='body-wrap' style=' box-sizing: border-box;
    font-size: 14px; width: 100%; background-color: transparent; margin: 0;'>
    <tr style=' box-sizing: border-box; font-size: 14px;margin:
        0;'>
        <td style=' box-sizing: border-box; font-size: 14px;
            vertical-align: top; margin: 0;' valign='top'></td>
        <td class='container' width='600' style=' box-sizing:
            border-box; font-size: 14px; vertical-align: top; display: block !important; max-width: 600px !important;
            clear: both !important; margin: 0 auto;' valign='top'>
            <div class='content' style=' box-sizing:
                border-box; font-size: 14px; max-width: 600px; display: block; margin: 0 auto; padding: 20px;'>
                <table class='main' width='100%' cellpadding='0' cellspacing='0' itemprop='action' itemscope
                    itemtype='http://schema.org/ConfirmAction' style='box-sizing: border-box; font-size: 14px; border-radius: 3px; margin: 0;
                    border: none;'>
                    <tr style=' box-sizing: border-box;
                        font-size: 14px; margin: 0;'>
                        <td class='content-wrap' style='
                            box-sizing: border-box; color: #495057; font-size: 14px; vertical-align: top; margin:
                            0;padding: 30px; box-shadow: 0 0.75rem 1.5rem rgba(18,38,63,.03); ;border-radius: 7px;
                            background-color: #fff;' valign='top'>
                            <meta itemprop='name' content='Confirm Email' style='box-sizing: border-box; font-size: 14px; margin:
                                0;' />
                            <table width='100%' cellpadding='0' cellspacing='0'
                                style='box-sizing: border-box; font-size: 14px; margin: 0;'>
                                <tr style=' box-sizing:
                                    border-box; font-size: 14px; margin: 0;'>
                                    <td class='content-block' style='box-sizing: border-box; font-size: 14px;
                                        vertical-align: top; margin: 0; padding: 0 0 20px;' valign='top'>" + message + @"</td>
                                </tr>

                                <tr style='box-sizing:
                                    border-box; font-size: 14px; margin: 0;'>
                                    <td class='content-block' style='text-align: center;box-sizing: border-box; font-size: 14px;
                                        vertical-align: top; margin: 0; padding: 0;' valign='top'><img style='max-height:100px;'
                                            src='https://ditib.it/assets/italya/ItalyaLogo.png' /></td>
                                </tr>
                                <tr style='box-sizing:
                                    border-box; font-size: 14px; margin: 0;'>
                                    <td class='content-block' style='text-align: center;box-sizing: border-box; font-size: 14px;
                                        vertical-align: top; margin: 0; padding: 0;' valign='top'>© 2024 <a
                                            href='https://ditib.it/'>İtalya Diyanet Vakfı </a></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>"
                })
                {
                    if (attachments != null)
                    {
                        mailMessage.Attachments.Add(attachments);
                    }
                    mailMessage.To.Add(mail);
                    smtp.Send(mailMessage);
                }
                response.Message = "Başarılı";
                response.Result = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Result = false;
                return response;
            }
        }

        public static MailResponse SendMailWithCC(string message, string subject, string mail, List<string>? ccList, Attachment? attachments)
        {
            var response = new MailResponse();
            try
            {
                mail = mail.ToLower();
                var receiverMail = new MailAddress("noreply@ditib.it", "İtalya Diyanet Vakfı");
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("noreply@ditib.it", "DitibItalia@2024")
                };
                using (var mess = new MailMessage()
                {
                    From = receiverMail,
                    Subject = subject,
                    IsBodyHtml = true,
                    Body = @"<table class='body-wrap' style=' box-sizing: border-box;
    font-size: 14px; width: 100%; background-color: transparent; margin: 0;'>
    <tr style=' box-sizing: border-box; font-size: 14px;margin:
        0;'>
        <td style=' box-sizing: border-box; font-size: 14px;
            vertical-align: top; margin: 0;' valign='top'></td>
        <td class='container' width='600' style=' box-sizing:
            border-box; font-size: 14px; vertical-align: top; display: block !important; max-width: 600px !important;
            clear: both !important; margin: 0 auto;' valign='top'>
            <div class='content' style=' box-sizing:
                border-box; font-size: 14px; max-width: 600px; display: block; margin: 0 auto; padding: 20px;'>
                <table class='main' width='100%' cellpadding='0' cellspacing='0' itemprop='action' itemscope
                    itemtype='http://schema.org/ConfirmAction' style='box-sizing: border-box; font-size: 14px; border-radius: 3px; margin: 0;
                    border: none;'>
                    <tr style=' box-sizing: border-box;
                        font-size: 14px; margin: 0;'>
                        <td class='content-wrap' style='
                            box-sizing: border-box; color: #495057; font-size: 14px; vertical-align: top; margin:
                            0;padding: 30px; box-shadow: 0 0.75rem 1.5rem rgba(18,38,63,.03); ;border-radius: 7px;
                            background-color: #fff;' valign='top'>
                            <meta itemprop='name' content='Confirm Email' style='box-sizing: border-box; font-size: 14px; margin:
                                0;' />
                            <table width='100%' cellpadding='0' cellspacing='0'
                                style='box-sizing: border-box; font-size: 14px; margin: 0;'>
                                <tr style=' box-sizing:
                                    border-box; font-size: 14px; margin: 0;'>
                                    <td class='content-block' style='box-sizing: border-box; font-size: 14px;
                                        vertical-align: top; margin: 0; padding: 0 0 20px;' valign='top'>" + message + @"</td>
                                </tr>

                                <tr style='box-sizing:
                                    border-box; font-size: 14px; margin: 0;'>
                                    <td class='content-block' style='box-sizing: border-box; font-size: 14px;
                                        vertical-align: top; margin: 0; padding: 0 0 20px;' valign='top'>
                                        <b>Bir Hafız Yetiştiriyorum</b>
                                    </td>
                                </tr>

                                <tr style = 'box-sizing:
                                    border-box; font-size: 14px; margin: 0;'>
                                    <td class='content-block' style='box-sizing: border-box; font-size: 14px;
                                        vertical-align: top; margin: 0; padding: 0 0 20px;' valign='top'>
                                        Mail/Bülten aboneliğinden çıkmak için <a href='https://hafiz.musdav.org.tr/Home/UnSubscribe?mail=" + mail + @"'> tıklayınız...</a>
                                    </td>
                                </tr>
<tr style='box-sizing:
                                    border-box; font-size: 14px; margin: 0;'>
                                    <td class='content-block' style='text-align: center;box-sizing: border-box; font-size: 14px;
                                        vertical-align: top; margin: 0; padding: 0;' valign='top'><img style='max-height:100px;'
                                            src='https://ditib.it/assets/italya/ItalyaLogo.png' /></td>
                                </tr>
                                <tr style='box-sizing:
                                    border-box; font-size: 14px; margin: 0;'>
                                    <td class='content-block' style='text-align: center;box-sizing: border-box; font-size: 14px;
                                        vertical-align: top; margin: 0; padding: 0;' valign='top'>© 2024 <a
                                            href='https://ditib.it/'>İtalya Diyanet Vakfı </a></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>"
                })
                {
                    if (ccList?.Any() ?? false)
                    {
                        foreach (var item in ccList)
                        {
                            mess.CC.Add(item);
                        }
                    }
                    if (attachments != null)
                    {
                        mess.Attachments.Add(attachments);
                    }
                    mess.To.Add(mail);
                    smtp.Send(mess);
                }
                response.Message = "Başarılı";
                response.Result = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Result = false;
                return response;
            }
        }
    }
}

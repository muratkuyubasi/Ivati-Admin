using System;
using System.IO;
using System.Net;
using System.Text;

namespace ContentManagement.Helper
{
    public static class SMSHelper
    {
        public static ResponseVM Send(string message, string phone)
        {
            message += " SMS İptal: ";

            phone = phone.Replace("(", "");
            phone = phone.Replace(")", "");
            phone = phone.Replace(" ", "");
            phone = phone.Replace("-", "");

            var splitPhone = phone.Split("+");
            if (splitPhone.Length > 1)
                phone = "00" + splitPhone[1];
            else
                phone = "00" + splitPhone[0];

            String xml = "<request>";
            xml += "<authentication>";
            xml += "<username>5323054122</username>";
            xml += "<password>VakifGLOBAL2019..</password>";
            xml += "</authentication>";
            xml += "<order>";
            xml += "<sender>DITIB ITALIA</sender>";
            xml += "<message>";
            xml += "<text>" + message + "</text>";
            xml += "<receipents>";
            xml += "<number>" + phone + "</number>";
            xml += "</receipents>";
            xml += "</message>";
            xml += "</order>";
            xml += "</request>";

            var result = XMLPOST("http://api.iletimerkezi.com/v1/send-sms", xml);

            return result;
        }

        private static ResponseVM XMLPOST(string PostAddress, string xmlData)
        {
            var respon = new ResponseVM();
            try
            {
                var res = "";
                byte[] bytes = Encoding.UTF8.GetBytes(xmlData);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(PostAddress);

                request.Method = "POST";
                request.ContentLength = bytes.Length;
                request.ContentType = "text/xml";
                request.Timeout = 300000000;
                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(bytes, 0, bytes.Length);
                }

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        string message = String.Format(
                        "POST failed. Received HTTP {0}",
                        response.StatusCode);
                        throw new ApplicationException(message);
                    }

                    Stream responseStream = response.GetResponseStream();
                    using (StreamReader rdr = new StreamReader(responseStream))
                    {
                        res = rdr.ReadToEnd();
                    }

                    respon.Result = true;
                    respon.Message = res;
                    return respon;
                }
            }
            catch (Exception ex)
            {
                respon.Message = ex.Message;
                respon.Result = false;
                return respon;
            }
        }
    }

    public class ResponseVM
    {
        public string Message { get; set; }
        public bool Result { get; set; }
    }
}
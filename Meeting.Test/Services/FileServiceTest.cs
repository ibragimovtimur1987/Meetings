using Meeting.Services.Services.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Meeting.Test.Services
{
    public class FileServiceTest : IFileService
    {
        public bool SaveVideoGtm(string downloadUrl, string path)
        {
            //ServicePointManager.Expect100Continue = true;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
            //     | SecurityProtocolType.Tls11
            //     | SecurityProtocolType.Tls12
            //     | SecurityProtocolType.Ssl3;
           // WebClient client = new WebClient();
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            //ServicePointManager.ServerCertificateValidationCallback = (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => { return true; };

            //HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(durl);
            //webRequest.Method = "GET";

            ////here add your certificate

            //using (WebResponse webResponse = webRequest.GetResponse())
            //{
            //    var d = webResponse.GetResponseStream();
            //    File.WriteAllBytes()
            //    // return responseData;
            //}
          //  client.DownloadFile(downloadUrl, path);

            //string durl = "https://cdn.recordingassets.logmeininc.com/200000000000215929/200000000000226271/a93d615f-e937-407a-9fda-f0d4d4f622af/recording/5955441365102668290/transcode/5955441365102668290.mp4?Policy=eyJTdGF0ZW1lbnQiOiBbeyJSZXNvdXJjZSI6Imh0dHBzOi8vY2RuLnJlY29yZGluZ2Fzc2V0cy5sb2dtZWluaW5jLmNvbS8yMDAwMDAwMDAwMDAyMTU5MjkvMjAwMDAwMDAwMDAwMjI2MjcxL2E5M2Q2MTVmLWU5MzctNDA3YS05ZmRhLWYwZDRkNGY2MjJhZi8qIiwiQ29uZGl0aW9uIjp7IkRhdGVMZXNzVGhhbiI6eyJBV1M6RXBvY2hUaW1lIjoxNTgzMjQzMzI4fSwiSXBBZGRyZXNzIjp7IkFXUzpTb3VyY2VJcCI6IjAuMC4wLjAvMCJ9fX1dfQ__&Signature=WZQX8wUxPfR8eeh1U5lYMQBAbxunfvY~pTYqZHi57giv6sGZko3855FSlrgfXcWKiYdCaRI0-hSbYOzrJJVL5qUMZqaWfD3bPRCWN5sHlA4T4PGc4uQ4Jas8OQyLx1FwD4OccGgnyXBKnwlyoHiy9n8~Lqi169xAd0HhtytM39qnHLmcmKWkNcS~2LhICo8Y1ooVOn0Bm-8M2mKudMyZDjLVfNzq0fuSAiQw~2Ho8~zigKdPgB5xh-yo8S~RmLibeG8YHB6pWKxARN24owVX-xtKfA8Lm-lIc1A849FBLvVoQSMZ1dq99ThMGG9NusGRJtaK8v6rz-~lD5AE5F0PDw__&Key-Pair-Id=APKAI2Z3PWL3BWDDZ5IA&response-content-disposition=attachment";
            //path = @"D:\temp\Video3.mp4";
            //X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            //store.Open(OpenFlags.ReadOnly);
            //X509Certificate2Collection certs =
            //                  store.Certificates.Find(X509FindType.FindByTimeValid, DateTime.Now, false);




            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            //ServicePointManager.ServerCertificateValidationCallback = (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => { return true; };

            //HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(durl);
            //webRequest.Method = "GET";
            //webRequest.Timeout = 1000000;

            //for (int i = 0; i < certs.Count; i++)
            //{
            //    if (certs[i].SerialNumber == "0B412F09A94D5092B1F6BCB45B583480")
            //        webRequest.ClientCertificates.Add(certs[i]);
            //}
            //// client. = ;
            ////here add your certificate
            //// client.DownloadFile(durl,path);
            //using (WebResponse webResponse = webRequest.GetResponse())
            //{
            //    var s = webResponse.GetResponseStream();
            //    using (Stream file = File.Create(path))
            //    {
            //        CopyStream(s, file);
            //    }
            //    // return responseData;
            //}

            return true;
        }
        public bool DeleteVideoGtm(string path)
        {
            if (!File.Exists(path))
            {
              // logger.Info($"Начинаем удаление файла {path}");
                File.Delete(path);
                return true;
                //  logger.Info($"Файл {path} удален");
            }
            return false;

        }
        public void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[8 * 1024];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
        }
    }
}

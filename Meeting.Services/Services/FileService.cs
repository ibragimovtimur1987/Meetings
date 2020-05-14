using Meeting.Services.Services.Interface;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Meeting.Services.Services
{
    public class FileService : IFileService
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        public bool SaveVideoGtm(string downloadUrl, string path)
        {
            WebClient client = new WebClient();
            if (!File.Exists(path))
            {
                logger.Info($"Начинаем загрузку файла {downloadUrl} из gtm {path}");
                client.DownloadFile(downloadUrl, path);
                logger.Info($"Файл {downloadUrl} загружен из gtm {path}");
            }
            else
            {
                logger.Info($"Файл {downloadUrl} уже был загружен из gtm по пути {path}");
            }
            return true;
        }
        public bool DeleteVideoGtm(string path)
        {
            if (File.Exists(path))
            {
                logger.Info($"Начинаем удаление файла {path}");
                File.Delete(path);
                logger.Info($"Файл {path} удален");
                return true;
               
            }
            logger.Info($"Файл {path} не удален тк не найден");
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

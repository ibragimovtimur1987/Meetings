using Gotomeeting.Core;
using Gotomeeting.Core.Models;
using LogMeIn.GoToMeeting.Api.Model;
using Meeting.Services.Services;
using Meeting.Services.Services.Interface;
using Meeting.Utils;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Unity;

using VimeoDotNet;
using static Meeting.Entities.Constants;

namespace Meeting
{
    class Program
    {
        static void Main(string[] args)
        {
          //  Console.WriteLine("Start");
            Logger logger = LogManager.GetCurrentClassLogger();
            logger.Info("Start");
            UnityContainer unity = new UnityContainer();
            UnityRegistrator.RegisterServices(unity);
            IMainService mainService = unity.Resolve<IMainService>();
            //   Meeting.Services.Services.MainService mainService = new MainService(unity);
            try
            {
                //  string skipWebinarLicenses = System.Configuration.ConfigurationManager.AppSettings[AppConfigConst.SkipWebinarLicenses];
                // string takeWebinarLicenses = System.Configuration.ConfigurationManager.AppSettings[AppConfigConst.TakeWebinarLicenses];
                if (args.Length == 0 || args.Length == 1)
                {
                    logger.Info("Please enter a numeric arguments.");
                    return ;
                }
                int skipWebinarLicenses;
                bool skipWebinarLicensesExsist = int.TryParse(args[0], out skipWebinarLicenses);
                if (!skipWebinarLicensesExsist)
                {
                    logger.Info("Please enter a skipWebinarLicenses argument.");
                    return ;
                }
                int takeWebinarLicenses;
                bool takeWebinarLicensesExsist = int.TryParse(args[1], out takeWebinarLicenses);
                if (!takeWebinarLicensesExsist)
                {
                    Console.WriteLine("Please enter a takeWebinarLicensesExsist argument.");
                    return ;
                }
                logger.Info($"skipWebinarLicenses {skipWebinarLicenses} takeWebinarLicenses {takeWebinarLicenses}");
                //  int skipWebinarLicensesInt = Convert.ToInt32(skipWebinarLicenses);
                //  int takeWebinarLicensesInt = Convert.ToInt32(takeWebinarLicenses);
                DateTime dateTimeNow = DateTime.Now;
                while (true)
                {
                    mainService.TransferVideo(skipWebinarLicenses, takeWebinarLicenses);
                    Thread.Sleep(4000000);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                mainService.MailService.SendErrorDeveloper(ex.Message.ToString(), $"Произошла ошибка в приложении Meeting");
            }
            logger.Info("Finish");
           // Console.WriteLine("Finish");
           // Console.ReadKey();
        }

    }
}

using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Meeting.Services.Services.Interface;
using Meeting.Utils;
using Unity;
using NLog;
using Meeting.Test.Utils;

namespace Meeting.Test.Tests.Main
{
    /// <summary>
    /// Summary description for MainTestService
    /// </summary>
    [TestClass]
    public class MainServiceTest
    {
        public MainServiceTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        [TestMethod]
        public void CreateMeetings()
        {
            Logger logger = LogManager.GetCurrentClassLogger();
            logger.Info("Start");


            var unity = new UnityContainer();
            UnityTestRegistrator.RegisterServices(unity);
            //IMailService mainService = unity.Resolve<IMainService>();
           // Meeting.Services.Services.MainService MainService = new Meeting.Services.Services.MainService(unity);
            IMainService mainService = unity.Resolve<IMainService>();
            try
            {
                mainService.CreateMeetings();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                mainService.MailService.SendErrorDeveloper(ex.Message.ToString(), $"Произошла ошибка в приложении Meeting");
            }
        }
        [TestMethod]
        public void TransferVideo()
        {
            Logger logger = LogManager.GetCurrentClassLogger();
            logger.Info("Start");


            var unity = new UnityContainer();
            UnityTestRegistrator.RegisterServices(unity);
            //  Meeting.Services.Services.MainService MainService = new Meeting.Services.Services.MainService(unity);
            IMainService mainService = unity.Resolve<IMainService>();
            try
            {
                mainService.TransferVideo(0,30);
              //  MainService.TransferVideo();
            }
            catch(Exception ex)
            {
                logger.Error(ex);
               // MainService.MailService.SendErrorDeveloper(ex.Message.ToString(), $"Произошла ошибка в приложении Meeting");
            }
        }
    }
}

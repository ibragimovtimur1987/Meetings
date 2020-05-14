using System;
using Meeting.Services.Services;
using Meeting.Services.Services.Interface;
using Meeting.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;

namespace Meeting.Test.Tests
{
    [TestClass]
    public class SpecialistWebServiceTest
    {
        [TestMethod]
        public void RefreshAlbum()
        {
            var unity = new UnityContainer();
            UnityRegistrator.RegisterServices(unity);

            ISpecialistWebService specialistReplService = unity.Resolve<ISpecialistWebService>();
           // ISpecialistGtmService specialistGtmService = unity.Resolve<ISpecialistGtmService>();
          //  specialistReplService.SpecialistGtmService = specialistGtmService;
          //  IVimeoGetVideosService vimeoGetVideosService = unity.Resolve <IVimeoGetVideosService>();
          //  specialistReplService.VimeoGetVideosService = vimeoGetVideosService;
            specialistReplService.RefreshAlbum(6892405); ;
        }
    }
}

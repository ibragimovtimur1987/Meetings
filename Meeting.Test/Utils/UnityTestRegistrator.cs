using Gotomeeting.Core;
using Meeting.Services.Interface;
using Meeting.Services.Services;
using Meeting.Services.Services.Interface;
using Meeting.Test.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Meeting.Test.Utils
{
    public static class UnityTestRegistrator
    {
        public static IUnityContainer Container { get; private set; }
        public static void RegisterServices(IUnityContainer container)
        {
            Container = container;

            container
                .RegisterType<IGotomeetingService, GotomeetingServiceTest>()
                .RegisterType<IVimeoSevice, VimeoService>()
                .RegisterType<IFileService, Meeting.Test.Services.FileServiceTest>()
                .RegisterType<ISpecialistGtmService, SpecialistGtmService>()
                .RegisterType<ISpecialistReplService, SpecialistReplService>()
                .RegisterType<IPioneerService, PioneerServiceTest>()
                .RegisterType<IMailService, MailService>()
                .RegisterType<IGetGroupService, GetGroupService>()
                .RegisterType<IMainService, MainService>()
                .RegisterType<IVimeoGetVideosService, VimeoGetVideosService>()
                .RegisterType<ISpecialistWebService, SpecialistWebService>();

            //IGotomeetingService gotomeetingService = container.Resolve<IGotomeetingService>();
            //IFileService fileService = container.Resolve<IFileService>();
            //IVimeoSevice vimeoSevice = container.Resolve<IVimeoSevice>();
            //ISpecialistGtmService specialistGtmService = container.Resolve<ISpecialistGtmService>();
            //ISpecialistReplService specialistReplService = container.Resolve<ISpecialistReplService>();
            //IPioneerService pioneerService = container.Resolve<IPioneerService>();
            //IMailService mailService = container.Resolve<IMailService>();
            //IGetGroupService getGroupService = container.Resolve<IGetGroupService>();

        }

        //public static void RegisterControls(IUnityContainer container, Assembly assembly)
        //{
        //    Type[] assemblyTypes = assembly.GetTypes();
        //    foreach (Type type in assemblyTypes)
        //    {
        //        if (typeof(IController).IsAssignableFrom(type))
        //        {
        //            container.RegisterType(type, type);
        //        }
        //    }
        //}
    }
}

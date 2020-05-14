using Gotomeeting.Core;
using Meeting.Services.Interface;
using Meeting.Services.Services;
using Meeting.Services.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Meeting.Utils
{
    public static class UnityRegistrator
    {
        public static IUnityContainer Container { get; private set; }
        public static void RegisterServices(IUnityContainer container)
        {
            Container = container;

            container
                .RegisterType<IGotomeetingService, GotomeetingService>()
                .RegisterType<IVimeoSevice, VimeoService>()
                .RegisterType<IFileService, FileService>()
                .RegisterType<ISpecialistGtmService, SpecialistGtmService>()
                .RegisterType<ISpecialistReplService, SpecialistReplService>()
                .RegisterType<IPioneerService, PioneerService>()
                .RegisterType<IMailService, MailService>()
                .RegisterType<IMainService, MainService>()
                .RegisterType<IGetGroupService, GetGroupService>()
                .RegisterType<IVimeoGetVideosService, VimeoGetVideosService>()
                .RegisterType<ISpecialistWebService, SpecialistWebService>()
                ;

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

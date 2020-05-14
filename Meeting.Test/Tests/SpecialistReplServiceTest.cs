using System;
using Meeting.Services.Services.Interface;
using Meeting.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;

namespace Meeting.Test.Tests
{
    [TestClass]
    public class SpecialistReplService
    {
        [TestMethod]
        public void GetNameAlbum()
        {

            var unity = new UnityContainer();
            UnityRegistrator.RegisterServices(unity);

            ISpecialistReplService specialistReplService = unity.Resolve<ISpecialistReplService>();
            specialistReplService.GetNameAlbum("320010");
        }
    }
}

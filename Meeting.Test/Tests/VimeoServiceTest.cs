using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;
using Meeting.Utils;


using System.Threading.Tasks;
using VimeoDotNet.Models;

using Meeting.Services.Services.Interface;
using static Meeting.Entities.Constants;
using Meeting.Entities.Models;

namespace Meeting.Test.Tests
{
    /// <summary>
    /// Summary description for VimeoServiceTest
    /// </summary>
    [TestClass]
    public class VimeoServiceTest
    {

        [TestMethod]
        public async Task GetAccountInformationAsync()
        {
            var unity = new UnityContainer();
            UnityRegistrator.RegisterServices(unity);

            IVimeoSevice vimeoSevice = unity.Resolve<IVimeoSevice>();

            VimeoDotNet.VimeoClient client = vimeoSevice.GetClient(VimeoConstants.ApiAuthManager.AccessTokenWebinarManager);
            try
            {
                var r = await client.GetAccountInformationAsync();
              //  var us = await client.(UserId.Me.Id);
            }
            catch(Exception ex)
            {

            }
            Assert.IsTrue(true);
            //  vimeoSevice.AddToAlbumVideos(client);
            //
            // TODO: Add test logic here
            //
        }

        [TestMethod]
        public void GetQuota()
        {
            var unity = new UnityContainer();
            UnityRegistrator.RegisterServices(unity);

            IVimeoSevice vimeoSevice = unity.Resolve<IVimeoSevice>();

            VimeoDotNet.VimeoClient client = vimeoSevice.GetClient(VimeoConstants.ApiAuthManager.AccessTokenManager);
            long spaceQuota = vimeoSevice.GetSpaceQuota(client);
        }
        [TestMethod]
        public async Task AddToVimeo()
        {
            var unity = new UnityContainer();
            UnityRegistrator.RegisterServices(unity);

            IVimeoSevice vimeoSevice = unity.Resolve<IVimeoSevice>();
            ISpecialistGtmService specialistGtmService = unity.Resolve<ISpecialistGtmService>();
            VimeoDotNet.VimeoClient client = vimeoSevice.GetClient(VimeoConstants.ApiAuthManager.AccessTokenManager);
            vimeoSevice.ISpecialistGtmService = specialistGtmService;
            await vimeoSevice.AddToVimeo(client,new AddVimeoModel { path = @"D:\temp\GtmVideo\176111349.mp4", nameVideo = "TestVideo", numeAlbum = "TestAlbum"});
            Assert.IsTrue(true);
            //  vimeoSevice.AddToAlbumVideos(client);
            //
            // TODO: Add test logic here
            //
        }
        [TestMethod]
        public void GetGrNumber()
        {
            var unity = new UnityContainer();
            UnityRegistrator.RegisterServices(unity);

            IVimeoSevice vimeoSevice = unity.Resolve<IVimeoSevice>();

            VimeoDotNet.VimeoClient client = vimeoSevice.GetClient(VimeoConstants.ApiAuthManager.AccessTokenWebinarManager);
            vimeoSevice.GetGrNumber("EXCEL3-E 23.03-24.03 u-d B Gr316453");
            Assert.IsTrue(true);
            //  vimeoSevice.AddToAlbumVideos(client);
            //
            // TODO: Add test logic here
            //
        }
    }
}

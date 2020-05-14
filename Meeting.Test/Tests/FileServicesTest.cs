using Meeting.Services.Services;
using Meeting.Services.Services.Interface;
using Meeting.Test.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Meeting.Test.Tests
{
    [TestClass]
    public class FileServicesTest
    {
        [TestMethod]
        public void SaveVideoGtm()
        {
            var unity = new UnityContainer();
            UnityTestRegistrator.RegisterServices(unity);
           // string durl = "https://cdn.recordingassets.logmeininc.com/200000000000215929/200000000000226271/a93d615f-e937-407a-9fda-f0d4d4f622af/recording/5955441365102668290/transcode/5955441365102668290.mp4?Policy=eyJTdGF0ZW1lbnQiOiBbeyJSZXNvdXJjZSI6Imh0dHBzOi8vY2RuLnJlY29yZGluZ2Fzc2V0cy5sb2dtZWluaW5jLmNvbS8yMDAwMDAwMDAwMDAyMTU5MjkvMjAwMDAwMDAwMDAwMjI2MjcxL2E5M2Q2MTVmLWU5MzctNDA3YS05ZmRhLWYwZDRkNGY2MjJhZi8qIiwiQ29uZGl0aW9uIjp7IkRhdGVMZXNzVGhhbiI6eyJBV1M6RXBvY2hUaW1lIjoxNTgzMjM3MjM5fSwiSXBBZGRyZXNzIjp7IkFXUzpTb3VyY2VJcCI6IjAuMC4wLjAvMCJ9fX1dfQ__&Signature=P6GZbqGZJfCQLhX0OQ-BStMJ9PragXjXajwNG1QNH5OdyP8uLGayRqZh7zqP1c4hx56GgWNrf1zTQf6hV5ytCZH5zT2Trpjz998RZzbIcGDd1XPtBp54zzSEL2m~cMullrZlc-aXtNGs6jaZBW1ap2hxHVQPqOm~~JdqldQT3oXXh603pm6zrl0E0EZbpCImx1z93fUcOG6UIGIZWYkuQ9q9T7NEbGKVeMi2jvtzWv~Q-wTfcjnadS0YHcYygLFbqEAIb2IgApWE8bBaPlxIcCoYhKdfJx7ceMnpn44bf7cV8yMHimex4eJoSyXmap8vmMc0oYs851LnYKyJcSHtMw__&Key-Pair-Id=APKAI2Z3PWL3BWDDZ5IA&response-content-disposition=attachment";
            string durl = "https://cdn.recordingassets.logmeininc.com/200000000000215929/200000000000226271/a93d615f-e937-407a-9fda-f0d4d4f622af/recording/5955441365102668290/transcode/5955441365102668290.mp4?Policy=eyJTdGF0ZW1lbnQiOiBbeyJSZXNvdXJjZSI6Imh0dHBzOi8vY2RuLnJlY29yZGluZ2Fzc2V0cy5sb2dtZWluaW5jLmNvbS8yMDAwMDAwMDAwMDAyMTU5MjkvMjAwMDAwMDAwMDAwMjI2MjcxL2E5M2Q2MTVmLWU5MzctNDA3YS05ZmRhLWYwZDRkNGY2MjJhZi8qIiwiQ29uZGl0aW9uIjp7IkRhdGVMZXNzVGhhbiI6eyJBV1M6RXBvY2hUaW1lIjoxNTgzMjQzMzI4fSwiSXBBZGRyZXNzIjp7IkFXUzpTb3VyY2VJcCI6IjAuMC4wLjAvMCJ9fX1dfQ__&Signature=WZQX8wUxPfR8eeh1U5lYMQBAbxunfvY~pTYqZHi57giv6sGZko3855FSlrgfXcWKiYdCaRI0-hSbYOzrJJVL5qUMZqaWfD3bPRCWN5sHlA4T4PGc4uQ4Jas8OQyLx1FwD4OccGgnyXBKnwlyoHiy9n8~Lqi169xAd0HhtytM39qnHLmcmKWkNcS~2LhICo8Y1ooVOn0Bm-8M2mKudMyZDjLVfNzq0fuSAiQw~2Ho8~zigKdPgB5xh-yo8S~RmLibeG8YHB6pWKxARN24owVX-xtKfA8Lm-lIc1A849FBLvVoQSMZ1dq99ThMGG9NusGRJtaK8v6rz-~lD5AE5F0PDw__&Key-Pair-Id=APKAI2Z3PWL3BWDDZ5IA&response-content-disposition=attachment";
            IFileService fileService = unity.Resolve<IFileService>();
            string path = @"D:\temp\Video3.mp4";
          //  string path = $"{GtmConstants.FileConstants.Path}\\1111{GtmConstants.FileConstants.Expansion}";
            fileService.SaveVideoGtm(durl, path);
        }
    }
}

using FluentAssertions;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using SoundConnect.Survey.Application.ErrorLogger;
using System;
using System.IO;

namespace SoundConnect.Survey.Application.Tests
{
    [TestFixture]
    public class EmailSenderAppServiceTest
    {
        private Mock<ILogger> _logger;

        private EmailSenderAppService _sut;

        private IFixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _logger = _fixture.Freeze<Mock<ILogger>>();
            _sut = _fixture.Create<EmailSenderAppService>();

            CreateFolderForTest(); 
        }

        private void CreateFolderForTest()
        {
            string path = @"C:\FaxLogs\Emails";

            try
            {
                if (Directory.Exists(path))
                {
                    return;
                }

                DirectoryInfo di = Directory.CreateDirectory(path);
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
            finally { }
        }

        [Test]
        public void SendEmail_ValidEmailAddress_ReturnTrue()
        {
            var byteArrayFile = _fixture.Create<byte[]>();
            var toEmailAddress = "test@testing.com";

            var actual = _sut.SendEmail(byteArrayFile, toEmailAddress);

            actual.Should().BeTrue();
        }

        [Test]
        public void SendEmail_ValidEmailAddress_ReturnFalse()
        {
            var byteArrayFile = _fixture.Create<byte[]>();
            var toEmailAddress = "testtesting.com";

            var actual = _sut.SendEmail(byteArrayFile, toEmailAddress);

            actual.Should().BeFalse();
        }
    }
}

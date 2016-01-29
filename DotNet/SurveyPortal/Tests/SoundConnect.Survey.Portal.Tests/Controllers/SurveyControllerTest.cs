using FluentAssertions;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using SoundConnect.Survey.Application.DTOs;
using SoundConnect.Survey.Application.Interfaces;
using SoundConnect.Survey.Core.Common;
using SoundConnect.Survey.Portal.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SoundConnect.Survey.Portal.Tests.Controllers
{
    [TestFixture]
    public class SurveyControllerTest
    {
        private Mock<ISurveyAppService> _surveyAppService;

        private SurveyController _sut;

        private IFixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _surveyAppService = new Mock<ISurveyAppService>();
            _sut = new SurveyController(_surveyAppService.Object);
        }

        [Test]
        public void Survey_SurveyCodeNullorEmpty_ReturnNull()
        {
            var surveyCode = String.Empty;

            var actual = _sut.Index(surveyCode);

            actual.Should().BeNull();
        }

        [Test]
        public void Survey_SurveyCodeNotNullorEmpty_CallGetBySurveyCodeMethod()
        {
            var expected = _fixture.Create<SurveyDto>();
            string code = _fixture.Create<string>();
            _surveyAppService.Setup(m => m.GetBySurveyCode(code)).Returns(expected).Verifiable();

            var actual = _sut.Index(code);

            actual.Should().NotBeNull();
            _surveyAppService.VerifyAll();
        }

        [Test]
        public void Survey_SurveyDtoNull_ReturnMessageSurveyNotExists()
        {
            string expectedMessage = @"The survey not exists";
            SurveyDto expected = null;
            string code = _fixture.Create<string>();
            _surveyAppService.Setup(m => m.GetBySurveyCode(code)).Returns(expected);

            var actual = _sut.Index(code) as ViewResult;

            actual.Should().NotBeNull();
            actual.Should().BeOfType<ViewResult>();
            expectedMessage.Should().Equals(actual.ViewBag.Message);
        }

        [Test]
        public void Survey_SurveyCodeForPCPSurvey_ReturnRedirectToRouteResult()
        {
            var expected = _fixture.Create<SurveyDto>();
            string code = _fixture.Create<string>();
            expected.SurveyType = 0;
            _surveyAppService.Setup(m => m.GetBySurveyCode(code)).Returns(expected);

            var actual = _sut.Index(code);

            actual.Should().NotBeNull();
            actual.Should().BeOfType<RedirectToRouteResult>();
        }

        [Test]
        public void Survey_SurveyCodeForSpecialistSurvey_ReturnRedirectToRouteResult()
        {
            var expected = _fixture.Create<SurveyDto>();
            string code = _fixture.Create<string>();
            expected.SurveyType = (SurveyType)1;
            _surveyAppService.Setup(m => m.GetBySurveyCode(code)).Returns(expected);

            var actual = _sut.Index(code);

            actual.Should().NotBeNull();
            actual.Should().BeOfType<RedirectToRouteResult>();
        }

        [Test]
        public void Survey_SurveyCodeForCareTeamSurvey_ReturnRedirectToRouteResult()
        {
            var expected = _fixture.Create<SurveyDto>();
            string code = _fixture.Create<string>();
            expected.SurveyType = (SurveyType)2;
            _surveyAppService.Setup(m => m.GetBySurveyCode(code)).Returns(expected);

            var actual = _sut.Index(code);

            actual.Should().NotBeNull();
            actual.Should().BeOfType<RedirectToRouteResult>();
        }
    }
}

using FluentAssertions;

using Moq;

using NUnit.Framework;

using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

using SoundConnect.Survey.Application;
using SoundConnect.Survey.Application.DTOs;
using SoundConnect.Survey.Application.Interfaces;
using SoundConnect.Survey.Application.Common;

using SoundConnect.Survey.Core.Repositories;
using SoundConnect.Survey.Core.Entities;

using System.Collections.Generic;


namespace SoundConnect.Survey.Portal.Tests.Application
{
    [TestFixture]
    public class SurveyAppServiceTest
    {
        private Mock<ISurveyRepository> _surveyAppRepository;

        private SurveyAppService _sut;

        private IFixture _fixture;

        private Core.Entities.Survey _expected;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {            
            DtoMappings.Map();
        }

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _surveyAppRepository = _fixture.Freeze<Mock<ISurveyRepository>>();
            _sut = _fixture.Create<SurveyAppService>();
            _expected = _fixture.Create<SoundConnect.Survey.Core.Entities.Survey>();
        }

        [Test]
        public void GetBySurveyCode_NotNullOrEmpty_ReturnSurveyDto()
        {
            string code = _fixture.Create<string>();
            _surveyAppRepository.Setup(x => x.GetBySurveyCode(code)).Returns(_expected).Verifiable();

            var actual = _sut.GetBySurveyCode(code);

            actual.ShouldBeEquivalentTo(_expected);
            _surveyAppRepository.VerifyAll();
        }

        [Test]
        public void GetBySurveyCode_NullOrEmpty_ReturnNull()
        {
            string code = string.Empty;
            _surveyAppRepository.Setup(x => x.GetBySurveyCode(code)).Returns(_expected);

            //Act
            var actual = _sut.GetBySurveyCode(code);

            actual.Should().BeNull();
        }

        [Test]
        public void UpdateComplete_SurveyIdExist_UpdateCompleteSurveyMethodWasCall()
        {
            int surveyId = _expected.Id;
            _surveyAppRepository.Setup(d => d.UpdateCompleteSurvey(surveyId)).Verifiable();

            _sut.UpdateComplete(_expected.Id);

            _surveyAppRepository.VerifyAll();
        }

    }
}

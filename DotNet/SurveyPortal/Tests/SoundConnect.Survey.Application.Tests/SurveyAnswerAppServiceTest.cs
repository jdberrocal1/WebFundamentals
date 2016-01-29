using FluentAssertions;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using SoundConnect.Survey.Application.Common;
using SoundConnect.Survey.Application.DTOs;
using SoundConnect.Survey.Application.Interfaces;
using SoundConnect.Survey.Core.Entities;
using SoundConnect.Survey.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundConnect.Survey.Application.Tests
{
    [TestFixture]
    public class SurveyAnswerAppServiceTest
    {
        private Mock<ISurveyAnswerRepository> _surveyAnswerAppRepository;

        private SurveyAnswerAppService _sut;

        private IFixture _fixture;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            DtoMappings.Map();
        }

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _surveyAnswerAppRepository = _fixture.Freeze<Mock<ISurveyAnswerRepository>>();
            _sut = _fixture.Create<SurveyAnswerAppService>();
        }

        [Test]
        public void Insert_SurveyAnswerDtoNotNull_ReturnSurveyAnswerDto()
        {
            int id = _fixture.Create<int>();
            int surveyId = _fixture.Create<int>();
            DateTime respondDate = _fixture.Create<DateTime>();
            var surveyAnswer = new SurveyAnswer() { Id = id, SurveyId = surveyId, RespondDate = respondDate};
            var expectedsurveyAnswerDto = new SurveyAnswerDto() { Id = id, SurveyId = surveyId, RespondDate = respondDate };
            _surveyAnswerAppRepository.Setup(x => x.Create(It.IsAny<SurveyAnswer>())).Returns(surveyAnswer).Verifiable();

            var actual = _sut.Insert(expectedsurveyAnswerDto);

            actual.ShouldBeEquivalentTo(expectedsurveyAnswerDto);
            _surveyAnswerAppRepository.VerifyAll();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Insert_SurveyAnswerDtoNull_ReturnSurveyAnswerDto()
        {
            int id = _fixture.Create<int>();
            int surveyId = _fixture.Create<int>();
            DateTime respondDate = _fixture.Create<DateTime>();
            var surveyAnswer = new SurveyAnswer() { Id = id, SurveyId = surveyId, RespondDate = respondDate };
            var expectedsurveyAnswerDto = new SurveyAnswerDto() { Id = id, SurveyId = surveyId, RespondDate = respondDate };
            _surveyAnswerAppRepository.Setup(x => x.Create(It.IsAny<SurveyAnswer>())).Throws<ArgumentNullException>();

            var actual = _sut.Insert(null);
        }
    }
}

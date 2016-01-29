using FluentAssertions;
using Moq;

using NUnit.Framework;

using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture;

using SoundConnect.Survey.Application.DTOs;
using SoundConnect.Survey.Application;
using SoundConnect.Survey.Core.Repositories;
using SoundConnect.Survey.Core.Entities;
using SoundConnect.Survey.Application.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SoundConnect.Survey.Portal.Tests.Application
{
    [TestFixture]
    public class AnswerAppServiceTest
    {
        private Mock<IAnswerRepository> _answerRepository;

        private AnswerAppService _sut;

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
            _answerRepository = _fixture.Freeze<Mock<IAnswerRepository>>();
            _sut = _fixture.Create<AnswerAppService>();
        }


        [Test]
        public void GetBySurveyId_GreaterThanZero_ReturnAnswerDtoList()
        {
            var id = _fixture.Create<int>();
            var expected = _fixture.CreateMany<Answer>().ToList();
            _answerRepository.Setup(x => x.GetBySurveyId(id)).Returns(expected).Verifiable();

            var actual = _sut.GetBySurveyId(id);

            actual.ShouldAllBeEquivalentTo(expected);
            _answerRepository.VerifyAll();
        }

        [Test]
        public void GetBySurveyId_LessThanOrEqualZero_ReturnNull()
        {
            var actual = _sut.GetBySurveyId(0);

            //actual.Should().BeNull();
        }


        [Test]
        public void Insert_WhenAnswerDtoIsNotEmpty_CreateMethodWasCall()
        {
            var answerDtoList = _fixture.CreateMany<AnswerDto>(1).ToList();
            _answerRepository.Setup(
                d =>
                d.Create(
                    It.Is<Answer>(
                        p =>
                        p.Id.Equals(answerDtoList.First().Id)
                        && p.SurveyAnswerId.Equals(answerDtoList.First().SurveyAnswerId)
                        && p.Note.Equals(answerDtoList.First().Note)
                        && p.ResponseText.Equals(answerDtoList.First().ResponseText)
                        && p.QuestionId.Equals(answerDtoList.First().QuestionId)))).Verifiable();

            _sut.Insert(answerDtoList);

            _answerRepository.VerifyAll();
        }
    }
}

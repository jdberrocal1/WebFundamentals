using FluentAssertions;

using Moq;

using NUnit.Framework;

using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

using SoundConnect.Survey.Application;
using SoundConnect.Survey.Application.Common;
using SoundConnect.Survey.Core.Common;
using SoundConnect.Survey.Core.Entities;
using SoundConnect.Survey.Core.Repositories;
using System.Linq;


namespace SoundConnect.Survey.Portal.Tests.Application
{
    [TestFixture]
    public class QuestionAppServiceTest
    {
        private Mock<IQuestionRepository> _questionRepository;

        private QuestionAppService _sut;

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
            _questionRepository = _fixture.Freeze<Mock<IQuestionRepository>>();
            _sut = _fixture.Create<QuestionAppService>();
        }

        [Test]
        public void GetBySurveyType_GreaterThanZero_ReturnQuestionDtoList()
        {
            var type = _fixture.Create<SurveyType>();
            var expected = _fixture.CreateMany<Question>().ToList();
            _questionRepository.Setup(x => x.GetBySurveyType(type)).Returns(expected).Verifiable();

            var actual = _sut.GetBySurveyType(type);

            actual.ShouldAllBeEquivalentTo(expected);
            _questionRepository.VerifyAll();
        }

        /*[Test]
        public void GetBySurveyType_LessThanZero_ReturnNull()
        {
            var actual = _sut.GetBySurveyType(-1);

            actual.Should().BeNull();
        }*/
    }
}

using FluentAssertions;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using SoundConnect.Survey.Application.Common;
using SoundConnect.Survey.Core.Entities;
using SoundConnect.Survey.Data;
using SoundConnect.Survey.DBContext.Repository;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;

namespace SoundConnect.Survey.DBContext.Tests
{
    [TestFixture]
    public class AnswerRepositoryTest
    {
        private Mock<IDataContext> _dataContext;
        private AnswerRepository _sut;

        private IFixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _dataContext = new Mock<IDataContext>();

            _sut = new AnswerRepository(_dataContext.Object);
        }

        [Test]
        public void GetBySurveyId_SurveyIdExists_ReturnAnswer() 
        {
            var surveyIdArray = _fixture.Create<int[]>();
            var expectedAnswerList = _fixture.Create<List<Answer>>().AsQueryable();
            expectedAnswerList.ElementAt(0).SurveyAnswerId = surveyIdArray[0];
            expectedAnswerList.ElementAt(1).SurveyAnswerId = surveyIdArray[1];
            expectedAnswerList.ElementAt(2).SurveyAnswerId = surveyIdArray[2];
            var dbSetMock = new Mock<IDbSet<Answer>>();
            dbSetMock.As<IQueryable<Answer>>().Setup(m => m.Provider).Returns(expectedAnswerList.Provider);
            dbSetMock.As<IQueryable<Answer>>().Setup(m => m.Expression).Returns(expectedAnswerList.Expression);
            dbSetMock.As<IQueryable<Answer>>().Setup(m => m.ElementType).Returns(expectedAnswerList.ElementType);
            dbSetMock.As<IQueryable<Answer>>().Setup(m => m.GetEnumerator()).Returns(expectedAnswerList.GetEnumerator());
            _dataContext.Setup(d => d.Answer).Returns(dbSetMock.Object);

            var actual = _sut.GetBySurveyId(surveyIdArray[1]);

            actual.Should().NotBeNull();
            actual.Count.Should().Equals(1);
            actual.First().Note.Should().Equals(expectedAnswerList.ElementAt(1).Note);
        }

        [Test]
        public void GetBySurveyId_SurveyIdNotExists_ReturnEmptyObject()
        {
            var expectedAnswerList = _fixture.Create<List<Answer>>().AsQueryable();
            expectedAnswerList.ElementAt(0).SurveyAnswerId = 12;
            expectedAnswerList.ElementAt(1).SurveyAnswerId = 15;
            expectedAnswerList.ElementAt(2).SurveyAnswerId = 24;
            var dbSetMock = new Mock<IDbSet<Answer>>();
            dbSetMock.As<IQueryable<Answer>>().Setup(m => m.Provider).Returns(expectedAnswerList.Provider);
            dbSetMock.As<IQueryable<Answer>>().Setup(m => m.Expression).Returns(expectedAnswerList.Expression);
            dbSetMock.As<IQueryable<Answer>>().Setup(m => m.ElementType).Returns(expectedAnswerList.ElementType);
            dbSetMock.As<IQueryable<Answer>>().Setup(m => m.GetEnumerator()).Returns(expectedAnswerList.GetEnumerator());
            _dataContext.Setup(d => d.Answer).Returns(dbSetMock.Object);

            var actual = _sut.GetBySurveyId(13);

            actual.Should().NotBeNull();
            actual.Count.Should().Equals(0);
        }
    }
}

using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using System.Data.Common;
using SoundConnect.Survey.EntityFramework;
using SoundConnect.Survey.DBContext.Repository;
using SoundConnect.Survey.Data;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using SoundConnect.Survey.Core.Entities;
using System.Data.Entity;
using SoundConnect.Survey.Core.Common;

namespace SoundConnect.Survey.DBContext.Tests
{
    [TestFixture]
    public class QuestionRepositoryTest
    {
        private Mock<IDataContext> _dataContext;
        private QuestionRepository _sut;

        private IFixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _dataContext = new Mock<IDataContext>();

            _sut = new QuestionRepository(_dataContext.Object);
        }

        [Test]
        public void GetBySurveyType_SurveyTypeExists_ReturnQuestion()
        {
            var expectedQuestionList = _fixture.Create<List<Question>>().AsQueryable();
            expectedQuestionList.ElementAt(0).SurveyType = 0;
            expectedQuestionList.ElementAt(1).SurveyType = 1;
            expectedQuestionList.ElementAt(2).SurveyType = 1;
            var dbSetMock = new Mock<IDbSet<Question>>();
            dbSetMock.As<IQueryable<Question>>().Setup(m => m.Provider).Returns(expectedQuestionList.Provider);
            dbSetMock.As<IQueryable<Question>>().Setup(m => m.Expression).Returns(expectedQuestionList.Expression);
            dbSetMock.As<IQueryable<Question>>().Setup(m => m.ElementType).Returns(expectedQuestionList.ElementType);
            dbSetMock.As<IQueryable<Question>>().Setup(m => m.GetEnumerator()).Returns(expectedQuestionList.GetEnumerator());
            _dataContext.Setup(d => d.Question).Returns(dbSetMock.Object);

            var actual = _sut.GetBySurveyType((SurveyType)1);

            actual.Should().NotBeNull();
            actual.Count.Should().Equals(2);
            actual.First().QuestionText.Should().Equals(expectedQuestionList.ElementAt(1).QuestionText);
        }

        [Test]
        public void GetBySurveyType_SurveyTypeNotExists_ReturnEmptyObject()
        {
            var expectedQuestionList = _fixture.Create<List<Question>>().AsQueryable();
            expectedQuestionList.ElementAt(0).SurveyType = 0;
            expectedQuestionList.ElementAt(1).SurveyType = 1;
            expectedQuestionList.ElementAt(2).SurveyType = 1;
            var dbSetMock = new Mock<IDbSet<Question>>();
            dbSetMock.As<IQueryable<Question>>().Setup(m => m.Provider).Returns(expectedQuestionList.Provider);
            dbSetMock.As<IQueryable<Question>>().Setup(m => m.Expression).Returns(expectedQuestionList.Expression);
            dbSetMock.As<IQueryable<Question>>().Setup(m => m.ElementType).Returns(expectedQuestionList.ElementType);
            dbSetMock.As<IQueryable<Question>>().Setup(m => m.GetEnumerator()).Returns(expectedQuestionList.GetEnumerator());
            _dataContext.Setup(d => d.Question).Returns(dbSetMock.Object);

            var actual = _sut.GetBySurveyType((SurveyType)2);

            actual.Should().NotBeNull();
            actual.Count.Should().Equals(0);
        }
    }
}

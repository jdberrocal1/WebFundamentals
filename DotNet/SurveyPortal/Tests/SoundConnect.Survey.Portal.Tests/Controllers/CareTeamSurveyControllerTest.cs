using FluentAssertions;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using SoundConnect.Survey.Application.DTOs;
using SoundConnect.Survey.Application.Interfaces;
using SoundConnect.Survey.Core.Common;
using SoundConnect.Survey.Portal.Controllers;
using SoundConnect.Survey.Portal.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SoundConnect.Survey.Portal.Tests.Controllers
{
    [TestFixture]
    public class CareTeamSurveyControllerTest
    {
        private Mock<ISurveyAppService> _surveyAppService;

        private Mock<IQuestionAppService> _questionAppService;

        private Mock<IAnswerAppService> _answerAppService;

        private Mock<ISurveyAnswerAppService> _surveyAnswerAppService;

        private Mock<IEmailSenderAppService> _emailSenderAppService;

        private CareTeamSurveyController _sut;

        private IFixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _surveyAppService = new Mock<ISurveyAppService>();
            _questionAppService = new Mock<IQuestionAppService>();
            _answerAppService = new Mock<IAnswerAppService>();
            _surveyAnswerAppService = new Mock<ISurveyAnswerAppService>();
            _emailSenderAppService = new Mock<IEmailSenderAppService>();
            _sut = new CareTeamSurveyController(_surveyAppService.Object, _questionAppService.Object, _answerAppService.Object, _surveyAnswerAppService.Object, _emailSenderAppService.Object);
        }

        private List<QuestionDto> SetQuestionKeyValue(List<QuestionDto> questionsDto)
        {
            questionsDto[0].Key = "CT_Q_1";
            questionsDto[1].Key = "CT_Q_2";
            questionsDto[2].Key = "CT_Q_3A";
            questionsDto[3].Key = "CT_Q_3B";
            questionsDto[4].Key = "CT_Q_3C";
            questionsDto[5].Key = "CT_Q_3D";
            questionsDto[6].Key = "CT_Q_3E";
            questionsDto[7].Key = "CT_Q_3F";
            questionsDto[8].Key = "CT_Q_3G";
            questionsDto[9].Key = "CT_Q_3H";
            questionsDto[10].Key = "CT_Q_3I";
            questionsDto[11].Key = "CT_Q_3J";
            questionsDto[12].Key = "CT_Q_3K";
            questionsDto[13].Key = "CT_Q_3L";
            questionsDto[14].Key = "CT_Q_3M";
            questionsDto[15].Key = "CT_Q_3N";
            questionsDto[16].Key = "CT_Q_3O";
            questionsDto[17].Key = "CT_Q_3WHY";
            questionsDto[18].Key = "CT_Q_4";
            questionsDto[19].Key = "CT_Q_5";
            questionsDto[20].Key = "CT_Q_6";
            return questionsDto;
        }

        [Test]
        public void Index_CareTeamSurveyExist_ReturnViewResult()
        {
            var surveyDto = _fixture.Build<SurveyDto>()
                                    .With(x => x.IsComplete, false)
                                    .With(x => x.IsExpired, false)
                                    .With(x => x.SurveyType, Core.Common.SurveyType.CareTeam)
                                    .Create();
            string code = _fixture.Create<string>();
            _surveyAppService.Setup(m => m.GetBySurveyCode(code)).Returns(surveyDto);

            var actual = _sut.Index(code);

            actual.Should().NotBeNull();
            actual.Should().BeOfType<ViewResult>();
        }

        [Test]
        public void Index_CodeNotNullOrEmptyAndExists_ReturnCareTeamSurvey()
        {
            var expected = _fixture.Build<SurveyDto>()
                                    .With(x => x.IsComplete, false)
                                    .With(x => x.IsExpired, false)
                                    .With(x => x.SurveyType, Core.Common.SurveyType.CareTeam)
                                    .Create();
            string code = _fixture.Create<string>();
            _surveyAppService.Setup(m => m.GetBySurveyCode(code)).Returns(expected);

            var actual = ((_sut.Index(code) as ViewResult).Model);

            actual.Should().NotBeNull();
            actual.Should().BeOfType<CareTeamSurvey>();
        }

        [Test]
        public void Index_CodeNotNullOrEmpty_GetBySurveyCodeMethodWasCalled()
        {
            var expected = _fixture.Build<SurveyDto>()
                                    .With(x => x.IsComplete, false)
                                    .With(x => x.IsExpired, false)
                                    .With(x => x.SurveyType, Core.Common.SurveyType.CareTeam)
                                    .Create();
            string code = _fixture.Create<string>();
            _surveyAppService.Setup(m => m.GetBySurveyCode(code)).Returns(expected).Verifiable();

            var actual = _sut.Index(code);

            actual.Should().NotBeNull();
            _surveyAppService.VerifyAll();
        }

        [Test]
        public void Index_SurveyDtoNotNull_GetBySurveyTypeMethodWasCalled()
        {
            var surveyDto = _fixture.Build<SurveyDto>()
                                    .With(x => x.IsComplete, false)
                                    .With(x => x.IsExpired, false)
                                    .With(x => x.SurveyType, Core.Common.SurveyType.CareTeam)
                                    .Create();
            string code = _fixture.Create<string>();
            var questionsDto = _fixture.Build<QuestionDto>()
                                       .With(x => x.SurveyType, surveyDto.SurveyType)
                                       .CreateMany(21).ToList();
            SetQuestionKeyValue(questionsDto);
            _surveyAppService.Setup(m => m.GetBySurveyCode(code)).Returns(surveyDto);
            _questionAppService.Setup(m => m.GetBySurveyType(surveyDto.SurveyType)).Returns(questionsDto).Verifiable();

            var actual = _sut.Index(code);

            actual.Should().NotBeNull();
            _surveyAppService.VerifyAll();
        }

        [Test]
        public void Index_SurveyAnswerDtoNotNull_InsertMethodWasCall()
        {
            var careTeamSurvey = _fixture.Create<CareTeamSurvey>();
            var surveyDto = _fixture.Build<SurveyDto>()
                                    .With(x => x.IsComplete, false)
                                    .With(x => x.IsExpired, false)
                                    .With(x => x.SurveyType, Core.Common.SurveyType.CareTeam)
                                    .Create();
            var surveyAnswerDto = _fixture.Create<SurveyAnswerDto>();

            var questionsDto = _fixture.Build<QuestionDto>()
                                       .With(x => x.SurveyType, surveyDto.SurveyType)
                                       .CreateMany(21).ToList();
            SetQuestionKeyValue(questionsDto);
            _surveyAppService.Setup(m => m.GetBySurveyCode(careTeamSurvey.Survey.SurveyCode)).Returns(surveyDto);
            _questionAppService.Setup(q => q.GetBySurveyType(surveyDto.SurveyType)).Returns(questionsDto);
            _answerAppService.Setup(d => d.Insert(It.IsAny<List<AnswerDto>>()));
            _surveyAnswerAppService.Setup(s => s.Insert(It.IsAny<SurveyAnswerDto>())).Returns(surveyAnswerDto).Verifiable();
            

            _sut.Index(careTeamSurvey);

            _answerAppService.VerifyAll();
        }

        [Test]
        public void Index_SurveyCodeNotExists_ReturnMessageSurveyNotExits()
        {
            string expectedMessage = @"The survey not exists";
            SurveyDto expectedSurveyDto = null;
            string code = _fixture.Create<string>();
            _surveyAppService.Setup(m => m.GetBySurveyCode(code)).Returns(expectedSurveyDto);

            var actual = _sut.Index(code) as ViewResult;

            actual.Should().NotBeNull();
            actual.Should().BeOfType<ViewResult>();
            expectedMessage.Should().Equals(actual.ViewBag.Message);
        }

        [Test]
        public void Index_SurveyTypeDifferentToCareTeam_ReturnRedirectToRouteResult()
        {
            SurveyDto expectedSurveyDto = _fixture.Build<SurveyDto>()
                                    .With(x => x.IsComplete, false)
                                    .With(x => x.IsExpired, false)
                                    .With(x => x.SurveyType, Core.Common.SurveyType.Specialist)
                                    .Create();
            string code = _fixture.Create<string>();
            _surveyAppService.Setup(m => m.GetBySurveyCode(code)).Returns(expectedSurveyDto);

            var actual = _sut.Index(code);

            actual.Should().NotBeNull();
            actual.Should().BeOfType<RedirectToRouteResult>();
        }

        [Test]
        public void Index_SurveyCodeIsNullOrEmpty_ReturnNull()
        {
            SurveyDto expectedSurveyDto = null;
            string code = string.Empty;
            _surveyAppService.Setup(m => m.GetBySurveyCode(code)).Returns(expectedSurveyDto);

            var actual = _sut.Index(code);

            actual.Should().BeNull();
        }

        [Test]
        public void Index_CareTeamSurveyAnswered_InsertMethodWasCall()
        {
            var careTeamSurvey = _fixture.Create<CareTeamSurvey>();
            var surveyDto = _fixture.Build<SurveyDto>()
                                    .With(x => x.IsComplete, false)
                                    .With(x => x.IsExpired, false)
                                    .Create();
            var surveyAnswerDto = _fixture.Create<SurveyAnswerDto>();

            var questionsDto = _fixture.Build<QuestionDto>()
                                       .With(x => x.SurveyType, surveyDto.SurveyType)
                                       .CreateMany(21).ToList();
            SetQuestionKeyValue(questionsDto);
            _surveyAppService.Setup(m => m.GetBySurveyCode(careTeamSurvey.Survey.SurveyCode)).Returns(surveyDto);
            _questionAppService.Setup(q => q.GetBySurveyType(surveyDto.SurveyType)).Returns(questionsDto);
            _surveyAnswerAppService.Setup(s => s.Insert(It.IsAny<SurveyAnswerDto>())).Returns(surveyAnswerDto);
            _answerAppService.Setup(d => d.Insert(It.IsAny<List<AnswerDto>>())).Verifiable();

            _sut.Index(careTeamSurvey);

            _answerAppService.VerifyAll();
        }

        [Test]
        public void Index_CareTeamSurveyAnswered_ReturnMessageSurveyWasCompleted()
        {
            string expectedMessage = @"The survey was completed successfully.";
            var careTeamSurvey = _fixture.Create<CareTeamSurvey>();

            var actual = _sut.Index(careTeamSurvey) as ViewResult;

            actual.Should().NotBeNull();
            actual.Should().BeOfType<ViewResult>();
            expectedMessage.Should().Equals(actual.ViewBag.Message);
        }

        [Test]
        public void Index_CareTeamSurveyModelInvalid_ReturnTheViewWithTheError()
        {
            int expectedError = 1;
            string expectedErrorMessage = @"Is empty";
            _sut.ViewData.ModelState.AddModelError("Question1", "Is empty");
            var careTeamSurvey = _fixture.Create<CareTeamSurvey>();
            careTeamSurvey.Question1.QuestionText = String.Empty;
            var results = new List<ValidationResult>();
            var actual = _sut.Index(careTeamSurvey);

            actual.Should().NotBeNull();
            actual.Should().BeOfType<ViewResult>();
            expectedError.Should().Be(_sut.ViewData.ModelState.Count);
            expectedErrorMessage.Should().Be(_sut.ViewData.ModelState.Values.ElementAt(0).Errors[0].ErrorMessage);
        }

        [Test]
        public void Index_IsEscalationReportTrue_GetHospitalDetailsByNameMethodWasCall()
        {
            var careTeamSurvey = _fixture.Create<CareTeamSurvey>();
            careTeamSurvey.Question3E.Response = "Disagree";
            var surveyDto = _fixture.Build<SurveyDto>()
                                    .With(x => x.IsComplete, false)
                                    .With(x => x.IsExpired, false)
                                    .With(x => x.SurveyType, SurveyType.CareTeam)
                                    .Create();
            var surveyAnswerDto = _fixture.Create<SurveyAnswerDto>();
            var hosiptalDetails = _fixture.Create<HospitalDetailsDto>();
            var questionsDto = _fixture.Build<QuestionDto>()
                                       .With(x => x.SurveyType, surveyDto.SurveyType)
                                       .CreateMany(21).ToList();
            SetQuestionKeyValue(questionsDto);
            int surveyId = surveyDto.Id;
            _surveyAppService.Setup(m => m.GetBySurveyCode(careTeamSurvey.Survey.SurveyCode)).Returns(surveyDto);
            _questionAppService.Setup(q => q.GetBySurveyType(surveyDto.SurveyType)).Returns(questionsDto);
            _surveyAnswerAppService.Setup(s => s.Insert(It.IsAny<SurveyAnswerDto>())).Returns(surveyAnswerDto);
            _surveyAppService.Setup(e => e.GetHospitalDetailsByName(careTeamSurvey.Survey.FacilityName)).Returns(hosiptalDetails).Verifiable();

            _sut.Index(careTeamSurvey);

            _surveyAppService.VerifyAll();
        }

        [Test]
        public void Index_IsEscalationReportTrue_SendEmailMethodWasCall()
        {
            var careTeamSurvey = _fixture.Create<CareTeamSurvey>();
            careTeamSurvey.Question3E.Response = "Strongly Disagree";
            var surveyDto = _fixture.Build<SurveyDto>()
                                    .With(x => x.IsComplete, false)
                                    .With(x => x.IsExpired, false)
                                    .With(x => x.SurveyType, SurveyType.CareTeam)
                                    .Create();
            var surveyAnswerDto = _fixture.Create<SurveyAnswerDto>();
            var hosiptalDetails = _fixture.Create<HospitalDetailsDto>();
            var byteArrayFile = _fixture.Create<byte[]>();
            var questionsDto = _fixture.Build<QuestionDto>()
                                       .With(x => x.SurveyType, surveyDto.SurveyType)
                                       .CreateMany(21).ToList();
            SetQuestionKeyValue(questionsDto);
            int surveyId = surveyDto.Id;
            _surveyAppService.Setup(m => m.GetBySurveyCode(careTeamSurvey.Survey.SurveyCode)).Returns(surveyDto);
            _questionAppService.Setup(q => q.GetBySurveyType(surveyDto.SurveyType)).Returns(questionsDto);
            _surveyAnswerAppService.Setup(s => s.Insert(It.IsAny<SurveyAnswerDto>())).Returns(surveyAnswerDto);
            _surveyAppService.Setup(e => e.GetHospitalDetailsByName(careTeamSurvey.Survey.FacilityName)).Returns(hosiptalDetails);
            _emailSenderAppService.Setup(s => s.SendEmail(It.IsAny<byte[]>(), It.IsAny<string>())).Verifiable();

            _sut.Index(careTeamSurvey);

            _emailSenderAppService.VerifyAll();
        }

        [Test]
        public void GetDataToExportPCPSurvey_PCPSurveyCompleted_ReturnByteArray()
        {
            var careTeamSurvey = _fixture.Create<CareTeamSurvey>();
            var hosiptalDetails = _fixture.Create<HospitalDetailsDto>();

            var actual = _sut.GetDataToExportCareTeamSurvey(careTeamSurvey, hosiptalDetails);

            actual.Should().NotBeNull();
            actual.Should().BeOfType<byte[]>();
        }
    }
}

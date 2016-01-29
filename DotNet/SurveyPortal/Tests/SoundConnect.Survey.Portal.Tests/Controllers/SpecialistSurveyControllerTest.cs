using FluentAssertions;
using Moq;

using NUnit.Framework;

using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture;

using SoundConnect.Survey.Portal.Controllers;
using SoundConnect.Survey.Application.Interfaces;
using SoundConnect.Survey.Application.DTOs;
using SoundConnect.Survey.Portal.Models;

using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using SoundConnect.Survey.Core.Common;
using System.ComponentModel.DataAnnotations;
using SoundConnect.Survey.Portal.Modules;

namespace SoundConnect.Survey.Portal.Tests.Controllers
{
    [TestFixture]
    public class SpecialistSurveyControllerTest
    {
        private Mock<ISurveyAppService> _surveyAppService;

        private Mock<IQuestionAppService> _questionAppService;

        private Mock<IAnswerAppService> _answerAppService;

        private Mock<ISurveyAnswerAppService> _surveyAnswerAppService;

        private Mock<IEmailSenderAppService> _emailSenderAppService;

        private SpecialistSurveyController _sut;

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
            _sut = new SpecialistSurveyController(_surveyAppService.Object, _questionAppService.Object, _answerAppService.Object, _surveyAnswerAppService.Object, _emailSenderAppService.Object);
        }

        private List<QuestionDto> SetQuestionKeyValue(List<QuestionDto> questionsDto)
        {
            questionsDto[0].Key = "SP_Q_1";
            questionsDto[1].Key = "SP_Q_2A";
            questionsDto[2].Key = "SP_Q_2B";
            questionsDto[3].Key = "SP_Q_2C";
            questionsDto[4].Key = "SP_Q_2D";
            questionsDto[5].Key = "SP_Q_2E";
            questionsDto[6].Key = "SP_Q_2F";            
            questionsDto[7].Key = "SP_Q_2WHY";
            questionsDto[8].Key = "SP_Q_3";
            questionsDto[9].Key = "SP_Q_4";
            questionsDto[10].Key = "SP_Q_5";
            return questionsDto;
        }


        [Test]
        public void Index_SpecialistSurveyExist_ReturnViewResult()
        {
            var surveyDto = _fixture.Build<SurveyDto>()
                                    .With(x => x.IsComplete, false)
                                    .With(x => x.IsExpired, false)
                                    .With(x => x.SurveyType,Core.Common.SurveyType.Specialist)
                                    .Create();
            string code = _fixture.Create<string>();            
            _surveyAppService.Setup(m => m.GetBySurveyCode(code)).Returns(surveyDto);            

            var actual = _sut.Index(code);

            actual.Should().NotBeNull();
            actual.Should().BeOfType<ViewResult>();
        }

        [Test]
        public void Index_CodeNotNullOrEmptyAndExists_ReturnSpecialistSurvey()
        {
            var expected = _fixture.Build<SurveyDto>()
                                    .With(x => x.IsComplete, false)
                                    .With(x => x.IsExpired, false)
                                    .With(x => x.SurveyType, Core.Common.SurveyType.Specialist)
                                    .Create();
            string code = _fixture.Create<string>();            
            _surveyAppService.Setup(m => m.GetBySurveyCode(code)).Returns(expected);

            var actual = ((_sut.Index(code) as ViewResult).Model);

            actual.Should().NotBeNull();
            actual.Should().BeOfType<SpecialistSurvey>();
        }

        [Test]
        public void Index_CodeNotNullOrEmpty_GetBySurveyCodeMethodWasCalled()
        {
            var expected = _fixture.Build<SurveyDto>()
                                    .With(x => x.IsComplete, false)
                                    .With(x => x.IsExpired, false)
                                    .With(x => x.SurveyType, Core.Common.SurveyType.Specialist)
                                    .Create();
            string code = _fixture.Create<string>();            
            _surveyAppService.Setup(m => m.GetBySurveyCode(code)).Returns(expected).Verifiable();

            var actual = _sut.Index(code);

            actual.Should().NotBeNull();
            _surveyAppService.VerifyAll();
        }

        [Test]
        public void Index_SurveyCodeNotExists_ReturnMessageSurveyNotExists()
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
        public void Index_SurveyTypeDifferentToSpecialist_ReturnRedirectToRouteResult()
        {
            SurveyDto expectedSurveyDto = _fixture.Build<SurveyDto>()
                                    .With(x => x.IsComplete, false)
                                    .With(x => x.IsExpired, false)
                                    .With(x => x.SurveyType, Core.Common.SurveyType.PCP)
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
        public void Index_SurveyDtoIsComplete_ReturnMessageAlreadyTaken()
        {
            var expected = _fixture.Build<SurveyDto>()
                                    .With(x => x.IsComplete, true)
                                    .With(x => x.IsExpired, false)
                                    .With(x => x.SurveyType, Core.Common.SurveyType.Specialist)
                                    .Create();
            string expectedMessage = string.Format("Hello Dr {0}.Thank you for your interest in providing feedback for the Hospitalist Team at {1}.It looks like you have already taken this survey, but please look out for another opportunity to provide feedback next quarter.", expected.ProviderLastName, expected.FacilityName);
            string code = _fixture.Create<string>();
            _surveyAppService.Setup(m => m.GetBySurveyCode(code)).Returns(expected).Verifiable();

            var actual = _sut.Index(code) as ViewResult;

            actual.Should().NotBeNull();
            actual.Should().BeOfType<ViewResult>();
            expectedMessage.Should().Equals(actual.ViewBag.Message);
        }

        [Test]
        public void Index_SurveyDtoIsExpired_ReturnMessageAfterSurveyClosed()
        {
            var expected = _fixture.Build<SurveyDto>()
                                    .With(x => x.IsComplete, false)
                                    .With(x => x.IsExpired, true)
                                    .With(x => x.SurveyType, Core.Common.SurveyType.Specialist)
                                    .Create();
            string expectedMessage = string.Format("Hello Dr {0}. Thank you for your interest in providing feedback for the Hospitalist Team at {1}. This quarter’s survey has closed, but please look out for another opportunity to provide feedback next quarter.", expected.ProviderLastName, expected.FacilityName);
            string code = _fixture.Create<string>();
            _surveyAppService.Setup(m => m.GetBySurveyCode(code)).Returns(expected).Verifiable();

            var actual = _sut.Index(code) as ViewResult;

            actual.Should().NotBeNull();
            actual.Should().BeOfType<ViewResult>();
            expectedMessage.Should().Equals(actual.ViewBag.Message);
        }

        [Test]
        public void Index_SpecialistSurveyAnswered_InsertMethodWasCall()
        {
            var specialistSurvey = _fixture.Create<SpecialistSurvey>();
            var surveyDto = _fixture.Build<SurveyDto>()
                                    .With(x => x.IsComplete, false)
                                    .With(x => x.IsExpired, false)
                                    .Create();
            var surveyAnswerDto = _fixture.Create<SurveyAnswerDto>();
             
            var questionsDto = _fixture.Build<QuestionDto>()
                                       .With(x => x.SurveyType, surveyDto.SurveyType)                                       
                                       .CreateMany(11).ToList();
            SetQuestionKeyValue(questionsDto);
            _surveyAppService.Setup(m => m.GetBySurveyCode(specialistSurvey.Survey.SurveyCode)).Returns(surveyDto);
            _questionAppService.Setup(q => q.GetBySurveyType(surveyDto.SurveyType)).Returns(questionsDto);
            _surveyAnswerAppService.Setup(s => s.Insert(It.IsAny<SurveyAnswerDto>())).Returns(surveyAnswerDto);
            _answerAppService.Setup(d => d.Insert(It.IsAny<List<AnswerDto>>())).Verifiable();

            _sut.Index(specialistSurvey);

            _answerAppService.VerifyAll();
        }

        [Test]
        public void Index_SpecialistSurveyAnswered_UpdateCompleteMethodWasCall()
        {
            var specialistSurvey = _fixture.Create<SpecialistSurvey>();
            var surveyDto = _fixture.Build<SurveyDto>()
                                    .With(x => x.IsComplete, false)
                                    .With(x => x.IsExpired, false)
                                    .Create();
            var surveyAnswerDto = _fixture.Create<SurveyAnswerDto>();
            var questionsDto = _fixture.Build<QuestionDto>()
                                       .With(x => x.SurveyType, surveyDto.SurveyType)
                                       .CreateMany(11).ToList();
            SetQuestionKeyValue(questionsDto);
            int surveyId = surveyDto.Id;
            _surveyAppService.Setup(m => m.GetBySurveyCode(specialistSurvey.Survey.SurveyCode)).Returns(surveyDto);
            _questionAppService.Setup(q => q.GetBySurveyType(surveyDto.SurveyType)).Returns(questionsDto);
            _surveyAnswerAppService.Setup(s => s.Insert(It.IsAny<SurveyAnswerDto>())).Returns(surveyAnswerDto);
            _surveyAppService.Setup(d => d.UpdateComplete(surveyId)).Verifiable();

            _sut.Index(specialistSurvey);

            _answerAppService.VerifyAll();
        }

        [Test]
        public void Index_SpecialistSurveyAnswered_ReturnMessageSurveyWasCompleted()
        {
            string expectedMessage = @"The survey was completed successfully.";
            var specialistSurvey = _fixture.Create<SpecialistSurvey>();

            var actual = _sut.Index(specialistSurvey) as ViewResult;

            actual.Should().NotBeNull();
            actual.Should().BeOfType<ViewResult>();
            expectedMessage.Should().Equals(actual.ViewBag.Message);
        }

        [Test]
        public void Index_SpecialistSurveyModelInvalid_ReturnTheViewWithTheError()
        {
            int expectedError = 1;
            string expectedErrorMessage = @"Is empty";
            _sut.ViewData.ModelState.AddModelError("Question1", "Is empty");
            var specialistSurvey = _fixture.Create<SpecialistSurvey>();
            specialistSurvey.Question1.QuestionText = String.Empty;
            var results = new List<ValidationResult>();
            var actual = _sut.Index(specialistSurvey);

            actual.Should().NotBeNull();
            actual.Should().BeOfType<ViewResult>();
            expectedError.Should().Be(_sut.ViewData.ModelState.Count);
            expectedErrorMessage.Should().Be(_sut.ViewData.ModelState.Values.ElementAt(0).Errors[0].ErrorMessage);
        }

        [Test]
        public void Index_IsEscalationReportTrue_GetHospitalDetailsByNameMethodWasCall()
        {
            var specialistSurvey = _fixture.Create<SpecialistSurvey>();
            specialistSurvey.Question2E.Response = "Disagree";
            var surveyDto = _fixture.Build<SurveyDto>()
                                    .With(x => x.IsComplete, false)
                                    .With(x => x.IsExpired, false)
                                    .Create();
            var surveyAnswerDto = _fixture.Create<SurveyAnswerDto>();
            var hosiptalDetails = _fixture.Create<HospitalDetailsDto>();
            var questionsDto = _fixture.Build<QuestionDto>()
                                       .With(x => x.SurveyType, surveyDto.SurveyType)
                                       .CreateMany(13).ToList();
            SetQuestionKeyValue(questionsDto);
            int surveyId = surveyDto.Id;
            _surveyAppService.Setup(m => m.GetBySurveyCode(specialistSurvey.Survey.SurveyCode)).Returns(surveyDto);
            _questionAppService.Setup(q => q.GetBySurveyType(surveyDto.SurveyType)).Returns(questionsDto);
            _surveyAnswerAppService.Setup(s => s.Insert(It.IsAny<SurveyAnswerDto>())).Returns(surveyAnswerDto);
            _surveyAppService.Setup(d => d.UpdateComplete(It.IsAny<int>()));
            _surveyAppService.Setup(e => e.GetHospitalDetailsByName(specialistSurvey.Survey.FacilityName)).Returns(hosiptalDetails).Verifiable();

            _sut.Index(specialistSurvey);

            _surveyAppService.VerifyAll();
        }

        [Test]
        public void Index_IsEscalationReportTrue_SendEmailTemporalMethodWasCall()
        {
            var specialistSurvey = _fixture.Create<SpecialistSurvey>();
            specialistSurvey.Question2E.Response = "Strongly Disagree";
            var surveyDto = _fixture.Build<SurveyDto>()
                                    .With(x => x.IsComplete, false)
                                    .With(x => x.IsExpired, false)
                                    .Create();
            var surveyAnswerDto = _fixture.Create<SurveyAnswerDto>();
            var hosiptalDetails = _fixture.Create<HospitalDetailsDto>();
            var byteArrayFile = _fixture.Create<byte[]>();
            var questionsDto = _fixture.Build<QuestionDto>()
                                       .With(x => x.SurveyType, surveyDto.SurveyType)
                                       .CreateMany(13).ToList();
            SetQuestionKeyValue(questionsDto);
            int surveyId = surveyDto.Id;
            _surveyAppService.Setup(m => m.GetBySurveyCode(specialistSurvey.Survey.SurveyCode)).Returns(surveyDto);
            _questionAppService.Setup(q => q.GetBySurveyType(surveyDto.SurveyType)).Returns(questionsDto);
            _surveyAnswerAppService.Setup(s => s.Insert(It.IsAny<SurveyAnswerDto>())).Returns(surveyAnswerDto);
            _surveyAppService.Setup(d => d.UpdateComplete(It.IsAny<int>()));
            _surveyAppService.Setup(e => e.GetHospitalDetailsByName(specialistSurvey.Survey.FacilityName)).Returns(hosiptalDetails);
            _emailSenderAppService.Setup(s => s.SendEmail(It.IsAny<byte[]>(), It.IsAny<string>())).Verifiable();

            _sut.Index(specialistSurvey);

            _emailSenderAppService.VerifyAll();
        }

        [Test]
        public void GetDataToExportSpecialistSurvey_SpecialistSurveyCompleted_ReturnByteArray()
        {
            var specialistSurvey = _fixture.Create<SpecialistSurvey>();
            var hosiptalDetails = _fixture.Create<HospitalDetailsDto>();

            var actual = _sut.GetDataToExportSpecialistSurvey(specialistSurvey, hosiptalDetails);

            actual.Should().NotBeNull();
            actual.Should().BeOfType<byte[]>();
        }
    }
}

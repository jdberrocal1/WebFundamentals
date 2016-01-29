using ClosedXML.Excel;
using SoundConnect.Survey.Application.Common.EmailDetails;
using SoundConnect.Survey.Application.DTOs;
using SoundConnect.Survey.Application.Interfaces;
using SoundConnect.Survey.Core.Common;
using SoundConnect.Survey.Portal.Models;
using SoundConnect.Survey.Portal.Modules;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace SoundConnect.Survey.Portal.Controllers
{
    public class SpecialistSurveyController : Controller
    {
        //initialize service object
        ISurveyAppService _serviceSurvey;
        IQuestionAppService _serviceQuestion;
        IAnswerAppService _serviceAnswer;
        ISurveyAnswerAppService _serviceSurveyAnswer;
        IEmailSenderAppService _serviceEmailSender;

        public SpecialistSurveyController(ISurveyAppService serviceSurvey, IQuestionAppService serviceQuestion, IAnswerAppService serviceAnswer, ISurveyAnswerAppService serviceSurveyAnswer, IEmailSenderAppService emailSender)
        {
            _serviceSurvey = serviceSurvey;
            _serviceQuestion = serviceQuestion;
            _serviceAnswer = serviceAnswer;
            _serviceSurveyAnswer = serviceSurveyAnswer;
            _serviceEmailSender = emailSender;
            @ViewBag.HeadingTitle = "Partner Satisfaction Survey";
        }

        // GET: Specialist
        public ActionResult Index(string surveyCode)
        {
            if (!string.IsNullOrEmpty(surveyCode))
            {
                var surveyDto = _serviceSurvey.GetBySurveyCode(surveyCode);

                if (surveyDto == null)
                {
                    ViewBag.Message = Messages.GetMessage(Messages.MessageType.NotExist);
                    return View("../Message/Message");
                }

                if (surveyDto != null && surveyDto.SurveyType != Core.Common.SurveyType.Specialist)
                {
                    return RedirectToAction("Index", "Survey", new { surveyCode = surveyCode });
                }

                if (surveyDto != null && surveyDto.IsComplete)
                {
                    ViewBag.Message = Messages.GetMessage(Messages.MessageType.Completed, surveyDto.ProviderLastName, surveyDto.FacilityName);
                    return View("../Message/Message");
                }

                if (surveyDto != null && surveyDto.IsExpired)
                {
                    ViewBag.Message = Messages.GetMessage(Messages.MessageType.Expired, surveyDto.ProviderLastName, surveyDto.FacilityName);
                    return View("../Message/Message");
                }

                if (surveyDto != null)
                {
                    var questionsDto = _serviceQuestion.GetBySurveyType(surveyDto.SurveyType);
                    SpecialistSurvey specialistSurvey = new SpecialistSurvey(surveyDto);

                    var props = specialistSurvey.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();
                    props.ForEach(prop =>
                    {
                        if (prop.PropertyType == typeof(Question))
                        {
                            var attr = prop.GetCustomAttribute<QuestionKeyAttribute>();
                            var question = new Question();
                            if (questionsDto != null && questionsDto.Count > 0)
                            {
                                var questionDto = questionsDto.First(d => d.Key.Equals(attr.QuestionKey));
                                if (questionDto != null)
                                {
                                    question.QuestionText = questionDto.QuestionText;
                                }
                            }
                            prop.SetValue(specialistSurvey, question);
                        }
                    });

                    return View(specialistSurvey);
                }                
            }
            return null;//View("../Message/Message");
        }

        //[ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Index(SpecialistSurvey specialistSurvey)
        {
            if (ModelState.IsValid)
            {
                var surveyDto = _serviceSurvey.GetBySurveyCode(specialistSurvey.Survey.SurveyCode);
                bool isEscalationReport = false;
                if (surveyDto != null  && !surveyDto.IsComplete && !surveyDto.IsExpired)
                {
                    List<AnswerDto> answerList = new List<AnswerDto>();
                    var questionsDto = _serviceQuestion.GetBySurveyType(surveyDto.SurveyType);
                    var props = specialistSurvey.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();
                    var surveyAnswerDto = new SurveyAnswerDto()
                    {
                        SurveyId = specialistSurvey.Survey.Id,
                        RespondDate = DateTime.Today
                    };
                    surveyAnswerDto = _serviceSurveyAnswer.Insert(surveyAnswerDto);
                    props.ForEach(prop =>
                    {
                        if (prop.PropertyType == typeof(Question))
                        {
                            var attr = prop.GetCustomAttribute<QuestionKeyAttribute>();
                            var question = questionsDto.First(d => d.Key.Equals(attr.QuestionKey));
                            var answer = new AnswerDto()
                            {
                                SurveyAnswerId = surveyAnswerDto.Id,
                                QuestionId = question.Id,
                                ResponseText = ((Question)prop.GetValue(specialistSurvey, null)).Response,
                                Note = ((Question)prop.GetValue(specialistSurvey, null)).Note

                            };
                            answerList.Add(answer);

                            //To know if Disagre or Strongly Disagree was selected as an answer, for send an email
                            if ((answer.ResponseText != null) && QuestionKey.SpecialistSurvey.ContainsKey(question.Key))
                            {
                                if (answer.ResponseText.ToLower().Equals("disagree") || answer.ResponseText.ToLower().Equals("strongly disagree"))
                                    isEscalationReport = true;
                            }
                        }
                    });

                    //Add answer
                    _serviceAnswer.Insert(answerList);
                    _serviceSurvey.UpdateComplete(specialistSurvey.Survey.Id);
                    ViewBag.Message = Messages.GetMessage(Messages.MessageType.Succefully);

                    //Send Email To Performance Management
                    if (isEscalationReport)
                    {
                        HospitalDetailsDto hospitalDetails = _serviceSurvey.GetHospitalDetailsByName(specialistSurvey.Survey.FacilityName);
                        byte[] fileToSend = GetDataToExportSpecialistSurvey(specialistSurvey, hospitalDetails);
                        _serviceEmailSender.SendEmail(fileToSend, ConfigurationManager.AppSettings["PerformanceManagementEmail"].ToString());
                    }
                    
                    return View("../Message/Message");

                }

                if (surveyDto != null && surveyDto.IsComplete)
                {
                    ViewBag.Message = Messages.GetMessage(Messages.MessageType.Completed, surveyDto.ProviderLastName, surveyDto.FacilityName);
                    return View("../Message/Message");
                }
            }
            return View(specialistSurvey);
        }

        public byte[] GetDataToExportSpecialistSurvey(SpecialistSurvey specialistSurvey, HospitalDetailsDto hospitalDetails)
        {
            string fileName = @"C:\Escalation_Specialist_Report_" + Guid.NewGuid() + ".xlsx";
            var configureCells = new ConfigureEscalationReport();
            var workbook = new XLWorkbook();

            var worksheet = workbook.Worksheets.Add("Survey Escalation Report");

            configureCells.ConfigureCell(worksheet, specialistSurvey.Survey.SurveyType);

            #region Headers
            worksheet.Cell("A1").Value = "Survey Type";
            worksheet.Cell("B1").Value = "Region";
            worksheet.Cell("C1").Value = "Site";
            worksheet.Cell("D1").Value = "Site #";
            worksheet.Cell("E1").Value = "Provider Last Name";
            worksheet.Cell("F1").Value = "Provider First Name";
            worksheet.Cell("G1").Value = "Date of Survey";
            worksheet.Cell("H1").Value = specialistSurvey.Question2A.QuestionText;
            worksheet.Cell("I1").Value = specialistSurvey.Question2B.QuestionText;
            worksheet.Cell("J1").Value = specialistSurvey.Question2C.QuestionText;
            worksheet.Cell("K1").Value = specialistSurvey.Question2D.QuestionText;
            worksheet.Cell("L1").Value = specialistSurvey.Question2E.QuestionText;
            worksheet.Cell("M1").Value = specialistSurvey.Question2F.QuestionText;
            worksheet.Cell("N1").Value = specialistSurvey.Question2Why.QuestionText;
            #endregion

            #region Rows
            // write some values into column 2
            worksheet.Cell("A2").Value = "Specialist";
            worksheet.Cell("B2").Value = hospitalDetails.RegionName;
            worksheet.Cell("C2").Value = specialistSurvey.Survey.FacilityName;
            worksheet.Cell("D2").Value = hospitalDetails.SiteNumber;
            worksheet.Cell("E2").Value = specialistSurvey.Survey.ProviderLastName;
            worksheet.Cell("F2").Value = specialistSurvey.Survey.ProviderFirstName;
            worksheet.Cell("G2").Value = DateTime.Now.ToString("MM/dd/yyyy");
            worksheet.Cell("H2").Value = specialistSurvey.Question2A.Response;
            worksheet.Cell("I2").Value = specialistSurvey.Question2B.Response;
            worksheet.Cell("J2").Value = specialistSurvey.Question2C.Response;
            worksheet.Cell("K2").Value = specialistSurvey.Question2D.Response;
            worksheet.Cell("L2").Value = specialistSurvey.Question2E.Response;
            worksheet.Cell("M2").Value = specialistSurvey.Question2F.Response;
            worksheet.Cell("N2").Value = specialistSurvey.Question2Why.Response;
            #endregion

            workbook.SaveAs(fileName);

            var bytes = System.IO.File.ReadAllBytes(fileName);
            System.IO.FileInfo newFile = new System.IO.FileInfo(fileName);
            if (newFile.Exists)
                newFile.Delete();

            return bytes;
        }
    }
}
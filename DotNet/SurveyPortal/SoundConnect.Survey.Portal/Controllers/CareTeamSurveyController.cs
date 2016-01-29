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
    public class CareTeamSurveyController : Controller
    {
        // initialize service object
        ISurveyAppService _serviceSurvey;
        IQuestionAppService _serviceQuestion;
        IAnswerAppService _serviceAnswer;
        ISurveyAnswerAppService _serviceSurveyAnswer;
        IEmailSenderAppService _serviceEmailSender;

        public CareTeamSurveyController(ISurveyAppService serviceSurvey, IQuestionAppService serviceQuestion, IAnswerAppService serviceAnswer, ISurveyAnswerAppService serviceSurveyAnswer, IEmailSenderAppService emailSender)
        {
            _serviceSurvey = serviceSurvey;
            _serviceQuestion = serviceQuestion;
            _serviceAnswer = serviceAnswer;
            _serviceSurveyAnswer = serviceSurveyAnswer;
            _serviceEmailSender = emailSender;
            @ViewBag.HeadingTitle = "Care Team Survey";
        }

        // GET: CareTeamSurvey
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

                if (surveyDto != null && surveyDto.SurveyType != Core.Common.SurveyType.CareTeam)
                {
                    return RedirectToAction("Index", "Survey", new { surveyCode = surveyCode });
                }

                if (surveyDto != null)
                {
                        var questions = _serviceQuestion.GetBySurveyType(surveyDto.SurveyType);
                        CareTeamSurvey careTeamSurvey = new CareTeamSurvey(surveyDto);

                        var props = careTeamSurvey.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();
                        props.ForEach(prop =>
                        {
                            if (prop.PropertyType == typeof(Question))
                            {
                                var attr = prop.GetCustomAttribute<QuestionKeyAttribute>();
                                var question = new Question();
                                if (questions != null && questions.Count > 0)
                                {
                                    var questionDto = questions.First(d => d.Key.Equals(attr.QuestionKey));
                                    if (questionDto != null)
                                    {
                                        question.QuestionText = questionDto.QuestionText;
                                    }
                                }
                                prop.SetValue(careTeamSurvey, question);
                            }
                        });

                        return View(careTeamSurvey);
                }
            }
            return null;
        }

        [HttpPost]
        public ActionResult Index(CareTeamSurvey careTeamSurvey)
        {
            if (ModelState.IsValid)
            {
                var surveyDto = _serviceSurvey.GetBySurveyCode(careTeamSurvey.Survey.SurveyCode);
                bool isEscalationReport = false;
                if (surveyDto != null)
                {
                    List<AnswerDto> answerList = new List<AnswerDto>();
                    var questions = _serviceQuestion.GetBySurveyType(surveyDto.SurveyType);
                    var props = careTeamSurvey.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();
                    var surveyAnswerDto = new SurveyAnswerDto()
                    {
                        SurveyId = careTeamSurvey.Survey.Id,
                        RespondDate = DateTime.Today
                    };
                    surveyAnswerDto = _serviceSurveyAnswer.Insert(surveyAnswerDto);


                    props.ForEach(prop =>
                    {
                        if (prop.PropertyType == typeof(Question))
                        {
                            var attr = prop.GetCustomAttribute<QuestionKeyAttribute>();
                            var question = questions.First(d => d.Key.Equals(attr.QuestionKey));
                            var answer = new AnswerDto()
                            {
                                SurveyAnswerId = surveyAnswerDto.Id,
                                QuestionId = question.Id,
                                ResponseText = ((Question)prop.GetValue(careTeamSurvey, null)).Response,
                                Note = ((Question)prop.GetValue(careTeamSurvey, null)).Note
                            };
                            answerList.Add(answer);

                            //To know if Disagre or Strongly Disagree was selected as an answer, for send an email
                            if ((answer.ResponseText != null) && QuestionKey.CareTeamSurvey.ContainsKey(question.Key)) 
                            {
                                if (answer.ResponseText.ToLower().Equals("disagree") || answer.ResponseText.ToLower().Equals("strongly disagree"))
                                    isEscalationReport = true;
                            }
                        }
                    });

                    // Add answer
                    _serviceAnswer.Insert(answerList);
                    ViewBag.Message = Messages.GetMessage(Messages.MessageType.Succefully);

                    //Send Email To Performance Management
                    if (isEscalationReport)
                    {
                        HospitalDetailsDto hospitalDetails = _serviceSurvey.GetHospitalDetailsByName(careTeamSurvey.Survey.FacilityName);
                        byte[] fileToSend = GetDataToExportCareTeamSurvey(careTeamSurvey, hospitalDetails);
                        _serviceEmailSender.SendEmail(fileToSend, ConfigurationManager.AppSettings["PerformanceManagementEmail"].ToString());
                    }

                    return View("../Message/Message");

                }
            }
            return View(careTeamSurvey);
        }

        public byte[] GetDataToExportCareTeamSurvey(CareTeamSurvey careTeamSurvey, HospitalDetailsDto hospitalDetails)
        {
            string fileName = @"C:\Escalation_CareTeam_Report_" + Guid.NewGuid() + ".xlsx";
            var configureCells = new ConfigureEscalationReport();
            var workbook = new XLWorkbook();

            var worksheet = workbook.Worksheets.Add("Survey Escalation Report");

            configureCells.ConfigureCell(worksheet,careTeamSurvey.Survey.SurveyType);
            
            #region Headers
            worksheet.Cell("A1").Value = "Survey Type";
            worksheet.Cell("B1").Value = "Region";
            worksheet.Cell("C1").Value = "Site";
            worksheet.Cell("D1").Value = "Site #";
            worksheet.Cell("E1").Value = "Date of Survey";
            worksheet.Cell("F1").Value = careTeamSurvey.Question3A.QuestionText;
            worksheet.Cell("G1").Value = careTeamSurvey.Question3B.QuestionText;
            worksheet.Cell("H1").Value = careTeamSurvey.Question3C.QuestionText;
            worksheet.Cell("I1").Value = careTeamSurvey.Question3D.QuestionText;
            worksheet.Cell("J1").Value = careTeamSurvey.Question3E.QuestionText;
            worksheet.Cell("K1").Value = careTeamSurvey.Question3F.QuestionText;
            worksheet.Cell("L1").Value = careTeamSurvey.Question3G.QuestionText;
            worksheet.Cell("M1").Value = careTeamSurvey.Question3H.QuestionText;
            worksheet.Cell("N1").Value = careTeamSurvey.Question3I.QuestionText;
            worksheet.Cell("O1").Value = careTeamSurvey.Question3J.QuestionText;
            worksheet.Cell("P1").Value = careTeamSurvey.Question3K.QuestionText;
            worksheet.Cell("Q1").Value = careTeamSurvey.Question3L.QuestionText;
            worksheet.Cell("R1").Value = careTeamSurvey.Question3M.QuestionText;
            worksheet.Cell("S1").Value = careTeamSurvey.Question3N.QuestionText;
            worksheet.Cell("T1").Value = careTeamSurvey.Question3O.QuestionText;
            worksheet.Cell("U1").Value = careTeamSurvey.Question3Why.QuestionText;
            #endregion

            #region Rows
            // write some values into column 2
            worksheet.Cell("A2").Value = "Care Team";
            worksheet.Cell("B2").Value = hospitalDetails.RegionName;
            worksheet.Cell("C2").Value = careTeamSurvey.Survey.FacilityName;
            worksheet.Cell("D2").Value = hospitalDetails.SiteNumber;
            worksheet.Cell("E2").Value = DateTime.Now.ToString("MM/dd/yyyy");
            worksheet.Cell("F2").Value = careTeamSurvey.Question3A.Response;
            worksheet.Cell("G2").Value = careTeamSurvey.Question3B.Response;
            worksheet.Cell("H2").Value = careTeamSurvey.Question3C.Response;
            worksheet.Cell("I2").Value = careTeamSurvey.Question3D.Response;
            worksheet.Cell("J2").Value = careTeamSurvey.Question3E.Response;
            worksheet.Cell("K2").Value = careTeamSurvey.Question3F.Response;
            worksheet.Cell("L2").Value = careTeamSurvey.Question3G.Response;
            worksheet.Cell("M2").Value = careTeamSurvey.Question3H.Response;
            worksheet.Cell("N2").Value = careTeamSurvey.Question3I.Response;
            worksheet.Cell("O2").Value = careTeamSurvey.Question3J.Response;
            worksheet.Cell("P2").Value = careTeamSurvey.Question3K.Response;
            worksheet.Cell("Q2").Value = careTeamSurvey.Question3L.Response;
            worksheet.Cell("R2").Value = careTeamSurvey.Question3M.Response;
            worksheet.Cell("S2").Value = careTeamSurvey.Question3N.Response;
            worksheet.Cell("T2").Value = careTeamSurvey.Question3O.Response;
            worksheet.Cell("U2").Value = careTeamSurvey.Question3Why.Response;
            #endregion

            workbook.SaveAs(fileName);

            var bytes = System.IO.File.ReadAllBytes(fileName);
            System.IO.FileInfo newFile = new System.IO.FileInfo(fileName);
            if (newFile.Exists)
                newFile.Delete();

            return bytes;
        }

        public IXLWorksheet CreateColumnsForCareTeam(CareTeamSurvey careTeamSurvey, bool questionOrResponse)
        {


            return null;
        }
    }
}
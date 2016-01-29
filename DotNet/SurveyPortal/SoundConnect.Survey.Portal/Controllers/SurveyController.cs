using SoundConnect.Survey.Application.Interfaces;
using SoundConnect.Survey.Core.Common;
using System.Web.Mvc;

namespace SoundConnect.Survey.Portal.Controllers
{
    public class SurveyController : Controller
    {

        //initialize service object
        ISurveyAppService _serviceSurvey;

        public SurveyController(ISurveyAppService serviceSurvey)
        {
            _serviceSurvey = serviceSurvey;            
        }

        // GET: Survey       
        public ActionResult Index(string surveyCode)
        {
            if (!string.IsNullOrEmpty(surveyCode))
            {
                var surveyDto = _serviceSurvey.GetBySurveyCode(surveyCode);
                if (surveyDto != null)
                {
                    if (surveyDto.SurveyType == SurveyType.PCP)
                    {
                        return RedirectToAction("Index", "PCPSurvey", new { surveyCode = surveyCode });
                    }
                    else if (surveyDto.SurveyType == SurveyType.Specialist)
                    {
                        return RedirectToAction("Index", "SpecialistSurvey", new { surveyCode = surveyCode });
                    }
                    else if (surveyDto.SurveyType == SurveyType.CareTeam)
                    {
                        return RedirectToAction("Index", "CareTeamSurvey", new { surveyCode = surveyCode });
                    }
                }
                else
                {
                    ViewBag.Message = Messages.GetMessage(Messages.MessageType.NotExist);
                    return View("../Message/Message");
                }
            }

            return null;//View("../Message/Message");
        }
    }
}
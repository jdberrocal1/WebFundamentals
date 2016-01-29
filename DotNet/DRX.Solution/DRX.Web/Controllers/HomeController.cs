using System.Web.Mvc;
using DRX.Common.Models.Contacts.Interfaces;

namespace DRX.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IContactDomainService _contactDomainService;

        public HomeController(IContactDomainService contactDomainService)
        {
            _contactDomainService = contactDomainService;
        }

        // GET: Home
        public ActionResult Index()
        {
            var result =_contactDomainService.GetAll();
            return View(result);
        }
    }
}
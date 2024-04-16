using Abp.Web.Mvc.Alerts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpaceLoops.BAL.Services;
using SpaceLoops.DAL.Entity;

namespace SpaceLoops.Controllers
{
    public class AdminController : Controller
    {
        private readonly ContactServices _contactServices;
        public AdminController(ContactServices contactServices)
        {
            _contactServices = contactServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ContactList()
        {
            ViewBag.ContactList = _contactServices.GetAllContact();
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult UpdateContact(Guid contactId)
        {
            Contact contact = _contactServices.GetContactById(contactId);
            return View(contact);
        }

        [HttpPost]
        public IActionResult UpdateContact(Contact contact)
        {
            _contactServices.UpdateContact(contact);
            TempData["SuccessMessage"] = "Contacts Edited Successfully";
            return RedirectToAction("contactlist", "admin");
        }

        public IActionResult TestPage()
        {
            return View();
        }

        [HttpPost]

        public IActionResult DeleteContact(Guid contactId)
        {
            _contactServices.DeleteContact(contactId);
            TempData["DeleteMessage"] = "Contact Deleted Successfully";
            return RedirectToAction("contactlist", "admin");
        }

    }
}

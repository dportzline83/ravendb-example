using System;
using Microsoft.AspNet.Mvc;
using RavenExample.Data;
using Raven.Client;
using System.Linq;

namespace RavenExample.People
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
    }

    public class PeopleController : Controller
    {
        private readonly IDocumentSession _documentSession;
        private readonly DocumentStoreLifecycle _storeLifecycle;

        public PeopleController(DocumentStoreLifecycle storeLifecycle)
        {
            _storeLifecycle = storeLifecycle;
            _documentSession = _storeLifecycle.Store.OpenSession();
        }

        public JsonResult Index()
        {
            var people = _documentSession.Query<Person>().ToList();
            return Json(people);
       } 

        [HttpGet]
        public ActionResult CreatePerson()
        {
            var person = new Person();
            return View(person);
        }

        [HttpPost]
        public void CreatePerson(Person person)
        {
            if (!ModelState.IsValid)
            {
                Context.Response.StatusCode = 400;
            }
            _documentSession.Store(person);
        }

        protected override void Dispose(bool disposing)
        {
            _documentSession.SaveChanges();
            _documentSession.Dispose();
            base.Dispose(disposing);
        }
    }
}
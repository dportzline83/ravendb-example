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

        public ActionResult Index()
        {
            var people =
                _documentSession.Query<Person>()
                .ToList();
            return View(people);
        }

        // paging
        public ActionResult All()
        {
            var people =
                _documentSession.Query<Person>()
                .Take(10000)
                .ToList();
            return View("Index", people);
        }

        // stats and stale data
        public ActionResult Statistics()
        {
            RavenQueryStatistics statistics;
            var people =
                _documentSession.Query<Person>()
                .Statistics(out statistics)
                .ToList();
            ViewBag.IsStale = statistics.IsStale;
            return View("Index", people);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Person person)
        {
            if (ModelState.IsValid)
            {
                _documentSession.Store(person);
                return RedirectToAction("Index");
            }
            else
            {
                return View(person);
            }
        }

        [HttpPost]
        public ActionResult Create100()
        {
            DateTime date =  DateTime.Now;
            for (int i = 0; i < 100; i++)
            {
                date = date.AddSeconds(1);
                _documentSession.Store(new Person
                {
                    FirstName = "Generated",
                    LastName = "Person",
                    Birthdate = date
                });

            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _documentSession.SaveChanges();
            _documentSession.Dispose();
            base.Dispose(disposing);
        }
    }
}
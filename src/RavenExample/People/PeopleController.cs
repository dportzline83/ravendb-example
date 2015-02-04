using System;
using Microsoft.AspNet.Mvc;
using RavenExample.Data;
using Raven.Client;
using System.Linq;
using System.Collections.Generic;
using Raven.Client.Linq;

namespace RavenExample.People
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
    }

    [Route("[Controller]/[Action]")]
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

        #region Creating documents
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
            DateTime date = DateTime.Now;
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
            return RedirectToAction("Statistics");
        }
        #endregion

        #region Editing documents
        [HttpPost]
        public ActionResult Edit(Person person)
        {
            if (ModelState.IsValid)
            {
                var savedPerson = _documentSession.Load<Person>(person.Id);
                savedPerson.FirstName = person.FirstName;
                savedPerson.LastName = person.LastName;
                savedPerson.Birthdate = person.Birthdate;

                return View(person);
            }
            else
            {
                return View(person);
            }
        }
        #endregion

        #region Deleting documents
        [HttpPost]
        public ActionResult Delete(int id)
        {
            _documentSession.Delete<Person>(id);
            return RedirectToAction("Index");
        }
        #endregion

        #region Loading by id
        [HttpGet("{id:int}")]
        public ActionResult Edit(int id)
        {
            var person = _documentSession.Load<Person>(id);
            if (person == null) ModelState.AddModelError(null, "There is no person with that id");
            return View(person);
        }
        #endregion

        #region Loading multiple docs by id
        public ActionResult LoadByIds([FromQuery] IEnumerable<int> id)
        {
            IEnumerable<ValueType> ids = id.Select(x => (ValueType)x);
            var person = _documentSession.Load<Person>(ids);
            return View("Index", person);
        }
        #endregion

        #region Querying with linq
        public ActionResult ByBirthdate()
        {
            var people = _documentSession.Query<Person>()
                .Where(x => x.Birthdate > DateTime.Now)
                .OrderByDescending(x => x.Birthdate); // Sorting on some fields requires an index
            return View("Index", people);
        }
        #endregion

        #region Advanced Raven.Client.Linq queries
        public ActionResult ByFirstName()
        {
            var people = _documentSession.Query<Person>()
                .Where(x => x.FirstName.In("Darrel", "Sam"))
                .OrderByDescending(x => x.Birthdate);
            return View("Index", people);
        }

        /***
            You can also use: Where/Any, Where/ContainsAny, Where/ContainsAll

            Make sure your queries are being interpreted correctly.
        ***/
        #endregion

        #region Paging
        public ActionResult All()
        {
            var people =
                _documentSession.Query<Person>()
                .Take(10000) // overcome the default result size limit
                .ToList();
            return View("Index", people);
        }
        #endregion

        #region Stats and stale data
        public ActionResult Statistics()
        {
            RavenQueryStatistics statistics;
            var people =
                _documentSession.Query<Person>()
                .Statistics(out statistics)
                .ToList();
            ViewBag.IsStale = statistics.IsStale;
            return View("Index", people);

            #region Waiting for non-stale results
            // WaitForNonStaleResults(DateTime)
            // WaitForNonStaleResultsAsOf(DateTime, DateTime)
            // WaitForNonStaleResultsAsOfNow(DateTime)
            // WaitForNonStaleResultsAsOfLastWrite(DateTime)
            #endregion
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            _documentSession.SaveChanges();
            _documentSession.Dispose();
            base.Dispose(disposing);
        }
    }
}
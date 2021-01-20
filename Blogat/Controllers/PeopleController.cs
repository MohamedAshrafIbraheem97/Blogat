using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Blogat.Models;

namespace Blogat.Controllers
{
    public class PeopleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: People
        public ActionResult Index()
        {
            return View(db.Persons.ToList());
        }

        // GET: People/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            var person = db.Persons.Find(id);
            if (person == null)
            {
                return View("NotFound");
            }
            return View(person);
        }

        // GET: People/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,DateOfBirth")] Person person)
        {
            if (!ModelState.IsValid)
                return View(person);

            db.Persons.Add(person);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

            // GET: People/Edit/5
            public ActionResult Edit(int? id)
        {
            if (id == null)
                return View("NotFound");

            Person person = db.Persons.Find(id);
            if (person == null)
                return View("NotFound");

            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,DateOfBirth")] Person person)
        {
            if (!ModelState.IsValid)
                return View(person);

            // because if there's more than one user modifing at the same time
            db.Entry(person).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
            
        }

        // GET: People/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return View("NotFound");
            
            Person person = db.Persons.Find(id);
            if (person == null)
                return View("NotFound");

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = db.Persons.Find(id);
            db.Persons.Remove(person);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

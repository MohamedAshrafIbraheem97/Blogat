using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Blogat.Models;
using Blogat.ViewModels;

namespace Blogat.Controllers
{
    public class BlogsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Blogs
        public ActionResult Index()
        {
            var blogs = db.Blogs.Include(b => b.Category).Include(b => b.PersonBloged);
            return View(blogs.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Like(int? id)
        {
            if (id == null)
                return RedirectToAction("Details", "Blogs", new { id = id });

            var blogFound = db.Blogs.SingleOrDefault(b => b.Id == id);
            if (blogFound != null)
            {
                blogFound.BlogLikes++;
                db.SaveChanges();
                return RedirectToAction("Details", "Blogs", new { id = id });
            }

            else
                return RedirectToAction("Details", "Blogs", new { id = id });

        }

        // GET: Blogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            
            var blog = db.Blogs.Find(id);
            

            var viewModel = new BlogsViewModel
            {
                Blog = blog,
                Person = db.Persons.SingleOrDefault(p => p.Id == blog.PersonId),
                Category = db.Categories.SingleOrDefault(c => c.Id == blog.CategoryId),
                Comments = db.Comments.Where(c => c.BlogId == blog.Id).ToList(),
                Comment = null,
                Categories = null,
                People = null
            };

            if (blog == null)
            {
                return View("NotFound");
            }
            return View(viewModel);
        }


        // GET: Blogs/Create
        public ActionResult Create()
        {
            //ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName");
            //ViewBag.PersonId = new SelectList(db.Persons, "Id", "FirstName");
            var viewModel = new BlogsViewModel
            {
                Blog = new Blog(),
                Categories = db.Categories.ToList(),
                People = db.Persons.ToList(),
                
            };
            return View(viewModel);
        }

        // POST: Blogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Heading,Body,PersonId,CategoryId")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                TempData["Message"] = "You Have Successfully Cretaed Your Blog";
                TempData["Id"] = blog.Id;

                blog.TimeBloged = DateTime.Now;
                blog.BlogLikes = 0;
                db.Blogs.Add(blog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", blog.CategoryId);
            ViewBag.PersonId = new SelectList(db.Persons, "Id", "FirstName", blog.PersonId);
            return View(blog);
        }

        

        // GET: Blogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return View("NotFound");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", blog.CategoryId);
            ViewBag.PersonId = new SelectList(db.Persons, "Id", "FirstName", blog.PersonId);
            return View(blog);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Heading,Body,TimeBloged,BlogLikes,PersonId,CategoryId")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                TempData["Message"] = "You Have Successfully Edited Your Blog";
                TempData["Id"] = blog.Id;
                db.Entry(blog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", blog.CategoryId);
            ViewBag.PersonId = new SelectList(db.Persons, "Id", "FirstName", blog.PersonId);
            return View(blog);
        }

        // GET: Blogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return View("NotFound");
            }
            return View(blog);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Blog blog = db.Blogs.Find(id);
            db.Blogs.Remove(blog);
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

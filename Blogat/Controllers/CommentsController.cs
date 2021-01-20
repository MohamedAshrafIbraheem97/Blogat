using Blogat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blogat.Controllers
{
    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        
        // GET: Comments
        public ActionResult Index()
        {
            return View();
        }

        // GET: Comments/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Comments/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: Comments/Create
        [HttpPost]
        public ActionResult Create(Comment comment,int blogId ,FormCollection collection)
        {
            var blogFound = db.Blogs.SingleOrDefault(b => b.Id == blogId);
            
            if (blogFound != null)
            {
                comment.TimeCommented = DateTime.Now;
                comment.CommentLikes = 0;
                comment.PersonCommented = db.Persons.FirstOrDefault();
                comment.PersonId = db.Persons.FirstOrDefault().Id;
                comment.Blog = blogFound;
                comment.BlogId = blogFound.Id;

                    
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Details", "Blogs", new { id = blogId });
                
            }
           
            return RedirectToAction("Details", "Blogs", new { id = blogId });
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Like(int commentId)
        {
            var commentFound = db.Comments.SingleOrDefault(b => b.Id == commentId);
            if (commentFound != null)
            {
                commentFound.CommentLikes++;
                db.SaveChanges();
                return RedirectToAction("Details", "Blogs", new { id = commentFound.BlogId });
            }

            else
                return RedirectToAction("Index", "Blogs");

        }

        // GET: Comments/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Comments/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

       
        // POST: Comments/Delete/5
        [HttpPost]
        public ActionResult Delete(int commentId)
        {
            var commentFound = db.Comments.SingleOrDefault(b => b.Id == commentId);
            var blogId = commentFound.BlogId;
            if (commentFound != null)
            {
                db.Comments.Remove(commentFound);
                db.SaveChanges();
                return RedirectToAction("Details", "Blogs", new { id = blogId });
            }

            else
                return RedirectToAction("Index", "Blogs");

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

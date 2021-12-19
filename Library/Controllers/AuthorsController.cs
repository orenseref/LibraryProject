using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Library.Models.Entities;

namespace Library.Controllers
{
    public class AuthorsController : Controller
    {
        private LibraryDBEntities db = new LibraryDBEntities();

        // GET: Authors
        public async Task<ActionResult> Index()
        {
            return View(await db.TBLAuthor.ToListAsync());
        }

        // GET: Authors/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBLAuthor tBLAuthor = await db.TBLAuthor.FindAsync(id);
            if (tBLAuthor == null)
            {
                return HttpNotFound();
            }
            return View(tBLAuthor);
        }

        // GET: Authors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AuthorID,AuthorName")] TBLAuthor tBLAuthor)
        {
            if (ModelState.IsValid)
            {
                db.TBLAuthor.Add(tBLAuthor);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tBLAuthor);
        }

        // GET: Authors/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBLAuthor tBLAuthor = await db.TBLAuthor.FindAsync(id);
            if (tBLAuthor == null)
            {
                return HttpNotFound();
            }
            return View(tBLAuthor);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AuthorID,AuthorName")] TBLAuthor tBLAuthor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tBLAuthor).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tBLAuthor);
        }

        // GET: Authors/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBLAuthor tBLAuthor = await db.TBLAuthor.FindAsync(id);
            if (tBLAuthor == null)
            {
                return HttpNotFound();
            }
            return View(tBLAuthor);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TBLAuthor tBLAuthor = await db.TBLAuthor.FindAsync(id);
            db.TBLAuthor.Remove(tBLAuthor);
            await db.SaveChangesAsync();
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

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
    public class CategoriesController : Controller
    {
        private LibraryDBEntities db = new LibraryDBEntities();

        // GET: Categories
        public async Task<ActionResult> Index()
        {
            return View(await db.TBLCategory.ToListAsync());
        }

        // GET: Categories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBLCategory tBLCategory = await db.TBLCategory.FindAsync(id);
            if (tBLCategory == null)
            {
                return HttpNotFound();
            }
            return View(tBLCategory);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CategoryID,CategoryName")] TBLCategory tBLCategory)
        {
            if (ModelState.IsValid)
            {
                db.TBLCategory.Add(tBLCategory);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tBLCategory);
        }

        // GET: Categories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBLCategory tBLCategory = await db.TBLCategory.FindAsync(id);
            if (tBLCategory == null)
            {
                return HttpNotFound();
            }
            return View(tBLCategory);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CategoryID,CategoryName")] TBLCategory tBLCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tBLCategory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tBLCategory);
        }

        // GET: Categories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBLCategory tBLCategory = await db.TBLCategory.FindAsync(id);
            if (tBLCategory == null)
            {
                return HttpNotFound();
            }
            return View(tBLCategory);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TBLCategory tBLCategory = await db.TBLCategory.FindAsync(id);
            db.TBLCategory.Remove(tBLCategory);
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

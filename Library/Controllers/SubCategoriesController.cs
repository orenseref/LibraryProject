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
    public class SubCategoriesController : Controller
    {
        private LibraryDBEntities db = new LibraryDBEntities();

        // GET: SubCategories
        public async Task<ActionResult> Index()
        {
            var tBLSubCategory = db.TBLSubCategory.Include(t => t.TBLCategory);
            return View(await tBLSubCategory.ToListAsync());
        }

        // GET: SubCategories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBLSubCategory tBLSubCategory = await db.TBLSubCategory.FindAsync(id);
            if (tBLSubCategory == null)
            {
                return HttpNotFound();
            }
            return View(tBLSubCategory);
        }

        // GET: SubCategories/Create
        public ActionResult Create()
        {
            ViewBag.Category = new SelectList(db.TBLCategory, "CategoryID", "CategoryName");
            return View();
        }

        // POST: SubCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SubCategoryID,SubCategoryName,Category")] TBLSubCategory tBLSubCategory)
        {
            if (ModelState.IsValid)
            {
                db.TBLSubCategory.Add(tBLSubCategory);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Category = new SelectList(db.TBLCategory, "CategoryID", "CategoryName", tBLSubCategory.Category);
            return View(tBLSubCategory);
        }

        // GET: SubCategories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBLSubCategory tBLSubCategory = await db.TBLSubCategory.FindAsync(id);
            if (tBLSubCategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.Category = new SelectList(db.TBLCategory, "CategoryID", "CategoryName", tBLSubCategory.Category);
            return View(tBLSubCategory);
        }

        // POST: SubCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SubCategoryID,SubCategoryName,Category")] TBLSubCategory tBLSubCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tBLSubCategory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Category = new SelectList(db.TBLCategory, "CategoryID", "CategoryName", tBLSubCategory.Category);
            return View(tBLSubCategory);
        }

        // GET: SubCategories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBLSubCategory tBLSubCategory = await db.TBLSubCategory.FindAsync(id);
            if (tBLSubCategory == null)
            {
                return HttpNotFound();
            }
            return View(tBLSubCategory);
        }

        // POST: SubCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TBLSubCategory tBLSubCategory = await db.TBLSubCategory.FindAsync(id);
            db.TBLSubCategory.Remove(tBLSubCategory);
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

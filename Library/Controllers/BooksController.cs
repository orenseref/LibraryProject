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
    public class BooksController : Controller
    {
        private LibraryDBEntities db = new LibraryDBEntities();

        // GET: Books
        public async Task<ActionResult> Index(string p)
        {
            var Book = from b in db.TBLBook select b;
            if(!string.IsNullOrEmpty(p))
            {
                Book = Book.Where(t => t.BookName.Contains(p));
            }
            var tBLBook = db.TBLBook.Include(t => t.TBLAuthor).Include(t => t.TBLCategory).Include(t => t.TBLSubCategory);
            return View(await tBLBook.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBLBook tBLBook = await db.TBLBook.FindAsync(id);
            if (tBLBook == null)
            {
                return HttpNotFound();
            }
            return View(tBLBook);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            var CategoryList = db.TBLCategory.Select(ctg =>
                                            new { ctg.CategoryID, ctg.CategoryName }).ToList();
            ViewBag.Author = new SelectList(db.TBLAuthor, "AuthorID", "AuthorName");

            ViewBag.CategorySelectList = new SelectList(CategoryList, "CategoryID", "CategoryName");

            ViewBag.SubCategoryListSelectList = new SelectList(new List<TBLSubCategory>(), "SubCategoryID", "SubCategoryName");

            return View();
            
            //ViewBag.Category = new SelectList(db.TBLCategory, "CategoryID", "CategoryName");
            //ViewBag.SubCategory = new SelectList(db.TBLSubCategory, "SubCategoryID", "SubCategoryName");
            //return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "BookID,BookName,Category,SubCategory,Author,Statu")] TBLBook tBLBook, string CategoryID)
        {
            if (ModelState.IsValid)
            {
                db.TBLBook.Add(tBLBook);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Author = new SelectList(db.TBLAuthor, "AuthorID", "AuthorName", tBLBook.Author);
            var CategoryList = db.TBLCategory.Select(ctg =>
                                            new { ctg.CategoryID, ctg.CategoryName }).ToList();

            ViewBag.CategorySelectList = new SelectList(CategoryList, "CategoryID", "CategoryName", CategoryID);

            var SubCategoryList = db.TBLSubCategory.Where(sub => sub.Category.ToString() == CategoryID).Select(sub => new { sub.SubCategoryID, sub.SubCategoryName }).ToList();

            ViewBag.SubCategoryListSelectList = new SelectList(SubCategoryList, "SubCategoryID", "SubCategoryName", tBLBook.SubCategory);

            return View(tBLBook);

           
            //ViewBag.Category = new SelectList(db.TBLCategory, "CategoryID", "CategoryName", tBLBook.Category);
            //ViewBag.SubCategory = new SelectList(db.TBLSubCategory, "SubCategoryID", "SubCategoryName", tBLBook.SubCategory);
            //return View(tBLBook);
        }

        public JsonResult GetSubCategoryByCategory(string CategoryID)
        {
            var SubCategoryList = db.TBLSubCategory.Where(sub => sub.Category.ToString() == CategoryID)
                                                      .Select(sub => new { sub.SubCategoryID, sub.SubCategoryName }).ToList();

            return Json(SubCategoryList, JsonRequestBehavior.AllowGet);
        }

        // GET: Books/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBLBook tBLBook = await db.TBLBook.FindAsync(id);
            if (tBLBook == null)
            {
                return HttpNotFound();
            }
            ViewBag.Author = new SelectList(db.TBLAuthor, "AuthorID", "AuthorName", tBLBook.Author);
            ViewBag.Category = new SelectList(db.TBLCategory, "CategoryID", "CategoryName", tBLBook.Category);
            ViewBag.SubCategory = new SelectList(db.TBLSubCategory, "SubCategoryID", "SubCategoryName", tBLBook.SubCategory);
            return View(tBLBook);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "BookID,BookName,Category,SubCategory,Author,Statu")] TBLBook tBLBook)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tBLBook).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Author = new SelectList(db.TBLAuthor, "AuthorID", "AuthorName", tBLBook.Author);
            ViewBag.Category = new SelectList(db.TBLCategory, "CategoryID", "CategoryName", tBLBook.Category);
            ViewBag.SubCategory = new SelectList(db.TBLSubCategory, "SubCategoryID", "SubCategoryName", tBLBook.SubCategory);
            return View(tBLBook);
        }

        // GET: Books/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBLBook tBLBook = await db.TBLBook.FindAsync(id);
            if (tBLBook == null)
            {
                return HttpNotFound();
            }
            return View(tBLBook);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TBLBook tBLBook = await db.TBLBook.FindAsync(id);
            db.TBLBook.Remove(tBLBook);
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

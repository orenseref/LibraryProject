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
    public class BorrowsController : Controller
    {
        private LibraryDBEntities db = new LibraryDBEntities();

        // GET: Borrows
        public async Task<ActionResult> Index()
        {
            var tBLBorrow = db.TBLBorrow.Include(t => t.TBLBook).Include(t => t.TBLMember);
            return View(await tBLBorrow.ToListAsync());
        }

        // GET: Borrows/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBLBorrow tBLBorrow = await db.TBLBorrow.FindAsync(id);
            if (tBLBorrow == null)
            {
                return HttpNotFound();
            }
            return View(tBLBorrow);
        }

        // GET: Borrows/Create
        public ActionResult Create()
        {
            ViewBag.Book = new SelectList(db.TBLBook, "BookID", "BookName");
            ViewBag.Member = new SelectList(db.TBLMember, "MemberID", "MemberName");
            return View();
        }

        // POST: Borrows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "BorrowID,Book,Member,BorrowDate,ReturnDate")] TBLBorrow tBLBorrow)
        {
            if (ModelState.IsValid)
            {
                db.TBLBorrow.Add(tBLBorrow);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Book = new SelectList(db.TBLBook, "BookID", "BookName", tBLBorrow.Book);
            ViewBag.Member = new SelectList(db.TBLMember, "MemberID", "MemberName", tBLBorrow.Member);
            return View(tBLBorrow);
        }

        // GET: Borrows/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBLBorrow tBLBorrow = await db.TBLBorrow.FindAsync(id);
            if (tBLBorrow == null)
            {
                return HttpNotFound();
            }
            ViewBag.Book = new SelectList(db.TBLBook, "BookID", "BookName", tBLBorrow.Book);
            ViewBag.Member = new SelectList(db.TBLMember, "MemberID", "MemberName", tBLBorrow.Member);
            return View(tBLBorrow);
        }

        // POST: Borrows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "BorrowID,Book,Member,BorrowDate,ReturnDate")] TBLBorrow tBLBorrow)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tBLBorrow).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Book = new SelectList(db.TBLBook, "BookID", "BookName", tBLBorrow.Book);
            ViewBag.Member = new SelectList(db.TBLMember, "MemberID", "MemberName", tBLBorrow.Member);
            return View(tBLBorrow);
        }

        // GET: Borrows/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBLBorrow tBLBorrow = await db.TBLBorrow.FindAsync(id);
            if (tBLBorrow == null)
            {
                return HttpNotFound();
            }
            return View(tBLBorrow);
        }

        // POST: Borrows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TBLBorrow tBLBorrow = await db.TBLBorrow.FindAsync(id);
            db.TBLBorrow.Remove(tBLBorrow);
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

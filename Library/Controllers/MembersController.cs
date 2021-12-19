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
    public class MembersController : Controller
    {
        private LibraryDBEntities db = new LibraryDBEntities();

        // GET: Members
        public async Task<ActionResult> Index()
        {
            return View(await db.TBLMember.ToListAsync());
        }

        // GET: Members/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBLMember tBLMember = await db.TBLMember.FindAsync(id);
            if (tBLMember == null)
            {
                return HttpNotFound();
            }
            return View(tBLMember);
        }

        // GET: Members/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MemberID,MemberName,MemberMail,MemberPhone")] TBLMember tBLMember)
        {
            if (ModelState.IsValid)
            {
                db.TBLMember.Add(tBLMember);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tBLMember);
        }

        // GET: Members/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBLMember tBLMember = await db.TBLMember.FindAsync(id);
            if (tBLMember == null)
            {
                return HttpNotFound();
            }
            return View(tBLMember);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MemberID,MemberName,MemberMail,MemberPhone")] TBLMember tBLMember)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tBLMember).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tBLMember);
        }

        // GET: Members/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBLMember tBLMember = await db.TBLMember.FindAsync(id);
            if (tBLMember == null)
            {
                return HttpNotFound();
            }
            return View(tBLMember);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TBLMember tBLMember = await db.TBLMember.FindAsync(id);
            db.TBLMember.Remove(tBLMember);
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

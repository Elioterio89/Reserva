using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Reserva.Models;
using PagedList;
using Microsoft.Ajax.Utilities;

namespace Reserva.Controllers
{
    public class CalibresController : Controller
    {
        private ReservaBDEntities db = new ReservaBDEntities();

        // GET: Calibres
        public async Task<ActionResult> Index(string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            List<Calibre> lCalibre = db.Calibres.DistinctBy(x => x.Descricao).OrderBy(x => x.Id).ToList();

            int pageSize = 7;
            int pageNumber = (page ?? 1);

            return View(lCalibre.OrderBy(x=>x.Id).ToPagedList(pageNumber, pageSize));
        }

        // GET: Calibres/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calibre calibre = await db.Calibres.FindAsync(id);
            if (calibre == null)
            {
                return HttpNotFound();
            }
            return View(calibre);
        }

        // GET: Calibres/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Calibres/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Descricao")] Calibre calibre)
        {
            if (ModelState.IsValid)
            {
                db.Calibres.Add(calibre);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(calibre);
        }

        // GET: Calibres/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calibre calibre = await db.Calibres.FindAsync(id);
            if (calibre == null)
            {
                return HttpNotFound();
            }
            return View(calibre);
        }

        // POST: Calibres/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Descricao")] Calibre calibre)
        {
            if (ModelState.IsValid)
            {
                db.Entry(calibre).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(calibre);
        }

        // GET: Calibres/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calibre calibre = await db.Calibres.FindAsync(id);
            if (calibre == null)
            {
                return HttpNotFound();
            }
            return View(calibre);
        }

        // POST: Calibres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Calibre calibre = await db.Calibres.FindAsync(id);
            db.Calibres.Remove(calibre);
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

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

namespace Reserva.Controllers
{
    public class FabricantesController : Controller
    {
        private ReservaBDEntities db = new ReservaBDEntities();

        // GET: Fabricantes
        public async Task<ActionResult> Index()
        {
            return View(await db.Fabricantes.ToListAsync());
        }

        // GET: Fabricantes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fabricante fabricante = await db.Fabricantes.FindAsync(id);
            if (fabricante == null)
            {
                return HttpNotFound();
            }
            return View(fabricante);
        }

        // GET: Fabricantes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fabricantes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Descricao")] Fabricante fabricante)
        {
            if (ModelState.IsValid)
            {
                db.Fabricantes.Add(fabricante);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(fabricante);
        }

        // GET: Fabricantes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fabricante fabricante = await db.Fabricantes.FindAsync(id);
            if (fabricante == null)
            {
                return HttpNotFound();
            }
            return View(fabricante);
        }

        // POST: Fabricantes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Descricao")] Fabricante fabricante)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fabricante).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(fabricante);
        }

        // GET: Fabricantes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fabricante fabricante = await db.Fabricantes.FindAsync(id);
            if (fabricante == null)
            {
                return HttpNotFound();
            }
            return View(fabricante);
        }

        // POST: Fabricantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Fabricante fabricante = await db.Fabricantes.FindAsync(id);
            db.Fabricantes.Remove(fabricante);
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

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
    public class AlmoxarifadoesController : Controller
    {
        private ReservaBDEntities db = new ReservaBDEntities();

        // GET: Almoxarifadoes
        public async Task<ActionResult> Index()
        {
            return View(await db.Almoxarifadoes.ToListAsync());
        }

        // GET: Almoxarifadoes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Almoxarifado almoxarifado = await db.Almoxarifadoes.FindAsync(id);
            if (almoxarifado == null)
            {
                return HttpNotFound();
            }
            return View(almoxarifado);
        }

        // GET: Almoxarifadoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Almoxarifadoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Descricao,Sigla")] Almoxarifado almoxarifado)
        {
            if (ModelState.IsValid)
            {
                db.Almoxarifadoes.Add(almoxarifado);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(almoxarifado);
        }

        // GET: Almoxarifadoes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Almoxarifado almoxarifado = await db.Almoxarifadoes.FindAsync(id);
            if (almoxarifado == null)
            {
                return HttpNotFound();
            }
            return View(almoxarifado);
        }

        // POST: Almoxarifadoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Descricao,Sigla")] Almoxarifado almoxarifado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(almoxarifado).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(almoxarifado);
        }

        // GET: Almoxarifadoes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Almoxarifado almoxarifado = await db.Almoxarifadoes.FindAsync(id);
            if (almoxarifado == null)
            {
                return HttpNotFound();
            }
            return View(almoxarifado);
        }

        // POST: Almoxarifadoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Almoxarifado almoxarifado = await db.Almoxarifadoes.FindAsync(id);
            db.Almoxarifadoes.Remove(almoxarifado);
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

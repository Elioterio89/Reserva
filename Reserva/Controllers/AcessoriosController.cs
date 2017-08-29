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
    public class AcessoriosController : Controller
    {
        private ReservaBDEntities db = new ReservaBDEntities();

        // GET: Acessorios
        public async Task<ActionResult> Index()
        {
            var acessorios = db.Acessorios.Include(a => a.Almoxarifado).Include(a => a.Fabricante);
            return View(await acessorios.ToListAsync());
        }

        // GET: Acessorios/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Acessorio acessorio = db.Acessorios.Where(x => x.Id == id).FirstOrDefault();
            if (acessorio == null)
            {
                return HttpNotFound();
            }
            return View(acessorio);
        }

        // GET: Acessorios/Create
        public ActionResult Create()
        {
            ViewBag.AlmoxarifadoId = new SelectList(db.Almoxarifadoes, "Id", "Descricao");
            ViewBag.FabricanteId = new SelectList(db.Fabricantes, "Id", "Descricao");
            return View();
        }

        // POST: Acessorios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Tombo,AlmoxarifadoId,Disponivel,FabricanteId,Id,Descricao")] Acessorio acessorio)
        {
            if (ModelState.IsValid)
            {
                db.Materiais.Add(acessorio);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AlmoxarifadoId = new SelectList(db.Almoxarifadoes, "Id", "Descricao", acessorio.AlmoxarifadoId);
            ViewBag.FabricanteId = new SelectList(db.Fabricantes, "Id", "Descricao", acessorio.FabricanteId);
            return View(acessorio);
        }

        // GET: Acessorios/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Acessorio acessorio = db.Acessorios.Where(x => x.Id == id).FirstOrDefault();
            if (acessorio == null)
            {
                return HttpNotFound();
            }
            ViewBag.AlmoxarifadoId = new SelectList(db.Almoxarifadoes, "Id", "Descricao", acessorio.AlmoxarifadoId);
            ViewBag.FabricanteId = new SelectList(db.Fabricantes, "Id", "Descricao", acessorio.FabricanteId);
            return View(acessorio);
        }

        // POST: Acessorios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Tombo,AlmoxarifadoId,Disponivel,FabricanteId,Id,Descricao")] Acessorio acessorio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(acessorio).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AlmoxarifadoId = new SelectList(db.Almoxarifadoes, "Id", "Descricao", acessorio.AlmoxarifadoId);
            ViewBag.FabricanteId = new SelectList(db.Fabricantes, "Id", "Descricao", acessorio.FabricanteId);
            return View(acessorio);
        }

        // GET: Acessorios/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Acessorio acessorio = db.Acessorios.Where(x => x.Id == id).FirstOrDefault();
            if (acessorio == null)
            {
                return HttpNotFound();
            }
            return View(acessorio);
        }

        // POST: Acessorios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Acessorio acessorio = db.Acessorios.Where(x => x.Id == id).FirstOrDefault();
            db.Materiais.Remove(acessorio);
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

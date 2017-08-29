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
    public class ArmamentoesController : Controller
    {
        private ReservaBDEntities db = new ReservaBDEntities();

        // GET: Armamentoes
        public async Task<ActionResult> Index()
        {
            var armamentos = db.Armamentos.Include(a => a.Almoxarifado).Include(a => a.Fabricante);
            return View(await armamentos.ToListAsync());
        }

        // GET: Armamentoes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Armamento armamento = db.Armamentos.Where(x => x.Id == id).FirstOrDefault();
            
            if (armamento == null)
            {
                return HttpNotFound();
            }
            return View(armamento);
        }

        // GET: Armamentoes/Create
        public ActionResult Create()
        {
            ViewBag.AlmoxarifadoId = new SelectList(db.Almoxarifadoes, "Id", "Descricao");
            ViewBag.FabricanteId = new SelectList(db.Fabricantes, "Id", "Descricao");
            return View();
        }

        // POST: Armamentoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Tombo,AlmoxarifadoId,Disponivel,FabricanteId,Id,NSerie,Modelo,MunicaoId")] Armamento armamento)
        {
            if (ModelState.IsValid)
            {
                db.Materiais.Add(armamento);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AlmoxarifadoId = new SelectList(db.Almoxarifadoes, "Id", "Descricao", armamento.AlmoxarifadoId);
            ViewBag.FabricanteId = new SelectList(db.Fabricantes, "Id", "Descricao", armamento.FabricanteId);
            return View(armamento);
        }

        // GET: Armamentoes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Armamento armamento = db.Armamentos.Where(x => x.Id == id).FirstOrDefault();
            if (armamento == null)
            {
                return HttpNotFound();
            }
            ViewBag.AlmoxarifadoId = new SelectList(db.Almoxarifadoes, "Id", "Descricao", armamento.AlmoxarifadoId);
            ViewBag.FabricanteId = new SelectList(db.Fabricantes, "Id", "Descricao", armamento.FabricanteId);
            return View(armamento);
        }

        // POST: Armamentoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Tombo,AlmoxarifadoId,Disponivel,FabricanteId,Id,NSerie,Modelo,MunicaoId")] Armamento armamento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(armamento).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AlmoxarifadoId = new SelectList(db.Almoxarifadoes, "Id", "Descricao", armamento.AlmoxarifadoId);
            ViewBag.FabricanteId = new SelectList(db.Fabricantes, "Id", "Descricao", armamento.FabricanteId);
            return View(armamento);
        }

        // GET: Armamentoes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Armamento armamento = db.Armamentos.Where(x => x.Id == id).FirstOrDefault();
            if (armamento == null)
            {
                return HttpNotFound();
            }
            return View(armamento);
        }

        // POST: Armamentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Armamento armamento = db.Armamentos.Where(x => x.Id == id).FirstOrDefault();
            db.Materiais.Remove(armamento);
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

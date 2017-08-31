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
using Microsoft.Ajax.Utilities;

namespace Reserva.Controllers
{
    public class AcessoriosController : Controller
    {
        private ReservaBDEntities db = new ReservaBDEntities();

        // GET: Acessorios
        public async Task<ActionResult> Index()
        {
            var acessorios = db.Acessorios.Include(m => m.Fabricante).DistinctBy(x => x.Lote);
            List<Acessorio> lAcessorios = new List<Acessorio>();
            foreach (Acessorio oAcessorio in acessorios.ToList())
            {
                oAcessorio.Estoque = db.Acessorios.Where(x => x.Lote == oAcessorio.Lote).ToList().Count();

                lAcessorios.Add(oAcessorio);
            }
            return View(lAcessorios.Where(x => x.AlmoxarifadoId == OperadorViewModel.USUARIO.AlmoxarifadoId));
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
        public async Task<ActionResult> Create([Bind(Include = "Tombo,AlmoxarifadoId,Disponivel,FabricanteId,Id,Descricao")] Acessorio acessorio, FormCollection form)
        {
            int vLote;
            if (ModelState.IsValid)
            {
                if (db.Acessorios.Count() > 0)
                {
                    List<Acessorio> oAce = db.Acessorios.OrderBy(x => x.Lote).ToList();

                    vLote = oAce.LastOrDefault().Lote + 1;
                }
                else
                {
                    vLote = 1;
                }

                for (int i = 1; i <= Convert.ToInt32(form["QtdAce"]); i++)
                {

                    Acessorio oAcessorio = new Acessorio();
                    oAcessorio.AlmoxarifadoId = OperadorViewModel.USUARIO.AlmoxarifadoId;
                    oAcessorio.Descricao = acessorio.Descricao;
                    oAcessorio.Disponivel = true;
                    oAcessorio.FabricanteId = acessorio.FabricanteId;
                    oAcessorio.Lote = vLote;


                    db.Materiais.Add(oAcessorio);
                    await db.SaveChangesAsync();
                }

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

                int vLote = db.Acessorios.Where(x => x.Id == acessorio.Id).ToList().FirstOrDefault().Lote;
                foreach (Acessorio oAcessorio in db.Acessorios.Where(x => x.Lote == vLote).ToList())
                {
                    oAcessorio.FabricanteId = acessorio.FabricanteId;
                    oAcessorio.Descricao = acessorio.Descricao;
                    

                    db.Entry(oAcessorio).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }
                return RedirectToAction("Index");
            }
            ViewBag.AlmoxarifadoId = new SelectList(db.Almoxarifadoes, "Id", "Descricao", acessorio.AlmoxarifadoId);
            ViewBag.FabricanteId = new SelectList(db.Fabricantes, "Id", "Descricao", acessorio.FabricanteId);
            return View(acessorio);
        }

        public async Task<ActionResult> Add(int? id)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add([Bind(Include = "Tombo,AlmoxarifadoId,Disponivel,FabricanteId,Id,Descricao")] Acessorio oAce, FormCollection form)
        {
            if (ModelState.IsValid)
            {

                Acessorio acessorio = db.Acessorios.Where(x => x.Id == oAce.Id).ToList().FirstOrDefault();

                for (int i = 1; i <= Convert.ToInt32(form["QtdAce"]); i++)
                {

                    Acessorio oAcessorio = new Acessorio();
                    oAcessorio.AlmoxarifadoId = OperadorViewModel.USUARIO.AlmoxarifadoId;
                    oAcessorio.Descricao = acessorio.Descricao;
                    oAcessorio.Disponivel = true;
                    oAcessorio.FabricanteId = acessorio.FabricanteId;
                    oAcessorio.Lote = acessorio.Lote;



                    db.Materiais.Add(oAcessorio);
                    await db.SaveChangesAsync();
                }

                return RedirectToAction("Index");
            }
            ViewBag.AlmoxarifadoId = new SelectList(db.Almoxarifadoes, "Id", "Descricao", oAce.AlmoxarifadoId);
            ViewBag.FabricanteId = new SelectList(db.Fabricantes, "Id", "Descricao", oAce.FabricanteId);
            return View(oAce);
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
            int vLote = db.Acessorios.Where(x => x.Id == id).ToList().FirstOrDefault().Lote;
            foreach (Acessorio oAcessorio in db.Acessorios.Where(x => x.Lote == vLote).ToList())
            {
                db.Materiais.Remove(oAcessorio);
                await db.SaveChangesAsync();
            }

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

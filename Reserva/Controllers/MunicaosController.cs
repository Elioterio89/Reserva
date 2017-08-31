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
    public class MunicaosController : Controller
    {
        private ReservaBDEntities db = new ReservaBDEntities();

        // GET: Municaos
        public async Task<ActionResult> Index()
        {
            var municoes = db.Municoes.Include(m => m.Fabricante).Include(m => m.Calibre).DistinctBy(x => x.Lote);
            List<Municao> lMunicoes = new List<Municao>();
            foreach (Municao oMunicao in municoes.ToList())
            {
                oMunicao.Estoque = db.Municoes.Where(x => x.Lote == oMunicao.Lote).ToList().Count();

                lMunicoes.Add(oMunicao);
            }

            return View(lMunicoes.Where(x=>x.AlmoxarifadoId==OperadorViewModel.USUARIO.AlmoxarifadoId));
        }

        // GET: Municaos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Municao municao = db.Municoes.Where(x => x.Id == id).FirstOrDefault();

            if (municao == null)
            {
                return HttpNotFound();
            }
            return View(municao);
        }

        // GET: Municaos/Create
        public ActionResult Create()
        {

            ViewBag.FabricanteId = new SelectList(db.Fabricantes, "Id", "Descricao");
            ViewBag.CalibreId = new SelectList(db.Calibres, "Id", "Descricao");
            return View();
        }

        // POST: Municaos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Tombo,AlmoxarifadoId,Disponivel,FabricanteId,Id,Descricao,CalibreId,QuantidadeBala")] Municao municao, FormCollection form)
        {
            int vLote;
            if (ModelState.IsValid)
            {
                if (db.Municoes.Count() > 0)
                {
                    List<Municao> oMun = db.Municoes.OrderBy(x => x.Lote).ToList();

                    vLote = oMun.LastOrDefault().Lote + 1;
                }
                else
                {
                    vLote = 1;
                }

                for (int i = 1; i <= Convert.ToInt32(form["QtdMun"]); i++)
                {

                    Municao oMunicao = new Municao();
                    oMunicao.AlmoxarifadoId = OperadorViewModel.USUARIO.AlmoxarifadoId;
                    oMunicao.CalibreId = municao.CalibreId;
                    oMunicao.Descricao = municao.Descricao;
                    oMunicao.Disponivel = true;
                    oMunicao.FabricanteId = municao.FabricanteId;
                    oMunicao.QuantidadeBala = municao.QuantidadeBala;
                    oMunicao.Lote = vLote;



                    db.Materiais.Add(oMunicao);
                    await db.SaveChangesAsync();
                }

                return RedirectToAction("Index");
            }

            ViewBag.FabricanteId = new SelectList(db.Fabricantes, "Id", "Descricao", municao.FabricanteId);
            ViewBag.CalibreId = new SelectList(db.Calibres, "Id", "Descricao", municao.CalibreId);
            return View(municao);
        }

        // GET: Municaos/Add/5
        public async Task<ActionResult> Add(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Municao municao = db.Municoes.Where(x => x.Id == id).FirstOrDefault();
            if (municao == null)
            {
                return HttpNotFound();
            }
            ViewBag.AlmoxarifadoId = new SelectList(db.Almoxarifadoes, "Id", "Descricao", municao.AlmoxarifadoId);
            ViewBag.FabricanteId = new SelectList(db.Fabricantes, "Id", "Descricao", municao.FabricanteId);
            ViewBag.CalibreId = new SelectList(db.Calibres, "Id", "Descricao", municao.CalibreId);
            return View(municao);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add([Bind(Include = "Tombo,AlmoxarifadoId,Disponivel,FabricanteId,Id,Descricao,CalibreId,QuantidadeBala")] Municao oMun, FormCollection form)
        {
            if (ModelState.IsValid)
            {
               
                 Municao municao = db.Municoes.Where(x => x.Id == oMun.Id).ToList().FirstOrDefault();
                
                for (int i = 1; i <= Convert.ToInt32(form["QtdMun"]); i++)
                {

                    Municao oMunicao = new Municao();
                    oMunicao.AlmoxarifadoId = OperadorViewModel.USUARIO.AlmoxarifadoId;
                    oMunicao.CalibreId = municao.CalibreId;
                    oMunicao.Descricao = municao.Descricao;
                    oMunicao.Disponivel = true;
                    oMunicao.FabricanteId = municao.FabricanteId;
                    oMunicao.QuantidadeBala = municao.QuantidadeBala;
                    oMunicao.Lote = municao.Lote;



                    db.Materiais.Add(oMunicao);
                    await db.SaveChangesAsync();
                }

                return RedirectToAction("Index");
            }
            ViewBag.AlmoxarifadoId = new SelectList(db.Almoxarifadoes, "Id", "Descricao", oMun.AlmoxarifadoId);
            ViewBag.FabricanteId = new SelectList(db.Fabricantes, "Id", "Descricao", oMun.FabricanteId);
            ViewBag.CalibreId = new SelectList(db.Calibres, "Id", "Descricao", oMun.CalibreId);
            return View(oMun);
        }

        // GET: Municaos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Municao municao = db.Municoes.Where(x => x.Id == id).FirstOrDefault();
            if (municao == null)
            {
                return HttpNotFound();
            }
            ViewBag.AlmoxarifadoId = new SelectList(db.Almoxarifadoes, "Id", "Descricao", municao.AlmoxarifadoId);
            ViewBag.FabricanteId = new SelectList(db.Fabricantes, "Id", "Descricao", municao.FabricanteId);
            ViewBag.CalibreId = new SelectList(db.Calibres, "Id", "Descricao", municao.CalibreId);
            return View(municao);
        }

        // POST: Municaos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Tombo,AlmoxarifadoId,Disponivel,FabricanteId,Id,Descricao,CalibreId,QuantidadeBala")] Municao municao)
        {
            if (ModelState.IsValid)
            {

                int vLote = db.Municoes.Where(x => x.Id == municao.Id).ToList().FirstOrDefault().Lote;
                foreach (Municao oMunicao in db.Municoes.Where(x => x.Lote == vLote).ToList())
                {
                    oMunicao.FabricanteId = municao.FabricanteId;
                    oMunicao.Descricao = municao.Descricao;
                    oMunicao.CalibreId = municao.CalibreId;
                    oMunicao.QuantidadeBala = municao.QuantidadeBala;

                    db.Entry(oMunicao).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }

                return RedirectToAction("Index");
            }
            ViewBag.AlmoxarifadoId = new SelectList(db.Almoxarifadoes, "Id", "Descricao", municao.AlmoxarifadoId);
            ViewBag.FabricanteId = new SelectList(db.Fabricantes, "Id", "Descricao", municao.FabricanteId);
            ViewBag.CalibreId = new SelectList(db.Calibres, "Id", "Descricao", municao.CalibreId);
            return View(municao);
        }

        // GET: Municaos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Municao municao = db.Municoes.Where(x => x.Id == id).FirstOrDefault();
            if (municao == null)
            {
                return HttpNotFound();
            }
            return View(municao);
        }

        // POST: Municaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {

            int vLote = db.Municoes.Where(x => x.Id == id).ToList().FirstOrDefault().Lote;
            foreach (Municao oMunicao in db.Municoes.Where(x => x.Lote == vLote).ToList())
            {
                db.Materiais.Remove(oMunicao);
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

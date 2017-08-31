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
    public class ArmamentoesController : Controller
    {
        private ReservaBDEntities db = new ReservaBDEntities();

        // GET: Armamentoes
        public async Task<ActionResult> Index()
        {

            var armamentos = db.Armamentos.Include(a => a.Almoxarifado).Include(a => a.Fabricante).DistinctBy(x => x.Lote);
            List<Armamento> lArmamento = new List<Armamento>();

            foreach (Armamento oArmamento in armamentos.ToList())
            {
                oArmamento.Estoque = db.Armamentos.Where(x => x.Lote == oArmamento.Lote).ToList().Count();

                lArmamento.Add(oArmamento);
            }

            return View(lArmamento.Where(x => x.AlmoxarifadoId == OperadorViewModel.USUARIO.AlmoxarifadoId));
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
            ViewBag.MunicaoId = new SelectList(db.Municoes.DistinctBy(x=>x.Lote), "Id", "Descricao");

            return View();
        }

        // POST: Armamentoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Tombo,AlmoxarifadoId,Disponivel,FabricanteId,Id,NSerie,Modelo,MunicaoId")] Armamento armamento, FormCollection form)
        {
            int vLote;
            if (ModelState.IsValid)
            {
                if (db.Armamentos.Count() > 0)
                {
                    List<Armamento> oArm = db.Armamentos.OrderBy(x => x.Lote).ToList();

                    vLote = oArm.LastOrDefault().Lote + 1;
                }
                else
                {
                    vLote = 1;
                }

                for (int i = 1; i <= Convert.ToInt32(form["QtdArm"]); i++)
                {

                    Armamento oArmamento = new Armamento();
                    oArmamento.AlmoxarifadoId = OperadorViewModel.USUARIO.AlmoxarifadoId;
                    oArmamento.MunicaoId = armamento.MunicaoId;
                    oArmamento.Modelo = armamento.Modelo;
                    oArmamento.Disponivel = true;
                    oArmamento.FabricanteId = armamento.FabricanteId;
                    oArmamento.NSerie = armamento.NSerie;
                    oArmamento.Lote = vLote;



                    db.Materiais.Add(oArmamento);
                    await db.SaveChangesAsync();
                }

                return RedirectToAction("Index");
            }

            ViewBag.AlmoxarifadoId = new SelectList(db.Almoxarifadoes, "Id", "Descricao", armamento.AlmoxarifadoId);
            ViewBag.FabricanteId = new SelectList(db.Fabricantes, "Id", "Descricao", armamento.FabricanteId);
            ViewBag.MunicaoId = new SelectList(db.Municoes, "Id", "Descricao", armamento.MunicaoId);
            return View(armamento);
        }

        // GET: Municaos/Add/5
        public async Task<ActionResult> Add(int? id)
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
            ViewBag.MunicaoId = new SelectList(db.Municoes, "Id", "Descricao", armamento.MunicaoId);
            return View(armamento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add([Bind(Include = "Tombo,AlmoxarifadoId,Disponivel,FabricanteId,Id,Modelo,MunicaoId,NSerie")] Armamento oArm, FormCollection form)
        {
            if (ModelState.IsValid)
            {

                Armamento armamento = db.Armamentos.Where(x => x.Id == oArm.Id).ToList().FirstOrDefault();

                for (int i = 1; i <= Convert.ToInt32(form["QtdArm"]); i++)
                {

                    Armamento oArmamento = new Armamento();
                    oArmamento.AlmoxarifadoId = OperadorViewModel.USUARIO.AlmoxarifadoId;
                    oArmamento.MunicaoId = armamento.MunicaoId;
                    oArmamento.Modelo = armamento.Modelo;
                    oArmamento.Disponivel = true;
                    oArmamento.FabricanteId = armamento.FabricanteId;
                    oArmamento.NSerie = armamento.NSerie;
                    oArmamento.Lote = armamento.Lote;



                    db.Materiais.Add(oArmamento);
                    await db.SaveChangesAsync();
                }

                return RedirectToAction("Index");
            }
            ViewBag.AlmoxarifadoId = new SelectList(db.Almoxarifadoes, "Id", "Descricao", oArm.AlmoxarifadoId);
            ViewBag.FabricanteId = new SelectList(db.Fabricantes, "Id", "Descricao", oArm.FabricanteId);
            ViewBag.MunicaoId = new SelectList(db.Municoes, "Id", "Descricao", oArm.MunicaoId);

            return View(oArm);
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
            ViewBag.MunicaoId = new SelectList(db.Municoes.DistinctBy(x=>x.Lote), "Id", "Descricao", armamento.MunicaoId);
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
                int vLote = db.Armamentos.Where(x => x.Id == armamento.Id).ToList().FirstOrDefault().Lote;
                foreach (Armamento oArmamento in db.Armamentos.Where(x => x.Lote == vLote).ToList())
                {
                    oArmamento.FabricanteId = armamento.FabricanteId;
                    oArmamento.Modelo = armamento.Modelo;
                    oArmamento.MunicaoId = armamento.MunicaoId;
                    oArmamento.NSerie = armamento.NSerie;



                    db.Entry(oArmamento).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }

                return RedirectToAction("Index");
            }

            ViewBag.AlmoxarifadoId = new SelectList(db.Almoxarifadoes, "Id", "Descricao", armamento.AlmoxarifadoId);
            ViewBag.FabricanteId = new SelectList(db.Fabricantes, "Id", "Descricao", armamento.FabricanteId);
            ViewBag.MunicaoId = new SelectList(db.Municoes, "Id", "Descricao", armamento.MunicaoId);
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
            int vLote = db.Armamentos.Where(x => x.Id == id).ToList().FirstOrDefault().Lote;
            foreach (Armamento pArmamento in db.Armamentos.Where(x => x.Lote == vLote).ToList())
            {
                db.Materiais.Remove(pArmamento);
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

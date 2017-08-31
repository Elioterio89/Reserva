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
    public class MilitarsController : Controller
    {
        private ReservaBDEntities db = new ReservaBDEntities();

        // GET: Militars
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NomeFull = String.IsNullOrEmpty(sortOrder) ? "nome_desc" : "";
            ViewBag.NomeGuerra = sortOrder == "guerra" ? "guerra_desc" : "guerra";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var lMilitares = from m in db.Militars
                             select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                lMilitares = lMilitares.Where(s => s.Nome.ToUpper().Contains(searchString.ToUpper())
                                       || s.NomeGuerra.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "nome_desc":
                    lMilitares = lMilitares.OrderByDescending(m => m.Nome);
                    break;
                case "guerra":
                    lMilitares = lMilitares.OrderBy(m => m.NomeGuerra);
                    break;
                case "guerra_desc":
                    lMilitares = lMilitares.OrderByDescending(m => m.NomeGuerra);
                    break;
                default:
                    lMilitares = lMilitares.OrderBy(m => m.Nome);
                    break;
            }
            int pageSize = 7;
            int pageNumber = (page ?? 1);

            return View(lMilitares.ToPagedList(pageNumber, pageSize));
        }

        // GET: Militars/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Militar militar = await db.Militars.FindAsync(id);
            if (militar == null)
            {
                return HttpNotFound();
            }
            return View(militar);
        }

        // GET: Militars/Create
        public ActionResult Create()
        {
            ViewBag.PatenteId = new SelectList(db.Patentes, "Id", "Descricao");
            return View();
        }

        // POST: Militars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Cautelar(Cautela cautela)
        //{


        //    if (ModelState.IsValid)
        //    {
        //        db.Militars.Add(militar);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.PatenteId = new SelectList(db.Patentes, "Id", "Descricao", militar.PatenteId);
        //    return View(militar);
        //}

        // GET: Militars/Edit/5
        public async Task<ActionResult> Retirar(int? id, string sortOrder, Cautela cautela)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Militar militar = db.Militars.Where(x => x.Id == id).First();
            ViewBag.Cautelador = militar.NomeDeGuerra;

            ViewBag.Acessorio = sortOrder == "acess" ? "acess" : "acess";
            ViewBag.Municao = sortOrder == "muni" ? "muni" : "muni";
            ViewBag.Armamento = String.IsNullOrEmpty(sortOrder) ? "arma" : "arma";


            var lMateriais = from m in db.Materiais.DistinctBy(x => x.Lote)
                             select m;

            switch (sortOrder)
            {
                case "acess":

                    List<Acessorio> lAcessorio = new List<Acessorio>();
                    foreach (var oAces in db.Acessorios.DistinctBy(x => x.Lote))
                    {
                        oAces.QtdDisponivel = db.Acessorios.Where(x => x.Lote == oAces.Lote && x.Disponivel == true).ToList().Count();
                        oAces.Natureza = oAces.Descricao;
                        lAcessorio.Add(oAces);
                    }
                    lMateriais = from m in lAcessorio
                                 select m;

                    break;
                case "muni":
                    List<Municao> lMunicao = new List<Municao>();
                    foreach (var oMun in db.Municoes.DistinctBy(x => x.Lote))
                    {
                        oMun.QtdDisponivel = db.Municoes.Where(x => x.Lote == oMun.Lote && x.Disponivel == true).ToList().Count();
                        oMun.Natureza = oMun.Descricao + " " + oMun.Calibre.Descricao;
                        lMunicao.Add(oMun);
                    }

                    lMateriais = from m in lMunicao
                                 select m;

                    break;
                default:
                    List<Armamento> lArmamento = new List<Armamento>();
                    foreach (var oArm in db.Armamentos.DistinctBy(x => x.Lote))
                    {
                        oArm.QtdDisponivel = db.Armamentos.Where(x => x.Lote == oArm.Lote && x.Disponivel == true).ToList().Count();
                        oArm.Natureza = oArm.Modelo + " " + oArm.Municao.Calibre.Descricao;
                        lArmamento.Add(oArm);
                    }
                    lMateriais = from m in lArmamento
                                 select m;

                    break;

            }



            if (lMateriais == null)
            {
                return HttpNotFound();
            }

            return View(lMateriais.ToList());
        }

        // POST: Militars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nome,NomeGuerra,Matricula,PatenteId")] Militar militar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(militar).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PatenteId = new SelectList(db.Patentes, "Id", "Descricao", militar.PatenteId);
            return View(militar);
        }

        // GET: Militars/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Militar militar = await db.Militars.FindAsync(id);
            if (militar == null)
            {
                return HttpNotFound();
            }
            return View(militar);
        }

        // POST: Militars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Militar militar = await db.Militars.FindAsync(id);
            db.Militars.Remove(militar);
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

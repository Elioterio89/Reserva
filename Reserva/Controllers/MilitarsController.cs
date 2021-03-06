﻿using System;
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

        // GET: Militars
        public async Task<ActionResult> Index2(string sortOrder, string currentFilter, string searchString, int? page)
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

        public async Task<ActionResult> DevolverCautela()
        {
            Militar oMilitar = db.Militars.Where(x => x.Id == OperadorViewModel.Militar.Id).FirstOrDefault();

            List<Cautela> lCautela = new List<Cautela>();

            lCautela = db.Cautelas.Where(x => x.Usuario.Id == oMilitar.Id && x.Operacaos.Where(o => o.Cautelado == true).Count() > 0).ToList();

            foreach (var oCau in lCautela)
            {
                foreach (var Op in oCau.Operacaos)
                {
                    Material oMaterial = db.Materiais.Where(x => x.Tombo == Op.MaterialTombo).FirstOrDefault();

                    oMaterial.Disponivel = true;
                    db.Entry(oMaterial).State = EntityState.Modified;
                    db.SaveChanges();

                    Op.Cautelado = false;
                    db.Entry(Op).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            Session.Remove("Militar");
            return RedirectToAction("Index2");
        }

        public async Task<ActionResult> SalvarCautela()
        {
            List<Material> lMateriais = new List<Material>();
            lMateriais = OperadorViewModel.Carrinho;

            List<Operacao> Operacoes = new List<Operacao>();

            foreach (Material oMat in lMateriais)
            {
                Operacao oOper = new Operacao();

                oOper.Cautelado = true;
                oOper.Material = oMat;
                oOper.MaterialTombo = oMat.Tombo;

                Operacoes.Add(oOper);

            }

            Cautela oCautela = new Cautela();

            Almoxarifado oAlmo = db.Almoxarifadoes.Where(x => x.Id == OperadorViewModel.USUARIO.Almoxarifado.Id).FirstOrDefault();
            Operador oOperador = db.Operadores.Where(x => x.Id == OperadorViewModel.USUARIO.Id).FirstOrDefault();
            Usuario oUser = db.Usuarios.Where(x => x.Id == OperadorViewModel.Militar.Id).FirstOrDefault();

            oCautela.Data = DateTime.Now;
            oCautela.NRegistro = "";
            oCautela.Almoxarifado = oAlmo;
            oCautela.Operador = oOperador;
            oCautela.Usuario = oUser;
            oCautela.Operacaos = Operacoes;

            db.Cautelas.Add(oCautela);
            db.SaveChanges();

            //foreach (Operacao oOper in Operacoes)
            //{
            //    oOper.CautelaId = oCautela.Id;
            //    oOper.Cautela = oCautela;

            //    db.Operacaos.Add(oOper);
            //    db.SaveChanges();
            //}

            foreach (Material oMat in lMateriais)
            {
                oMat.Disponivel = false;
                db.Entry(oMat).State = EntityState.Modified;
                db.SaveChanges();
            }

            oCautela.NRegistro = oCautela.Id.ToString() + oCautela.Almoxarifado.Id.ToString() + oCautela.Operador.Id.ToString() + oCautela.Usuario.Id.ToString();
            db.Entry(oCautela).State = EntityState.Modified;
            db.SaveChanges();

            Session.Remove("Carrinho");
            Session.Remove("Militar");

            return RedirectToAction("Index");
        }

        public ActionResult RetirarDoCarrinho(int id)
        {
            List<Material> lMateriais = new List<Material>();
            lMateriais = OperadorViewModel.Carrinho;

            Material lMaterial = lMateriais.Where(x => x.Tombo == id).FirstOrDefault();
            lMateriais.Remove(lMaterial);
            Session.Remove("Carrinho");

            Session.Add("Carrinho", lMateriais);

            return RedirectToAction("Retirar");
        }

        public ActionResult DevolverMaterialUnico(int id)
        {
            Operacao oOper = db.Operacaos.Where(x => x.Id == id).FirstOrDefault();

            Material oMaterial = db.Materiais.Where(x => x.Tombo == oOper.MaterialTombo).FirstOrDefault();

            oMaterial.Disponivel = true;
            db.Entry(oMaterial).State = EntityState.Modified;
            db.SaveChanges();

            oOper.Cautelado = false;
            db.Entry(oOper).State = EntityState.Modified;
            db.SaveChanges();


            Session.Remove("Militar");
            return RedirectToAction("Index2");
        }

        public ActionResult AdicionarAoCarrinho(int id)
        {
            Material lMaterial = db.Materiais.Where(x => x.Tombo == id).FirstOrDefault();

            if (db.Acessorios.Where(x => x.Tombo == lMaterial.Tombo).Count() > 0)
            {
                lMaterial.Nome = db.Acessorios.Where(x => x.Tombo == lMaterial.Tombo).FirstOrDefault().Descricao;
                lMaterial.Natureza = "Acessorio";
            }
            else
            {
                if (db.Armamentos.Where(x => x.Tombo == lMaterial.Tombo).Count() > 0)
                {
                    lMaterial.Nome = db.Armamentos.Where(x => x.Tombo == lMaterial.Tombo).FirstOrDefault().Modelo + " - "
                        + db.Armamentos.Where(x => x.Tombo == lMaterial.Tombo).FirstOrDefault().Municao.Calibre.Descricao;
                    lMaterial.Natureza = "Armamento";
                }
                else
                {
                    lMaterial.Nome = db.Municoes.Where(x => x.Tombo == lMaterial.Tombo).FirstOrDefault().Descricao + " - "
                                + db.Municoes.Where(x => x.Tombo == lMaterial.Tombo).FirstOrDefault().Calibre.Descricao;
                    lMaterial.Natureza = "Munição";
                }
            }

            lMaterial.FabricanteNome = lMaterial.Fabricante.Descricao;
            lMaterial.Disponivel = false;
            List<Material> lMateriais = new List<Material>();

            if (OperadorViewModel.Carrinho != null)
            {
                lMateriais = OperadorViewModel.Carrinho;

            }

            lMateriais.Add(lMaterial);


            Session.Add("Carrinho", lMateriais);


            return RedirectToAction("Retirar");
        }

        public async Task<ActionResult> Devolver(int? id)
        {
            List<Cautela> lCautela = new List<Cautela>();            

            lCautela = db.Cautelas.Where(x => x.Usuario.Id == id).ToList();

            Militar militar = db.Militars.Where(x => x.Id == id).FirstOrDefault();
            ViewBag.Cautelador = militar.NomeDeGuerra;
            Session.Add("Militar", militar);


            if (lCautela.Count > 0)
            {
                foreach (var oCau in lCautela)
                {
                    foreach (var Op in oCau.Operacaos)
                    {
                        if (db.Acessorios.Where(x => x.Tombo == Op.Material.Tombo).Count() > 0)
                        {
                            Op.DescricaoMaterial = db.Acessorios.Where(x => x.Tombo == Op.Material.Tombo).FirstOrDefault().Descricao;

                        }
                        else
                        {
                            if (db.Armamentos.Where(x => x.Tombo == Op.Material.Tombo).Count() > 0)
                            {
                                Op.DescricaoMaterial = db.Armamentos.Where(x => x.Tombo == Op.Material.Tombo).FirstOrDefault().Modelo + " - "
                                    + db.Armamentos.Where(x => x.Tombo == Op.Material.Tombo).FirstOrDefault().Municao.Calibre.Descricao;

                            }
                            else
                            {
                                Op.DescricaoMaterial = db.Municoes.Where(x => x.Tombo == Op.Material.Tombo).FirstOrDefault().Descricao + " - "
                                            + db.Municoes.Where(x => x.Tombo == Op.Material.Tombo).FirstOrDefault().Calibre.Descricao;

                            }
                        }
                    }
                }
            }

            return View(lCautela.OrderBy(x=>x.Data).ToList());
        }

        public async Task<ActionResult> Retirar(int? id, string sortOrder = "")
        {
            if (id == null)
            {
                id = OperadorViewModel.Militar.Id;
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            Militar militar = db.Militars.Where(x => x.Id == id).FirstOrDefault();
            ViewBag.Cautelador = militar.NomeDeGuerra;
            Session.Add("Militar", militar);

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

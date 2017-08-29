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
using System.Globalization;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Reserva.Controllers
{
    public class OperadorsController : Controller
    {
        private ReservaBDEntities db = new ReservaBDEntities();

        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Operadors
        public async Task<ActionResult> Index()
        {
            var operadores = db.Operadores.Include(o => o.Patente).Include(o => o.Almoxarifado);
            return View(await operadores.ToListAsync());
        }

        // GET: Operadors/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Operador operador = db.Operadores.Where(x => x.Id == id).FirstOrDefault();
            if (operador == null)
            {
                return HttpNotFound();
            }
            return View(operador);
        }

        // GET: Operadors/Create
        public ActionResult Create()
        {
            ViewBag.PatenteId = new SelectList(db.Patentes, "Id", "Descricao");
            ViewBag.AlmoxarifadoId = new SelectList(db.Almoxarifadoes, "Id", "Descricao");
            return View();
        }

        // POST: Operadors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nome,NomeGuerra,Matricula,PatenteId,Email,AutenticacaoID,ADM,AlmoxarifadoId,CautelaId")] Operador operador, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                RegisterViewModel oRegistro = new RegisterViewModel();
                oRegistro.Usuario = form["UserName"];
                oRegistro.Senha = form["Password"];
                oRegistro.SenhaConfirma = form["ConfirmarPassword"];

                var user = new ApplicationUser { UserName = oRegistro.Usuario, Email = operador.Email };
                var result = await UserManager.CreateAsync(user, oRegistro.Senha);

                if (result.Succeeded)
                {
                    var vUser = await UserManager.FindByNameAsync(oRegistro.Usuario);

                    operador.AutenticacaoID = vUser.Id;
                    db.Militars.Add(operador);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                AddErrors(result);
            }

            ViewBag.PatenteId = new SelectList(db.Patentes, "Id", "Descricao", operador.PatenteId);
            ViewBag.AlmoxarifadoId = new SelectList(db.Almoxarifadoes, "Id", "Descricao", operador.AlmoxarifadoId);
            return View(operador);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        // GET: Operadors/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Operador operador =  db.Operadores.Where(x=>x.Id==id).FirstOrDefault();
            if (operador == null)
            {
                return HttpNotFound();
            }
            ViewBag.PatenteId = new SelectList(db.Patentes, "Id", "Descricao", operador.PatenteId);
            ViewBag.AlmoxarifadoId = new SelectList(db.Almoxarifadoes, "Id", "Descricao", operador.AlmoxarifadoId);
            return View(operador);
        }

        // POST: Operadors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nome,NomeGuerra,Matricula,PatenteId,Email,AutenticacaoID,ADM,AlmoxarifadoId,CautelaId")] Operador operador, FormCollection form)
        {

            Operador oOperador = await db.Operadores.FindAsync(operador.Id);
            oOperador.Email = operador.Email;
            oOperador.AlmoxarifadoId = operador.AlmoxarifadoId;
            oOperador.ADM = operador.ADM;
            oOperador.Nome = operador.Nome;

            if (ModelState.IsValid)
            {
                if (form["Password"].Length > 0)
                {
                    RegisterViewModel oRegistro = new RegisterViewModel();
                    oRegistro.Senha = form["Password"];
                    oRegistro.SenhaConfirma = form["ConfirmarPassword"];

                    var vUser = await UserManager.FindByIdAsync(oOperador.AutenticacaoID);

                    var result = await UserManager.ResetPasswordAsync(vUser.Id, "alteracao", oRegistro.Senha);
                    if (!result.Succeeded)
                    {
                        AddErrors(result);
                        ViewBag.AlmoxarifadoId = new SelectList(db.Almoxarifadoes, "Id", "Descricao", operador.AlmoxarifadoId);
                        return View(operador);
                    }
                }

                db.Entry(operador).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PatenteId = new SelectList(db.Patentes, "Id", "Descricao", operador.PatenteId);
            ViewBag.AlmoxarifadoId = new SelectList(db.Almoxarifadoes, "Id", "Descricao", operador.AlmoxarifadoId);
            return View(operador);
        }

        // GET: Operadors/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Operador operador = db.Operadores.Where(x => x.Id == id).FirstOrDefault();
            if (operador == null)
            {
                return HttpNotFound();
            }
            return View(operador);
        }

        // POST: Operadors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Operador operador = db.Operadores.Where(x => x.Id == id).FirstOrDefault();
            db.Militars.Remove(operador);
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

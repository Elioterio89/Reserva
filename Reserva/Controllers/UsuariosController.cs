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
    public class UsuariosController : Controller
    {
        private ReservaBDEntities db = new ReservaBDEntities();

        // GET: Usuarios
        public async Task<ActionResult> Index()
        {
            var usuarios = db.Usuarios.Include(u => u.Patente);
            return View(await usuarios.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario =  db.Usuarios.Where(x=>x.Id==id).FirstOrDefault();
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            ViewBag.PatenteId = new SelectList(db.Patentes, "Id", "Descricao");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nome,NomeGuerra,Matricula,PatenteId,NomeDeGuerra")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Militars.Add(usuario);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.PatenteId = new SelectList(db.Patentes, "Id", "Descricao", usuario.PatenteId);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Where(x => x.Id == id).FirstOrDefault();
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.PatenteId = new SelectList(db.Patentes, "Id", "Descricao", usuario.PatenteId);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nome,NomeGuerra,Matricula,PatenteId")] Usuario usuario)
        {
            Usuario oUser =  db.Usuarios.Where(x=>x.Id==usuario.Id).FirstOrDefault();
            oUser.Nome = usuario.Nome;
            oUser.NomeGuerra = usuario.NomeGuerra;
            oUser.PatenteId = usuario.PatenteId;

            if (ModelState.IsValid)
            {
                db.Entry(oUser).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PatenteId = new SelectList(db.Patentes, "Id", "Descricao", oUser.PatenteId);
            return View(oUser);
        }

        // GET: Usuarios/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Where(x => x.Id == id).FirstOrDefault();
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuarios.Where(x => x.Id == id).FirstOrDefault();
            db.Militars.Remove(usuario);
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

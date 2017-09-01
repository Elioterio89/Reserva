using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Reserva.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Net;


namespace Reserva.Controllers
{
    public class HomeController : Controller
    {

        private ReservaBDEntities db = new ReservaBDEntities();

        public ActionResult Index()
        {
            IndexModel model = new IndexModel();

            int idReserva = OperadorViewModel.USUARIO.AlmoxarifadoId;

            model.Reserva = db.Almoxarifadoes.Where(x => x.Id == idReserva).FirstOrDefault().Descricao;

            model.AceEstoque = db.Acessorios.Where(x => x.AlmoxarifadoId == idReserva).Count().ToString();
            model.ArmEstoque = db.Armamentos.Where(x => x.AlmoxarifadoId == idReserva).Count().ToString();
            model.MunEstoque = db.Municoes.Where(x => x.AlmoxarifadoId == idReserva).Count().ToString();

            model.AceFora = db.Acessorios.Where(x => x.AlmoxarifadoId == idReserva && x.Disponivel==false).Count().ToString();
            model.ArmFora = db.Armamentos.Where(x => x.AlmoxarifadoId == idReserva && x.Disponivel == false).Count().ToString();
            model.MunFora = db.Municoes.Where(x => x.AlmoxarifadoId == idReserva && x.Disponivel == false).Count().ToString();

            model.AceLivre = db.Acessorios.Where(x => x.AlmoxarifadoId == idReserva && x.Disponivel == true).Count().ToString();
            model.ArmLivre = db.Armamentos.Where(x => x.AlmoxarifadoId == idReserva && x.Disponivel == true).Count().ToString();
            model.MunLivre = db.Municoes.Where(x => x.AlmoxarifadoId == idReserva && x.Disponivel == true).Count().ToString();
            

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
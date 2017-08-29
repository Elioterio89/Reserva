using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

using System.Configuration;
using System.Linq;
using System.Web;

namespace Reserva.Models
{
    public class OperadorViewModel
    {
        public static Operador USUARIO
        {
            get { return (Operador)HttpContext.Current.Session["OPERADOR"]; }
        }

    }


}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LS.Tareas.Api.Models
{
    public class Respuesta
    {
        public int Codigo { get; set; }
        public string Mensaje { get; set; }

        public Respuesta()
        {
            Codigo = 0;
            Mensaje = "OK";
        }

        public override string ToString()
        {
            string str = String.Format("{0}-{1}", Codigo, Mensaje);
            return str;
        }

        public void SetError(Exception exception)
        {
            Codigo = 9;
            Mensaje = String.Format("Detalles: {0}", exception.Message);
        }

        public void SetError(string mensajeError)
        {
            Codigo = 9;
            Mensaje = String.Format("Detalles: {0}", mensajeError);
        }

        public bool EsOK()
        {
            bool isOK = Codigo.Equals(0);
            return isOK;
        }

        public Respuesta GetRespuesta()
        {
            Respuesta respuesta = new Respuesta { Codigo = Codigo, Mensaje = Mensaje };
            return respuesta;
        }
    }

    public class Resultado<T> : Respuesta
    {
        public T Data { get; set; }
    }
}
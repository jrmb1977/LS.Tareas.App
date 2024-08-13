using LS.Tareas.Api.DataBase;
using LS.Tareas.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LS.Tareas.Api.Controllers
{
    public class ValuesController : ApiController
    {
        private TareaManager _manager;

        public ValuesController()
        {
            TareaPendiente tarea = new TareaPendiente();
            _manager = new TareaManager(tarea);
        }

        #region SELECT

        private HttpResponseMessage GetTareas()
        {
            bool SelectShort = true;
            Resultado<List<TareaPendiente>> resultado = _manager.Select(SelectShort);
            if (resultado.EsOK())
            {
                Resultado<List<TareasView>> respuesta = new Resultado<List<TareasView>> { Codigo = resultado.Codigo, Mensaje = resultado.Mensaje, Data = resultado.Data.Select(c => c.ToListView()).ToList() };
                var response = Request.CreateResponse(HttpStatusCode.OK, respuesta);
                return response;
            }
            else
            {
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError, resultado.GetRespuesta());
                return response;
            }
        }

        private HttpResponseMessage GetTarea(int id)
        {
            if (id <= 0)
            {
                var response = Request.CreateResponse(HttpStatusCode.BadRequest, new Respuesta { Codigo = 9, Mensaje = String.Format("Id NO Válido: {0}", id) });
                return response;
            }
            TareaPendiente tarea = new TareaPendiente(id);
            _manager = new TareaManager(tarea);
            Resultado<TareaPendiente> resultado = _manager.SelectWhere();
            if (resultado.EsOK())
            {
                TareaPendiente tareaPendiente = resultado.Data;
                if (tareaPendiente.ID.Equals(0))
                {
                    var response = Request.CreateResponse(HttpStatusCode.NotFound, new Respuesta { Codigo = 9, Mensaje = String.Format("Id NO Encontrado: {0}", id) });
                    return response;
                }
                else
                {
                    Resultado<TareaView> respuesta = new Resultado<TareaView> { Codigo = resultado.Codigo, Mensaje = resultado.Mensaje, Data = resultado.Data.ToDetailsView() };
                    var response = Request.CreateResponse(HttpStatusCode.OK, respuesta);
                    return response;
                }
            }
            else
            {
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError, resultado.GetRespuesta());
                return response;
            }
        }


        // GET api/values
        public HttpResponseMessage Get()
        {
            return GetTareas();
        }

        // GET api/values/5
        public HttpResponseMessage Get(int id)
        {
            return GetTarea(id);
        }

        #endregion

        #region INSERT

        private HttpResponseMessage Create([FromBody] TareaEditView view)
        {
            if (view == null)
            {
                var response = Request.CreateResponse(HttpStatusCode.BadRequest, new Respuesta { Codigo = 9, Mensaje = "No se encontró el Objeto Tarea en el Body" });
                return response;
            }
            else
            {
                bool EsInsert = true;
                Respuesta respuestaParams = view.EsValido(EsInsert);
                if (respuestaParams.EsOK())
                {
                    TareaPendiente tarea = view.ToTarea();
                    _manager = new TareaManager(tarea);
                    Respuesta respuesta = _manager.Insert();
                    bool EsOK = respuesta.EsOK();
                    var response = Request.CreateResponse(EsOK ? HttpStatusCode.OK : HttpStatusCode.InternalServerError, respuesta);
                    return response;
                }
                else
                {
                    var response = Request.CreateResponse(HttpStatusCode.BadRequest, respuestaParams);
                    return response;
                }
            }
        }

        // POST api/values
        public HttpResponseMessage Post([FromBody] TareaEditView value)
        {
            return Create(value);
        }

        #endregion

        #region UPDATE

        private HttpResponseMessage UpdateStatus(int id)
        {
            if (id <= 0)
            {
                var response = Request.CreateResponse(HttpStatusCode.BadRequest, new Respuesta { Codigo = 9, Mensaje = String.Format("Id NO Válido: {0}", id) });
                return response;
            }
            else
            {
                TareaPendiente tarea = new TareaPendiente(id);
                _manager = new TareaManager(tarea);
                Respuesta respuesta = _manager.UpdateStatus();
                bool EsOK = respuesta.EsOK();
                var response = Request.CreateResponse(EsOK ? HttpStatusCode.OK : HttpStatusCode.InternalServerError, respuesta);
                return response;
            }
        }

        private HttpResponseMessage Update([FromBody] TareaEditView view)
        {
            if (view == null)
            {
                var response = Request.CreateResponse(HttpStatusCode.BadRequest, new Respuesta { Codigo = 9, Mensaje = "No se encontró el Objeto Tarea en el Body" });
                return response;
            }
            else
            {
                bool EsInsert = false;
                Respuesta respuestaParams = view.EsValido(EsInsert);
                if (respuestaParams.EsOK())
                {
                    TareaPendiente tarea = view.ToTarea();
                    _manager = new TareaManager(tarea);
                    Respuesta respuesta = _manager.Update();
                    bool EsOK = respuesta.EsOK();
                    var response = Request.CreateResponse(EsOK ? HttpStatusCode.OK : HttpStatusCode.InternalServerError, respuesta);
                    return response;
                }
                else
                {
                    var response = Request.CreateResponse(HttpStatusCode.BadRequest, respuestaParams);
                    return response;
                }
            }
        }

        // PUT api/values/5
        public HttpResponseMessage Put(int id)
        {
            return UpdateStatus(id);
        }

        // PUT api/values
        public HttpResponseMessage Put([FromBody] TareaEditView value)
        {
            return Update(value);
        }

        #endregion

        #region DELETE

        private HttpResponseMessage Remove(int id)
        {
            if (id <= 0)
            {
                var response = Request.CreateResponse(HttpStatusCode.BadRequest, new Respuesta { Codigo = 9, Mensaje = String.Format("Id NO Válido: {0}", id) });
                return response;
            }
            else
            {
                TareaPendiente tarea = new TareaPendiente(id);
                _manager = new TareaManager(tarea);
                Respuesta respuesta = _manager.Delete();
                bool EsOK = respuesta.EsOK();
                var response = Request.CreateResponse(EsOK ? HttpStatusCode.OK : HttpStatusCode.InternalServerError, respuesta);
                return response;
            }
        }

        // DELETE api/values/5
        public HttpResponseMessage Delete(int id)
        {
            return Remove(id);
        }

        #endregion
    }
}

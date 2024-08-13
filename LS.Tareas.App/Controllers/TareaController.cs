using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using LS.Tareas.App.Models;
using LS.Tareas.App.Api;

namespace LS.Tareas.App.Controllers
{
    public class TareaController : Controller
    {
        private RequestAPI _request;

        public TareaController()
        {
            _request = new RequestAPI();
        }

        // GET: Tarea
        public ActionResult Index()
        {
            Resultado<List<TareasView>> respuesta = _request.GetTareas();
            return View(respuesta.Data);
        }

        private Resultado<TareaView> GetDetailsView(int id)
        {
            Resultado<TareaView> resultado = _request.GetTarea(id);
            return resultado;
        }

        // GET: Tarea/Details/5
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index");
            Resultado<TareaView> respuesta = GetDetailsView(id.Value);
            return View(respuesta.Data);
        }

        // GET: Tarea/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tarea/Create
        [System.Web.Mvc.HttpPost]
        public ActionResult Create([FromBody] TareaEditView view)
        {
            Respuesta respuestaEdit = _request.CreateTarea(view);
            if (respuestaEdit.EsOK())
                return RedirectToAction("Index");
            else
            {
                Resultado<TareaEditView> respuesta = new Resultado<TareaEditView> { Codigo = respuestaEdit.Codigo, Mensaje = respuestaEdit.Mensaje, Data = view };
                return View(respuesta.Data);
            }
        }

        // GET: Tarea/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index");
            Resultado<TareaView> resultado = GetDetailsView(id.Value);
            Resultado<TareaEditView> respuesta = new Resultado<TareaEditView> { Codigo = resultado.Codigo, Mensaje = resultado.Mensaje, Data = resultado.Data.ToEditView() };
            return View(respuesta.Data);
        }

        // GET: Tarea/UpdateStatus/5
        public ActionResult UpdateStatus(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index");
            Respuesta respuestaEdit = _request.UpdateStatusTarea(id.Value);
            if (respuestaEdit.EsOK())
                return RedirectToAction("Index");
            else
            {
                return RedirectToAction("Details", new { id = id });
            }
        }

        // POST: Tarea/Edit/5
        [System.Web.Mvc.HttpPost]
        public ActionResult Edit([FromBody] TareaEditView view)
        {
            Respuesta respuestaEdit = _request.UpdateTarea(view);
            if (respuestaEdit.EsOK())
                return RedirectToAction("Index");
            else
            {
                Resultado<TareaEditView> respuesta = new Resultado<TareaEditView> { Codigo = respuestaEdit.Codigo, Mensaje = respuestaEdit.Mensaje, Data = view };
                return View(respuesta.Data);
            }
        }

        // GET: Tarea/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index");
            Respuesta respuestaEdit = _request.DeleteTarea(id.Value);
            if (respuestaEdit.EsOK())
                return RedirectToAction("Index");
            else
            {
                return RedirectToAction("Details", new { id = id });
            }
        }
    }
}

using LS.Tareas.Api.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace LS.Tareas.Api.DataBase
{
    public class TareaManager : TableManager
    {
        public TareaManager(TareaPendiente tarea) : base(tarea.GetSQLManager())
        {

        }

        public Resultado<List<TareaPendiente>> Select(bool SelectShort)
        {
            bool UseProcedure = true;
            Resultado<List<TareaPendiente>> respuesta = new Resultado<List<TareaPendiente>>();
            try
            {
                _dataBaseManager.Open();
                DataTable dt = SelectShort ? _dataBaseManager.SelectShort(UseProcedure) : _dataBaseManager.Select(UseProcedure);
                if (dt.Rows.Count > 0)
                {
                    List<TareaPendiente> tareas = new List<TareaPendiente>();
                    foreach (DataRow row in dt.Rows)
                    {
                        TareaPendiente tarea = new TareaPendiente(row);
                        tareas.Add(tarea);
                    }
                    respuesta.Data = tareas;
                }
                else
                    respuesta.Data = new List<TareaPendiente>();
            }
            catch (Exception ex)
            {
                respuesta.SetError(ex);
            }
            _dataBaseManager.Close();
            return respuesta;
        }

        public Resultado<TareaPendiente> SelectWhere()
        {
            bool UseProcedure = true;
            Resultado<TareaPendiente> respuesta = new Resultado<TareaPendiente>();
            try
            {
                _dataBaseManager.Open();
                DataTable dt = _dataBaseManager.SelectWhere(UseProcedure);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    TareaPendiente tarea = new TareaPendiente(row);
                    respuesta.Data = tarea;
                }
                else
                    respuesta.Data = new TareaPendiente();
            }
            catch (Exception ex)
            {
                respuesta.SetError(ex);
            }
            _dataBaseManager.Close();
            return respuesta;
        }

        public Respuesta Insert()
        {
            bool UseProcedure = true;
            Respuesta respuesta = new Respuesta();
            try
            {
                _dataBaseManager.Open();
                int numberOfRowsAffected = _dataBaseManager.Insert(UseProcedure);
            }
            catch (Exception ex)
            {
                respuesta.SetError(ex);
            }
            _dataBaseManager.Close();
            return respuesta;
        }

        public Respuesta Update()
        {
            bool UseProcedure = true;
            Respuesta respuesta = new Respuesta();
            try
            {
                _dataBaseManager.Open();
                int numberOfRowsAffected = _dataBaseManager.UpdateWhere(UseProcedure);
            }
            catch (Exception ex)
            {
                respuesta.SetError(ex);
            }
            _dataBaseManager.Close();
            return respuesta;
        }

        public Respuesta UpdateStatus()
        {
            bool UseProcedure = true;
            Respuesta respuesta = new Respuesta();
            try
            {
                _dataBaseManager.Open();
                int numberOfRowsAffected = _dataBaseManager.UpdateStatusWhere(UseProcedure);
            }
            catch (Exception ex)
            {
                respuesta.SetError(ex);
            }
            _dataBaseManager.Close();
            return respuesta;
        }

        public Respuesta Delete()
        {
            bool UseProcedure = true;
            Respuesta respuesta = new Respuesta();
            try
            {
                _dataBaseManager.Open();
                int numberOfRowsAffected = _dataBaseManager.DeleteWhere(UseProcedure);
            }
            catch (Exception ex)
            {
                respuesta.SetError(ex);
            }
            _dataBaseManager.Close();
            return respuesta;
        }
    }
}
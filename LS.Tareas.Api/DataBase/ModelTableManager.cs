using CentrosMedicos.Data.Models.Neptuno;
using CentrosMedicos.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CentrosMedicos.Data.Models.Medcheck;
using System.Reflection;

namespace CentrosMedicos.Data.Database.NeptunoMedical
{
    public interface ISQLManager<T>
    {
        SQLManager GetSQLManager();
        T GetModel(DataRow row);
    }

    public class ModelTableManager<T> where T : ISQLManager<T>, new()
    {
        private DataBaseManager dataBaseManager;
        private T model;

        public ModelTableManager(T t)
        {
            if (t == null)
                model = new T();
            else
                model = t;
            SQLManager sqlManager = t.GetSQLManager();
            IDataBase dataBase = new SQLData();
            dataBaseManager = new DataBaseManager(dataBase, sqlManager);
        }

        public void Refresh(T t)
        {
            SQLManager sqlManager = t.GetSQLManager();
            dataBaseManager.Refresh(sqlManager);
        }

        #region Select private

        private Resultado<List<DataRow>> SelectRows(Func<bool, DataTable> method, bool UseProcedure = true)
        {
            Respuesta respuesta = new Respuesta();
            List<DataRow> model = new List<DataRow>();

            try
            {
                dataBaseManager.Open();
                DataTable dt = method(UseProcedure);
                if (dt.Rows.Count > 0)
                {
                    model = dt.AsEnumerable().ToList();
                }
            }
            catch (Exception ex)
            {
                respuesta.SetError(ex);
            }
            dataBaseManager.Close();
            Resultado<List<DataRow>> resultado = new Resultado<List<DataRow>>(respuesta, model);
            return resultado;
        }

        private DataTable SelectRowsChildrenWhere(bool UseProcedure = true)
        {
            DataTable dt = dataBaseManager.SelectChildrenWhere(UseProcedure);
            return dt;
        }

        private DataTable SelectRowWhere(bool UseProcedure = true)
        {
            DataTable dt = dataBaseManager.SelectWhere(UseProcedure);
            return dt;
        }

        private DataTable SelectRowsAll(bool UseProcedure = true)
        {
            DataTable dt = dataBaseManager.Select(UseProcedure);
            return dt;
        }

        private DataTable SelectRowsWhere(bool UseProcedure = true)
        {
            DataTable dt = dataBaseManager.SelectWhereTopLike(UseProcedure);
            return dt;
        }

        private DataTable SelectRowsWhereParent(bool UseProcedure = true)
        {
            DataTable dt = dataBaseManager.SelectWhereParent(UseProcedure);
            return dt;
        }

        #endregion

        #region Select

        public Resultado<List<U>> SelectChildrenWhere<U>(bool UseProcedure = true) where U : ISQLManager<U>, new()
        {
            U u = new U();
            Respuesta respuesta = new Respuesta();
            List<U> lista = new List<U>();

            Resultado<List<DataRow>> resultadoChildren = SelectRows(SelectRowsChildrenWhere, UseProcedure);
            respuesta = resultadoChildren.GetRespuesta();
            if (resultadoChildren.EsOK())
            {
                List<DataRow> rows = resultadoChildren.Data;
                lista = rows.Select(row => u.GetModel(row)).ToList();
            }

            Resultado<List<U>> resultado = new Resultado<List<U>>(respuesta, lista);
            return resultado;
        }

        public Resultado<List<T>> Select(bool UseProcedure = true)
        {
            Respuesta respuesta = new Respuesta();
            List<T> lista = new List<T>();
            Resultado<List<DataRow>> resultadoSelect = SelectRows(SelectRowsAll, UseProcedure);
            respuesta = resultadoSelect.GetRespuesta();
            if (resultadoSelect.EsOK())
            {
                List<DataRow> listaRows = resultadoSelect.Data;
                lista = listaRows.Select(row => model.GetModel(row)).ToList();
            }
            Resultado<List<T>> resultado = new Resultado<List<T>>(respuesta, lista);
            return resultado;
        }

        public Resultado<List<T>> SelectWhereTopLike(bool UseProcedure = true)
        {
            Respuesta respuesta = new Respuesta();
            List<T> lista = new List<T>();
            Resultado<List<DataRow>> resultadoSelect = SelectRows(SelectRowsWhere, UseProcedure);
            respuesta = resultadoSelect.GetRespuesta();
            if (resultadoSelect.EsOK())
            {
                List<DataRow> listaRows = resultadoSelect.Data;
                lista = listaRows.Select(row => model.GetModel(row)).ToList();
            }
            Resultado<List<T>> resultado = new Resultado<List<T>>(respuesta, lista);
            return resultado;
        }

        public Resultado<T> SelectWhere(bool UseProcedure = true)
        {
            Respuesta respuesta = new Respuesta();
            T t = new T();
            Resultado<List<DataRow>> resultadoSelect = SelectRows(SelectRowWhere, UseProcedure);
            respuesta = resultadoSelect.GetRespuesta();
            if (resultadoSelect.EsOK())
            {
                List<DataRow> listaRows = resultadoSelect.Data;
                if (listaRows.Count > 0)
                {
                    DataRow row = listaRows[0];
                    t = model.GetModel(row);
                }
            }
            Resultado<T> resultado = new Resultado<T>(respuesta, t);
            return resultado;
        }

        public Resultado<List<T>> SelectWhereParent(bool UseProcedure = true)
        {
            Respuesta respuesta = new Respuesta();
            List<T> lista = new List<T>();

            Resultado<List<DataRow>> resultadoChildren = SelectRows(SelectRowsWhereParent, UseProcedure);
            respuesta = resultadoChildren.GetRespuesta();
            if (resultadoChildren.EsOK())
            {
                List<DataRow> rows = resultadoChildren.Data;
                lista = rows.Select(row => model.GetModel(row)).ToList();
            }

            Resultado<List<T>> resultado = new Resultado<List<T>>(respuesta, lista);
            return resultado;
        }

        #endregion

        #region Insert

        public Resultado<int[]> InsertOtputs(bool UseProcedure = true)
        {
            Respuesta respuesta = new Respuesta();
            int[] outputs = new int[] { };
            try
            {
                dataBaseManager.Open();
                outputs = dataBaseManager.Insert(UseProcedure);
            }
            catch (Exception ex)
            {
                respuesta.SetError(ex);
            }
            dataBaseManager.Close();
            Resultado<int[]> resultado = new Resultado<int[]>(respuesta, outputs);
            return resultado;
        }

        public Respuesta Insert(bool UseProcedure = true)
        {
            Respuesta respuesta = new Respuesta();
            Resultado<int[]> resultadoInsert = InsertOtputs(UseProcedure);
            respuesta = resultadoInsert.GetRespuesta();
            return respuesta;
        }

        public Resultado<int> InsertIdentity(bool UseProcedure = true)
        {
            Respuesta respuesta = new Respuesta();
            int lastInsertedId = 0;
            Resultado<int[]> resultadoInsert = InsertOtputs(UseProcedure);
            respuesta = resultadoInsert.GetRespuesta();
            if (resultadoInsert.EsOK())
            {
                int[] outputs = resultadoInsert.Data;
                if (outputs.Length > 0)
                    lastInsertedId = outputs[0];
            }
            Resultado<int> resultado = new Resultado<int>(respuesta, lastInsertedId);
            return resultado;
        }

        #endregion

        #region Update

        public Respuesta Update(bool UseProcedure = true)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                dataBaseManager.Open();
                int numberOfRowsAffected = dataBaseManager.UpdateWhere(UseProcedure);
            }
            catch (Exception ex)
            {
                respuesta.SetError(ex);
            }
            dataBaseManager.Close();
            return respuesta;
        }

        #endregion
    }
}

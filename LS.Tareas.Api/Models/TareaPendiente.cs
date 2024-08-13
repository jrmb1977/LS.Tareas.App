using LS.Tareas.Api.DataBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace LS.Tareas.Api.Models
{
    public class TareaPendiente : Entidad
    {
        #region Constantes Stored Procedures Names

        public const string spSelect = "spSelectTareasPendientes";
        public const string spSelectRow = "spSelectTareaPendiente";
        public const string spInsert = "spInsertTareaPendiente";
        public const string spUpdate = "spUpdateTareaPendiente";
        public const string spUpdateStatus = "spUpdateStatusTareaPendiente";
        public const string spDelete = "spDeleteTareaPendiente";

        #endregion

        #region Constantes SQL Statements

        public const string sqlSelect = "SELECT [ID],[Titulo],[Descripcion],[FechaCreacion],[FechaVencimiento],[Completada],[FechaCompletada] FROM [dbo].[tblTareaPendiente] ";
        public const string sqlSelectShort = "SELECT [ID],[Titulo],[Completada] FROM [dbo].[tblTareaPendiente] ";
        public const string sqlInsert = "INSERT INTO [dbo].[tblTareaPendiente]([Titulo],[Descripcion],[FechaVencimiento]) ";
        public const string sqlValues = "VALUES(@Titulo,@Descripcion,@FechaVencimiento) ";
        public const string sqlUpdate = "UPDATE [dbo].[tblTareaPendiente] ";
        public const string sqlSet = "SET [Titulo]=@Titulo,[Descripcion]=@Descripcion,[FechaVencimiento]=@FechaVencimiento ";
        public const string sqlSetStatus = "SET [Completada]=1,[FechaCompletada]=GETDATE() ";
        public const string sqlDelete = "DELETE FROM [dbo].[tblTareaPendiente] ";
        public const string sqlFilter = "WHERE [ID] = @ID ";

        #endregion

        #region Parametros SQL Statements

        public Dictionary<string, object> paramsSelect
        {
            get
            {
                var sqlParams = new Dictionary<string, object>();
                sqlParams.Add("@ID", ID);
                return sqlParams;
            }
        }

        public Dictionary<string, object> paramsInsert
        {
            get
            {
                var sqlParams = new Dictionary<string, object>();
                sqlParams.Add("@Titulo", Titulo);
                sqlParams.Add("@Descripcion", Descripcion);
                sqlParams.Add("@FechaVencimiento", FechaVencimiento);
                return sqlParams;
            }
        }

        public Dictionary<string, object> paramsUpdate
        {
            get
            {
                var sqlParams = new Dictionary<string, object>();
                sqlParams.Add("@ID", ID);
                sqlParams.Add("@Titulo", Titulo);
                sqlParams.Add("@Descripcion", Descripcion);
                sqlParams.Add("@FechaVencimiento", FechaVencimiento);
                return sqlParams;
            }
        }

        #endregion

        #region Propiedades

        public readonly int ID = 0;//IDENTITY(1,1)
        public string Titulo { get; set; }//VARCHAR(50)
        public string Descripcion { get; set; }//VARCHAR(250)
        public DateTime FechaCreacion { get; set; }// GETDATE()
        public DateTime FechaVencimiento { get; set; }// MAYOR QUE FechaCreacion
        public bool Completada { get; set; }//BIT 0 (S/N)
        public DateTime FechaCompletada { get; set; }//NULL

        #endregion

        #region Constructores

        private void SetDefault()
        {
            DateTime dt = new DateTime(1900, 1, 1);
            Titulo = "";
            Descripcion = "";
            FechaCreacion = dt;
            FechaVencimiento = dt;
            Completada = false;
            FechaCompletada = dt;
        }

        public TareaPendiente()
        {
            ID = 0;
            SetDefault();
        }

        public TareaPendiente(int id)
        {
            ID = id;
            SetDefault();
        }

        public TareaPendiente(DataRow row) : base(row)
        {

        }

        #endregion

        #region Get Methods to retrieve View Classes and SQL Manager

        public SQLManager GetSQLManager()
        {
            SQLManager sqlManager = new SQLManager();

            #region SP Names

            sqlManager.SPSelect = spSelect;
            sqlManager.SPSelectRow = spSelectRow;
            sqlManager.SPInsert = spInsert;
            sqlManager.SPUpdate = spUpdate;
            sqlManager.SPUpdateStatus = spUpdateStatus;
            sqlManager.SPDelete = spDelete;

            #endregion

            #region Select All

            sqlManager.sqlSelect = sqlSelect;

            #endregion

            #region Select Row

            sqlManager.sqlSelectShort = sqlSelectShort;
            sqlManager.sqlFilter = sqlFilter;// Update, Delete, Select
            sqlManager.paramsSelect = paramsSelect;

            #endregion

            #region Insert

            sqlManager.sqlInsert = sqlInsert;
            sqlManager.sqlValues = sqlValues;
            sqlManager.paramsInsert = paramsInsert;

            #endregion

            #region Update

            sqlManager.sqlUpdate = sqlUpdate;
            sqlManager.sqlSet = sqlSet;
            sqlManager.paramsUpdate = paramsUpdate;

            #endregion

            #region Delete

            sqlManager.sqlDelete = sqlDelete;
            sqlManager.paramsDelete = paramsSelect;

            #endregion

            return sqlManager;
        }

        public TareasView ToListView()
        {
            TareasView view = new TareasView();
            view.ID = ID;
            view.Titulo = Titulo;
            view.Completada = Completada;
            return view;
        }

        public TareaView ToDetailsView()
        {
            TareaView view = new TareaView();
            view.ID = ID;
            view.Titulo = Titulo;
            view.Descripcion = Descripcion;
            view.FechaCreacion = FechaCreacion;
            view.FechaVencimiento = FechaVencimiento;
            view.Completada = Completada;
            view.FechaCompletada = FechaCompletada == null ? new DateTime(1900, 1, 1) : FechaCompletada;
            return view;
        }

        public TareaEditView ToEditView()
        {
            TareaEditView view = new TareaEditView();
            view.ID = ID;
            view.Titulo = Titulo;
            view.Descripcion = Descripcion;
            view.FechaVencimiento = FechaVencimiento;
            return view;
        }

        #endregion
    }

    public class TareasView
    {
        [DisplayName("ID")]
        public int ID { get; set; }//IDENTITY(1,1)

        [DisplayName("Título")]
        public string Titulo { get; set; }//VARCHAR(50)

        [DisplayName("Completada")]
        public bool Completada { get; set; }//BIT 0 (S/N)
    }

    public class TareaView
    {
        [DisplayName("ID")]
        public int ID { get; set; }//IDENTITY(1,1)

        [DisplayName("Título")]
        public string Titulo { get; set; }//VARCHAR(50)

        [DisplayName("Descripción")]
        public string Descripcion { get; set; }//VARCHAR(250)

        [DisplayName("Fecha Creación")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d-MMM-yyyy HH:mm:ss}")]
        public DateTime FechaCreacion { get; set; }

        [DisplayName("Fecha Vencimiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d-MMM-yyyy}")]
        public DateTime FechaVencimiento { get; set; }// MAYOR QUE FechaCreacion

        [DisplayName("Completada")]
        public bool Completada { get; set; }//BIT 0 (S/N)

        [DisplayName("Fecha Completada")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d-MMM-yyyy HH:mm:ss}")]
        public DateTime FechaCompletada { get; set; }//NULL

        public TareaEditView ToEditView()
        {
            TareaEditView view = new TareaEditView();
            view.ID = ID;
            view.Titulo = Titulo;
            view.Descripcion = Descripcion;
            view.FechaVencimiento = FechaVencimiento;
            return view;
        }
    }

    public class TareaEditView
    {
        public int ID { get; set; }//IDENTITY(1,1)

        [DisplayName("Título")]
        [Required, MaxLength(50)]
        public string Titulo { get; set; }//VARCHAR(50)

        [DisplayName("Descripción")]
        [Required, MaxLength(250)]
        public string Descripcion { get; set; }//VARCHAR(250)

        [DisplayName("Fecha Vencimiento")]
        [Required, DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaVencimiento { get; set; }// MAYOR QUE FechaCreacion

        public Respuesta EsValido(bool EsInsert)
        {
            bool IdOK = EsInsert ? true : ID > 0;
            bool TituloOK = !string.IsNullOrEmpty(Titulo);
            bool DescripcionOK = !string.IsNullOrEmpty(Descripcion);
            bool FechaVencimientoOK = FechaVencimiento != null;
            bool EsOK = IdOK && TituloOK && DescripcionOK && FechaVencimientoOK;
            if (EsOK)
            {
                return new Respuesta();
            }

            StringBuilder builder = new StringBuilder();
            if (!IdOK)
            {
                builder.AppendLine(String.Format("ID NO es válido: {0}", ID));
            }
            if (!TituloOK)
            {
                builder.AppendLine("Falta el Campo Título");
            }
            if (!DescripcionOK)
            {
                builder.AppendLine("Falta el Campo Descripción");
            }
            if (!FechaVencimientoOK)
            {
                builder.AppendLine("Falta el Campo Fecha de Vencimiento");
            }
            string mensaje = builder.ToString();
            Respuesta respuesta = new Respuesta { Codigo = 9, Mensaje = mensaje };
            return respuesta;

        }

        public TareaPendiente ToTarea()
        {
            TareaPendiente tarea = new TareaPendiente(ID);
            tarea.Titulo = Titulo;
            tarea.Descripcion = Descripcion;
            tarea.FechaVencimiento = FechaVencimiento;
            return tarea;
        }
    }
}
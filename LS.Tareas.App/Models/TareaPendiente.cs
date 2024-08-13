using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;

namespace LS.Tareas.App.Models
{
    public class TareaPendiente
    {
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

        //public TareaPendiente ToTarea()
        //{
        //    TareaPendiente tarea = new TareaPendiente(ID);
        //    tarea.Titulo = Titulo;
        //    tarea.Descripcion = Descripcion;
        //    tarea.FechaVencimiento = FechaVencimiento;
        //    return tarea;
        //}
    }
}
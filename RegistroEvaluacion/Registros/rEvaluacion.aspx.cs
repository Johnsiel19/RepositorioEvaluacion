using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Entidades;

namespace RegistroEvaluacion.Registros
{
    public partial class rEvaluacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {



            if (!Page.IsPostBack)
            {
                CargarCategorias();
                CargarEstudiantes();
                EvaluacionIdTextBox.Text = "0";
                ValorTextBox.Text = "0";
                PuntosPeridosTextBox.Text = "0";
                LogradosTextBox.Text = "0";
                ViewState["Evaluacion"] = new Evaluaciones();
                // ViewState["Detalle"] = new Pagos().Detalle;
                BindGrid();

            }

            int ID = Utilitarios.Utils.ToInt(Request.QueryString["id"]);

            if (ID > 0)
            {
                RepositorioBase<Evaluaciones> repositorio = new RepositorioBase<Evaluaciones>();
                var us = repositorio.Buscar(ID);

                if (us == null)
                {
                    Utilitarios.Utils.ShowToastr(this.Page, "Registro No encontrado", "Error", "error");
                }
                else
                {
                    LlenaCampo(us);
                }
            }

        }


        private void CargarCategorias()
        {
            if (!Page.IsPostBack)
            {
                RepositorioBase<Categorias> db = new RepositorioBase<Categorias>();


                CategoriaDropDownList.DataSource = db.GetList(t => true);
                CategoriaDropDownList.DataValueField = "CategoriaId";
                CategoriaDropDownList.DataTextField = "Descripcion";
                CategoriaDropDownList.DataBind();

                ViewState["Evaluacion"] = new Evaluaciones();
            }

        }

        private void CargarEstudiantes()
        {
            if (!Page.IsPostBack)
            {
                RepositorioBase<Estudiantes> db = new RepositorioBase<Estudiantes>();


                EstudianteDropDownList.DataSource = db.GetList(t => true);
                EstudianteDropDownList.DataValueField = "EstudianteId";
                EstudianteDropDownList.DataTextField = "Nombres";
                EstudianteDropDownList.DataBind();

                ViewState["Evaluacion"] = new Evaluaciones();
            }



        }
        protected void BindGrid()
        {
            if (ViewState["Evaluacion"] != null)
            {
                Grid.DataSource = ((Evaluaciones)ViewState["Evaluacion"]).Detalles;
                Grid.DataBind();


            }
        }


        private void Limpiar()
        {
            EvaluacionIdTextBox.Text = "0";


            PuntosPeridosTextBox.Text = "0";
            LogradosTextBox.Text = "0";
            Grid.DataSource = null;
            Grid.DataBind();


        }

        public Evaluaciones LlenaClase()
        {
            Evaluaciones evaliacion = (Evaluaciones)ViewState["Evaluacion"];
            evaliacion.EvaluacionId = Convert.ToInt32(EvaluacionIdTextBox.Text);
            evaliacion.TotalPerdido = Convert.ToDecimal(PuntosPeridosTextBox.Text);
            evaliacion.EstudianteId = Convert.ToInt32(EstudianteDropDownList.SelectedValue);
            evaliacion.Fecha = DateTime.Now;
 


            return evaliacion;
        }

        private void LlenaCampo(Evaluaciones evaluaciones)
        {
            ((Evaluaciones)ViewState["Evaluacion"]).Detalles = evaluaciones.Detalles;
            EstudianteDropDownList.Text = evaluaciones.EstudianteId.ToString();
            EvaluacionIdTextBox.Text = evaluaciones.EvaluacionId.ToString();
            PuntosPeridosTextBox.Text = evaluaciones.TotalPerdido.ToString();
            fechaTextBox.Text = evaluaciones.Fecha.ToString("yyyy-MM-dd");

            this.BindGrid();



        }

        private bool ExisteEnLaBaseDeDatos()
        {
            RepositorioBase<Evaluaciones> db = new RepositorioBase<Evaluaciones>();
            Evaluaciones evaluacion = db.Buscar(Convert.ToInt32(EvaluacionIdTextBox.Text));
            return (evaluacion != null);

        }


        protected void AgregarButton_Click(object sender, EventArgs e)
        {
            Evaluaciones evaluacion = new Evaluaciones();

            decimal peridos = ( Convert.ToDecimal(ValorTextBox.Text) - Convert.ToDecimal(LogradosTextBox.Text));

            evaluacion = (Evaluaciones)ViewState["Evaluacion"];

            //pago.Detalle.Add(new Entidades.PagosDetalle(0,0, Convert.ToDateTime(FechaTextBox.Text),Convert.ToDecimal( MontoAnalisisTextBox.Text),0,Convert.ToDecimal( MontoAPagarTextBox.Text)));
            evaluacion.Detalles.Add(new Entidades.EvaluacionesDetalle(0, 0,
                Convert.ToInt32(CategoriaDropDownList.SelectedValue),
               Convert.ToDecimal( ValorTextBox.Text), Convert.ToDecimal(LogradosTextBox.Text), peridos));

            ViewState["Evaluacion"] = evaluacion;

            this.BindGrid();

            Grid.Columns[1].Visible = false;
     

            decimal Calculador = 0;
            foreach (var item in evaluacion.Detalles)
            {
                Calculador = Calculador + item.Perdidos;
            }
            PuntosPeridosTextBox.Text = Calculador.ToString();

            ValorTextBox.Text = "0";
            LogradosTextBox.Text = "0";
            

        }

        protected void NuevoButton_Click(object sender, EventArgs e)
        {
            Limpiar();

        }

        protected void GuardarButton_Click(object sender, EventArgs e)
        {
            Evaluaciones evaluacion;
            bool paso = false;

            evaluacion = LlenaClase();


            if (EvaluacionIdTextBox.Text == 0.ToString())
            {
                paso = EvaluacionBLL.Guardar(evaluacion);
            }
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    Utilitarios.Utils.ShowToastr(this.Page, "Se ha podido modificar", "Error", "error");
                    return;
                }
                paso = EvaluacionBLL.Modificar(evaluacion);
            }

            if (paso)
                Utilitarios.Utils.ShowToastr(this.Page, " Se ha Guardado", "Exito", "success");
            else
                Utilitarios.Utils.ShowToastr(this.Page, "Se profujo un error al guardar", "Error", "error");
            Limpiar();


        }

        protected void EliminarButton_Click(object sender, EventArgs e)
        {
            if (Utilitarios.Utils.ToInt(EvaluacionIdTextBox.Text) > 0)
            {
                int id = Convert.ToInt32(EvaluacionIdTextBox.Text);

                if (EvaluacionBLL.Eliminar(id))
                {

                    Utilitarios.Utils.ShowToastr(this.Page, "Eliminado con exito!!", "Eliminado", "info");
                }
                else
                    Utilitarios.Utils.ShowToastr(this.Page, "Fallo al Eliminar :(", "Error", "error");
                Limpiar();
            }
            else
            {
                Utilitarios.Utils.ShowToastr(this.Page, "No se pudo eliminar, Anlaisis no existe", "error", "error");
            }


        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            int id;

            RepositorioBase<Evaluaciones> db = new RepositorioBase<Evaluaciones>();
            Evaluaciones evaluacio = new Evaluaciones();
            int.TryParse(EvaluacionIdTextBox.Text, out id);
            Limpiar();


            evaluacio = db.Buscar(id);

            if (evaluacio != null)
            {

                LlenaCampo(evaluacio);

            }
            else
            {
                Utilitarios.Utils.ShowToastr(this.Page, "No se encontro esa evaluacion", "Error", "error");

            }

        }

   


        protected void Grid_RowDeleting1(object sender, GridViewDeleteEventArgs e)
        {

            Evaluaciones evaluaciones = new Evaluaciones();
            evaluaciones = (Evaluaciones)ViewState["Evaluacion"];
            ViewState["Evaluaciones"] = evaluaciones.Detalles;

            int Fila = e.RowIndex;

            evaluaciones.Detalles.RemoveAt(Fila);
            this.BindGrid();



            decimal Calculador = 0;

            foreach (var item in evaluaciones.Detalles)
            {
                Calculador = Calculador + item.Perdidos;
            }
            PuntosPeridosTextBox.Text = Calculador.ToString();

            ValorTextBox.Text = "0";
            LogradosTextBox.Text = "0";
        }
    }
}
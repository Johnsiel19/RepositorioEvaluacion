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
    public partial class rEstudiantes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {


                int ID = Utilitarios.Utils.ToInt(Request.QueryString["id"]);

                if (ID > 0)
                {
                    RepositorioBase<Estudiantes> repositorio = new RepositorioBase<Estudiantes>();
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

        }

        private void Limpiar()
        {
            EstudianteIdTextBox.Text = string.Empty;
            NombresTextBox.Text = string.Empty;
            PuntosPeridosTextBox.Text = 0.ToString();
            fechaTextBox.Text = DateTime.Now.ToString("dd-MM-yyyy");

        }

        public Estudiantes LlenaClase()
        {
            Estudiantes estudiante = new Estudiantes();
            estudiante.EstudianteId = Convert.ToInt32(EstudianteIdTextBox.Text);
            estudiante.Nombres = NombresTextBox.Text;
            estudiante.Fecha = Convert.ToDateTime(fechaTextBox.Text);
            estudiante.PuntosPerdidos = Convert.ToDecimal(PuntosPeridosTextBox.Text);

            return estudiante;
        }


        private void LlenaCampo(Estudiantes estudiante)
        {
            EstudianteIdTextBox.Text = estudiante.EstudianteId.ToString();
            NombresTextBox.Text = estudiante.Nombres;
            PuntosPeridosTextBox.Text = estudiante.PuntosPerdidos.ToString();
            fechaTextBox.Text = estudiante.Fecha.ToString("yyyy-MM-dd");
        }

        private bool ExisteEnLaBaseDeDatos()
        {
            RepositorioBase<Estudiantes> db = new RepositorioBase<Estudiantes>();
            Estudiantes estudiante = db.Buscar(Convert.ToInt32(EstudianteIdTextBox.Text));
            return (estudiante != null);

        }



        private bool ValidarCampos()
        {
            bool paso = true;
            if (NombresTextBox.Text == string.Empty)
            {
                Utilitarios.Utils.ShowToastr(this.Page, "El campo descripcion no puede estar vacio", "Error", "error");

                NombresTextBox.Focus();
                paso = false;
            }


            return paso;
        }






        protected void BuscarButton_Click1(object sender, EventArgs e)
        {
            int id;

            RepositorioBase<Estudiantes> db = new RepositorioBase<Estudiantes>();
            Estudiantes estudiante = new Estudiantes();
            int.TryParse(EstudianteIdTextBox.Text, out id);
            Limpiar();

            estudiante = db.Buscar(id);

            if (estudiante != null)
            {

                LlenaCampo(estudiante);

            }
            else
            {
                Utilitarios.Utils.ShowToastr(this.Page, "No se encontro ess Estudiante", "Error", "error");

            }

        }

        protected void NuevoButton_Click(object sender, EventArgs e)
        {
            Limpiar();

        }

        protected void GuardarButton_Click1(object sender, EventArgs e)
        {
            RepositorioBase<Estudiantes> db = new RepositorioBase<Estudiantes>();
            Estudiantes estudiante;
            bool paso = false;


            if (!ValidarCampos())
                return;

            estudiante = LlenaClase();


            if (EstudianteIdTextBox.Text == Convert.ToString(0))
            {
                paso = db.Guardar(estudiante);
            }
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    Utilitarios.Utils.ShowToastr(this.Page, "Este registro no existe", "Error", "error");
                    return;
                }
                paso = db.Modificar(estudiante);
            }

            if (paso)
                Utilitarios.Utils.ShowToastr(this.Page, "Ha guardado Correctamente", "Exito", "success");
            else
                Utilitarios.Utils.ShowToastr(this.Page, "Se profujo un error al guardar", "Error", "error");
            Limpiar();

        }

        protected void EliminarButton_Click1(object sender, EventArgs e)
        {
            if (Utilitarios.Utils.ToInt(EstudianteIdTextBox.Text) > 0)
            {
                int id = Convert.ToInt32(EstudianteIdTextBox.Text);
                RepositorioBase<Estudiantes> repositorio = new RepositorioBase<Estudiantes>();
                if (repositorio.Eliminar(id))
                {

                    Utilitarios.Utils.ShowToastr(this.Page, "Eliminado con exito!!", "Eliminado", "info");
                }
                else
                    Utilitarios.Utils.ShowToastr(this.Page, "Fallo al Eliminar :(", "Error", "error");
                Limpiar();
            }
            else
            {
                Utilitarios.Utils.ShowToastr(this.Page, "No se pudo eliminar un estudiante que no existe", "error", "error");

            }
        }
    }
}
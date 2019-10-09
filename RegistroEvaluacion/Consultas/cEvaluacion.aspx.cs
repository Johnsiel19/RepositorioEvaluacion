using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using BLL;
using System.Linq.Expressions;

namespace RegistroEvaluacion.Consultas
{
    public partial class cEvaluacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DesdeFecha.Text = DateTime.Today.ToString("yyyy-MM-dd");
                HastaFecha.Text = DateTime.Today.ToString("yyyy-MM-dd");
            }

        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            Expression<Func<Evaluaciones, bool>> filtros = x => true;
            RepositorioBase<Evaluaciones> repositorio = new RepositorioBase<Evaluaciones>();

            DateTime Desde = Utilitarios.Utils.ToDateTime(DesdeFecha.Text);
            DateTime Hasta = Utilitarios.Utils.ToDateTime(HastaFecha.Text);

            int id;
            id = Utilitarios.Utils.ToInt(CriterioTextBox.Text);

            if (CheckBoxFecha.Checked == true)
            {
                switch (FiltroDropDown.SelectedIndex)
                {
                    case 0:
                        break;
                    case 1:
                        filtros = c => c.EvaluacionId == id && c.Fecha >= Desde && c.Fecha <= Hasta;
                        break;
                    case 2:
                        filtros = c => c.EstudianteId ==id && c.Fecha >= Desde && c.Fecha <= Hasta;
                        break;
                    case 3:
                        filtros = c => c.TotalPerdido== id && c.Fecha >= Desde && c.Fecha <= Hasta;
                        break;
                }
            }
            else
            {
                switch (FiltroDropDown.SelectedIndex)
                {
                    case 0:
                        break;
                    case 1:
                        filtros = c => c.EvaluacionId == id;
                        break;
                    case 2:
                        filtros = c => c.EstudianteId == id;
                        break;
                    case 3:
                        filtros = c => c.TotalPerdido == id;
                        break;

                }
            }
            Grid.DataSource = repositorio.GetList(filtros);
            Grid.DataBind();


        }

      
    }
}
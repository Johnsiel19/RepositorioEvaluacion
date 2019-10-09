using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Entidades;

namespace RegistroEvaluacion.Consultas
{
    public partial class cCategorias : System.Web.UI.Page
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
            Expression<Func<Categorias, bool>> filtros = x => true;
            RepositorioBase<Categorias> repositorio = new RepositorioBase<Categorias>();

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
                        filtros = c => c.CategoriaId == id && c.Fecha >= Desde && c.Fecha <= Hasta;
                        break;
                    case 2:
                        filtros = c => c.Descripcion.Contains(CriterioTextBox.Text) && c.Fecha >= Desde && c.Fecha <= Hasta;
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
                        filtros = c => c.CategoriaId == id;
                        break;
                    case 2:
                        filtros = c => c.Descripcion.Contains(CriterioTextBox.Text);
                        break;
                }
            }
            Grid.DataSource = repositorio.GetList(filtros);
            Grid.DataBind();
             
        }
    }
}
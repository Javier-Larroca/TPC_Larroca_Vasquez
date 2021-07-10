using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace TPC_Larroca_Vasquez
{
    public partial class BajaDeMedico : System.Web.UI.Page
    {
        private MedicoNegocio negocio = new MedicoNegocio();
        public List<Medico> listaDeMedicos;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Request.QueryString["Id"] != null)
                {

                    int idMedicoAEliminar = int.Parse(Request.QueryString["Id"]);
                    darDeBajaMedico(idMedicoAEliminar);
                }

                listaDeMedicos = negocio.listarMedicos();
                Session.Add("ListaDeMedicos", listaDeMedicos);
            }
            catch
            {
                Response.Redirect("Inicio");
            }

        }

        protected void Filtro_TextChanged(object sender, EventArgs e)
        {
            List<Medico> listaFiltrada = new List<Medico>();

            try
            {
                //Valido si ingreso un numero para buscar Matricula. En linea 49 y 55 arrojo una excepción para detener
                //la ejecución del código
                bool esNumero = Filtro.Text.All(char.IsDigit);
                if (Filtro.Text != "" && opcionDeBusqueda.SelectedValue != "Todos")
                {
                    if (!esNumero && opcionDeBusqueda.SelectedValue == "Matricula") throw new Exception("No es numero");
                    listaFiltrada = listaDeMedicos.FindAll(
                        Busqueda => Busqueda.Nombre.ToUpper().Contains(Filtro.Text.ToUpper()) && opcionDeBusqueda.SelectedValue == "Nombre"
                        || Busqueda.Apellido.ToUpper().Contains(Filtro.Text.ToUpper()) && opcionDeBusqueda.SelectedValue == "Apellido"
                        || Busqueda.Matricula >= int.Parse(!esNumero ? "0" : Filtro.Text) && opcionDeBusqueda.SelectedValue == "Matricula");

                    if (listaFiltrada.Count == 0) throw new Exception("Sin resultados");

                    listaDeMedicos = listaFiltrada;
                    Session.Remove("ListaDeMedicos");
                    Session.Add("ListaDeMedicos", listaDeMedicos);
                }
            }
            catch (Exception ex)
            {
                //Evaluo excepciones en caso de ser arrojadas y muestro o no los warnings correspondientes
                if (ex.Message.ToUpper() == "NO ES NUMERO") Warning.Visible = true;
                if (ex.Message.ToUpper() == "SIN RESULTADOS") SinResultados.Visible = true;
            }
        }

        private void darDeBajaMedico(int id)
        {
            if (negocio.bajaDeMedico(id)) SuccessBajaMedico.Visible = true;
        }
    }
}
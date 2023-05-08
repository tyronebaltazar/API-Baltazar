using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Categorias()
        {
            List<Categoria> listado = ListarCategorias();
            return View();
        }


        #region Categorías

        string cadenaConexion = ConfigurationManager.ConnectionStrings["Conexion"].ToString();

        private List<Categoria> ListarCategorias()
        {
            List<Categoria> listaCategorias = new List<Categoria>();

            using (SqlConnection sqlConnection = new SqlConnection(cadenaConexion))
            {
                // Abrir la conexion
                sqlConnection.Open();

                // Le indico el nombre del store y le paso la cadena de conexion
                SqlCommand command = new SqlCommand("SP_CATEGORIA_LEER", sqlConnection);

                // Le indico que va a ejecutar un store
                command.CommandType = CommandType.StoredProcedure;

                // ejecutamos el store
                SqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    Categoria categoria = new Categoria();
                    categoria.C_categoria = dr["C_CATEGORIA"].ToString();
                    categoria.Nombre = dr["NOMBRE"].ToString();
                    categoria.Estado = dr["ESTADO"].ToString();
                    categoria.Ind_estado = dr["IND_ESTADO"].ToString();

                    listaCategorias.Add(categoria);
                }

            }

            return listaCategorias;
        }


        #endregion

    }
}
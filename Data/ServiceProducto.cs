using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Entity;
using Entity.Dto.Request.ProductoRequest;
using Entity.Dto.Response.ProductoResponse;

namespace Data
{
    public class ServiceProducto
    {
        string CadenaConexion = ConfigurationManager.ConnectionStrings["Conexion"].ToString();

        public ProductoResponse ConsultarProducto()
        {
            ProductoResponse response = new ProductoResponse();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(CadenaConexion))
                {
                    // Abrimos bd
                    sqlConnection.Open();

                    // Operaciones
                    SqlCommand cmd = new SqlCommand("sp_producto_leer", sqlConnection);

                    // Indicamos al sqlcommand que ejecutaré un store procedure
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Ejecuto el store
                    SqlDataReader dr = cmd.ExecuteReader();

                    // Validamos y leemos filas
                    if (dr.HasRows)
                    {
                        List<Producto> Lsproducto = new List<Producto>();
                        while (dr.Read())
                        {
                            Producto Producto = new Producto();
                            Producto.C_PRODUCTO = dr["C_PRODUCTO"].ToString();
                            Producto.C_CATEGORIA = dr["C_CATEGORIA"].ToString();
                            Producto.CATEGORIA = dr["CATEGORIA"].ToString();
                            Producto.NOMBRE_PRODUCTO = dr["NOMBRE_PRODUCTO"].ToString();
                            Producto.TIPO_UNIDAD = dr["TIPO_UNIDAD"].ToString();
                            Producto.C_UNIDAD_PRIN = dr["C_UNIDAD_PRIN"].ToString();
                            Producto.UNIDAD_PRIN = dr["UNIDAD_PRIN"].ToString();
                            Producto.C_UNIDAD_AUX = dr["C_UNIDAD_AUX"].ToString();
                            Producto.UNIDAD_AUX = dr["UNIDAD_AUX"].ToString();
                            Producto.FACTOR = Convert.ToDecimal(dr["FACTOR"]);
                            Producto.ESTADO = dr["ESTADO"].ToString();
                            Producto.IND_ESTADO = dr["IND_ESTADO"].ToString();
                            Lsproducto.Add(Producto);
                        }

                        response.CodigoRespuesta = Responses.SUCCESS_CODE;
                        response.NombreRespuesta = Responses.SUCCESS_MESSAGE;
                        response.Producto = Lsproducto;
                    }
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.CodigoRespuesta = Responses.ERROR_CODE;
                response.NombreRespuesta = string.Concat(Responses.ERROR_MESSAGE, ": ", ex.Message);
                return response;
            }
        }
        public ProductoResponse CrearProducto(ProductoCrearRequest request)
        {
            ProductoResponse response = new ProductoResponse();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(CadenaConexion))
                {
                    // Abrimos bd
                    sqlConnection.Open();

                    // Operaciones
                    SqlCommand cmd = new SqlCommand("sp_producto_crear", sqlConnection);

                    // Pasar parametros al store
                    cmd.Parameters.AddWithValue("@C_CATEGORIA", request.C_CATEGORIA);
                    cmd.Parameters.AddWithValue("@NOMBRE_PRODUCTO", request.NOMBRE_PRODUCTO);
                    cmd.Parameters.AddWithValue("@TIPO_UNIDAD", request.TIPO_UNIDAD);
                    cmd.Parameters.AddWithValue("@C_UNIDAD_PRIN", request.C_UNIDAD_PRIN);
                    cmd.Parameters.AddWithValue("@C_UNIDAD_AUX", request.C_UNIDAD_AUX);
                    cmd.Parameters.AddWithValue("@FACTOR", request.FACTOR);
                    cmd.Parameters.AddWithValue("@IND_ESTADO", request.IND_ESTADO);


                    // Indicamos al sqlcommand que ejecutaré un store procedure
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Ejecuto el store
                    SqlDataReader dr = cmd.ExecuteReader();

                    // Validamos y leemos filas
                    if (dr.HasRows)
                    {
                        List<Producto> Lsproducto = new List<Producto>();
                        if (dr.Read())
                        {
                            Producto Producto = new Producto();
                            Producto.C_PRODUCTO = dr["C_PRODUCTO"].ToString();
                            Producto.C_CATEGORIA = dr["C_CATEGORIA"].ToString();
                            Producto.CATEGORIA = dr["CATEGORIA"].ToString();
                            Producto.NOMBRE_PRODUCTO = dr["NOMBRE_PRODUCTO"].ToString();
                            Producto.TIPO_UNIDAD = dr["TIPO_UNIDAD"].ToString();
                            Producto.C_UNIDAD_PRIN = dr["C_UNIDAD_PRIN"].ToString();
                            Producto.UNIDAD_PRIN = dr["UNIDAD_PRIN"].ToString();
                            Producto.C_UNIDAD_AUX = dr["C_UNIDAD_AUX"].ToString();
                            Producto.UNIDAD_AUX = dr["UNIDAD_AUX"].ToString();
                            Producto.FACTOR = Convert.ToDecimal(dr["FACTOR"]);
                            Producto.ESTADO = dr["ESTADO"].ToString();
                            Producto.IND_ESTADO = dr["IND_ESTADO"].ToString();

                            Lsproducto.Add(Producto);
                        }
                        response.CodigoRespuesta = Responses.SUCCESS_CODE;
                        response.NombreRespuesta = Responses.SUCCESS_MESSAGE;
                        response.Producto = Lsproducto;
                    }
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.CodigoRespuesta = Responses.ERROR_CODE;
                response.NombreRespuesta = string.Concat(Responses.ERROR_MESSAGE, ": ", ex.Message);
                return response;
            }
        }
        public ProductoResponse ActualizarProducto(ProductoActualizaRequest request)
        {
            ProductoResponse response = new ProductoResponse();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(CadenaConexion))
                {
                    // Abrimos bd
                    sqlConnection.Open();

                    // Operaciones
                    SqlCommand cmd = new SqlCommand("SP_producto_actualizar", sqlConnection);

                    // Pasar parametros al store
                    cmd.Parameters.AddWithValue("@C_PRODUCTO", request.C_PRODUCTO);
                    cmd.Parameters.AddWithValue("@C_CATEGORIA", request.C_CATEGORIA);
                    cmd.Parameters.AddWithValue("@NOMBRE_PRODUCTO", request.NOMBRE_PRODUCTO);
                    cmd.Parameters.AddWithValue("@TIPO_UNIDAD", request.TIPO_UNIDAD);
                    cmd.Parameters.AddWithValue("@C_UNIDAD_PRIN", request.C_UNIDAD_PRIN);
                    cmd.Parameters.AddWithValue("@C_UNIDAD_AUX", request.C_UNIDAD_AUX);
                    cmd.Parameters.AddWithValue("@FACTOR", request.FACTOR);
                    cmd.Parameters.AddWithValue("@IND_ESTADO", request.IND_ESTADO);
                    // Indicamos al sqlcommand que ejecutaré un store procedure
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Ejecuto el store
                    SqlDataReader dr = cmd.ExecuteReader();

                    // Validamos y leemos filas
                    if (dr.HasRows)
                    {
                        List<Producto> Lsproducto = new List<Producto>();
                        if (dr.Read())
                        {
                            Producto Producto = new Producto();
                            Producto.C_PRODUCTO = dr["C_PRODUCTO"].ToString();
                            Producto.C_CATEGORIA = dr["C_CATEGORIA"].ToString();
                            Producto.CATEGORIA = dr["CATEGORIA"].ToString();
                            Producto.NOMBRE_PRODUCTO = dr["NOMBRE_PRODUCTO"].ToString();
                            Producto.TIPO_UNIDAD = dr["TIPO_UNIDAD"].ToString();
                            Producto.C_UNIDAD_PRIN = dr["C_UNIDAD_PRIN"].ToString();
                            Producto.UNIDAD_PRIN = dr["UNIDAD_PRIN"].ToString();
                            Producto.C_UNIDAD_AUX = dr["C_UNIDAD_AUX"].ToString();
                            Producto.UNIDAD_AUX = dr["UNIDAD_AUX"].ToString();
                            Producto.FACTOR = Convert.ToDecimal(dr["FACTOR"]);
                            Producto.ESTADO = dr["ESTADO"].ToString();
                            Producto.IND_ESTADO = dr["IND_ESTADO"].ToString();
                            Lsproducto.Add(Producto);
                        }
                        response.CodigoRespuesta = Responses.SUCCESS_CODE;
                        response.NombreRespuesta = Responses.SUCCESS_MESSAGE;
                        response.Producto = Lsproducto;
                    }

                    return response;
                }
            }
            catch (Exception ex)
            {
                response.CodigoRespuesta = Responses.ERROR_CODE;
                response.NombreRespuesta = string.Concat(Responses.ERROR_MESSAGE, ": ", ex.Message);
                return response;
            }
        }
        public ProductoResponse EliminarProducto(string id)
        {
            ProductoResponse response = new ProductoResponse();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(CadenaConexion))
                {
                    // Abrimos bd
                    sqlConnection.Open();

                    // Operaciones
                    SqlCommand cmd = new SqlCommand("sp_producto_eliminar", sqlConnection);

                    // Pasar parametros al store
                    cmd.Parameters.AddWithValue("@C_PRODUCTO", id);

                    // Indicamos al sqlcommand que ejecutaré un store procedure
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Ejecuto el store
                    SqlDataReader dr = cmd.ExecuteReader();

                    // Validamos y leemos filas
                    if (dr.HasRows)
                    {
                        List<Producto> Lsproducto = new List<Producto>();
                        if (dr.Read())
                        {
                            Producto Producto = new Producto();
                            Producto.C_PRODUCTO = dr["C_PRODUCTO"].ToString();
                            Producto.C_CATEGORIA = dr["C_CATEGORIA"].ToString();
                            Producto.CATEGORIA = dr["CATEGORIA"].ToString();
                            Producto.NOMBRE_PRODUCTO = dr["NOMBRE_PRODUCTO"].ToString();
                            Producto.TIPO_UNIDAD = dr["TIPO_UNIDAD"].ToString();
                            Producto.C_UNIDAD_PRIN = dr["C_UNIDAD_PRIN"].ToString();
                            Producto.UNIDAD_PRIN = dr["UNIDAD_PRIN"].ToString();
                            Producto.C_UNIDAD_AUX = dr["C_UNIDAD_AUX"].ToString();
                            Producto.UNIDAD_AUX = dr["UNIDAD_AUX"].ToString();
                            Producto.FACTOR = Convert.ToDecimal(dr["FACTOR"]);
                            Producto.ESTADO = dr["ESTADO"].ToString();
                            Producto.IND_ESTADO = dr["IND_ESTADO"].ToString();
                            Lsproducto.Add(Producto);

                        }
                        response.CodigoRespuesta = Responses.SUCCESS_CODE;
                        response.NombreRespuesta = Responses.SUCCESS_MESSAGE;
                        response.Producto = Lsproducto;
                    }

                    return response;
                }
            }
            catch (Exception ex)
            {
                response.CodigoRespuesta = Responses.ERROR_CODE;
                response.NombreRespuesta = string.Concat(Responses.ERROR_MESSAGE, ": ", ex.Message);
                return response;
            }
        }
    }
}

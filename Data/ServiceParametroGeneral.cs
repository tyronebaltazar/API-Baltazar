using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entity.Dto.Response.ParametroGeneralResponse;

using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Entity;
using Entity.Dto.Request.ParametroGeneralRequest;

namespace Data
{
    public class ServiceParametroGeneral
    {
        string cadenaConexion = ConfigurationManager.ConnectionStrings["Conexion"].ToString();

        public ParametroGeneralResponse ConsultarParametroGeneral()
        {
            ParametroGeneralResponse response = new ParametroGeneralResponse();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(cadenaConexion))
                {
                    // Abrimos bd
                    sqlConnection.Open();

                    // Operaciones
                    SqlCommand cmd = new SqlCommand("SP_PARAM_GENERAL_LEER", sqlConnection);

                    // Indicamos al sqlcommand que ejecutaré un store procedure
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Ejecuto el store
                    SqlDataReader dr = cmd.ExecuteReader();

                    // Validamos y leemos filas
                    if (dr.HasRows)
                    {
                        List<ParametroGeneral> lstParametroGeneral = new List<ParametroGeneral>();
                        while (dr.Read())
                        {
                            ParametroGeneral parametroGeneral = new ParametroGeneral();
                            parametroGeneral.c_parametro_general = dr["C_PARAMETRO_GENERAL"].ToString();
                            parametroGeneral.tipo_parametro = dr["TIPO_PARAMETRO"].ToString();
                            parametroGeneral.descripcion_parametro = dr["DESCRIPCION_PARAMETRO"].ToString();
                            parametroGeneral.ind_estado = dr["IND_ESTADO"].ToString();
                            parametroGeneral.codigo_parametro_str_1 = dr["CODIGO_PARAMETRO_STR_1"].ToString();
                            parametroGeneral.codigo_parametro_str_2 = dr["CODIGO_PARAMETRO_STR_2"].ToString();
                            parametroGeneral.codigo_parametro_int_1 = Convert.ToInt32(dr["CODIGO_PARAMETRO_INT_1"]);
                            parametroGeneral.codigo_parametro_int_2 = Convert.ToInt32(dr["CODIGO_PARAMETRO_INT_2"]);
                            parametroGeneral.codigo_parametro_decimal_1 = Convert.ToDecimal(dr["CODIGO_PARAMETRO_DECIMAL_1"]);
                            parametroGeneral.codigo_parametro_decimal_2 = Convert.ToDecimal(dr["CODIGO_PARAMETRO_DECIMAL_2"]);
                            lstParametroGeneral.Add(parametroGeneral);
                        }
                        response.CodigoRespuesta = Responses.SUCCESS_CODE;
                        response.NombreRespuesta = Responses.SUCCESS_MESSAGE;
                        response.ParametrosGenerales = lstParametroGeneral;
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
        public ParametroGeneralResponse CrearParametroGeneral(ParametroGeneralCrearRequest request)
        {
            ParametroGeneralResponse response = new ParametroGeneralResponse();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(cadenaConexion))
                {
                    // Abrimos bd
                    sqlConnection.Open();

                    // Operaciones
                    SqlCommand cmd = new SqlCommand("SP_PARAM_GENERAL_INSERTAR", sqlConnection);

                    // Pasar parametros al store
                    cmd.Parameters.AddWithValue("@TIPO_PARAMETRO", request.tipo_parametro);
                    cmd.Parameters.AddWithValue("@DESCRIPCION_PARAMETRO", request.descripcion_parametro);
                    cmd.Parameters.AddWithValue("@IND_ESTADO", request.ind_estado);
                    cmd.Parameters.AddWithValue("@CODIGO_PARAMETRO_STR_1", request.codigo_parametro_str_1);
                    cmd.Parameters.AddWithValue("@CODIGO_PARAMETRO_STR_2", request.codigo_parametro_str_2);
                    cmd.Parameters.AddWithValue("@CODIGO_PARAMETRO_INT_1", request.codigo_parametro_int_1);
                    cmd.Parameters.AddWithValue("@CODIGO_PARAMETRO_INT_2", request.codigo_parametro_int_2);
                    cmd.Parameters.AddWithValue("@CODIGO_PARAMETRO_DECIMAL_1", request.codigo_parametro_decimal_1);
                    cmd.Parameters.AddWithValue("@CODIGO_PARAMETRO_DECIMAL_2", request.codigo_parametro_decimal_2);

                    // Indicamos al sqlcommand que ejecutaré un store procedure
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Ejecuto el store
                    SqlDataReader dr = cmd.ExecuteReader();
                    
                    // Validamos y leemos filas
                    if (dr.HasRows)
                    {
                        List<ParametroGeneral> lstParametroGeneral = new List<ParametroGeneral>();
                        if (dr.Read())
                        {
                            ParametroGeneral parametroGeneral = new ParametroGeneral();
                            parametroGeneral.c_parametro_general = dr["C_PARAMETRO_GENERAL"].ToString();
                            parametroGeneral.tipo_parametro = dr["TIPO_PARAMETRO"].ToString();
                            parametroGeneral.descripcion_parametro = dr["DESCRIPCION_PARAMETRO"].ToString();
                            parametroGeneral.ind_estado = dr["IND_ESTADO"].ToString();
                            parametroGeneral.codigo_parametro_str_1 = dr["CODIGO_PARAMETRO_STR_1"].ToString();
                            parametroGeneral.codigo_parametro_str_2 = dr["CODIGO_PARAMETRO_STR_2"].ToString();
                            parametroGeneral.codigo_parametro_int_1 = Convert.ToInt32(dr["CODIGO_PARAMETRO_INT_1"]);
                            parametroGeneral.codigo_parametro_int_2 = Convert.ToInt32(dr["CODIGO_PARAMETRO_INT_2"]);
                            parametroGeneral.codigo_parametro_decimal_1 = Convert.ToDecimal(dr["CODIGO_PARAMETRO_DECIMAL_1"]);
                            parametroGeneral.codigo_parametro_decimal_2 = Convert.ToDecimal(dr["CODIGO_PARAMETRO_DECIMAL_2"]);
                            lstParametroGeneral.Add(parametroGeneral);
                        }
                        response.CodigoRespuesta = Responses.SUCCESS_CODE;
                        response.NombreRespuesta = Responses.SUCCESS_MESSAGE;
                        response.ParametrosGenerales = lstParametroGeneral;
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

        public ParametroGeneralResponse ActualizarParametroGeneral(ParametroGeneralActualizaRequest request)
        {
            ParametroGeneralResponse response = new ParametroGeneralResponse();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(cadenaConexion))
                {
                    // Abrimos bd
                    sqlConnection.Open();

                    // Operaciones
                    SqlCommand cmd = new SqlCommand("SP_PARAM_GENERAL_ACTUALIZAR", sqlConnection);

                    // Pasar parametros al store
                    cmd.Parameters.AddWithValue("@C_PARAMETRO_GENERAL", request.c_parametro_general);
                    cmd.Parameters.AddWithValue("@TIPO_PARAMETRO", request.tipo_parametro);
                    cmd.Parameters.AddWithValue("@DESCRIPCION_PARAMETRO", request.descripcion_parametro);
                    cmd.Parameters.AddWithValue("@IND_ESTADO", request.ind_estado);
                    cmd.Parameters.AddWithValue("@CODIGO_PARAMETRO_STR_1", request.codigo_parametro_str_1);
                    cmd.Parameters.AddWithValue("@CODIGO_PARAMETRO_STR_2", request.codigo_parametro_str_2);
                    cmd.Parameters.AddWithValue("@CODIGO_PARAMETRO_INT_1", request.codigo_parametro_int_1);
                    cmd.Parameters.AddWithValue("@CODIGO_PARAMETRO_INT_2", request.codigo_parametro_int_2);
                    cmd.Parameters.AddWithValue("@CODIGO_PARAMETRO_DECIMAL_1", request.codigo_parametro_decimal_1);
                    cmd.Parameters.AddWithValue("@CODIGO_PARAMETRO_DECIMAL_2", request.codigo_parametro_decimal_2);

                    // Indicamos al sqlcommand que ejecutaré un store procedure
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Ejecuto el store
                    SqlDataReader dr = cmd.ExecuteReader();

                    // Validamos y leemos filas
                    if (dr.HasRows)
                    {
                        List<ParametroGeneral> lstParametroGeneral = new List<ParametroGeneral>();
                        if (dr.Read())
                        {
                            ParametroGeneral parametroGeneral = new ParametroGeneral();
                            parametroGeneral.c_parametro_general = dr["C_PARAMETRO_GENERAL"].ToString();
                            parametroGeneral.tipo_parametro = dr["TIPO_PARAMETRO"].ToString();
                            parametroGeneral.descripcion_parametro = dr["DESCRIPCION_PARAMETRO"].ToString();
                            parametroGeneral.ind_estado = dr["IND_ESTADO"].ToString();
                            parametroGeneral.codigo_parametro_str_1 = dr["CODIGO_PARAMETRO_STR_1"].ToString();
                            parametroGeneral.codigo_parametro_str_2 = dr["CODIGO_PARAMETRO_STR_2"].ToString();
                            parametroGeneral.codigo_parametro_int_1 = Convert.ToInt32(dr["CODIGO_PARAMETRO_INT_1"]);
                            parametroGeneral.codigo_parametro_int_2 = Convert.ToInt32(dr["CODIGO_PARAMETRO_INT_2"]);
                            parametroGeneral.codigo_parametro_decimal_1 = Convert.ToDecimal(dr["CODIGO_PARAMETRO_DECIMAL_1"]);
                            parametroGeneral.codigo_parametro_decimal_2 = Convert.ToDecimal(dr["CODIGO_PARAMETRO_DECIMAL_2"]);
                            lstParametroGeneral.Add(parametroGeneral);
                        }
                        response.CodigoRespuesta = Responses.SUCCESS_CODE;
                        response.NombreRespuesta = Responses.SUCCESS_MESSAGE;
                        response.ParametrosGenerales = lstParametroGeneral;
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

        public ParametroGeneralResponse EliminarParametroGeneral(string id)
        {
            ParametroGeneralResponse response = new ParametroGeneralResponse();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(cadenaConexion))
                {
                    // Abrimos bd
                    sqlConnection.Open();

                    // Operaciones
                    SqlCommand cmd = new SqlCommand("SP_PARAM_GENERAL_ELIMINAR", sqlConnection);

                    // Pasar parametros al store
                    cmd.Parameters.AddWithValue("@C_PARAMETRO_GENERAL", id);
                    
                    // Indicamos al sqlcommand que ejecutaré un store procedure
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Ejecuto el store
                    SqlDataReader dr = cmd.ExecuteReader();

                    // Validamos y leemos filas
                    if (dr.HasRows)
                    {
                        List<ParametroGeneral> lstParametroGeneral = new List<ParametroGeneral>();
                        if (dr.Read())
                        {
                            ParametroGeneral parametroGeneral = new ParametroGeneral();
                            parametroGeneral.c_parametro_general = dr["C_PARAMETRO_GENERAL"].ToString();
                            parametroGeneral.tipo_parametro = dr["TIPO_PARAMETRO"].ToString();
                            parametroGeneral.descripcion_parametro = dr["DESCRIPCION_PARAMETRO"].ToString();
                            parametroGeneral.ind_estado = dr["IND_ESTADO"].ToString();
                            parametroGeneral.codigo_parametro_str_1 = dr["CODIGO_PARAMETRO_STR_1"].ToString();
                            parametroGeneral.codigo_parametro_str_2 = dr["CODIGO_PARAMETRO_STR_2"].ToString();
                            parametroGeneral.codigo_parametro_int_1 = Convert.ToInt32(dr["CODIGO_PARAMETRO_INT_1"]);
                            parametroGeneral.codigo_parametro_int_2 = Convert.ToInt32(dr["CODIGO_PARAMETRO_INT_2"]);
                            parametroGeneral.codigo_parametro_decimal_1 = Convert.ToDecimal(dr["CODIGO_PARAMETRO_DECIMAL_1"]);
                            parametroGeneral.codigo_parametro_decimal_2 = Convert.ToDecimal(dr["CODIGO_PARAMETRO_DECIMAL_2"]);
                            lstParametroGeneral.Add(parametroGeneral);
                        }
                        response.CodigoRespuesta = Responses.SUCCESS_CODE;
                        response.NombreRespuesta = Responses.SUCCESS_MESSAGE;
                        response.ParametrosGenerales = lstParametroGeneral;
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

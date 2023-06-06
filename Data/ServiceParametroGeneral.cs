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
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Reflection;
using System.Text;
using Microsoft.ApplicationBlocks.Data;
using System.ComponentModel;

namespace Data
{
    public class Conexion
    {
        readonly String OSqlConnIN;
        private SqlTransaction oSqlTransaction = null;
        public Conexion()
        {
            ConnectionStringSettings settingconection;
            settingconection =
                  ConfigurationManager.ConnectionStrings["Conexion"];
            OSqlConnIN = settingconection.ConnectionString;
        }

        public Conexion(Int32 Id_DataBase)
        {

            if (Id_DataBase == 1)
            {
                ConnectionStringSettings settingconection;
                settingconection =
                      ConfigurationManager.ConnectionStrings["Conexion"];
                OSqlConnIN = settingconection.ConnectionString;
            }
        }
        public SqlConnection GetConnection()
        {
            SqlConnection cn = new SqlConnection(OSqlConnIN);
            try
            {
                cn.Open();
                return cn;
            }
            catch
            {
                return null;
            }
        }
        public SqlDataAdapter GetAdapter(String s_Sql)
        {
            SqlDataAdapter adp;
            try
            {
                adp = new SqlDataAdapter(s_Sql, OSqlConnIN);
                return adp;
            }
            catch
            {
                return null;
            }
        }

        private SqlDbType F_obtenerSQLType(String sNombreTipo)
        {
            SqlDbType tTipo;

            switch (sNombreTipo)
            {
                case "bit":
                    tTipo = SqlDbType.Bit;
                    break;

                case "char":
                    tTipo = SqlDbType.Char;
                    break;

                case "varchar":
                    tTipo = SqlDbType.VarChar;
                    break;

                case "decimal":
                    tTipo = SqlDbType.Decimal;
                    break;

                case "float":
                    tTipo = SqlDbType.Float;
                    break;

                case "int":
                    tTipo = SqlDbType.Int;
                    break;

                case "smallint":
                    tTipo = SqlDbType.SmallInt;
                    break;

                case "tinyint":
                    tTipo = SqlDbType.TinyInt;
                    break;

                case "datetime":
                    tTipo = SqlDbType.DateTime;
                    break;

                case "smalldatetime":
                    tTipo = SqlDbType.SmallDateTime;
                    break;

                case "nvarchar":
                    tTipo = SqlDbType.NVarChar;
                    break;

                case "image":
                    tTipo = SqlDbType.Image;
                    break;

                case "xml":
                    tTipo = SqlDbType.Xml;
                    break;

                case "text":
                    tTipo = SqlDbType.Text;
                    break;

                case "ntext":
                    tTipo = SqlDbType.NText;
                    break;

                case "bigint":
                    tTipo = SqlDbType.BigInt;
                    break;

                default:
                    throw (new Exception("Tipo de dato SQL no soportado:" + sNombreTipo));
            }

            return tTipo;
        }


        private DataSet ObtenerParametros(string sProcedure)
        {

            SqlParameter[] arParms = { new SqlParameter("@procedure_name", SqlDbType.NChar, 256),
                                      new SqlParameter("@group_number", SqlDbType.Int, 4),
                                      new SqlParameter("@procedure_schema", SqlDbType.NChar, 256),
                                      new SqlParameter("@parameter_name", SqlDbType.NChar, 256) };

            arParms[0].Value = sProcedure;
            arParms[1].Value = DBNull.Value;
            arParms[2].Value = DBNull.Value;
            arParms[3].Value = DBNull.Value;

            DataSet ds;

            if (oSqlTransaction != null)
            {
                ds = SqlHelper.ExecuteDataset
                    (
                    oSqlTransaction,
                    CommandType.StoredProcedure,
                    "sp_procedure_params_rowset",
                    arParms
                    );
            }
            else
            {
                ds = SqlHelper.ExecuteDataset
                    (
                    OSqlConnIN,
                    CommandType.StoredProcedure,
                    "sp_procedure_params_rowset",
                    arParms
                    );
            }

            if (ds.Tables[0].Rows.Count <= 0)
                throw new Exception("El store procedure " + sProcedure + " no existe o no tiene permisos para ejecutarlo.");
            else
                return ds;
        }

        public DataSet EjecutarQuery(String sQuery)
        {
            DataSet ds = null;

            if (oSqlTransaction != null)
            {
                ds = SqlHelper.ExecuteDataset
                    (
                    oSqlTransaction,
                    CommandType.Text,
                    sQuery
                    );
            }
            else
            {
                ds = SqlHelper.ExecuteDataset
                    (
                    this.OSqlConnIN,
                    CommandType.Text,
                    sQuery
                    );
            }
            return ds;
        }

        public void EjecutarQuerySinRetorno(String sQuery)
        {
            if (oSqlTransaction != null)
            {
                SqlHelper.ExecuteDataset
                    (
                    oSqlTransaction,
                    CommandType.Text,
                    sQuery
                    );
            }
            else
            {
                SqlHelper.ExecuteDataset
                    (
                    this.OSqlConnIN,
                    CommandType.Text,
                    sQuery
                    );
            }
        }

        public SqlDataReader EjecutarDataReader(String sProcedure, params object[] valores)
        {
            try
            {
                return SqlHelper.ExecuteReader(this.OSqlConnIN, sProcedure, valores);
            }
            catch (Exception ex)
            {
                Console.Write("Error: " + ex.Message);
                return null;
            }
        }

        public DataSet EjecutarDataSet(String sProcedure, params object[] valores)
        {
            SqlParameter[] arParms = new SqlParameter[valores.Length];


            DataSet ds = ObtenerParametros(sProcedure);

            if (ds.Tables.Count == 0)
                return null;
            else if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0]; //Estructura del Stored

                Int32 i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    //Omite el parámetro de retorno del procedimiento
                    if (!dr["Parameter_name"].Equals("@RETURN_VALUE"))
                    {
                        arParms[i] = new SqlParameter(dr["Parameter_name"].ToString(), F_obtenerSQLType(dr["Type_name"].ToString()))
                        {
                            Value = valores[i]
                        };
                        i++;
                    }
                }
                if (i != valores.Length)
                    throw new Exception("La cantidad de parámetros ingresados no coincide con las del procedimiento.");
            }
            //Se verifica si existe una Transaccion de BD activa
            ds = null;
            if (oSqlTransaction != null)
            {
                ds = SqlHelper.ExecuteDataset
                    (
                    oSqlTransaction,
                    CommandType.StoredProcedure,
                    sProcedure,
                    arParms
                    );
            }
            else
            {
                ds = SqlHelper.ExecuteDataset
                    (
                    OSqlConnIN,
                    CommandType.StoredProcedure,
                    sProcedure,
                    arParms
                    );
            }

            return ds;
        }


        public DataTable EjecutarDataTable(String sProcedure, params object[] valores)
        {
            SqlParameter[] arParms = new SqlParameter[valores.Length];

            DataTable dt = null;

            DataSet ds = ObtenerParametros(sProcedure);

            if (ds.Tables.Count == 0)
                return null;
            else if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
                Int32 i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    //Omite el parámetro de retorno del procedimiento
                    if (!dr["Parameter_name"].Equals("@RETURN_VALUE"))
                    {
                        arParms[i] = new SqlParameter(dr["Parameter_name"].ToString(), F_obtenerSQLType(dr["Type_name"].ToString()))
                        {
                            Value = valores[i]
                        };
                        i++;
                    }
                }
                if (i != valores.Length)
                    throw new Exception("La cantidad de parámetros ingresados no coincide con las del procedimiento.");
            }
            //Se verifica si existe una Transaccion de BD activa
            ds = null;
            dt = null;

            DataSet dsResult;
            if (oSqlTransaction != null)
                dsResult = SqlHelper.ExecuteDataset(oSqlTransaction, CommandType.StoredProcedure, sProcedure, arParms);
            else
                dsResult = SqlHelper.ExecuteDataset(OSqlConnIN, CommandType.StoredProcedure, sProcedure, arParms);

            if (oSqlTransaction != null) oSqlTransaction.Dispose();
            if (dsResult.Tables.Count > 0) dt = dsResult.Tables[0]; else dt = null;
            dsResult = null;
            if (dt != null) return dt.Copy(); else return null;
        }

        public String EjecutarEscalar(String sProcedure, params object[] valores)
        {
            SqlParameter[] arParms = new SqlParameter[valores.Length];

            DataSet ds = ObtenerParametros(sProcedure);

            if (ds.Tables.Count == 0)
                return null;
            else if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0]; //Estructura del Stored
                Int32 i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    //Omite el parámetro de retorno del procedimiento
                    if (!dr["Parameter_name"].Equals("@RETURN_VALUE"))
                    {
                        arParms[i] = new SqlParameter(dr["Parameter_name"].ToString(), F_obtenerSQLType(dr["Type_name"].ToString()))
                        {
                            Value = valores[i]
                        };
                        i++;
                    }
                }
                if (i != valores.Length)
                    throw new Exception("La cantidad de parámetros ingresados no coincide con las del procedimiento.");
            }

            //Se verifica si existe una Transaccion de BD activa
            String sValor;
            if (oSqlTransaction != null)
            {
                sValor = SqlHelper.ExecuteScalar
                    (
                    oSqlTransaction,
                    CommandType.StoredProcedure,
                    sProcedure,
                    arParms
                    ).ToString();
            }
            else
            {
                sValor = SqlHelper.ExecuteScalar
                    (
                    this.OSqlConnIN,
                    CommandType.StoredProcedure,
                    sProcedure,
                    arParms
                    ).ToString();
            }
            return sValor;
        }


        public void EjecutarSinRetorno(String sProcedure, params object[] valores)
        {
            SqlParameter[] arParms = new SqlParameter[valores.Length];


            //Obtiene los parámetros del procecimiento
            DataSet ds = ObtenerParametros(sProcedure);
            if (ds.Tables.Count == 0)
                return;
            else if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0]; //Estructura del Stored
                //if(dt.Rows.Count != valores.Length)
                //{	
                //    return;
                //}
                Int32 i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    //Omite el parámetro de retorno del procedimiento
                    if (!dr["Parameter_name"].Equals("@RETURN_VALUE"))
                    {
                        arParms[i] = new SqlParameter(dr["Parameter_name"].ToString(), F_obtenerSQLType(dr["Type_name"].ToString()))
                        {
                            Value = valores[i]
                        };
                        i++;
                    }
                }
                if (i != valores.Length)
                    throw new Exception("La cantidad de parámetros ingresados no coincide con las del procedimiento.");
            }

            //Se verifica si existe una Transaccion de BD activa
            if (oSqlTransaction != null)
            {
                SqlHelper.ExecuteNonQuery
                    (
                    this.oSqlTransaction,
                    CommandType.StoredProcedure,
                    sProcedure,
                    arParms
                    );
            }
            else
            {
                SqlHelper.ExecuteNonQuery
                    (
                    this.OSqlConnIN,
                    CommandType.StoredProcedure,
                    sProcedure,
                    arParms
                    );
            }
        }
        public string EjecutarretornodeOUTPUT(String sProcedure, int posicionOUT, params object[] valores)
        {
            string retorno;
            SqlParameter[] arParms = new SqlParameter[valores.Length];

            //Obtiene los parámetros del procecimiento
            DataSet ds = ObtenerParametros(sProcedure);
            if (ds.Tables.Count == 0)
                retorno = "Verifique el sp";
            else if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0]; //Estructura del Stored
                Int32 i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    //Omite el parámetro de retorno del procedimiento
                    if (!dr["Parameter_name"].Equals("@RETURN_VALUE"))
                    {
                        if (i == posicionOUT)
                        {
                            arParms[i] = new SqlParameter(dr["Parameter_name"].ToString(), SqlDbType.VarChar, 255)
                            {
                                Direction = ParameterDirection.InputOutput
                            };
                        }
                        else
                        {
                            arParms[i] = new SqlParameter(dr["Parameter_name"].ToString(), F_obtenerSQLType(dr["Type_name"].ToString()))
                            {
                                Value = valores[i]
                            };
                        }

                        i++;
                    }
                }
                if (i != valores.Length)
                    throw new Exception("La cantidad de parámetros ingresados no coincide con las del procedimiento.");
            }

            //Se verifica si existe una Transaccion de BD activa
            if (oSqlTransaction != null)
            {
                SqlHelper.ExecuteNonQuery
                    (
                    this.oSqlTransaction,
                    CommandType.StoredProcedure,
                    sProcedure,
                    arParms
                    );

                retorno = arParms[posicionOUT].SqlValue.ToString();
            }
            else
            {
                SqlHelper.ExecuteNonQuery
                    (
                    this.OSqlConnIN,
                    CommandType.StoredProcedure,
                    sProcedure,
                    arParms
                    );
                retorno = arParms[posicionOUT].SqlValue.ToString();
            }

            return retorno;
        }

        public string EjecutarConRetorno(String sProcedure, params object[] valores)
        {
            string retorno = String.Empty;
            int Cant;
            SqlParameter[] arParms = new SqlParameter[valores.Length];

            //Obtiene los parámetros del procecimiento
            DataSet ds = ObtenerParametros(sProcedure);
            if (ds.Tables.Count == 0)
                //return 0;
                retorno = "Verifique el sp";
            else if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0]; //Estructura del Stored
                Int32 i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    //Omite el parámetro de retorno del procedimiento
                    if (!dr["Parameter_name"].Equals("@RETURN_VALUE"))
                    {
                        arParms[i] = new SqlParameter(dr["Parameter_name"].ToString(), F_obtenerSQLType(dr["Type_name"].ToString()))
                        {
                            Value = valores[i]
                        };
                        i++;
                    }
                }
                if (i != valores.Length)
                    throw new Exception("La cantidad de parámetros ingresados no coincide con las del procedimiento.");
            }

            //Se verifica si existe una Transaccion de BD activa
            if (oSqlTransaction != null)
            {
                Cant = SqlHelper.ExecuteNonQuery
                    (
                    this.oSqlTransaction,
                    CommandType.StoredProcedure,
                    sProcedure,
                    arParms
                    );
            }
            else
            {
                Cant = SqlHelper.ExecuteNonQuery
                    (
                    this.OSqlConnIN,
                    CommandType.StoredProcedure,
                    sProcedure,
                    arParms
                    );
            }

            if (!string.IsNullOrEmpty(retorno))
            {
                retorno = Convert.ToString(Cant);
            }

            return retorno;
        }


        public void Commit()
        {

            oSqlTransaction.Commit();

        }

        public void Rollback()
        {
            oSqlTransaction.Rollback();

        }

        internal void EjecutarQuerySinRetorno(string p, string usuario, string xplora)
        {
            throw new NotImplementedException();
        }
        public int _EjecutarBulkCopy(string _Tabla_Destino, DataTable _DataTable)
        {
            try
            {
                using (SqlBulkCopy _BulkCopy = new SqlBulkCopy(OSqlConnIN))
                {
                    _BulkCopy.DestinationTableName = _Tabla_Destino;

                    _BulkCopy.WriteToServer(_DataTable);
                }

                return _DataTable.Rows.Count;
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                // finalizar procesos
            }
        }

        public int _EjecutarBulkCopy<T>(string _tabla, IList<T> _Lista)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));

            DataTable table = new DataTable("Tabla");

            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (T item in _Lista)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;

                table.Rows.Add(row);
            }

            return _EjecutarBulkCopy(_tabla, table);
        }

        public int _EjecutarBulkCopy<T>(string _tabla, IEnumerable<T> _Lista)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();

            DataTable output = new DataTable();

            foreach (var prop in properties)
            {
                output.Columns.Add(prop.Name, prop.PropertyType);
            }

            foreach (var item in _Lista)
            {
                DataRow row = output.NewRow();

                foreach (var prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item, null);
                }

                output.Rows.Add(row);
            }

            return _EjecutarBulkCopy(_tabla, output); ;
        }


        public DataTable _ConvertListToDataTable<T>(IEnumerable<T> _Lista)
        {

            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));

            DataTable table = new DataTable("Tabla");

            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (T item in _Lista)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;

                table.Rows.Add(row);
            }

            return table;
        }
    }
}
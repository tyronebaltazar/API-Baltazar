using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entity.Dto.Response.CategoriaResponse;
using Entity.Dto.Request.CategoriaRequest;
using System.Data.SqlClient;
using Entity;

namespace Data
{
    public class ServiceCategoria
    {
        Conexion Conexion = new Conexion();

        public CategoriaResponse ConsultarCategoria()
        {
            CategoriaResponse _response = new CategoriaResponse();
            try
            {
                using (SqlDataReader reader = Conexion.EjecutarDataReader("SP_CATEGORIA_LEER"))
                {
                    if (reader.HasRows)
                    {
                        List<Categoria> categorias = new List<Categoria>();
                        while (reader.Read())
                        {
                            Categoria categoria = new Categoria();
                            categoria.CodCategoria = reader["C_CATEGORIA"].ToString();
                            categoria.Nombre = reader["NOMBRE"].ToString();
                            categoria.Estado = reader["ESTADO"].ToString();
                            categoria.IndEstado = reader["IND_ESTADO"].ToString();
                            categorias.Add(categoria);
                        }
                        _response.CodigoRespuesta = Responses.SUCCESS_CODE;
                        _response.NombreRespuesta = Responses.SUCCESS_MESSAGE;
                        _response.Categorias = categorias;
                    }
                    return _response;
                }
            }
            catch (Exception ex)
            {
                _response.CodigoRespuesta = Responses.ERROR_CODE;
                _response.NombreRespuesta = string.Concat(Responses.ERROR_MESSAGE, ": ", ex.Message);
                return _response;
            }
        }
        public CategoriaResponse CrearCategoria(CategoriaCrearRequest _request)
        {
            CategoriaResponse _response = new CategoriaResponse();
            try
            {
                using (SqlDataReader reader = Conexion.EjecutarDataReader("SP_CATEGORIA_CREAR", _request.Nombre,
                                                                                                _request.IndEstado))
                {
                    if (!reader.HasRows)
                    {
                        _response.CodigoRespuesta = Responses.ERROR_CODE;
                        _response.NombreRespuesta = string.Concat(Responses.ERROR_MESSAGE, ": ", "No se insertó bien la categoría");
                        return _response;
                    }

                    List<Categoria> categorias = new List<Categoria>();
                    while (reader.Read())
                    {
                        Categoria categoria = new Categoria();
                        categoria.CodCategoria = reader["C_CATEGORIA"].ToString();
                        categoria.Nombre = reader["NOMBRE"].ToString();
                        categoria.Estado = reader["ESTADO"].ToString();
                        categoria.IndEstado = reader["IND_ESTADO"].ToString();
                        categorias.Add(categoria);
                    }
                    _response.CodigoRespuesta = Responses.SUCCESS_CODE;
                    _response.NombreRespuesta = Responses.SUCCESS_MESSAGE;
                    _response.Categorias = categorias;

                    return _response;
                }
            }
            catch (Exception ex)
            {
                _response.CodigoRespuesta = Responses.ERROR_CODE;
                _response.NombreRespuesta = string.Concat(Responses.ERROR_MESSAGE, ": ", ex.Message);
                return _response;
            }
        }
        public CategoriaResponse ActualizarCategoria(CategoriaActualizarRequest _request)
        {
            CategoriaResponse _response = new CategoriaResponse();
            try
            {
                SqlDataReader reader = Conexion.EjecutarDataReader("SP_CATEGORIA_ACTUALIZAR", _request.CodCategoria, _request.Nombre, _request.IndEstado);

                if (!reader.HasRows)
                {
                    _response.CodigoRespuesta = Responses.ERROR_CODE;
                    _response.NombreRespuesta = string.Concat(Responses.ERROR_MESSAGE, ": ", "Error al actualizar, asegurese de que la categoría exista.");
                    return _response;
                }

                List<Categoria> categorias = new List<Categoria>();
                while (reader.Read())
                {
                    Categoria categoria = new Categoria();
                    categoria.CodCategoria = reader["C_CATEGORIA"].ToString();
                    categoria.Nombre = reader["NOMBRE"].ToString();
                    categoria.Estado = reader["ESTADO"].ToString();
                    categoria.IndEstado = reader["IND_ESTADO"].ToString();
                    categorias.Add(categoria);
                }
                _response.CodigoRespuesta = Responses.SUCCESS_CODE;
                _response.NombreRespuesta = Responses.SUCCESS_MESSAGE;
                _response.Categorias = categorias;

                return _response;
            }
            catch (Exception ex)
            {
                _response.CodigoRespuesta = Responses.ERROR_CODE;
                _response.NombreRespuesta = string.Concat(Responses.ERROR_MESSAGE, ": ", ex.Message);
                return _response;
            }

        }
        public CategoriaResponse EliminarCategoria(string id)
        {
            CategoriaResponse _response = new CategoriaResponse();
            try
            {
                SqlDataReader reader = Conexion.EjecutarDataReader("SP_CATEGORIA_ELIMINAR", id);

                if (!reader.HasRows)
                {
                    _response.CodigoRespuesta = Responses.ERROR_CODE;
                    _response.NombreRespuesta = string.Concat(Responses.ERROR_MESSAGE, ": ", "Error al actualizar, asegurese de que la categoría exista.");
                    return _response;
                }
                
                List<Categoria> categorias = new List<Categoria>();
                while (reader.Read())
                {
                    Categoria categoria = new Categoria();
                    categoria.CodCategoria = reader["C_CATEGORIA"].ToString();
                    categoria.Nombre = reader["NOMBRE"].ToString();
                    categoria.Estado = reader["ESTADO"].ToString();
                    categoria.IndEstado = reader["IND_ESTADO"].ToString();
                    categorias.Add(categoria);
                }
                _response.CodigoRespuesta = Responses.SUCCESS_CODE;
                _response.NombreRespuesta = Responses.SUCCESS_MESSAGE;
                _response.Categorias = categorias;

                return _response;
            }
            catch (Exception ex)
            {
                _response.CodigoRespuesta = Responses.ERROR_CODE;
                _response.NombreRespuesta = string.Concat(Responses.ERROR_MESSAGE, ": ", ex.Message);
                return _response;
            }

        }
    }
}

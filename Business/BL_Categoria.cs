using Data;
using Entity.Dto.Request.CategoriaRequest;
using Entity.Dto.Response.CategoriaResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class BL_Categoria
    {
        ServiceCategoria ServiceCategoria = new ServiceCategoria();
    
        public CategoriaResponse ConsultarCategoria()
        {
            return ServiceCategoria.ConsultarCategoria();
        }

        public CategoriaResponse CrearCategoria(CategoriaCrearRequest _request)
        {
            return ServiceCategoria.CrearCategoria(_request);
        }

        public CategoriaResponse ActualizarCategoria(CategoriaActualizarRequest _request)
        {
            return ServiceCategoria.ActualizarCategoria(_request);
        }

        public CategoriaResponse EliminarCategoria(string id)
        {
            return ServiceCategoria.EliminarCategoria(id);
        }
    }
}

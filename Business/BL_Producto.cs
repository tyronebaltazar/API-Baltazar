using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Dto.Request.ProductoRequest;
using Entity.Dto.Response.ProductoResponse;
using Data;
namespace Business
{
    public class BL_Producto
    {
        ServiceProducto ServiceProducto = new ServiceProducto();

        public ProductoResponse ConsultarProducto()
        {
            return ServiceProducto.ConsultarProducto();
        }

        public ProductoResponse CrearProducto(ProductoCrearRequest req)
        {
            return ServiceProducto.CrearProducto(req);
        }

        public ProductoResponse ActualizarProducto(ProductoActualizaRequest req)
        {
            return ServiceProducto.ActualizarProducto(req);
        }

        public ProductoResponse EliminarProducto(string id)
        {
            return ServiceProducto.EliminarProducto(id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Business;
using Entity.Dto.Request.ProductoRequest;
using Entity.Dto.Response.ProductoResponse;

namespace WebApi.Controllers
{
    public class ProductoController : ApiController
    {
        
        BL_Producto BL_Producto = new BL_Producto();

        // GET api/<controller>
        public ProductoResponse Get()
        {
            return BL_Producto.ConsultarProducto();
        }

        // POST api/<controller>
        public ProductoResponse Post([FromBody] ProductoCrearRequest request)
        {
            return BL_Producto.CrearProducto(request);
        }

        // PUT api/<controller>/5
        public ProductoResponse Put([FromBody] ProductoActualizaRequest request)
        {
            return BL_Producto.ActualizarProducto(request);
        }

        // DELETE api/<controller>/5
        public ProductoResponse Delete(string id)
        {
            return BL_Producto.EliminarProducto(id);
        }

    }
}

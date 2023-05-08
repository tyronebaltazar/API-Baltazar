using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Entity.Dto.Response.CategoriaResponse;
using Business;
using Entity.Dto.Request.CategoriaRequest;

namespace WebApi.Controllers
{
    public class CategoriaController : ApiController
    {
        BL_Categoria BL_Categoria = new BL_Categoria();

        // GET api/<controller>
        public CategoriaResponse Get()
        {
            return BL_Categoria.ConsultarCategoria();
        }

        // POST api/<controller>
        public CategoriaResponse Post([FromBody] CategoriaCrearRequest request)
        {
            return BL_Categoria.CrearCategoria(request);
        }

        // PUT api/<controller>/5
        public CategoriaResponse Put([FromBody] CategoriaActualizarRequest request)
        {
            return BL_Categoria.ActualizarCategoria(request);
        }

        // DELETE api/<controller>/5
        public CategoriaResponse Delete(string id)
        {
            return BL_Categoria.EliminarCategoria(id);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Business;
using Entity.Dto.Request.ParametroGeneralRequest;
using Entity.Dto.Response.ParametroGeneralResponse;

namespace WebApi.Controllers
{
    public class ParametroGeneralController : ApiController
    {
        BL_ParametroGeneral BL_ParametroGeneral = new BL_ParametroGeneral();

        // GET api/<controller>
        public ParametroGeneralResponse Get()
        {
            return BL_ParametroGeneral.ConsultarParametroGeneral();
        }

        public ParametroGeneralResponse Post(ParametroGeneralCrearRequest req)
        {
            return BL_ParametroGeneral.CrearParametroGeneral(req);
        }

        public ParametroGeneralResponse Put(ParametroGeneralActualizaRequest req)
        {
            return BL_ParametroGeneral.ActualizarParametroGeneral(req);
        }

        public ParametroGeneralResponse Delete(string id)
        {
            return BL_ParametroGeneral.EliminarParametroGeneral(id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Business;
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
    }
}

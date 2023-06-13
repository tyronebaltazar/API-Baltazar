using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entity.Dto.Request.ParametroGeneralRequest;
using Entity.Dto.Response.ParametroGeneralResponse;
using Data;

namespace Business
{
    public class BL_ParametroGeneral
    {
        ServiceParametroGeneral ServiceParametroGeneral = new ServiceParametroGeneral();

        public ParametroGeneralResponse ConsultarParametroGeneral()
        {
            return ServiceParametroGeneral.ConsultarParametroGeneral();
        }

        public ParametroGeneralResponse CrearParametroGeneral(ParametroGeneralCrearRequest req)
        {
            return ServiceParametroGeneral.CrearParametroGeneral(req);
        }

        public ParametroGeneralResponse ActualizarParametroGeneral(ParametroGeneralActualizaRequest req)
        {
            return ServiceParametroGeneral.ActualizarParametroGeneral(req);
        }

        public ParametroGeneralResponse EliminarParametroGeneral(string id)
        {
            return ServiceParametroGeneral.EliminarParametroGeneral(id);
        }
    }
}

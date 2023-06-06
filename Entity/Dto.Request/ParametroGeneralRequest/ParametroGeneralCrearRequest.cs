using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dto.Request.ParametroGeneralRequest
{
    public class ParametroGeneralCrearRequest
    {
        public string tipo_parametro { get; set; }
        public string descripcion_parametro { get; set; }
        public string ind_estado { get; set; }
        public string codigo_parametro_str_1 { get; set; }
        public string codigo_parametro_str_2 { get; set; }
        public int codigo_parametro_int_1 { get; set; }
        public int codigo_parametro_int_2 { get; set; }
        public decimal codigo_parametro_decimal_1 { get; set; }
        public decimal codigo_parametro_decimal_2 { get; set; }
    }
}

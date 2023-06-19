using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dto.Request.ProductoRequest
{
    public class ProductoActualizaRequest
    {
        public string C_PRODUCTO { get; set; }
        public string C_CATEGORIA { get; set; }
        public string NOMBRE_PRODUCTO { get; set; }
        public string TIPO_UNIDAD { get; set; }
        public string C_UNIDAD_PRIN { get; set; }
        public string C_UNIDAD_AUX { get; set; }
        public decimal FACTOR { get; set; }
        public string ESTADO { get; set; }
        public string IND_ESTADO { get; set; }
    }
}

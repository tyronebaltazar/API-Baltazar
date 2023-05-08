using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dto.Request.CategoriaRequest
{
    public class CategoriaActualizarRequest
    {
        public string CodCategoria { get; set; }
        public string Nombre { get; set; }
        public string IndEstado { get; set; }
    }
}

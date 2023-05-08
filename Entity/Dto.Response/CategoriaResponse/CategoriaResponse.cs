using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dto.Response.CategoriaResponse
{
    public class CategoriaResponse : BaseResponse
    {
        public List<Categoria> Categorias { get; set; }
    }
    
    public class Categoria
    {
        public string CodCategoria { get; set; }
        public string Nombre { get; set; }
        public string Estado { get; set; }
        public string IndEstado { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace Pruebayaya.Models
{
    [Table("Categoria")]
    public class Categorias : Entidad
    {
        public string nombre { get; set; }
        public string descripcion { get; set; }
    }
}

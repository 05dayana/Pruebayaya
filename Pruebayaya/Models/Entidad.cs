using System.ComponentModel.DataAnnotations.Schema;

namespace Pruebayaya.Models
{
    [Table("Categoria")]
    public class Entidad
    {
        public long id { get; set; }
        public bool activo { get; set; }
        public DateTime creadoDate { get; set; }
    }
}

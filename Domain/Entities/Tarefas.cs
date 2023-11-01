
using Domain.Enumerator;

namespace Domain.Entities
{
    public class Tarefas: EntityBase
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public Status Status { get; set; }
        public int IdUsuario { get; set; }  
        public Usuario Usuario { get; set; }
    }
}

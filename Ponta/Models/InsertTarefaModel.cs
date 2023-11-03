

namespace Application.Models
{
    
    public class InsertTaskModel
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public int Status { get; set; }
        public int? UserId { get; set; }
    }
}

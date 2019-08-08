using System.Collections.Generic;


namespace Blog161.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public ICollection<Mensagem> Mensagens { get; set; }
    }
}

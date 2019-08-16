using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog161.Models
{
    public class Comentario
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public string Autor { get; set; }
        public int MensagemId { get; set; }
        [ForeignKey("MensagemId")]
        public Mensagem Mensagem { get; set; }
        [InverseProperty("Comentarios")]
        public virtual User User { get; set; }
    }
}

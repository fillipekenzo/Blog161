using Blog161.Models;
using System.Collections.Generic;

namespace Blog161.ViewModel
{
    public class VmComentariosMensagem
    {
        public IEnumerable<Mensagem> Mensagens { get; set; }
        public IEnumerable<Comentario> Comentarios { get; set; }
    }
}

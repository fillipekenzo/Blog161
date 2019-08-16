using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Blog161.Models
{
    public class User : IdentityUser
    {
        public string Nome { get; set; }

        public virtual ICollection<Mensagem> Mensagens { get; set; }
        public virtual ICollection<Comentario> Comentarios { get; set; }

    }
}


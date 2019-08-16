using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Blog161.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public virtual ICollection<Mensagem> Mensagens { get; set; }
        public virtual ICollection<Comentario> Comentarios { get; set; }

    }
}


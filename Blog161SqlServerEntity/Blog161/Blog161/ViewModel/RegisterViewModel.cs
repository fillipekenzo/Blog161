using System.ComponentModel.DataAnnotations;

namespace Blog161.ViewModel
{
    public class RegisterViewModel : LoginViewModel
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Por favor entre com seu nome.")]
        public string Nome { get; set; }

        [Display(Name = "Grupo de usuário")]
        [Required(ErrorMessage = "Por favor escolha seu grupo de usuário.")]
        public string Grupo { get; set; }

        [Required(ErrorMessage = "Por favor entre com seu email.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}

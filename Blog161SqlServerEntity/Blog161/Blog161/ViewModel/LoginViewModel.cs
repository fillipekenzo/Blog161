using System.ComponentModel.DataAnnotations;

namespace Blog161.ViewModel
{
    public class LoginViewModel
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Por favor entre com seu username.")]
        public string Username { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Por favor entre com sua senha.")]
        public string Senha { get; set; }

        [Display(Name = "Guardar login")]
        public bool RememberMe { get; set; }
    }
}

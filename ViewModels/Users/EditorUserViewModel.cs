using System.ComponentModel.DataAnnotations;

namespace MarcaTento.ViewModels.Users
{
    public class EditorUserViewModel
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Deve conter entre 3 e 30 caracteres")]
        public string Username { get; set; }
        [Required(ErrorMessage = "O campo Email é obrigatório")]
        [StringLength(100, ErrorMessage = "Deve conter até 100 caracteres")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo Senha é obrigatório")]
        [StringLength(60, ErrorMessage = "Deve conter até 60 caracteres")]
        public string PasswordHash { get; set; }
        [Required(ErrorMessage = "O campo Imagem é obrigatório")]
        [StringLength(200, ErrorMessage = "Deve conter até 200 caracteres")]
        public string Image { get; set; }
    }
}

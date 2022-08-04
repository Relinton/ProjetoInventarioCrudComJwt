
using System.ComponentModel.DataAnnotations;

namespace ConsumindoApiWebApp.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Nome")]
        [MaxLength(100)]
        public string Nome { get; set; }
        [Required]
        [Display(Name = "Login")]
        [MaxLength(80)]
        public string Login { get; set; }
        [Required]
        [Display(Name = "Senha")]
        [MaxLength(80)]
        public string Senha { get; set; }
        [Required]
        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }
        [Display(Name = "Email")]
        [MaxLength(250)]
        public string Email { get; set; }
    }
}


using System.ComponentModel.DataAnnotations;

namespace InventarioApiJwt.Models
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Nome")]
        [MaxLength(100)]
        [Required]
        public string Nome { get; set; }
        [Display(Name = "Preço")]
        [Required]
        public decimal Preco { get; set; }
        [Display(Name = "Estoque")]
        [Required]
        public int Estoque { get; set; }
        [Display(Name = "Imagem")]
        [MaxLength(250)]
        public string Imagem { get; set; }
    }
}

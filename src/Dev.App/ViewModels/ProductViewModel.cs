using Dev.App.Extensions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dev.App.ViewModels
{
    public class ProductViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [DisplayName("Fornecedor")]
        [Required(ErrorMessage = "O campo Fornecedor é obrigatório")]
        public Guid SupplierId { get; set; }
        [DisplayName("Fornecedor")]
        public SupplierViewModel Supplier { get; set; }
        [DisplayName("Nome")]
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo Nome deve conter de {2} até {1} caracteres", MinimumLength = 2)]
        public string Name { get; set; }
        [NotMapped]
        [ScaffoldColumn(false)]
        [DisplayName("Imagem")]
        public IFormFile ImageUpload { get; set; }
        [DisplayName("Imagem")]
        public string Image { get; set; }
        [Required(ErrorMessage = "O campo Descrição é obrigatório")]
        [StringLength(1000, ErrorMessage = "O campo Nome deve conter de {2} até {1} caracteres", MinimumLength = 0)]
        [DisplayName("Descrição")]
        public string Description { get; set; }
        [Required(ErrorMessage = "O campo Valor é obrigatório")]
        [DisplayName("Valor")]
        [Currency]
        public decimal Value { get; set; }
        [ScaffoldColumn(false)]
        public DateTime CreatedDate { get; set; }
        [DisplayName("Ativo?")]
        public bool Active { get; set; }
        [NotMapped]
        public IEnumerable<SupplierViewModel> AllSuppliers { get; internal set; }
    }
}

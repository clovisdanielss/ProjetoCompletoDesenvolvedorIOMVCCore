using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Dev.App.ViewModels
{
    public class AddressViewModel 
    {
        [Key]
        public Guid Id { get; set; }
        [DisplayName("Fornecedor")]
        [Required(ErrorMessage = "O campo Fornecedor é obrigatório")]
        public Guid SupplierId { get; set; }
        public SupplierViewModel Supplier { get; set; }
        [DisplayName("Logradouro")]
        [Required(ErrorMessage = "O campo Logradouro é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo Logradouro deve conter de {2} até {1} caracteres", MinimumLength = 3)]
        public string Street { get; set; }
        [DisplayName("Número")]
        [Required(ErrorMessage = "O campo Número é obrigatório")]
        [StringLength(6, ErrorMessage = "O campo Número deve conter de {2} até {1} caracteres", MinimumLength = 1)]
        public string Number { get; set; }
        [DisplayName("CEP")]
        [Required(ErrorMessage = "O campo CEP é obrigatório")]
        [StringLength(8, ErrorMessage = "O campo CEP deve conter {1} caracteres", MinimumLength = 8)]
        public string ZipCode { get; set; }
        [DisplayName("Bairro")]
        [Required(ErrorMessage = "O campo Bairro é obrigatório")]
        [StringLength(20, ErrorMessage = "O campo Bairro deve conter de {2} até {1} caracteres", MinimumLength = 2)]
        public string Neighboor { get; set; }
        [DisplayName("Cidade")]
        [Required(ErrorMessage = "O campo Cidade é obrigatório")]
        [StringLength(60, ErrorMessage = "O campo Cidade deve conter de {2} até {1} caracteres", MinimumLength = 2)]
        public string City { get; set; }
        [DisplayName("Estado")]
        [Required(ErrorMessage = "O campo Estado é obrigatório")]
        [StringLength(2, ErrorMessage = "O campo Estado deve conter {1} caracteres", MinimumLength = 2)]
        public string State { get; set; }
    }
}

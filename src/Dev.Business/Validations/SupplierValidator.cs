using Dev.Business.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.Business.Validations
{
    public class SupplierValidator : AbstractValidator<Supplier>
    {
        public SupplierValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage("O campo nome deve ser fornecido")
                .Length(2, 200)
                .WithMessage("O campo precisa ter de {MinLenght} até {MaxLenght} caractéres");

            When(p => p.Type == SupplierType.PF, () =>
            {
                RuleFor(p => p.Document)
                    .NotEmpty()
                    .WithMessage("O campo documento deve ser fornecido")
                    .Length(11)
                    .WithMessage("O CPF precisa de 11 caracteres");
            });

            When(p => p.Type == SupplierType.PJ, () =>
            {
                RuleFor(p => p.Document)
                    .NotEmpty()
                    .WithMessage("O campo documento deve ser fornecido")
                    .Length(14)
                    .WithMessage("O CNPJ precisa de 14 caracteres");
            });

            RuleFor(p => p.Type)
                .NotEmpty()
                .WithMessage("O campo tipo de fornecedor deve ser fornecido");
        }
    }
}

using Dev.Business.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.Business.Validations
{
    public class ProductValidator: AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage("O campo nome deve ser fornecido")
                .Length(2,200)
                .WithMessage("O campo precisa ter de {MinLenght} até {MaxLenght} caractéres");

            RuleFor(p => p.Description)
                .NotEmpty()
                .WithMessage("O campo descrição deve ser fornecido")
                .Length(0, 1000)
                .WithMessage("O campo precisa ter de {MinLenght} até {MaxLenght} caractéres");

            RuleFor(p => p.Value)
                .NotEmpty()
                .WithMessage("O campo valor deve ser fornecido")
                .GreaterThan(0)
                .WithMessage("O valor deve ser maior do que zero");
        }
    }
}

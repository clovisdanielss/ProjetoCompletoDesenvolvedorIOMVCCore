using Dev.Business.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.Business.Validations
{
    public class AddressValidator: AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(p => p.Street)
                .NotEmpty()
                .WithMessage("O campo logradouro deve ser fornecido")
                .Length(3, 200)
                .WithMessage("O campo logradouro precisa ter de {MinLenght} até {MaxLenght} caractéres");

            RuleFor(p => p.Number)
                .NotEmpty()
                .WithMessage("O campo número deve ser fornecido")
                .Length(1, 6)
                .WithMessage("O campo número precisa ter de {MinLenght} até {MaxLenght} caractéres");

            RuleFor(p => p.ZipCode)
                .NotEmpty()
                .WithMessage("O campo CEP deve ser fornecido")
                .Length(8)
                .WithMessage("O campo CEP precisa ter 8 caractéres");

            RuleFor(p => p.Neighboor)
                .NotEmpty()
                .WithMessage("O campo bairro deve ser fornecido")
                .Length(3, 20)
                .WithMessage("O campo bairro precisa ter de {MinLenght} até {MaxLenght} caractéres");

            RuleFor(p => p.City)
                .NotEmpty()
                .WithMessage("O campo cidade deve ser fornecido")
                .Length(3, 60)
                .WithMessage("O campo cidade precisa ter de {MinLenght} até {MaxLenght} caractéres");

            RuleFor(p => p.State)
                .NotEmpty()
                .WithMessage("O campo estado deve ser fornecido")
                .Length(2)
                .WithMessage("O campo cidade precisa ter 2 caractéres");
        }
    }
}

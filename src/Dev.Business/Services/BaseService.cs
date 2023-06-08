using Dev.Business.Interfaces;
using Dev.Business.Models;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.Business.Services
{
    public abstract class BaseService
    {
        private readonly INotifier _notifier;
        protected BaseService(INotifier notifier)
        {
            _notifier=notifier ?? throw new ArgumentNullException(nameof(notifier));
        }
        protected void Notify(ValidationResult val)
        {
            foreach(var error in val.Errors)
            {
                Notify(error);
            }
        }

        protected void Notify(ValidationFailure error)
        {
            _notifier.Handle(new Notification { Message = error.ErrorMessage });
        }

        protected bool Validate<TValidation, TEntity>(TValidation validation, TEntity entity) where TValidation: AbstractValidator<TEntity>
                                                                                              where TEntity : Entity
        {
            var validator = validation.Validate(entity);
            if (validator.IsValid) return true;
            Notify(validator);
            return false;
        }
    }
}

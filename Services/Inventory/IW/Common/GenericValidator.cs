using FluentValidation;

namespace IW.Common
{
    public abstract class GenericValidator<T>: AbstractValidator<T>
    {
        public GenericValidator() {

        }
        protected abstract void HandleValidateException(T instance);
    }
}

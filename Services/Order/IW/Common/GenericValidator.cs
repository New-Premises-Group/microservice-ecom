using FluentValidation;

namespace IW.Common
{
    public abstract class GenericValidator<T>: AbstractValidator<T>
    {
        public GenericValidator() {

        }
        protected abstract void HandleValidateException(T instance);
        protected bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }

    }
}

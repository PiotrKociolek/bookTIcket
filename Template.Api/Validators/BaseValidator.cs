using Template.Modules.Shared.Core.Enums;
using FluentValidation;

namespace Template.Api.Validators
{
    public class BaseValidator<T> : AbstractValidator<T>
    {
        public BaseValidator(){}

        protected bool ValidUserRole(string userRole)
        {
            if (Enum.IsDefined(typeof(RoleEnum), userRole))
            {
                return true;
            }
            return false;
        }
    }
}

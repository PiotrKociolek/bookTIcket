using System.Linq.Expressions;
using System.Reflection;

namespace Template.Modules.Shared.Application.Extensions
{
    public static class ApiExtensions
    {
        public static TModel Bind<TModel, TValue>(this TModel model, Expression<Func<TModel, TValue>> expression, TValue value)
        {
            if (expression.Body is MemberExpression memberSelectorExpression)
            {
                var property = memberSelectorExpression.Member as PropertyInfo;
                    
                if (property != null)
                {
                    property.SetValue(model, value);
                }
            }

            return model;
        }
    }
}
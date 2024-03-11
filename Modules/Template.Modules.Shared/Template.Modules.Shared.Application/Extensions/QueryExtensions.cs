using System.Collections;
using Template.Modules.Shared.Core.Exceptions;

namespace Template.Modules.Shared.Application.Extensions
{
    public static class QueryExtensions
    {
        public static async Task<T> OrFail<T>(this Task<T> task, string code = "not_found", string message = "error_not_found")
        {
            var instance = await task;       

            return instance.OrFail(code, message);
        }

        public static T OrFail<T>(this T instance, string code = "not_found", string message = "error_not_found")
        {           
            if (instance == null)
            {
                throw new NotFoundException(code, message);
            }
            
            if (instance is IEnumerable c && !c.GetEnumerator().MoveNext())
            {
                throw new NotFoundException(code, message);
            }           

            return instance;
        }
    }
}
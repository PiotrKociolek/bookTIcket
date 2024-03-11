using Template.Modules.Shared.Application.Types;
using Microsoft.EntityFrameworkCore;

namespace Template.Modules.Shared.Application.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<PagedResults<T>> ToPagedResultAsync<T>(
            this IQueryable<T> source,
            int skip,
            int raws,
            CancellationToken cancellationToken = default) where T : new()
        {
            var total = await source.CountAsync(cancellationToken);
            var items = await source.Skip(skip).Take(raws).ToListAsync(cancellationToken);
            return new PagedResults<T>(skip, raws, total, items);
        }

        public static async Task<PagedResults<TR>> ToPagedResultAsync<T, TR>(
            this IQueryable<T> source,
            int skip,
            int raws,
            Func<T, TR> converter,
            CancellationToken cancellationToken = default) where TR : new()
        {
            var total = await source.CountAsync(cancellationToken);
            var items = await source.Skip(skip).Take(raws).ToListAsync(cancellationToken);
            return new PagedResults<TR>(skip, raws, total, items.Select(converter).ToList());
        }
    }
}
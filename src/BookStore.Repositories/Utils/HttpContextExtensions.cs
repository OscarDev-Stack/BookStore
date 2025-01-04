using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
namespace BookStore.Repositories.Utils
{
    public static class HttpContextExtensions
    {
        public async static Task InsertPaginationHeader<T>(this HttpContext httpContext, IQueryable<T> queryable)
        {
            if(httpContext is null) throw new ArgumentNullException(nameof(httpContext));
            List<T> totalRecords = await queryable.ToListAsync();
            httpContext.Response.Headers.Add("x-total", totalRecords.Count.ToString());
        }
    }
}

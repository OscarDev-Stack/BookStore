using BookStore.Dto.Request;

namespace BookStore.Repositories.Utils
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PaginationRequestDto paginationDto)
        {
            return queryable.Skip((paginationDto.Page -1) * paginationDto.RecordsPerPage).Take(paginationDto.RecordsPerPage);
        }
    }
}

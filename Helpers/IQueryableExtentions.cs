using MoviesAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Helpers
{
    public static class IQueryableExtentions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PeginationDTO paginationDTO)
        {
            return queryable.Skip((paginationDTO.Page - 1) * (paginationDTO.RecordsPerPage))
                    .Take(paginationDTO.RecordsPerPage);
                
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Tesis.Helpers
{
    public class PagedList<T> : List<T>
    {
        public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        {
            CurrentPage = pageNumber;
            TotalPages = (int) Math.Ceiling(count/ (double) pageSize );
            PageSize = pageSize;
            TotalCount = count;
            AddRange(items);
        }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count =  source.Count(); // make it async //The provider for the source IQueryable doesn't implement IAsyncQueryProvider I was having this issue here that's why I take off the async from here.
            var items =  source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();// make it async
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}

using System;
using System.Linq;

namespace AJ.UtiliTools
{
    [Serializable]
    public class SearchResult<T>
    {
        public int Count { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public IQueryable<T> Results { get; set; }

        public SearchResult()
        {
            Count = 0;
            PageSize = 0;
            CurrentPage = 0;
            Results = null;
        }

        public SearchResult(IQueryable<T> results)
        {
            Count = 0;
            PageSize = 0;
            CurrentPage = 0;
            Results = results;
        }

        public SearchResult(int count, int pageSize, int currentPage)
        {
            Count = count;
            PageSize = pageSize;
            CurrentPage = currentPage;
            Results = null;
        }

        public SearchResult(int count, int pageSize, int currentPage, IQueryable<T> results)
        {
            Count = count;
            PageSize = pageSize;
            CurrentPage = currentPage;
            Results = results;
        }
    }

    public class SearchResultString
    {
        public String Result { get; set; }

        public SearchResultString()
        {
            Result = "";
        }

        public SearchResultString(String _result)
        {
            Result = _result;
        }
    }
}
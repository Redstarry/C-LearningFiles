using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsAPI.Models.PageModel
{
    public class PageInfo<T>:List<T>
    {
        //当前页数
        public int CurrentPage { get;private set; }
        //总页数
        public int TotalPages { get; private set; }
        //每一页的数据量
        public int PageSize { get; private set; }
        //总的数据量
        public int TotalCount { get; private set; }
        //判断是否有上一页
        public bool HasPrevious => CurrentPage > 1;
        //判断是否有下一页
        public bool HasNext => CurrentPage < TotalPages;

        //items 数据， count 总数量， pageNumber  当前页， pageSize 每一页的数量
        public PageInfo(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            CurrentPage = pageNumber;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }

        public static  PageInfo<T> Create(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            return new PageInfo<T>(items, count, pageNumber, pageSize);
        }
    }
}

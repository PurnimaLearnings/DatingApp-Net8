using API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace API.Helpers;

public class PagedList<T>: List<T>{
public PagedList(IEnumerable<T> items, int count, int pageNo, int pageSize){
CurrentPage=pageNo;
TotalPages=(int)Math.Ceiling(count/(double)pageSize);
PageSize=pageSize;
TotalCount=count;
AddRange(items);
}

public int CurrentPage { get; set; }
public int TotalPages { get; set; }
public int PageSize { get; set; }
public int TotalCount { get; set; }

public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNo, int PageSize){

var count = await source.CountAsync();
var items=await source.Skip((pageNo-1)*PageSize).Take(PageSize).ToListAsync();

return new PagedList<T>(items, count, pageNo, PageSize);
}


}
namespace Talabat.APIs.Helpers
{
    public class Pagination<T>
    {
        public Pagination(int pageSize, int pageIndex, int count,IReadOnlyList<T> Items) { 
           PageSize = pageSize;
           PageIndex = pageIndex;
           Data = Items;
            Count = count;
        }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<T> Data { get; set; }=new List<T>();
    }
}

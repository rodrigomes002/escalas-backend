namespace Escalas.Domain.Entities.Base
{
    public class PaginatedBase<T>
    {
        public List<T> Items { get; set; }
        public int TotalCount { get; set; }
    }
}

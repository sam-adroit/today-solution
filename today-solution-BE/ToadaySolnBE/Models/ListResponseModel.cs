namespace ToadaySolnBE.Models
{
    public class ListResponseModel<T>
    {
        public List<T> Model { get; set; }
        public PaginationModel Pagination { get; set; }
    }
}

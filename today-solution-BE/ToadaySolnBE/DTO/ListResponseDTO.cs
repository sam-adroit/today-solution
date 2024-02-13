using ToadaySolnBE.Models;
using ToadaySolnBE.Utils;

namespace ToadaySolnBE.DTO
{
    public class ListResponseDTO<T>
    {
        public ListResponseDTO(List<T> response, PaginationModel pagination) { 
            Response = response;
            Pagination = pagination;
        }
        public List<T> Response { get; set; }
        public PaginationModel Pagination { get; set; }
    }
}

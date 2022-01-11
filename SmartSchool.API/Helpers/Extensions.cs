using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SmartSchool.API.Helpers
{
    public static class Extensions
    {
        public static void AddPaginations(this HttpResponse response, int currentPage, int itemPerPage, int totalItems, int totalPages)
        {
            var paginationsHeader = new PaginationHeader(currentPage, itemPerPage, totalItems, totalPages);

            var camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();

            response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationsHeader, camelCaseFormatter));
            response.Headers.Add("Access-Control-Exponse-Header", "Paginations");
    }
}
}
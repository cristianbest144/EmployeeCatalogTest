namespace EmployeeCatalogApi.Models
{
    public class EmployeeFilterDto
    {
        public string? Name { get; set; } = null;
        public string? Position { get; set; } = null;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortColumn { get; set; } = "Name";
        public bool Ascending { get; set; } = true;
    }
}

namespace Presentation.Contracts.Companies
{
    public class CompanyPageRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public bool SortDesc { get; set; }
        public List<string>? IncludeProperties { get; set; }
        public string? NameFilter { get; set; }
        public string? StreetFilter { get; set; }
        public string? CityFilter { get; set; }
        public string? StateFilter { get; set; }
        public string? CountryFilter { get; set; }

    }
}

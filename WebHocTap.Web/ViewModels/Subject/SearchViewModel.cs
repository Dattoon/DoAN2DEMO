namespace WebHocTap.Web.ViewModels.Subject
{
    public class SearchViewModel
    {
        public string Keyword { get; set; }
        public List<SearchResultViewModel> Results { get; set; }
    }

    public class SearchResultViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
       
        public string DetailsUrl { get; set; }
    }
}

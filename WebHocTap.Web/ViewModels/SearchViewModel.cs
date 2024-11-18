using WebHocTap.Data.Entites;
using X.PagedList;

namespace WebHocTap.Web.Models
{
    public class SearchViewModel
    {
        public string Query { get; set; }
        public IPagedList<News> NewsResults { get; set; }
        public IPagedList<CategorySub> CourseResults { get; set; }
    }
}

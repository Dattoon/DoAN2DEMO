using System.Linq;
using WebHocTap.Data.Entites;
using WebHocTap.Data.Entites.Base;

namespace WebHocTap.Data.Reponsitory
{
    public class SearchRepository
    {
        private readonly WebHocTapDbContext _context;

        public SearchRepository(WebHocTapDbContext context)
        {
            _context = context;
        }

        public IQueryable<CategoryNew> SearchCategoryNew(string keyword)
        {
            return _context.categoryNews.Where(c => c.NameCategoryNew.Contains(keyword));
        }

        public IQueryable<CategorySub> SearchCategorySub(string keyword)
        {
            return _context.categorySubs.Where(c => c.NameCategorySub.Contains(keyword));
        }

        public IQueryable<Chapter> SearchChapter(string keyword)
        {
            return _context.chapters.Where(c => c.NameChapter.Contains(keyword));
        }

        public IQueryable<Lesson> SearchLesson(string keyword)
        {
            return _context.lessons.Where(l => l.Content.Contains(keyword) || l.Video.Contains(keyword));
        }

        public IQueryable<News> SearchNews(string keyword)
        {
            return _context.news.Where(n => n.Title.Contains(keyword) || n.Description.Contains(keyword));
        }

        public IQueryable<Role> SearchRole(string keyword)
        {
            return _context.roles.Where(r => r.RoleName.Contains(keyword));
        }

        public IQueryable<Subject> SearchSubject(string keyword)
        {
            return _context.subjects.Where(s => s.NameSubject.Contains(keyword));
        }

        public IQueryable<User> SearchUser(string keyword)
        {
            return _context.users.Where(u => u.UserName.Contains(keyword) || u.Gmail.Contains(keyword));
        }

        public IQueryable<Test> SearchTest(string keyword)
        {
            return _context.tests.Where(t => t.Question.Contains(keyword));
        }
    }
}

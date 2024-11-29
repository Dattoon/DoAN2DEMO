using WebHocTap.Data.Entites;
using WebHocTap.Data.Reponsitory;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace WebHocTap.Data.Repositories
{
    public class CommentRepository : BaseReponsitory
    {
        public CommentRepository(WebHocTapDbContext context, IHttpContextAccessor httpContext, ILogger<BaseReponsitory> logger)
            : base(context, httpContext, logger) { }

        public async Task AddCommentAsync(Comemt comment)
        {
            await _db.comemts.AddAsync(comment);
            await _db.SaveChangesAsync();
        }

        public async Task<List<Comemt>> GetCommentsByLessonIdAsync(int lessonId)
        {
            return await _db.comemts
                .Include(c => c.User)
                .Where(c => c.IdLesson == lessonId)
                .OrderByDescending(c => c.CreateAt)
                .ToListAsync();
        }
    }
}

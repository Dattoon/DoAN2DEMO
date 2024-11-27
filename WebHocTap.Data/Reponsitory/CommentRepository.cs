using WebHocTap.Data.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebHocTap.Data.Config;

namespace WebHocTap.Data.Repositories
{
    public class CommentRepository
    {
        private readonly WebHocTapDbContext _db;

        public CommentRepository(WebHocTapDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Lấy danh sách bình luận theo Id của bài học
        /// </summary>
        /// <param name="lessonId">Id bài học</param>
        /// <returns>Danh sách bình luận chưa bị xóa</returns>
        public async Task<IEnumerable<Comemt>> GetCommentsByLessonIdAsync(int lessonId)
        {
            if (lessonId <= 0)
                throw new ArgumentException("Lesson ID phải lớn hơn 0.", nameof(lessonId));

            return await _db.Set<Comemt>()
                            .AsNoTracking()
                            .Where(c => c.IdLesson == lessonId && c.DeleteAt == null)
                            .OrderByDescending(c => c.CreateAt) // Sắp xếp theo thời gian
                            .ToListAsync();
        }

        /// <summary>
        /// Thêm mới một bình luận
        /// </summary>
        /// <param name="comment">Đối tượng bình luận</param>
        /// <returns>Task hoàn thành sau khi lưu</returns>
        public async Task AddCommentAsync(Comemt comment)
        {
            if (comment == null)
                throw new ArgumentNullException(nameof(comment), "Comment không được null.");

            try
            {
                await _db.Set<Comemt>().AddAsync(comment);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Log lỗi (nếu cần thiết)
                throw new InvalidOperationException("Lỗi khi lưu bình luận vào cơ sở dữ liệu.", ex);
            }
        }
    }
}

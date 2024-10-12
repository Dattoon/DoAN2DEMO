using WebHocTap.Data.Entites.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebHocTap.Data.Reponsitory
{
    public partial class BaseReponsitory
    {
        public virtual async Task AddAsync<TEntity>(
           TEntity entity,
        bool isDeleted = false)
           where TEntity : BaseEntitty
        {
            this.BeforeAdd(entity, isDeleted);
            await _db.Set<TEntity>().AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// Nếu dữ liệu cần thêm > 1000 record mỗi lần thì không nên dùng hàm này
        /// </summary>
        public virtual async Task AddAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntitty
        {
            var len = entities.Count();
            for (int i = 0; i < len; i++)
            {
                this.BeforeAdd(entities.ElementAt(i));
            }
            await _db.AddRangeAsync(entities);
            await _db.SaveChangesAsync();
        }
    }
}

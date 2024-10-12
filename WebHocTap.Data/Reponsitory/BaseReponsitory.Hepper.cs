using WebHocTap.Data.Entites.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebHocTap.Data.Reponsitory
{
    public partial class BaseReponsitory
    {
        public WebHocTapDbContext DbContext { get => this._db; }

        protected string GetTableName<TEntity>()
        {
            return _db.Model.FindEntityType(typeof(TEntity)).GetTableName();
        }

        protected void BeforeAdd(BaseEntitty entity, bool isDeleted = false)
        {
            var now = DateTime.Now;
            entity.CreateAt = now;
            entity.UpdateAt = now;
            if (isDeleted)
            {
                entity.DeleteAt = now;
            }
            else
            {
                entity.DeleteAt = null;
            }
            if (_httpContext != null)
            {
                var userId = CurrentUserId();
                entity.CreateBy = userId;
            }
        }

        protected void BeforeUpdate(BaseEntitty entity)
        {
            var now = DateTime.Now;
            entity.UpdateAt = now;

        }

        protected int? CurrentUserId()
        {
            var nameIdentifier = _httpContext.HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier);
            if (nameIdentifier != null)
            {
                return Convert.ToInt32(nameIdentifier.Value);
            }
            return null;
        }

        protected void LogDebugQuery(IQueryable query)
        {
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                var queryString = query.ToQueryString();
                LogDebugQuery(queryString);
            }
        }
        protected void LogDebugQuery(string query)
        {
            _logger.LogDebug(query);
            Console.WriteLine($"{DateTime.Now:dd/MM/yyyy HH:mm:ss}\n{query}");
        }

        public virtual Expression<Func<TEntity, bool>> GetDefaultWhereExpr<TEntity>(bool selectFromTrash)
            where TEntity : BaseEntitty
        {
            Expression<Func<TEntity, bool>> defaultWhere;
            defaultWhere = selectFromTrash ? m => m.DeleteAt != null : m => m.DeleteAt == null;
            return defaultWhere;
        }

        public virtual Expression<Func<TEntity, bool>> GetDefaultWhereExprMst<TEntity>(bool selectFromTrash)
            where TEntity : BaseEntitty
        {
            Expression<Func<TEntity, bool>> defaultWhere;
            defaultWhere = selectFromTrash ? m => m.DeleteAt != null : m => m.DeleteAt == null;
            return defaultWhere;
        }
    }
}

﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using WebHocTap.Data.Entites.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WebHocTap.Data.Reponsitory
{
    public partial class BaseReponsitory
    {
        public virtual async Task<TEntity> FindAsync<TEntity>(
            int id,
            bool selectFromTrash = false)
            where TEntity : BaseEntitty
        {
            var defaultWhere = GetDefaultWhereExpr<TEntity>(selectFromTrash);
            var query = _db.Set<TEntity>()
                        .AsNoTracking()
                        .Where(defaultWhere)
                        .Where(m => m.Id == id);
            LogDebugQuery(query);
            return await query.FirstOrDefaultAsync();
        }
        public virtual async Task<TEntity> FindMstAsync<TEntity>(
            int id,
            bool selectFromTrash = false)
            where TEntity : BaseEntitty
        {
            var defaultWhere = GetDefaultWhereExprMst<TEntity>(selectFromTrash);
            var query = _db.Set<TEntity>()
                        .AsNoTracking()
                        .Where(defaultWhere)
                        .Where(m => m.Id == id);
            LogDebugQuery(query);
            return await query.FirstOrDefaultAsync();
        }
        public virtual async Task<TViewModel> FindAsync<TEntity, TViewModel>(
            int id,
            MapperConfiguration mapperConfig,
            bool selectFromTrash = false)
            where TEntity : BaseEntitty
        {
            var defaultWhere = GetDefaultWhereExpr<TEntity>(selectFromTrash);
            var query = _db.Set<TEntity>()
                        .AsNoTracking()
                        .Where(defaultWhere)
                        .Where(m => m.Id == id)
                        .ProjectTo<TViewModel>(mapperConfig);
            LogDebugQuery(query);
            return await query.SingleOrDefaultAsync();
        }

        public virtual async Task<TViewModel> GetOneAsync<TEntity, TViewModel>(
            int id,
            Expression<Func<TEntity, TViewModel>> selector,
            bool selectFromTrash = false)
            where TEntity : BaseEntitty
        {
            var defaultWhere = GetDefaultWhereExpr<TEntity>(selectFromTrash);
            var query = _db.Set<TEntity>()
                        .AsNoTracking()
                        .Where(defaultWhere)
                        .Where(m => m.Id == id)
                        .Select(selector);
            LogDebugQuery(query);
            return await query.SingleOrDefaultAsync();
        }

        public virtual async Task<TEntity> GetOneAsync<TEntity>(
            Expression<Func<TEntity, bool>> where,
            bool selectFromTrash = false)
            where TEntity : BaseEntitty
        {
            var defaultWhere = GetDefaultWhereExpr<TEntity>(selectFromTrash);
            var query = _db.Set<TEntity>()
                        .AsNoTracking()
                        .Where(defaultWhere)
                        .Where(where);
            LogDebugQuery(query);
            return await query.SingleOrDefaultAsync();
        }
        public virtual async Task<TEntity> GetOneOrderAsync<TEntity>(
        Expression<Func<TEntity, bool>> where,
        bool selectFromTrash = false)
        where TEntity : BaseEntitty
        {
            var defaultWhere = GetDefaultWhereExpr<TEntity>(selectFromTrash);
            var query = _db.Set<TEntity>()
                .OrderByDescending(o => o.CreateAt)
                        .AsNoTracking()
                        .Where(defaultWhere)
                        .Where(where);
            LogDebugQuery(query);
            return await query.FirstOrDefaultAsync();
        }

        public virtual async Task<TViewModel> GetOneAsync<TEntity, TViewModel>(
     Expression<Func<TEntity, bool>> where,
     MapperConfiguration mapperConfig,
     bool selectFromTrash = false)
     where TEntity : BaseEntitty
        {
            var defaultWhere = GetDefaultWhereExpr<TEntity>(selectFromTrash);
            var query = _db.Set<TEntity>()
                           .AsNoTracking()
                           .Where(defaultWhere)
                           .Where(where)
                           .ProjectTo<TViewModel>(mapperConfig);

            LogDebugQuery(query);
            var result = await query.SingleOrDefaultAsync();

            if (result == null)
            {
                // Handle the null value appropriately, for example, return a default instance or handle error
                return default;
            }

            return result;
        }

        public virtual async Task<TViewModel> GetOneAsync<TEntity, TViewModel>(
            Expression<Func<TEntity, bool>> where,
            Expression<Func<TEntity, TViewModel>> selector,
            bool selectFromTrash = false)
            where TEntity : BaseEntitty
        {
            var defaultWhere = GetDefaultWhereExpr<TEntity>(selectFromTrash);
            var query = _db.Set<TEntity>()
                           .AsNoTracking()
                           .Where(defaultWhere)
                           .Where(where)
                           .Select(selector);

            LogDebugQuery(query);
            var result = await query.SingleOrDefaultAsync();

            if (result == null)
            {
                // Handle the null value appropriately, for example, return a default instance or handle error
                return default;
            }

            return result;
        }



    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeeper.API.Helper
{
    public interface IHelper<TEntity, TModel>
    {
            int TotalPages { get; set; }
            int PageSize { get; set; }
            int Page { get; set; }
            int ItemCount { get; set; }

            IQueryable<TEntity> Sort(IQueryable<TEntity> query, int sort);
            List<TModel> Paginate(IQueryable<TEntity> query, int pageSize, int page);
            IQueryable<TEntity> Filter(IQueryable<TEntity> query, string filter);


        }
    }

}


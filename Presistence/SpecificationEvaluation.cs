using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts.SpecificationContracts;
using Microsoft.EntityFrameworkCore;

namespace Presistence
{
    static public class SpecificationEvaluation
    {
        static public IQueryable<T> ApplySpecification<T, TK>(this IQueryable<T> query, ISpecification<T, TK> specification) where T : Domain.Entities.BaseEntity<TK>
        {
            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }
            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            if (specification.OrderByDescending != null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }
            foreach (var include in specification.Includes)
            {
                query = query.Include(include);
            }
            return query;
        }
    }
}

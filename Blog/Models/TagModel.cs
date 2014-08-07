using System;
using System.Collections.Generic;
using System.Linq;
using AJ.UtiliTools;
using BC.Data;
using BC.Data.Context;
using LinqKit;

namespace BC.Models.Tag
{
    #region Services

    public interface ITagService
    {
        List<BC_Tag> Search(String name, int? PageSize, int? Page);
    }

    public class TagService : ITagService
    {
        private IRepository<BC_Tag> _genericRepository = new Repository<BC_Tag>(new DBDataContext());

        public List<BC_Tag> Search(String name, int? PageSize, int? Page)
        {
            var predicate = PredicateBuilder.True<BC_Tag>();

            if (name.HasValue())
            {
                predicate = predicate.And(p => p.Name.Contains(name));
            }

            IQueryable<BC_Tag> tags = _genericRepository.FindAll_Predicate(predicate, PageSize, Page, null).Results;

            return tags.GroupBy(t => t.Name).Select(g => g.First()).ToList();
        }
    }

    #endregion
}
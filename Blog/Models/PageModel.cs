using System;
using System.ComponentModel.DataAnnotations;
using AJ.UtiliTools;
using BC.Data;
using BC.Data.Context;
using BC.Models.Category;
using BC.Models.Page.Metadata;
using LinqKit;

namespace BC.Models
{
    [MetadataType(typeof(PageMetadata))]
    public partial class BC_Page
    {
        public String Blurb()
        {
            if (Body.Contains("<h5>"))
            {
                return Body.Substring(0, Body.IndexOf("<h5>"));
            }
            else if (Body.Contains("</p>"))
            {
                return Body.Substring(0, Body.IndexOf("</p>") + 4);
            }

            return Body;
        }
    }
}

namespace BC.Models.Page.Metadata
{
    public class PageMetadata
    {
        [Required()]
        [Unique()]
        [StringLength(256, ErrorMessage = "The Slug field may not be longer than 256 characters.")]
        public String Slug { get; set; }

        [Required()]
        [StringLength(256, ErrorMessage = "The Title field may not be longer than 256 characters.")]
        public String Title { get; set; }

        [Required()]
        public String Body { get; set; }

        [Display(Name = "Published")]
        public bool IsPublished { get; set; }

        public class UniqueAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                try
                {
                    IPageService PageService = new PageService();
                    BC_Page page = (BC_Page)validationContext.ObjectInstance;

                    if (page.PageID < 1)
                    {
                        if (PageService.GetPageBySlug(null, page.Slug) != null)
                        {
                            return new ValidationResult("The Slug field must be unique.");
                        }
                    }
                    else
                    {
                        BC_Page existingPage = PageService.GetPageBySlug(null, page.Slug);

                        if (existingPage != null)
                        {
                            if (existingPage.PageID != page.PageID)
                            {
                                return new ValidationResult("The Slug field must be unique.");
                            }
                        }
                    }
                }
                catch
                {
                    return new ValidationResult("The Slug field is invalid.");
                }

                return ValidationResult.Success;
            }
        }
    }
}

namespace BC.Models.Page
{
    #region Services

    public interface IPageService
    {
        SearchResult<BC_Page> Search(bool? published, String title, int? PageSize, int? Page);
        int Count(int? id, bool? published, String title);
        BC_Page GetPageByID(int id);
        BC_Page GetPageBySlug(bool? published, String slug);
        BC_Page GetInstance();
        BC_Page Insert(BC_Page page);
        BC_Page Update(BC_Page page);
        void Delete(BC_Page page);
    }

    public class PageService : IPageService
    {
        private IRepository<BC_Page> _genericRepository = new Repository<BC_Page>(new DBDataContext());

        public SearchResult<BC_Page> Search(bool? published, String title, int? PageSize, int? Page)
        {
            var predicate = PredicateBuilder.True<BC_Page>();

            if (published.HasValue)
            {
                predicate = predicate.And(p => p.IsPublished == published.Value);
            }

            if (!title.IsNullOrEmpty())
            {
                predicate = predicate.And(p => p.Title.Contains(title));
            }

            return _genericRepository.FindAll_Predicate(predicate, PageSize, Page, null);
        }

        public int Count(int? id, bool? published, String title)
        {
            var predicate = PredicateBuilder.True<BC_Page>();

            if (id.HasValue)
            {
                predicate = predicate.And(p => p.PageID == id.Value);
            }

            if (published.HasValue)
            {
                predicate = predicate.And(p => p.IsPublished == published.Value);
            }

            if (!title.IsNullOrEmpty())
            {
                predicate = predicate.And(p => p.Title.Contains(title));
            }

            return _genericRepository.Count(predicate);
        }

        public BC_Page GetPageByID(int id)
        {
            return _genericRepository.GetByID(id);
        }

        public BC_Page GetPageBySlug(bool? published, String slug)
        {
            ICategoryService CategoryService = new CategoryService();

            if (published.HasValue)
            {
                _genericRepository.First(p => String.Equals(p.Slug, slug, StringComparison.OrdinalIgnoreCase) && p.IsPublished == published);
            }

            return _genericRepository.First(p => String.Equals(p.Slug, slug, StringComparison.OrdinalIgnoreCase));
        }

        public BC_Page GetInstance()
        {
            BC_Page page = new BC_Page();

            page.IsPublished = true;

            return page;
        }

        public BC_Page Insert(BC_Page page)
        {
            if (page != null && page.PageID == 0)
            {
                page.Created = DateTime.UtcNow;
                page.Modified = DateTime.UtcNow;

                _genericRepository.Insert(page);
                _genericRepository.SaveAll();
            }

            return page;
        }

        public BC_Page Update(BC_Page page)
        {
            if (page != null && page.PageID != 0)
            {
                page.Modified = DateTime.UtcNow;

                _genericRepository.Update(page);
                _genericRepository.SaveAll();
            }

            return page;
        }

        public void Delete(BC_Page page)
        {
            if (page != null)
            {
                _genericRepository.MarkForDeletion(page);

                _genericRepository.SaveAll();
            }
        }
    }

    #endregion
}
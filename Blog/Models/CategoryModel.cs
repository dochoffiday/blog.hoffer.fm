using System;
using System.ComponentModel.DataAnnotations;
using AJ.UtiliTools;
using BC.Data;
using BC.Data.Context;
using BC.Models.Category.Metadata;
using LinqKit;

namespace BC.Models
{
    [MetadataType(typeof(CategoryMetadata))]
    public partial class BC_Category { }
}

namespace BC.Models.Category.Metadata
{
    public class CategoryMetadata
    {
        [Required()]
        [RegularExpression(@"^[a-zA-Z0-9\-]*$", ErrorMessage = "The Name field is invalid.")]
        [Unique(ErrorMessage = "Name must be unique.")]
        [StringLength(256, ErrorMessage = "The Name field may not be longer than 256 characters.")]
        public String Name { get; set; }

        public class UniqueAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                try
                {
                    ICategoryService CategoryService = new CategoryService();
                    BC_Category category = (BC_Category)validationContext.ObjectInstance;

                    if (category.CategoryID < 1)
                    {
                        if (CategoryService.Count(null, category.Name) > 0)
                        {
                            return new ValidationResult("The Name field must be unique.");
                        }
                    }
                    else
                    {
                        if (CategoryService.Count(null, category.Name) > 0)
                        {
                            if (CategoryService.Count(category.CategoryID, category.Name) == 0)
                            {
                                return new ValidationResult("The Name field must be unique.");
                            }
                        }
                    }
                }
                catch
                {
                    return new ValidationResult("The Name field is invalid.");
                }

                return ValidationResult.Success;
            }
        }
    }
}

namespace BC.Models.Category
{
    #region Services

    public interface ICategoryService
    {
        SearchResult<BC_Category> Search(String name);
        int Count(int? id, String name);
        BC_Category GetCategoryByID(int id);
        BC_Category GetCategoryByName(String name);
        BC_Category GetInstance();
        BC_Category Insert(BC_Category category);
        BC_Category Update(BC_Category category);
        void Delete(BC_Category category);
    }

    public class CategoryService : ICategoryService
    {
        private IRepository<BC_Category> _genericRepository = new Repository<BC_Category>(new DBDataContext());

        public SearchResult<BC_Category> Search(String name)
        {
            var predicate = PredicateBuilder.True<BC_Category>();

            if (!name.IsNullOrEmpty())
            {
                predicate = predicate.And(p => p.Name.Contains(name));
            }

            return _genericRepository.FindAll_Predicate(predicate, null, null, null);
        }

        public int Count(int? id, String name)
        {
            var predicate = PredicateBuilder.True<BC_Category>();

            if (id.HasValue)
            {
                predicate = predicate.And(p => p.CategoryID == id.Value);
            }

            if (!name.IsNullOrEmpty())
            {
                predicate = predicate.And(p => p.Name.Contains(name));
            }

            return _genericRepository.Count(predicate);
        }

        public BC_Category GetCategoryByID(int id)
        {
            return _genericRepository.GetByID(id);
        }

        public BC_Category GetCategoryByName(String name)
        {
            return _genericRepository.First(p => String.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        public BC_Category GetInstance()
        {
            BC_Category category = new BC_Category();

            return category;
        }

        public BC_Category Insert(BC_Category category)
        {
            if (category != null && category.CategoryID == 0)
            {
                _genericRepository.Insert(category);
                _genericRepository.SaveAll();
            }

            return category;
        }

        public BC_Category Update(BC_Category category)
        {
            if (category != null && category.CategoryID != 0)
            {
                _genericRepository.Update(category);
                _genericRepository.SaveAll();
            }

            return category;
        }

        public void Delete(BC_Category category)
        {
            if (category != null)
            {
                _genericRepository.MarkForDeletion(category);

                _genericRepository.SaveAll();
            }
        }
    }

    #endregion
}
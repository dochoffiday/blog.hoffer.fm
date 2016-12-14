using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using AJ.UtiliTools;
using BC.Data;
using BC.Data.Context;
using BC.Models.Category;
using BC.Models.Comment;
using BC.Models.Post;
using BC.Models.Post.Metadata;
using LinqKit;

namespace BC.Models
{
    [MetadataType(typeof(PostMetadata))]
    public partial class BC_Post
    {
        public DateTime LocalPublishDate
        {
            get
            {
                DateTime publishDate = DateTime.SpecifyKind(PublishDate, DateTimeKind.Utc);

                return publishDate.ToLocalTime();
            }
        }

        public List<BC_Category> Categories()
        {
            ICategoryService CategoryService = new CategoryService();

            return CategoryService.Search(null).Results.ToList();
        }

        public BC_Category Category()
        {
            ICategoryService CategoryService = new CategoryService();

            return CategoryService.GetCategoryByID(CategoryID);
        }

        public IQueryable<BC_Tag> Tags()
        {
            IPostService PostService = new PostService();

            return PostService.Tag_All(this);
        }

        public String Tags_AsString()
        {
            IPostService PostService = new PostService();
            StringBuilder sb = new StringBuilder();

            List<BC_Tag> tags = Tags().ToList();

            for(int r = 0; r < tags.Count; r++)
            {
                sb.Append(tags[r].Name);

                if(r < tags.Count - 1)
                {
                    sb.Append(", ");
                }
            }

            return sb.ToString();
        }

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

        public int Comment_Count()
        {
            ICommentService CommentService = new CommentService();

            return CommentService.Count(null, PostID, null);
        }

        public int Comment_Unread_Count()
        {
            ICommentService CommentService = new CommentService();

            return CommentService.Count(null, PostID, false);
        }
    }
}

namespace BC.Models.Post.Metadata
{
    public class PostMetadata
    {
        [Display(Name = "Category")]
        [Required()]
        [Range(0, Int32.MaxValue, ErrorMessage = "The Category field is required.")]
        public int CategoryID { get; set; }

        [Required()]
        [Unique()]
        [StringLength(256, ErrorMessage = "The Slug field may not be longer than 256 characters.")]
        public String Slug { get; set; }

        [Required()]
        [StringLength(256, ErrorMessage = "The Title field may not be longer than 256 characters.")]
        public String Title { get; set; }

        [Required()]
        public String Body { get; set; }

        [Display(Name = "Comments Are Open")]
        public bool IsCommentsOpen { get; set; }

        [Display(Name = "Show Comments")]
        public bool IsCommentsVisible { get; set; }

        [Display(Name = "Published")]
        public bool IsPublished { get; set; }

        [DisplayName("Publish Date")]
        public DateTime PublishDate { get; set; }

        public class UniqueAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                try
                {
                    IPostService PostService = new PostService();
                    ICategoryService CategoryService = new CategoryService();
                    BC_Post post = (BC_Post)validationContext.ObjectInstance;
                    BC_Category category = CategoryService.GetCategoryByID(post.CategoryID);

                    if (post.PostID < 1)
                    {
                        if (PostService.GetPostBySlug(null, category.Name, post.Slug) != null)
                        {
                            return new ValidationResult("The Slug field must be unique.");
                        }
                    }
                    else
                    {
                        BC_Post existingPost = PostService.GetPostBySlug(null, category.Name, post.Slug);

                        if (existingPost != null)
                        {
                            if (existingPost.PostID != post.PostID)
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

namespace BC.Models.Post
{
    #region Services

    public interface IPostService
    {
        SearchResult<BC_Post> Search(bool? published, int? categoryID, String title, int? PageSize, int? Page);
        SearchResult<BC_Post> GetByTag(bool? published, String tagName, int? PageSize, int? Page);
        int Count(int? id, bool? published, int? categoryID, String title);
        BC_Post GetPostByID(int id);
        BC_Post GetPostBySlug(bool? published, String categoryName, String slug);
        BC_Post GetInstance();
        BC_Post Insert(BC_Post post, String tags);
        BC_Post Update(BC_Post post, String tags);
        void Tag_Update(BC_Post post, String tags);
        IQueryable<BC_Tag> Tag_All(BC_Post post);
        void Delete(BC_Post post);
    }

    public class PostService : IPostService
    {
        private IRepository<BC_Post> _genericRepository = new Repository<BC_Post>(new DBDataContext());
        private IRepository<BC_Tag> _genericTagRepository = new Repository<BC_Tag>(new DBDataContext());
        private IRepository<BC_Comment> _genericCommentRepository = new Repository<BC_Comment>(new DBDataContext());

        public SearchResult<BC_Post> Search(bool? published, int? categoryID, String title, int? PageSize, int? Page)
        {
            var predicate = PredicateBuilder.True<BC_Post>();

            if (published.HasValue)
            {
                predicate = predicate.And(p => p.IsPublished == published.Value);
            }

            if (categoryID.HasValue)
            {
                predicate = predicate.And(p => p.CategoryID == categoryID.Value);
            }

            if (!title.IsNullOrEmpty())
            {
                predicate = predicate.And(p => p.Title.Contains(title));
            }

            OrderByGroup orderBy = new OrderByGroup();
            orderBy.Add(OrderByType.Desc, "PublishDate");

            return _genericRepository.FindAll_Predicate(predicate, PageSize, Page, orderBy);
        }

        public SearchResult<BC_Post> GetByTag(bool? published, String tagName, int? PageSize, int? Page)
        {
            List<int> postIDs = new List<int>();
            
            foreach (BC_Tag tag in _genericTagRepository.FindAll(t => String.Equals(t.Name, tagName, StringComparison.OrdinalIgnoreCase), null, null, null).Results)
            {
                if (!postIDs.Contains(tag.PostID))
                {
                    postIDs.Add(tag.PostID);
                }
            }

            var predicate = PredicateBuilder.True<BC_Post>();

            if (published.HasValue)
            {
                predicate = predicate.And(p => p.IsPublished == published.Value);
            }

            predicate = predicate.And(p => postIDs.Contains(p.PostID));

            OrderByGroup orderBy = new OrderByGroup();
            orderBy.Add(OrderByType.Desc, "PublishDate");

            return _genericRepository.FindAll_Predicate(predicate, PageSize, Page, orderBy);
        }

        public int Count(int? id, bool? published, int? categoryID, String title)
        {
            var predicate = PredicateBuilder.True<BC_Post>();

            if (id.HasValue)
            {
                predicate = predicate.And(p => p.PostID == id.Value);
            }

            if (published.HasValue)
            {
                predicate = predicate.And(p => p.IsPublished == published.Value);
            }

            if (categoryID.HasValue)
            {
                predicate = predicate.And(p => p.CategoryID == categoryID.Value);
            }

            if (!title.IsNullOrEmpty())
            {
                predicate = predicate.And(p => p.Title.Contains(title));
            }

            return _genericRepository.Count(predicate);
        }

        public BC_Post GetPostByID(int id)
        {
            return _genericRepository.GetByID(id);
        }

        public BC_Post GetPostBySlug(bool? published, String categoryName, String slug)
        {
            ICategoryService CategoryService = new CategoryService();

            BC_Category category = CategoryService.GetCategoryByName(categoryName);

            if(category == null) return null;

            if (published.HasValue)
            {
                return _genericRepository.First(p => String.Equals(p.Slug, slug, StringComparison.OrdinalIgnoreCase) && p.CategoryID == category.CategoryID && p.IsPublished == published.Value);
            }

            return _genericRepository.First(p => String.Equals(p.Slug, slug, StringComparison.OrdinalIgnoreCase) && p.CategoryID == category.CategoryID);
        }

        public BC_Post GetInstance()
        {
            BC_Post post = new BC_Post();

            post.PublishDate = DateTime.UtcNow;
            post.IsPublished = true;

            return post;
        }

        public BC_Post Insert(BC_Post post, String tags)
        {
            if (post != null && post.PostID == 0)
            {
                post.Created = DateTime.UtcNow;
                post.Modified = DateTime.UtcNow;

                _genericRepository.Insert(post);
                _genericRepository.SaveAll();
            }

            Tag_Update(post, tags);

            return post;
        }

        public BC_Post Update(BC_Post post, String tags)
        {
            if (post != null && post.PostID != 0)
            {
                post.Modified = DateTime.UtcNow;

                _genericRepository.Update(post);
                _genericRepository.SaveAll();
            }

            Tag_Update(post, tags);

            return post;
        }

        public void Tag_Update(BC_Post post, String tags)
        {
            List<String> tagList = new List<String>(tags.Split(","));

            foreach (BC_Tag tag in _genericTagRepository.FindAll(t => t.PostID == post.PostID, null, null, null).Results)
            {
                if (!tagList.Contains(tag.Name))
                {
                    _genericTagRepository.MarkForDeletion(tag);
                }
                else
                {
                    tagList.Remove(tag.Name);
                }
            }

            foreach (String tag in tagList)
            {
                BC_Tag newTag = new BC_Tag();

                newTag.PostID = post.PostID;
                newTag.Name = tag.Truncate(64);

                _genericTagRepository.Insert(newTag);
            }

            _genericTagRepository.SaveAll();
        }
        
        public IQueryable<BC_Tag> Tag_All(BC_Post post)
        {
            return _genericTagRepository.FindAll(t => t.PostID == post.PostID, null, null, null).Results;
        }

        public IQueryable<BC_Comment> Comment_All(BC_Post post)
        {
            return _genericCommentRepository.FindAll(t => t.PostID == post.PostID, null, null, null).Results;
        }

        public void Delete(BC_Post post)
        {
            if (post != null)
            {
                foreach (BC_Tag tag in Tag_All(post))
                {
                    _genericTagRepository.MarkForDeletion(tag);
                }

                foreach (BC_Comment comment in Comment_All(post))
                {
                    _genericCommentRepository.MarkForDeletion(comment);
                }

                _genericTagRepository.SaveAll();
                _genericCommentRepository.SaveAll();

                _genericRepository.MarkForDeletion(post);

                _genericRepository.SaveAll();
            }
        }
    }

    #endregion
}
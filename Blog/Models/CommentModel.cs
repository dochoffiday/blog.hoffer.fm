using System;
using System.ComponentModel.DataAnnotations;
using AJ.UtiliTools;
using BC.Data;
using BC.Data.Context;
using BC.Models.Comment.Metadata;
using LinqKit;

namespace BC.Models
{
    [MetadataType(typeof(CommentMetadata))]
    public partial class BC_Comment
    {
        public DateTime LocalCreated
        {
            get
            {
                DateTime created = DateTime.SpecifyKind(Created, DateTimeKind.Utc);

                return created.ToLocalTime();
            }
        }

    }
}

namespace BC.Models.Comment.Metadata
{
    public class CommentMetadata
    {
        [Required()]
        [StringLength(256, ErrorMessage = "The Name field may not be longer than 256 characters.")]
        public String Name { get; set; }

        [Email(ErrorMessage = "The Email field is invalid.")]
        [StringLength(256, ErrorMessage = "The Email field may not be longer than 256 characters.")]
        public String Email { get; set; }

        [StringLength(256, ErrorMessage = "The Url field may not be longer than 256 characters.")]
        public String Url { get; set; }

        [Required()]
        public String Text { get; set; }

        [Display(Name = "Administrator")]
        public bool IsUser { get; set; }

        [Display(Name = "Is Read")]
        public bool IsRead { get; set; }
    }
}

namespace BC.Models.Comment
{
    #region Services

    public interface ICommentService
    {
        SearchResult<BC_Comment> Search(int? postID, bool? isRead, int? PageSize, int? Page);
        int Count(int? id, int? postID, bool? isRead);
        BC_Comment GetCommentByID(int id);
        BC_Comment GetInstance();
        BC_Comment Insert(BC_Comment comment);
        BC_Comment Update(BC_Comment comment);
        void Delete(BC_Comment comment);
        void Save();
    }

    public class CommentService : ICommentService
    {
        private IRepository<BC_Comment> _genericRepository = new Repository<BC_Comment>(new DBDataContext());

        public SearchResult<BC_Comment> Search(int? postID, bool? isRead, int? PageSize, int? Page)
        {
            var predicate = PredicateBuilder.True<BC_Comment>();

            if (postID.HasValue)
            {
                predicate = predicate.And(p => p.PostID == postID.Value);
            }

            if (isRead.HasValue)
            {
                predicate = predicate.And(p => p.IsRead == isRead.Value);
            }

            OrderByGroup orderBy = new OrderByGroup();
            orderBy.Add(OrderByType.Asc, "CommentID");

            return _genericRepository.FindAll_Predicate(predicate, PageSize, Page, orderBy);
        }

        public int Count(int? id, int? postID, bool? isRead)
        {
            var predicate = PredicateBuilder.True<BC_Comment>();

            if (id.HasValue)
            {
                predicate = predicate.And(p => p.CommentID == id.Value);
            }

            if (postID.HasValue)
            {
                predicate = predicate.And(p => p.PostID == postID.Value);
            }

            if (isRead.HasValue)
            {
                predicate = predicate.And(p => p.IsRead == isRead.Value);
            }

            return _genericRepository.Count(predicate);
        }

        public BC_Comment GetCommentByID(int id)
        {
            return _genericRepository.GetByID(id);
        }

        public BC_Comment GetInstance()
        {
            BC_Comment comment = new BC_Comment();

            return comment;
        }

        public BC_Comment Insert(BC_Comment comment)
        {
            if (comment != null && comment.CommentID == 0)
            {
                comment.Created = DateTime.UtcNow;
                comment.Modified = DateTime.UtcNow;
                comment.IsRead = false;

                _genericRepository.Insert(comment);
                _genericRepository.SaveAll();
            }

            return comment;
        }

        public BC_Comment Update(BC_Comment comment)
        {
            if (comment != null && comment.CommentID != 0)
            {
                comment.Modified = DateTime.UtcNow;

                _genericRepository.Update(comment);
                _genericRepository.SaveAll();
            }

            return comment;
        }

        public void Delete(BC_Comment comment)
        {
            if (comment != null)
            {
                _genericRepository.MarkForDeletion(comment);

                _genericRepository.SaveAll();
            }
        }

        public void Save()
        {
            _genericRepository.SaveAll();
        }
    }

    #endregion
}
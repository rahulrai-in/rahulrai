﻿namespace RahulRai.Websites.Utilities.Common.Entities
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Helpers;
    using RegularTypes;

    #endregion

    /// <summary>
    ///     Class TableBlogEntity.
    /// </summary>
    public class TableBlogEntity
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="TableBlogEntity" /> class.
        /// </summary>
        public TableBlogEntity()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TableBlogEntity" /> class.
        /// </summary>
        /// <param name="post">The post.</param>
        public TableBlogEntity(BlogPost post)
        {
            Title = post.Title;
            Body = post.Body.SplitByLength(ApplicationConstants.ContentSplitLength).ToList();
            FormattedUri = post.BlogFormattedUri;
            BlogKey = post.BlogKey;
            BlogId = post.BlogId;
            CategoriesCsv = post.CategoriesCsv;
            PostedDate = post.PostedDate == DateTime.MinValue ? DateTime.UtcNow : post.PostedDate;
            EntityTag = post.EntityTag;
            IsDraft = post.IsDraft;
            IsDeleted = post.IsDeleted;
        }

        /// <summary>
        ///     Gets or sets the formatted URI.
        /// </summary>
        /// <value>The formatted URI.</value>
        public string FormattedUri { get; set; }

        /// <summary>
        ///     Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        ///     Gets or sets the body.
        /// </summary>
        /// <value>The body.</value>
        public IList<string> Body { get; set; }

        /// <summary>
        ///     Gets or sets the blog key.
        /// </summary>
        /// <value>The blog key.</value>
        public string BlogKey { get; set; }

        /// <summary>
        ///     Gets or sets the blog identifier.
        /// </summary>
        /// <value>The blog identifier.</value>
        public string BlogId { get; set; }

        /// <summary>
        ///     Gets or sets the posted date.
        /// </summary>
        /// <value>The posted date.</value>
        public DateTime PostedDate { get; set; }

        /// <summary>
        ///     Gets or sets the categories CSV.
        /// </summary>
        /// <value>The categories CSV.</value>
        public string CategoriesCsv { get; set; }

        /// <summary>
        ///     Gets or sets the entity tag.
        /// </summary>
        /// <value>The entity tag.</value>
        public string EntityTag { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is draft.
        /// </summary>
        /// <value><c>true</c> if this instance is draft; otherwise, <c>false</c>.</value>
        public bool IsDraft { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value><c>true</c> if this instance is deleted; otherwise, <c>false</c>.</value>
        public bool IsDeleted { get; set; }

        /// <summary>
        ///     Gets the blog post.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>BlogPost.</returns>
        public static BlogPost GetBlogPost(TableBlogEntity entity)
        {
            if (null == entity)
            {
                return null;
            }

            return new BlogPost
            {
                Title = entity.Title,
                BlogId = entity.BlogId,
                Body = entity.Body.Combine(),
                PostedDate = entity.PostedDate,
                EntityTag = entity.EntityTag,
                CategoriesCsv = entity.CategoriesCsv,
                IsDraft = entity.IsDraft,
                IsDeleted = entity.IsDeleted
            };
        }
    }
}
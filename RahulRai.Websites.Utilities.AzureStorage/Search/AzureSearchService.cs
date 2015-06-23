﻿namespace RahulRai.Websites.Utilities.AzureStorage.Search
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Common.Entities;
    using Common.Exceptions;
    using Microsoft.Azure.Search;
    using Microsoft.Azure.Search.Models;

    #endregion

    /// <summary>
    ///     Class AzureSearchService.
    /// </summary>
    public class AzureSearchService : IDisposable
    {
        /// <summary>
        ///     The index client
        /// </summary>
        private readonly SearchIndexClient indexClient;

        /// <summary>
        ///     The service client
        /// </summary>
        private readonly SearchServiceClient serviceClient;

        /// <summary>
        ///     The disposed
        /// </summary>
        private bool disposed;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AzureSearchService" /> class.
        /// </summary>
        /// <param name="searchServiceName">Name of the search service.</param>
        /// <param name="searchServiceKey">The search service key.</param>
        /// <param name="searchIndex">Index of the search.</param>
        public AzureSearchService(string searchServiceName, string searchServiceKey, string searchIndex)
        {
            serviceClient = new SearchServiceClient(searchServiceName, new SearchCredentials(searchServiceKey));
            CreateIndexIfNotExists(searchIndex, serviceClient);
            indexClient = serviceClient.Indexes.GetClient(searchIndex);
        }

        /// <summary>
        ///     Prevents a default instance of the <see cref="AzureSearchService" /> class from being created.
        /// </summary>
        private AzureSearchService()
        {
            //// Default constructor not allowed.
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Finalizes an instance of the <see cref="AzureSearchService" /> class.
        /// </summary>
        ~AzureSearchService()
        {
            Dispose(false);
        }

        /// <summary>
        ///     Creates the index if not exists.
        /// </summary>
        /// <param name="searchIndex">Index of the search.</param>
        /// <param name="serviceClient">The service client.</param>
        private static void CreateIndexIfNotExists(string searchIndex, ISearchServiceClient serviceClient)
        {
            if (!serviceClient.Indexes.Exists(searchIndex))
            {
                CreateSearchIndex(searchIndex, serviceClient);
            }
        }

        /// <summary>
        ///     Creates the index of the search.
        /// </summary>
        /// <param name="searchIndex">Index of the search.</param>
        /// <param name="serviceClient">The service client.</param>
        private static void CreateSearchIndex(string searchIndex, ISearchServiceClient serviceClient)
        {
            var definition = new Index
            {
                Name = searchIndex,
                Fields = new[]
                {
                    new Field("blogId", DataType.String)
                    {
                        IsKey = true
                    },
                    new Field("title", DataType.String)
                    {
                        IsSearchable = true,
                        IsFilterable = true
                    },
                    new Field("searchTags", DataType.Collection(DataType.String))
                    {IsSearchable = true, IsFilterable = true, IsFacetable = true}
                }
            };

            serviceClient.Indexes.Create(definition);
        }

        /// <summary>
        ///     Upserts the index of the data to.
        /// </summary>
        /// <param name="blogSearchEntity">The blog search entity.</param>
        /// <exception cref="RahulRai.Websites.Utilities.Common.Exceptions.BlogSystemException">failed on index</exception>
        public void UpsertDataToIndex(BlogSearch blogSearchEntity)
        {
            var documents =
                new[] {blogSearchEntity};

            try
            {
                indexClient.Documents.Index(IndexBatch.Create(documents.Select(IndexAction.Create)));
            }
            catch (IndexBatchException e)
            {
                // Sometimes when your Search service is under load, indexing will fail for some of the documents in
                // the batch. Depending on your application, you can take compensating actions like delaying and
                // retrying. For this simple demo, we just log the failed document keys and continue.
                Trace.WriteLine(
                    "Failed to index some of the documents: {0}",
                    String.Join(", ", e.IndexResponse.Results.Where(r => !r.Succeeded).Select(r => r.Key)));
                throw new BlogSystemException("failed on index");
            }
        }

        /// <summary>
        ///     Searches the documents.
        /// </summary>
        /// <param name="searchText">The search text.</param>
        /// <param name="filter">The filter.</param>
        /// <returns>IEnumerable&lt;BlogSearch&gt;.</returns>
        public IEnumerable<BlogSearch> SearchDocuments(string searchText, string filter = null)
        {
            // Execute search based on search text and optional filter
            var searchParameter = new SearchParameters();

            if (!String.IsNullOrEmpty(filter))
            {
                searchParameter.Filter = filter;
            }

            var response = indexClient.Documents.Search<BlogSearch>(searchText, searchParameter);
            return response.Select(result => result.Document);
        }

        /// <summary>
        ///     Deletes the data.
        /// </summary>
        /// <param name="idToDelete">The identifier to delete.</param>
        /// <exception cref="RahulRai.Websites.Utilities.Common.Exceptions.BlogSystemException">failed on index</exception>
        public void DeleteData(string idToDelete)
        {
            try
            {
                var batch = IndexBatch.Create(
                    new IndexAction(IndexActionType.Delete, new Document {{"blogId", idToDelete}}));
                indexClient.Documents.Index(batch);
            }
            catch (IndexBatchException e)
            {
                // Sometimes when your Search service is under load, indexing will fail for some of the documents in
                // the batch. Depending on your application, you can take compensating actions like delaying and
                // retrying. For this simple demo, we just log the failed document keys and continue.
                Trace.WriteLine(
                    "Failed to index some of the documents: {0}",
                    String.Join(", ", e.IndexResponse.Results.Where(r => !r.Succeeded).Select(r => r.Key)));
                throw new BlogSystemException("failed on index");
            }
        }

        /// <summary>
        ///     Deletes the index.
        /// </summary>
        /// <param name="indexName">Name of the index.</param>
        public void DeleteIndex(string indexName)
        {
            if (serviceClient.Indexes.Exists(indexName))
            {
                serviceClient.Indexes.Delete(indexName);
            }
        }

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
        ///     unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    indexClient.Dispose();
                    serviceClient.Dispose();
                }
            }
            disposed = true;
        }
    }
}
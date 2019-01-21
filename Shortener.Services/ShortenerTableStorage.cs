﻿using Microsoft.WindowsAzure.Storage.Table;
using Shortener.Models;
using System;
using System.Threading.Tasks;

namespace Shortener.Services
{
    public class ShortenerTableStorage : IRepository
    {
        private CloudTable _cloudTable;

        public ShortenerTableStorage(CloudTableClient cloudTableClient)
        {
            _cloudTable = cloudTableClient.GetTableReference("Shortener");
            _cloudTable.CreateIfNotExistsAsync();
        }

        /// <summary>
        /// Add Url to Azure Table Storage
        /// </summary>
        /// <param name="urlShortener"></param>
        /// <returns></returns>
        public async Task AddUrl(UrlShortener urlShortener)
        {
            // TODO: To rethink the use of partition key and row key.
            var shortenerEntity = new UrlShortenerEntity(urlShortener.UrlId.Substring(0, 1), urlShortener.UrlId)
            {
                LongUrl = urlShortener.LongUrl
            };
            var insertOperation = TableOperation.Insert(shortenerEntity);

            await _cloudTable.ExecuteAsync(insertOperation);
        }
    }
}

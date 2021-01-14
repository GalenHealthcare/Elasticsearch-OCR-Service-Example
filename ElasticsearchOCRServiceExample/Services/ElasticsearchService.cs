using System.Threading.Tasks;
using Nest;
using ElasticsearchOCRServiceExample.Models;
using System;

namespace ElasticsearchOCRServiceExample.Services
{
    public class ElasticsearchService
    {
        private string _indexName { get; set; }
        private ElasticClient _client { get; set; }

        public ElasticsearchService(string uri, string indexName)
        {
            _indexName = indexName;

            Uri node = new Uri(uri);
            ConnectionSettings settings = new ConnectionSettings(node);

            _client = new ElasticClient(settings);
        }

        public async Task IndexScannedDocument(string fileName, string content)
        {
            DocumentScan scan = new DocumentScan
            {
                Id = Guid.NewGuid().ToString(),
                FileName = fileName,
                Content = content,
                ScanDate = DateTime.Now,
            };

            await _client.IndexAsync(scan, idx => idx.Index(_indexName));
        }
    }
}

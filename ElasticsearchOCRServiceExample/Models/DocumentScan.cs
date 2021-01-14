using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticsearchOCRServiceExample.Models
{
    public class DocumentScan
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public string Content { get; set; }
        public DateTime ScanDate { get; set; }
    }
}

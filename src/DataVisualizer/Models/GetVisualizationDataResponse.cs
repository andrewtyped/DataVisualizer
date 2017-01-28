using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataVisualizer.Models
{
    public class GetVisualizationDataResponse
    {
        public string DataSourceId { get; set; }
        public int DataSourceVisualizationId { get; set; }
        public string DataSourceVisualizationDescription { get; set; }

        public IEnumerable<RecordResponse> Records {get; set;}
    }

    public class RecordResponse
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public IEnumerable<RecordPropertyResponse> Properties { get; set; }
    }

    public class RecordPropertyResponse
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}

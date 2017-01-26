using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataVisualizer.Models
{
    public class SaveRecordRequest
    {
        public string DataSourceId { get; set; }
        public int DataSourceVisualizationId { get; set; }
        public Dictionary<string, string> Properties { get; set; }
    }
}

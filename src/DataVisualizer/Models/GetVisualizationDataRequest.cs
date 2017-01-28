using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataVisualizer.Models
{
    public class GetVisualizationDataRequest
    {
        public string DataSourceId { get; set; }
        public int DataSourceVisualizationId { get; set; }

        /// <summary>
        /// The minimum date (inclusive) from which to return records
        /// </summary>
        public DateTime? MinDate { get; set; }

        /// <summary>
        /// The maximum date (exclusive) before which to return records
        /// </summary>
        public DateTime? MaxDate { get; set; }

    }
}

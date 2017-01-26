using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DataVisualizer.Persistence.Entity
{
    /// <summary>
    /// Represents a visualization configured for a specific data source.
    /// </summary>
    public class DataSourceVisualization
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DataSource DataSource { get; set; }
        public Visualization Visualization { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<DataSourceVisualizationProperty> Properties { get; set; }
    }
}

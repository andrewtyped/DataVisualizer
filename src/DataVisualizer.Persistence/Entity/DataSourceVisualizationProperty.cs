using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DataVisualizer.Persistence.Entity
{
    public class DataSourceVisualizationProperty
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DataSourceVisualization DataSourceVisualization { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}

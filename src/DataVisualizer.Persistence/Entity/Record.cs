using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DataVisualizer.Persistence.Entity
{
    /// <summary>
    /// Defines a data point on a <see cref="DataSourceVisualization"/> 
    /// </summary>
    public class Record
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DataSource DataSource { get; set; }
        public DataSourceVisualization DataSourceVisualization { get; set; }

        public ICollection<RecordProperty> Properties { get; set; }

        public DateTime DateCreated { get; set; }
    }
}

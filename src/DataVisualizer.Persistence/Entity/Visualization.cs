using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DataVisualizer.Persistence.Entity
{
    /// <summary>
    /// Represents a type of Visualization
    /// </summary>
    public class Visualization
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Type { get; set; }
        public ICollection<VisualizationProperty> Properties { get; set; }
    }
}

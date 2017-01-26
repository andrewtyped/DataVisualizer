using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataVisualizer.Persistence.Entity
{
    public class DataSource
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        public string Description { get; set; }

        public ICollection<DataSourceVisualization> DataSourceVisualizations { get; set; }

        public ICollection<Record> Records { get; set; }
    }
}

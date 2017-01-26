using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataVisualizer.Models
{
    public class DataSourceRegistrationRequest
    {
        /// <summary>
        /// Gets or sets the data source Id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the data source's location
        /// </summary>
        public string Description { get; set; }

        public VisualizationOptions Visualization { get; set; }
    }

    public class VisualizationOptions
    {
        public string Name { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the type of visualization
        /// </summary>
        public VisualizationType Type { get; set; }

        /// <summary>
        /// Gets or sets a dictionary of maps from datasource properties to visualization properties.
        /// See <see cref="VisualizationType"/> for supported visualization properties 
        /// Key: The data source property
        /// Value: The visualization property
        /// </summary>
        public Dictionary<string, string> DataSourceToVisualizationPropertyMap { get; set; }

    }

    public enum VisualizationType
    {
        /// <summary>
        /// A Line Chart.
        /// 
        /// Properties
        /// ----------
        /// 
        /// "labels" - Values to display on the chart's independant axis
        /// "data" - Values to display on the chart's dependant axis
        /// </summary>
        [Display(Name = "Line Chart")]
        LineChart
    }
}

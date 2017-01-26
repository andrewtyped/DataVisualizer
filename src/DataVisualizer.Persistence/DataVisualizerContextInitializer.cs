using DataVisualizer.Persistence.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataVisualizer.Persistence
{
    public static class DataVisualizerContextInitializer
    {
        public static void Initialize(DataVisualizerContext dataVisualizerContext)
        {
            dataVisualizerContext.Database.EnsureCreated();

            if(dataVisualizerContext.Visualizations.Any())
            {
                return;
            }

            var visualizations = new Visualization[]
            {
                new Visualization
                {
                    Type = "LineChart",
                    Properties = new VisualizationProperty[]
                    {
                        new VisualizationProperty { Value = "labels" },
                        new VisualizationProperty { Value = "data" },
                    }
                }
            };

            foreach(var visualization in visualizations)
            {
                dataVisualizerContext.Add(visualization);
            }

            dataVisualizerContext.SaveChanges();
        }
    }
}

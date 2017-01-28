using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataVisualizer.Models;
using DataVisualizer.Persistence;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DataVisualizer.Controllers
{
    public class DataReporterController : Controller
    {
        private readonly DataVisualizerContext dataVisualizerContext;
        public DataReporterController(DataVisualizerContext dataVisualizerContext)
        {
            if (dataVisualizerContext == null)
            {
                throw new ArgumentNullException(nameof(dataVisualizerContext));
            }

            this.dataVisualizerContext = dataVisualizerContext;
        }

        [HttpPost]
        public IActionResult GetVisualizationData([FromBody]GetVisualizationDataRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var properties = dataVisualizerContext.RecordProperties
                .Where(property => property.Record.DataSource.Id == request.DataSourceId)
                .Where(property => property.Record.DataSourceVisualization.Id == request.DataSourceVisualizationId);

            if (request.MinDate != null)
            {
                properties = properties.Where(property => property.Record.DateCreated >= request.MinDate.Value);
            }

            if (request.MaxDate != null)
            {
                properties = properties.Where(property => property.Record.DateCreated < request.MaxDate);
            }

            var recordResponses = properties
            .OrderBy(property => property.Record.DateCreated)
            .Select(property => new
            {
                DataSourceId = property.Record.DataSource.Id,
                DataSourceVisualizationId = property.Record.DataSourceVisualization.Id,
                RecordId = property.Record.Id,
                RecordDateCreated = property.Record.DateCreated,
                PropertyKey = property.Key,
                PropertyValue = property.Value
            })
            .ToList()
            .GroupBy(property => new { property.DataSourceId, property.DataSourceVisualizationId })
            .Select(grouping => new GetVisualizationDataResponse()
            {
                DataSourceId = grouping.Key.DataSourceId,
                DataSourceVisualizationId = grouping.Key.DataSourceVisualizationId,
                Records = grouping.GroupBy(dataSource => new {
                                                                dataSource.RecordId,
                                                                dataSource.RecordDateCreated
                                                             })
                                  .Select(dsGroup => new RecordResponse()
                                                    {
                                                        Id = dsGroup.Key.RecordId,
                                                        DateCreated = dsGroup.Key.RecordDateCreated,
                                                        Properties = dsGroup.Select(recordGroup => new RecordPropertyResponse()
                                                                                                    {
                                                                                                        Key = recordGroup.PropertyKey,
                                                                                                        Value = recordGroup.PropertyValue
                                                                                                    })
                                                    })
            });

            return new ObjectResult(recordResponses);
        }
    }
}

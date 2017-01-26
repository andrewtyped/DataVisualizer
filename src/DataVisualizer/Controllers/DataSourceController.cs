using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataVisualizer.Models;
using DataVisualizer.Persistence;
using DataVisualizer.Persistence.Entity;

namespace DataVisualizer.Controllers
{
    public class DataSourceController : Controller
    {
        private readonly DataVisualizerContext dataVisualizerContext;
        public DataSourceController(DataVisualizerContext dataVisualizerContext)
        {
            if (dataVisualizerContext == null)
            {
                throw new ArgumentNullException(nameof(dataVisualizerContext));
            }

            this.dataVisualizerContext = dataVisualizerContext;
        }

        public IActionResult Index(DataSourceRegistrationRequest registrationRequest)
        {
            return View(registrationRequest);
        }

        [HttpPost]
        public IActionResult RegisterDataSource(DataSourceRegistrationRequest registrationRequest)
        {
            if(registrationRequest == null)
            {
                throw new ArgumentNullException(nameof(registrationRequest));
            }

            if(registrationRequest.Visualization == null)
            {
                throw new ArgumentNullException(nameof(registrationRequest.Visualization));
            }

            if(string.IsNullOrWhiteSpace(registrationRequest.Id))
            {
                throw new ArgumentNullException(nameof(registrationRequest.Id));
            }

            var idExists = dataVisualizerContext.DataSources.Any(_dataSource => _dataSource.Id == registrationRequest.Id);

            if(idExists)
            {
                ModelState.AddModelError(nameof(registrationRequest.Id), "A device with this Id already exists");
                return RedirectToAction("Index", new { registrationOptions = registrationRequest });
            }

            var dataSource = GetDataSource(registrationRequest);

            dataVisualizerContext.Add(dataSource);

            try
            {
                dataVisualizerContext.SaveChanges();
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "The data source could not be saved. An error occurred while saving the data source.");
                return RedirectToAction("Index", new { registrationOptions = registrationRequest });
            }

            return RedirectToAction("Index", new { registrationOptions = registrationRequest });
        }

        private DataSource GetDataSource(DataSourceRegistrationRequest registrationOptions)
        {
            var dataSource = new DataSource()
            {
                Id = registrationOptions.Id,
                Description = registrationOptions.Description,
                DataSourceVisualizations = new List<DataSourceVisualization>
                                 {
                                    new DataSourceVisualization()
                                    {
                                        Name = registrationOptions.Visualization.Name,
                                        Description = registrationOptions.Visualization.Description,
                                        Visualization = dataVisualizerContext.Visualizations.Single(visualization => visualization.Type == registrationOptions.Visualization.Type.ToString()),
                                        Properties = registrationOptions.Visualization.DataSourceToVisualizationPropertyMap
                                                        .Select( kvp => new DataSourceVisualizationProperty()
                                                                            {
                                                                                Key = kvp.Key,
                                                                                Value = kvp.Value
                                                                            }).ToList()
                                    }
                                 }
            };

            return dataSource;

        }

        [HttpGet]
        public IActionResult GetVisualizationProperties(VisualizationType visualizationType)
        {
            var properties = dataVisualizerContext.VisualizationProperties
                .Where(visualizationProperty => visualizationProperty.Visualization.Type == visualizationType.ToString())
                .Select(visualizationProperty => visualizationProperty.Value)
                .ToList();

            return new ObjectResult(properties);
        }


        [HttpPost]
        public async Task<IActionResult> SaveRecord([FromBody]SaveRecordRequest saveRecordRequest)
        {
            if (saveRecordRequest == null)
            {
                throw new ArgumentNullException(nameof(saveRecordRequest));
            }

            if(string.IsNullOrWhiteSpace(saveRecordRequest.DataSourceId))
            {
                throw new ArgumentNullException(nameof(saveRecordRequest.DataSourceId));
            }

            if (saveRecordRequest.Properties == null)
            {
                throw new ArgumentNullException(nameof(saveRecordRequest.Properties));
            }

            try
            {
                var record = new Record()
                {
                    DataSource = dataVisualizerContext.DataSources
                                    .Single(dataSource => dataSource.Id == saveRecordRequest.DataSourceId),

                    DataSourceVisualization = dataVisualizerContext.DataSourceVisualizations
                                                .Single(dsv => dsv.Id == saveRecordRequest.DataSourceVisualizationId),

                    Properties = saveRecordRequest.Properties.Select(kvp => new RecordProperty()
                    {
                        Key = kvp.Key,
                        Value = kvp.Value
                    }).ToList()
                };


                dataVisualizerContext.Records.Add(record);
            }
            catch(Exception ex)
            {
                //TODO: Logging, better user error
                return new ObjectResult(false);
            }

            try
            {
                await dataVisualizerContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                //TODO: Logging, better user error
                return new ObjectResult(false);
            }

            return new ObjectResult(true);
        }

    }
}

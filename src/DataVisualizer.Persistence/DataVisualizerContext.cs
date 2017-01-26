using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using DataVisualizer.Persistence.Entity;

namespace DataVisualizer.Persistence
{
    public class DataVisualizerContext : DbContext
    {
        public DataVisualizerContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<DataSource> DataSources { get; set; }
        public DbSet<DataSourceVisualization> DataSourceVisualizations {get; set;}
        public DbSet<DataSourceVisualizationProperty> DataSourceVisualizationProperties { get; set; }
        public DbSet<Visualization> Visualizations { get; set; }
        public DbSet<VisualizationProperty> VisualizationProperties { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<RecordProperty> RecordProperties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataSource>().ToTable("DataSource");
            modelBuilder.Entity<DataSourceVisualization>().ToTable("DataSourceVisualization");
            modelBuilder.Entity<DataSourceVisualizationProperty>().ToTable("DataSourceVisualizationProperty");
            modelBuilder.Entity<Visualization>().ToTable("Visualization");
            modelBuilder.Entity<VisualizationProperty>().ToTable("VisualizationProperty");
            modelBuilder.Entity<Record>().ToTable("Record");
            modelBuilder.Entity<RecordProperty>().ToTable("RecordProperty");
        }
    }
}

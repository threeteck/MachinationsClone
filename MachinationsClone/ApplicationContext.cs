using MachinationsClone.Models;
using MachinationsClone.Models.Entities;
using MachinationsClone.Models.Entities.Geometry;
using MachinationsClone.Models.Entities.Graph;
using Microsoft.EntityFrameworkCore;

namespace MachinationsClone
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Graph> Graphs { get; set; }
        public DbSet<GraphElement> GraphElements { get; set; }
        public DbSet<GraphNode> GraphNodes { get; set; }
        public DbSet<GraphConnection> GraphConnections { get; set; }
        public DbSet<NodeType> NodeTypes { get; set; }
        public DbSet<ConnectionType> ConnectionTypes { get; set; }
        public DbSet<NodeGeometry> NodeGeometries { get; set; }
        public DbSet<GraphStatesGroup> GraphStatesGroups { get; set; }
        public DbSet<GraphState> GraphStates { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        
        // Seed the database with some initial data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            GraphGrammar.InitializeGraphGrammar();
            
            foreach (var nodeType in GraphGrammar.NodeTypes)
            {
                modelBuilder.Entity<NodeType>().HasData(nodeType);
            }
            
            foreach (var connectionType in GraphGrammar.ConnectionTypes)
            {
                modelBuilder.Entity<ConnectionType>().HasData(connectionType);
            }
        }
    }
}
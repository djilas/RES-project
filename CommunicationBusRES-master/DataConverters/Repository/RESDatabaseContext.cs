using DataModel.Model;
using System.Data.Entity;

namespace DataService.Repository
{
	public class RESDatabaseContext : DbContext
	{
		public RESDatabaseContext() : base("RESDatabaseContext")
		{
		}

		public DbSet<Resource> Resources { get; set; }

		public DbSet<ResourceType> ResourceTypes { get; set; }

		public DbSet<Relation> Relations { get; set; }

		public DbSet<RelationType> RelationTypes { get; set; }
	}
}
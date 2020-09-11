using System.ComponentModel.DataAnnotations;

namespace DataModel.Model
{
	public class Resource
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public ResourceType Type { get; set; }
	}
}

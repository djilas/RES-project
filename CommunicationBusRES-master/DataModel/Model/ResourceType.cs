using System.Collections.Generic;

namespace DataModel.Model
{
	public class ResourceType
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public ICollection<Resource> Resources { get; set; }
	}
}

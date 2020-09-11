using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataModel.Model
{
	public class ResourceType
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public ICollection<Resource> Resources { get; set; }
	}
}

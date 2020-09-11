using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataModel.Model
{
	public class RelationType
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }

		public ICollection<Relation> Relations { get; set; }
	}
}

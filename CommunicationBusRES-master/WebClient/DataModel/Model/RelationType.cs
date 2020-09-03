using System.Collections.Generic;

namespace DataModel.Model
{
	public class RelationType
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public ICollection<Relatation> Relations { get; set; }
	}
}

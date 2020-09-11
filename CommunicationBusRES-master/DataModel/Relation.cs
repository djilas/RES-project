using System.ComponentModel.DataAnnotations;

namespace DataModel.Model
{
	public class Relation
	{
		[Key]
		public int Id { get; set; }
		public Resource FirstResource { get; set; }
		public Resource SecondResource { get; set; }
		public RelationType Type { get; set; }
	}
}
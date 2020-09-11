namespace DataModel.Model
{
	public class Relatation
	{
		public int Id { get; set; }

		public Resource FirstResourceId { get; set; }

		public Resource SecondResourceId { get; set; }

		public RelationType Type { get; set; }
	}
}

namespace DataModel
{
	public class Relation
	{
		public int Id { get; set; }
		public int IdFirst { get; set; }
		public int IdSecond { get; set; }
		public TypeRelation Type { get; set; }

		public Relation()
		{

		}

		public Relation (int id, int idFirst, int idSecond, TypeRelation type)
		{
			this.Id = id;
			this.IdFirst = idFirst;
			this.IdSecond = idSecond;
			this.Type = type;
		}

	}
}

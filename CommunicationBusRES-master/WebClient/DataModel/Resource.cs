namespace DataModel
{
    public class Resource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Opis { get; set; }
        public TypeResource Type { get; set; }

        public Resource()
        {

        }

        public Resource(int id, string name, string opis, TypeResource type)
        {
            Id = id;
            Name = name;
            Opis = opis;
            Type = type;
        }

    }
}

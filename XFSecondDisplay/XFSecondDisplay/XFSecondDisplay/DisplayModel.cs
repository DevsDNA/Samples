namespace DisplayService
{
    public class DisplayModel
    {
        public DisplayModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public int Id { get; set; }

        public string Name { get; set; }
    }
}

namespace Lab1_Architecture_IS.Models
{
    public class CSVModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public float Volume { get; set; }
        public bool IsInteractive { get; set; }
        public CSVModel()
        {

        }

        public CSVModel(string name, string type, float volume, bool isInteractive)
        {
            Name = name;
            Type = type;
            Volume = volume;
            IsInteractive = isInteractive;
        }
    }
}

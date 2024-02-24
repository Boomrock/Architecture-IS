using Lab1_Architecture_IS.IOController;
using Lab1_Architecture_IS.Models;
using Lab1_Architecture_IS.CSVParser;

using System.IO;

namespace Lab1_Architecture_IS
{
    public class CSVFileController<Model> : IOController<Model>
    {
        public CSVFileController(string path, IParser<Model, string> parser) : base(path)
        {
            _parser = parser;
        }

        public override void Write(Model[] date)
        {
            using (StreamWriter writer = new StreamWriter(_path))
            {
                foreach (var item in date)
                {
                    var csvString = _parser.Parse(item);
                    writer.WriteLine(csvString);
                }
            }
        }
    }
}

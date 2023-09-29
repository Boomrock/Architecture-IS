using Lab1_Architecture_IS.IOController;
using Lab1_Architecture_IS.Models;
using Lab1_Architecture_IS.SCVParser;

using System.IO;

namespace Lab1_Architecture_IS
{
    internal class CSVFileController<Model> : IOController<Model>
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

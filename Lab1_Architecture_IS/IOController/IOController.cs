using Lab1_Architecture_IS.IOController.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace Lab1_Architecture_IS.IOController
{
    public  abstract class IOController<TDate> : IIOController<TDate>
    {
        protected readonly string _path;
        protected IParser<TDate, string> _parser;

        public IOController(string path)
        {
            _path = path;
        
        }
        
        public virtual TDate[] Read()
        {
            var file = new List<TDate>(10);
            // Создаем объект StreamReader для чтения из файла
            using (StreamReader reader = new StreamReader(_path))
            {
                do
                {
                    var data = _parser.Parse(reader.ReadLine());
                    file.Add(data);
                } while (!reader.EndOfStream);
            }
            return file.ToArray();
        }

        public abstract void Write(TDate[] date);
    }
}

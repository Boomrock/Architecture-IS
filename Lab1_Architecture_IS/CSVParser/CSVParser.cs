using Lab1_Architecture_IS.Models;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Text;

namespace Lab1_Architecture_IS.CSVParser
{
    public class CSVParser : ICSVParser<CSVModel>
    {
        private readonly string _csvtDelimiters;

        public CSVParser(string csvtDelimiters)
        {
            _csvtDelimiters = csvtDelimiters;
        }

        public CSVModel Parse(string data)
        {
            var field = data.Split(_csvtDelimiters);

            if (field.Length < 5) 
                return null;

            var model = new CSVModel();
  
            model.Id = Int16.Parse(field[0]);
            model.Name = field[1];
            model.Type = field[2];
            model.IsInteractive = Boolean.Parse(field[3]);
            model.Volume = float.Parse(field[4]);

            return model;
        }

        public string Parse(CSVModel obj)
        {
            var str = new StringBuilder();
            str.Append(obj.Id.ToString());
            str.Append(";");
            str.Append(obj.Name.ToString());
            str.Append(";");
            str.Append(obj.Type.ToString());
            str.Append(";");
            str.Append(obj.IsInteractive.ToString());
            str.Append(";");
            str.Append(obj.Volume.ToString());
            str.Append(";");
            return str.ToString();
        }
    }
}

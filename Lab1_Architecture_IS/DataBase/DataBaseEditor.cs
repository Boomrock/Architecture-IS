using Lab1_Architecture_IS.Models;
using System;
using System.Linq;

namespace Lab1_Architecture_IS.DataBase
{
    public class DataBaseEditor : IEditor<CSVModel>
    {
        private readonly DataBaseContext _dataBaseContext;

        public DataBaseEditor(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }

        public void Add(CSVModel model)
        {
            model.Id = null;
            _dataBaseContext.Models.Add(model);
        }


        public CSVModel Delete(int line)
        {
            var model = Read(line);
            _dataBaseContext.Models.Remove(model);
            return model;
        }

        public CSVModel Read(int line)
        {
            var model = _dataBaseContext.Models.Find(line);
            if (model != null)
            {
                return model;
            }
            throw new Exception($"The database does not contain models at index {line}");
        }

        public CSVModel[] ReadAll()
        {
            return _dataBaseContext.Models.ToArray();
        }

        public CSVModel Update(CSVModel model, int line)
        {
            model.Id = line;
            _dataBaseContext.Models.Update(model);
            return model;
        }

        public void WriteAll(CSVModel[] models)
        {
            _dataBaseContext.Models.AddRange(models);
        }
        public void Close()
        {
            _dataBaseContext.Dispose();
        }

        public async void Save()
        {
            await _dataBaseContext.SaveChangesAsync();
        }
    }
}

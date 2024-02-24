using Lab1_Architecture_IS.IOController;
using System.Collections.Generic;
using System.Data.SqlTypes;

namespace Lab1_Architecture_IS
{
    public class CSVEditor<Model> : IEditor<Model>
    {
        IOController<Model> _csvFileController;
        List<Model> _modelList = new List<Model>();

        public CSVEditor(IOController<Model> csvFileController)
        {
            _csvFileController = csvFileController;
        }

        public void Add(Model model)
        {
            if (_modelList.Count == 0)
                ReadAll();
            _modelList.Add(model);
            WriteAll(_modelList.ToArray());
        }

        public void Close()
        {
            throw new System.NotImplementedException();
        }

        public Model Delete(int line)
        {
            if(_modelList.Count == 0) 
                ReadAll();
            if (_modelList.Count <= line)
                return default;
            var model = _modelList[line];
            _modelList.RemoveAt(line);
            WriteAll(_modelList.ToArray());
            return model;
        }

        public Model Read(int line)
        {
            if(_modelList.Count == 0)
                ReadAll();
            if (_modelList.Count <= line)
                return default;
            return _modelList[line];
        }

        public Model[] ReadAll()
        {
            var models = _csvFileController.Read();
            _modelList = new List<Model>(models);
            return models;
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }

        public Model Update(Model model, int line)
        {
            if (_modelList.Count == 0)
                ReadAll();
            if (_modelList.Count <= line)
                return default;
            _modelList[line] = model;
            WriteAll(_modelList.ToArray());
            return model;
        }

        public void WriteAll(Model[] models)
        {
           _csvFileController.Write(models);
        }

    }
}

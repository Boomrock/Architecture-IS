namespace Lab1_Architecture_IS
{
    public interface IEditor<Model>
    {

        Model[] ReadAll();
        Model Read(int line);
        Model Update(Model model, int line);
        void WriteAll(Model[] models);
        void Add(Model model);
        Model Delete(int line);
        void Close();
        void Save();
    }
}
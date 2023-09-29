namespace Lab1_Architecture_IS
{
    internal interface IEditor<Model>
    {

        Model[] ReadAll();
        Model Read(int line);
        void Update(Model model, int line);
        void WriteAll(Model[] models);
        void Add(Model model);
        void Delete(int line);
    }
}
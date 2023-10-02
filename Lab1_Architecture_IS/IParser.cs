namespace Lab1_Architecture_IS
{
    public interface IParser< TObject, TData>
    {
        TObject Parse(TData data);
        TData Parse(TObject obj);
    }
}

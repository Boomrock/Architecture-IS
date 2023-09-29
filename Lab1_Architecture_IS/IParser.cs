namespace Lab1_Architecture_IS
{
    interface IParser< TObject, TData>
    {
        TObject Parse(TData data);
        TData Parse(TObject obj);
    }
}

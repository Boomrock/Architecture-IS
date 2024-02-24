
namespace Lab1_Architecture_IS.IOController.Interfaces
{
    interface IIOController<TDate>: IReader<TDate>, IWriter<TDate> {}
}

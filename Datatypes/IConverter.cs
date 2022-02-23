namespace Datatypes
{
    public interface IConverter<T>
    {
        T ConvertTo(Value value);
        Value ConvertFrom(T value);
    }
}
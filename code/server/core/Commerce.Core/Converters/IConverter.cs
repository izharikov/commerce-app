namespace Commerce.Core.Converters
{
    public interface IConverter
    {
        
    }

    public interface IConverter<in TSource, out TResult> : IConverter where TResult: class
    {
        TResult Convert(TSource source);
    }
}
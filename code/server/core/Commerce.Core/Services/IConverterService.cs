namespace Commerce.Core.Services
{
    public interface IConverterService
    {
        TOutput Convert<TInput, TOutput>(TInput source) where TOutput : class;
    }
}
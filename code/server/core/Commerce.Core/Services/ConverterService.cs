using System;
using Commerce.Core.Converters;
using Microsoft.Extensions.DependencyInjection;

namespace Commerce.Core.Services
{
    public class ConverterService : IConverterService
    {
        private readonly IServiceProvider _serviceProvider;

        public ConverterService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TOutput Convert<TInput, TOutput>(TInput source) where TOutput : class
        {
            var converter = _serviceProvider.GetService<IConverter<TInput, TOutput>>();
            return converter?.Convert(source);
        }
    }
}
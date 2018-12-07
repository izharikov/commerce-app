using System;
using System.Collections.Generic;
using System.Linq;

namespace Commerce.Core.Pipelines.Implementation
{
    public class DefaultPipelineImplementation<TInput, TOutput, TContext> : IPipeline<TInput, TOutput, TContext>, IPipelineInitializer
    {
        private bool _initialized;
        private IList<IPipelineBlock> _pipelineBlocks;

        public IEnumerable<IPipelineBlock> Blocks => _pipelineBlocks;

        public void Initialize(IServiceProvider serviceProvider, IList<Type> pipelineBlocks)
        {
            _pipelineBlocks = pipelineBlocks.Select(serviceProvider.GetService).Cast<IPipelineBlock>().ToList();
            _initialized = true;
        }

        public TOutput Run(TInput input, TContext context)
        {
            return (TOutput) Run((object) input, context);
        }

        public object Run(object input, object context)
        {
            if (!_initialized)
            {
                throw new InvalidOperationException($"{GetType().FullName} is not initialized.");
            }
            object result = input;
            foreach (var pipelineBlock in _pipelineBlocks)
            {
                result = pipelineBlock.Run(result, context);
            }

            return result;
        }
    }

    public interface IPipelineInitializer
    {
        void Initialize(IServiceProvider serviceProvider, IList<Type> pipelineBlocks);
    }
}
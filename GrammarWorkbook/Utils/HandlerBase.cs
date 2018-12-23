using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GrammarWorkbook.Data;
using MediatR;

namespace GrammarWorkbook.Utils
{
    public abstract class HandlerBase<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        protected readonly DatabaseContext Context;
        protected readonly IMapper Mapper;

        protected HandlerBase(DatabaseContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
    
    public abstract class HandlerBase<TRequest> : IRequestHandler<TRequest> where TRequest : IRequest
    {
        protected readonly DatabaseContext Context;
        protected readonly IMapper Mapper;

        protected HandlerBase(DatabaseContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        public abstract Task<MediatR.Unit> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
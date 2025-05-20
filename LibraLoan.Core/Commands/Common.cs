using MediatR;
using System.Collections.Generic;

namespace LibraLoan.Core.Commands
{
    public static class Common
    {
        public record GetAllCommand<TModel>(bool Reload = false) : IRequest<IEnumerable<TModel>>;
        public record ShowCreateCommand<TModel> : IRequest where TModel : class;
        public record CreateCommand<TModel, TDto>(TDto Model) : IRequest<TModel> where TDto : class where TModel : class;
        public record ShowUpdateCommand<TModel>(TModel Model) : IRequest where TModel : class;
        public record UpdateCommand<TDto>(TDto Model) : IRequest<bool> where TDto : class;
        public record DeleteCommand<TModel>(TModel Model) : IRequest<bool> where TModel : class;
    }
}

using LibraLoan.Core.Abstraction.Repositories;
using LibraLoan.Core.Models;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using static LibraLoan.Core.Commands.Common;

namespace LibraLoan.Features.Publishers.CommandHandlers
{
    internal class DeletePublisherCommandHandler(IPublishersRepository publishersRepository) : IRequestHandler<DeleteCommand<Publisher>, bool>
    {
        public async Task<bool> Handle(DeleteCommand<Publisher> request, CancellationToken cancellationToken)
        {
            return await publishersRepository.DeleteAsync(request.Model);
        }
    }
}

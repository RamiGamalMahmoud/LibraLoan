using LibraLoan.Core.Abstraction.Repositories;
using LibraLoan.Core.DTOs;
using LibraLoan.Core.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using static LibraLoan.Core.Commands.Common;

namespace LibraLoan.Features.Publishers.CommandHandlers
{
    internal class CreatePublisherCommandHandler(IPublishersRepository publishersRepository) : IRequestHandler<CreateCommand<Publisher, PublisherDto>, Publisher>
    {
        public async Task<Publisher> Handle(CreateCommand<Publisher, PublisherDto> request, CancellationToken cancellationToken)
        {
            return await publishersRepository.CreateAsync(request.Model);
        }
    }
}

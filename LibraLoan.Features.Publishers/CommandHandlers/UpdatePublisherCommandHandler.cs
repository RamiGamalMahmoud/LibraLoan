using LibraLoan.Core.Abstraction.Repositories;
using LibraLoan.Core.DTOs;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using static LibraLoan.Core.Commands.Common;

namespace LibraLoan.Features.Publishers.CommandHandlers
{
    internal class UpdatePublisherCommandHandler(IPublishersRepository publishersRepository) : IRequestHandler<UpdateCommand<PublisherDto>, bool>
    {
        public async Task<bool> Handle(UpdateCommand<PublisherDto> request, CancellationToken cancellationToken)
        {
            return await publishersRepository.UpdateAsync(request.Model);
        }
    }
}

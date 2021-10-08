using System.Threading;
using System.Threading.Tasks;
using Common.Commands;
using DataAccess.Abstractions;
using MediatR;

namespace Service.CommandHandlers
{
    public class DeleteForecastCommandHandler : IRequestHandler<DeleteForecastCommand, bool>
    {
        private readonly IForecastRepository _forecastRepository;

        public DeleteForecastCommandHandler(IForecastRepository forecastRepository)
        {
            _forecastRepository = forecastRepository;
        }

        public async Task<bool> Handle(DeleteForecastCommand request, CancellationToken cancellationToken)
        {
            await _forecastRepository.DeleteForecastAsync(request.Id,request.Year).ConfigureAwait(false);
            return await _forecastRepository.SaveChangesAsync() > 0;
        }
    }
}
using System.Collections.Generic;
using MediatR;

namespace Common.Commands
{
    public class AddUpdateForecastCommand : IRequest<List<ForecastCommandError>>
    {
        public IEnumerable<ForecastCommand> Forecasts { get; set; }
    }
}
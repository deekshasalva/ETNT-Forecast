using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common.Models;
using Common.Query;
using DataAccess.Abstractions;
using MediatR;

namespace Service.QueryHandlers
{
    public class GetForecastByFyYearHandler : IRequestHandler<GetForecastsByFyYearQuery, IEnumerable<ForecastViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly IForecastRepository _repository;

        public GetForecastByFyYearHandler(IForecastRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ForecastViewModel>> Handle(GetForecastsByFyYearQuery request,
            CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<ForecastViewModel>>(
                await _repository.GetAllForecastByFyYearAsync(request.FyYear));
        }
    }
}
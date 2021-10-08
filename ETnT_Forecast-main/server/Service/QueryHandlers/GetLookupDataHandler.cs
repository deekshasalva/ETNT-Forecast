using System.Threading;
using System.Threading.Tasks;
using Common.Models;
using Common.Query;
using DataAccess.Abstractions;
using DataAccess.DbSets;
using MediatR;

namespace Service.QueryHandlers
{
    public class GetLookupDataHandler : IRequestHandler<GetLookupQuery, LookupViewModel>
    {
        private readonly IForecastRepository _forecastRepository;
        private readonly ILookupRepository _lookupRepository;

        public GetLookupDataHandler(ILookupRepository lookupRepository, IForecastRepository forecastRepository)
        {
            _lookupRepository = lookupRepository;
            _forecastRepository = forecastRepository;
        }

        public async Task<LookupViewModel> Handle(GetLookupQuery request, CancellationToken cancellationToken)
        {
            return new LookupViewModel
            {
                Orgs = await _lookupRepository.GetAll<Org>(),
                Users = await _lookupRepository.GetAll<User>(),
                Projects = await _lookupRepository.GetAll<Project>(),
                Skills = await _lookupRepository.GetAll<Skill>(),
                Business = await _lookupRepository.GetAll<Business>(),
                Capabilities = await _lookupRepository.GetAll<Capability>(),
                ForecastConfidence = await _lookupRepository.GetAll<Category>(),
                FyYears = await _forecastRepository.GetAllYears()
            };
        }
    }
}
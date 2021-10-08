using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common.Commands;
using DataAccess.Abstractions;
using DataAccess.DbSets;
using MediatR;
using Microsoft.EntityFrameworkCore.Internal;

namespace Service.CommandHandlers
{
    public class AddUpdateForecastCommandHandler : IRequestHandler<AddUpdateForecastCommand, List<ForecastCommandError>>
    {
        private readonly IForecastRepository _forecastRepository;
        private readonly ILookupRepository _lookupRepository;
        private readonly IMapper _mapper;
        private readonly ForecastCommandValidator _validator;

        public AddUpdateForecastCommandHandler(IForecastRepository forecastRepository,
            ILookupRepository lookupRepository, ForecastCommandValidator validator, IMapper mapper)
        {
            _forecastRepository = forecastRepository;
            _lookupRepository = lookupRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<List<ForecastCommandError>> Handle(AddUpdateForecastCommand request, CancellationToken cancellationToken)
        {
            var forecastErrorRecords = new List<ForecastCommandError>();
            foreach (var forecastRequest in request.Forecasts)
            {
                var validationResult =
                    await _validator.ValidateAsync(forecastRequest, cancellationToken).ConfigureAwait(false);
                if (!validationResult.IsValid)
                {
                    var error = _mapper.Map<ForecastCommandError>(forecastRequest);
                    error.Errors = validationResult.ToString();
                    forecastErrorRecords.Add(error);
                    continue;
                }

                var forecast = await _forecastRepository
                    .AddUpdateForecastAsync(forecastRequest.Id, forecastRequest.Org, forecastRequest.Manager, forecastRequest.USFocal,
                        forecastRequest.Project, forecastRequest.SkillGroup, forecastRequest.Business,
                        forecastRequest.Capability, forecastRequest.Chargeline, forecastRequest.ForecastConfidence,
                        forecastRequest.Comments).ConfigureAwait(false);

                await _forecastRepository.AddReplaceForecastDataAsync(new ForecastData(forecast,
                    forecastRequest.Jan, forecastRequest.Feb, forecastRequest.Mar,
                    forecastRequest.Apr, forecastRequest.May, forecastRequest.June,
                    forecastRequest.July, forecastRequest.Aug, forecastRequest.Sep,
                    forecastRequest.Oct, forecastRequest.Nov, forecastRequest.Dec,
                    forecastRequest.Year)).ConfigureAwait(false);
            }

            await _forecastRepository.SaveChangesAsync().ConfigureAwait(false);

            return forecastErrorRecords;
        }
    }
}
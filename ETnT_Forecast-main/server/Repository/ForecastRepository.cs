using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Abstractions;
using DataAccess.DbSets;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ForecastRepository : IForecastRepository
    {
        private readonly ForecastContext _context;
        private readonly ILookupRepository _lookupRepository;

        public ForecastRepository(ForecastContext context, ILookupRepository lookupRepository)
        {
            _context = context;
            _lookupRepository = lookupRepository;
        }

        public async Task<Forecast> AddUpdateForecastAsync(Guid id, string orgName, string managerName,
            string usFocalName,
            string projectName,
            string skillName, string businessUnitName, string capabilityName, string chargeline,
            string forecastConfidenceValue, string comments)
        {
            var org = await _lookupRepository.GetOrgByNameAsync(orgName).ConfigureAwait(false);
            var manager = await _lookupRepository.GetUserByNameAsync(managerName).ConfigureAwait(false);
            var usFocal = await _lookupRepository.GetUserByNameAsync(usFocalName).ConfigureAwait(false);
            var project = await _lookupRepository.GetProjectByNameAsync(projectName).ConfigureAwait(false);
            var skill = await _lookupRepository.GetSkillByNameAsync(skillName).ConfigureAwait(false);
            var businessUnit = await _lookupRepository.GetBusinessByNameAsync(businessUnitName).ConfigureAwait(false);
            var capability = await _lookupRepository.GetCapabilityByNameAsync(capabilityName).ConfigureAwait(false);
            var forecastConfidence = await _lookupRepository.GetCategoryByNameAsync(forecastConfidenceValue)
                .ConfigureAwait(false);
            var forecast = _context.Forecasts.FirstOrDefault(x =>
                x.Id == id
                || (x.Org == org
                    && x.Project == project
                    && x.SkillGroup == skill
                    && x.Business == businessUnit
                    && x.Capability == capability)
            );

            if (forecast == null)
            {
                forecast = new Forecast(org, manager, usFocal, project, skill, businessUnit, capability, chargeline,
                    forecastConfidence, comments);
                await _context.Forecasts.AddAsync(forecast).ConfigureAwait(false);
            }
            else
            {
                forecast.Manager = manager;
                forecast.USFocal = usFocal;
                forecast.Chargeline = chargeline;
                forecast.ForecastConfidence = forecastConfidence;
                forecast.Comments = comments;
                forecast.Org = org;
                forecast.Project = project;
                forecast.SkillGroup = skill;
                forecast.Business = businessUnit;
                forecast.Capability = capability;
                forecast.UpdatedAt = DateTime.Now;
            }

            return forecast;
        }

        public async Task<IEnumerable<Forecast>> GetAllForecastByFyYearAsync(int fyYear)
        {
            //TODO: Optimize this
            return _context.Forecasts
                .Include(m => m.Business)
                .Include(m => m.Capability)
                .Include(m => m.ForecastConfidence)
                .Include(m => m.Manager)
                .Include(m => m.USFocal)
                .Include(m => m.ForecastData)
                .Include(m => m.Project)
                .Include(m => m.SkillGroup)
                .Include(m => m.Org)
                .Include(m => m.ForecastData)
                .ToList()
                .Select(forecast =>
                    new Forecast
                    {
                        Id = forecast.Id,
                        Org = forecast.Org,
                        Manager = forecast.Manager,
                        USFocal = forecast.USFocal,
                        Business = forecast.Business,
                        Capability = forecast.Capability,
                        Chargeline = forecast.Chargeline,
                        Comments = forecast.Comments,
                        Project = forecast.Project,
                        SkillGroup = forecast.SkillGroup,
                        ForecastConfidence = forecast.ForecastConfidence,
                        ForecastData = forecast.ForecastData.Where(x => x.Year == fyYear).ToList()
                    }
                )
                .Where(f => f.ForecastData.Any()).ToList();
        }

        public async Task AddReplaceForecastDataAsync(ForecastData newForecastData)
        {
            var forecastData = _context.ForecastData.FirstOrDefault(x =>
                x.Forecast == newForecastData.Forecast && x.Year == newForecastData.Year);
            if (forecastData != null) _context.ForecastData.Remove(forecastData);
            await _context.ForecastData.AddAsync(newForecastData).ConfigureAwait(false);
        }

        public async Task<IEnumerable<int>> GetAllYears()
        {
            return await _context.ForecastData.Select(x => x.Year).Distinct().ToListAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteForecastAsync(Guid id, int year)
        {
            var forecast = await _context.Forecasts.FirstOrDefaultAsync(x => x.Id == id);
            _context.ForecastData.Remove(
                _context.ForecastData.FirstOrDefault(x => x.Year == year && x.Forecast.Id == id));
            if (!forecast.ForecastData.Any())
                _context.Forecasts.Remove(forecast);
        }
    }
}
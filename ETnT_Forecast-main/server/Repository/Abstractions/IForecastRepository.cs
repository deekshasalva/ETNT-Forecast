using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.DbSets;

namespace DataAccess.Abstractions
{
    public interface IForecastRepository
    {
        Task<IEnumerable<Forecast>> GetAllForecastByFyYearAsync(int fyYear);

        /// <summary>
        ///     Add new Forecast or Update existing
        ///     Unique constraint - org, project, skill, business, capability
        /// </summary>
        /// <param name="id"></param>
        /// <param name="orgName"></param>
        /// <param name="managerName"></param>
        /// <param name="usFocalName"></param>
        /// <param name="projectName"></param>
        /// <param name="skillName"></param>
        /// <param name="businessUnitName"></param>
        /// <param name="capabilityName"></param>
        /// <param name="chargeline"></param>
        /// <param name="forecastConfidenceValue"></param>
        /// <param name="comments"></param>
        /// <returns></returns>
        Task<Forecast> AddUpdateForecastAsync(Guid id, string orgName, string managerName, string usFocalName,
            string projectName,
            string skillName, string businessUnitName, string capabilityName, string chargeline,
            string forecastConfidenceValue, string comments);

        /// <summary>
        ///     Add/Replace Forecast data by ForecastId, Year
        /// </summary>
        /// <param name="forecast"></param>
        /// <param name="forecastData"></param>
        /// <returns></returns>
        Task AddReplaceForecastDataAsync(ForecastData forecastData);

        Task DeleteForecastAsync(Guid id, int year);

        /// <summary>
        ///     Get all years for which data is available
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<int>> GetAllYears();

        Task<int> SaveChangesAsync();
    }
}
using System.Collections.Generic;
using Common.Models;
using MediatR;

namespace Common.Query
{
    public class GetForecastsByFyYearQuery : IRequest<IEnumerable<ForecastViewModel>>
    {
        public GetForecastsByFyYearQuery(int fyYear)
        {
            FyYear = fyYear;
        }

        public int FyYear { get; set; }
    }
}
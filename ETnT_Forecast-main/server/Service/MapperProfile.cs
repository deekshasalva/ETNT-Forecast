using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AutoMapper;
using Common.Commands;
using Common.Models;
using DataAccess.DbSets;

namespace Service
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<IEnumerable<Forecast>, IEnumerable<ForecastViewModel>>()
                .ConvertUsing<ForecastFlatMap>();

            CreateMap<TaskStatusViewModel, EventData>()
                .ReverseMap();

            CreateMap<ForecastCommandError, ForecastCommand>().ReverseMap();
        }
    }

    public class ForecastFlatMap : ITypeConverter<IEnumerable<Forecast>, IEnumerable<ForecastViewModel>>
    {
        public IEnumerable<ForecastViewModel> Convert(IEnumerable<Forecast> source,
            IEnumerable<ForecastViewModel> destination, ResolutionContext context)
        {
            return source.SelectMany(s => s.ForecastData.Select(o => new ForecastViewModel
            {
                Id = s.Id,
                Org = s.Org.Value,
                Manager = s.Manager.FullName,
                USFocal = s.USFocal.FullName,
                Project = s.Project.Value,
                SkillGroup = s.SkillGroup.Value,
                Business = s.Business.Value,
                Capability = s.Capability.Value,
                ForecastConfidence = s.ForecastConfidence.Value,
                Chargeline = s.Chargeline,
                Comments = s.Comments,
                Jan = o.Jan.ToString(CultureInfo.InvariantCulture),
                Feb = o.Feb.ToString(CultureInfo.InvariantCulture),
                Mar = o.Mar.ToString(CultureInfo.InvariantCulture),
                Apr = o.Apr.ToString(CultureInfo.InvariantCulture),
                May = o.May.ToString(CultureInfo.InvariantCulture),
                June = o.June.ToString(CultureInfo.InvariantCulture),
                July = o.July.ToString(CultureInfo.InvariantCulture),
                Aug = o.Aug.ToString(CultureInfo.InvariantCulture),
                Oct = o.Oct.ToString(CultureInfo.InvariantCulture),
                Sep = o.Sep.ToString(CultureInfo.InvariantCulture),
                Nov = o.Nov.ToString(CultureInfo.InvariantCulture),
                Dec = o.Dec.ToString(CultureInfo.InvariantCulture)
            })).ToList();
        }
    }
}
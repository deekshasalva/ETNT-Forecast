using System.Collections.Generic;

namespace DataAccess.DbSets
{
    public class Forecast : BaseEntity
    {
        public Forecast()
        {
        }

        public Forecast(Org org, User manager, User usFocal, Project project, Skill skillGroup, Business business,
            Capability capability, string chargeline, Category forecastConfidence, string comments)
        {
            Org = org;
            Manager = manager;
            USFocal = usFocal;
            Project = project;
            SkillGroup = skillGroup;
            Business = business;
            Capability = capability;
            Chargeline = chargeline;
            ForecastConfidence = forecastConfidence;
            Comments = comments;
            ForecastData = new List<ForecastData>();
        }

        public Org Org { get; set; }
        public User Manager { get; set; }
        public User USFocal { get; set; }
        public Project Project { get; set; }
        public Skill SkillGroup { get; set; }
        public Business Business { get; set; }
        public Capability Capability { get; set; }
        public string Chargeline { get; set; }
        public Category ForecastConfidence { get; set; }
        public string Comments { get; set; }
        public List<ForecastData> ForecastData { get; set; }
    }
}
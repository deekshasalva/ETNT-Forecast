using System;

namespace DataAccess.DbSets
{
    public class ForecastData : BaseEntity
    {
        public ForecastData()
        {
        }

        public ForecastData(Forecast forecast, string jan, string feb, string mar, string apr, string may,
            string june, string july, string aug, string sep, string oct, string nov, string dec, string year)
        {
            Forecast = forecast;
            Jan = Convert.ToDecimal(jan);
            Feb = Convert.ToDecimal(feb);
            Mar = Convert.ToDecimal(mar);
            Apr = Convert.ToDecimal(apr);
            May = Convert.ToDecimal(may);
            June = Convert.ToDecimal(june);
            July = Convert.ToDecimal(july);
            Aug = Convert.ToDecimal(aug);
            Sep = Convert.ToDecimal(sep);
            Oct = Convert.ToDecimal(oct);
            Nov = Convert.ToDecimal(nov);
            Dec = Convert.ToDecimal(dec);
            Year = Convert.ToInt32(year);
        }

        public Forecast Forecast { get; set; }
        public decimal Jan { get; set; }
        public decimal Feb { get; set; }
        public decimal Mar { get; set; }
        public decimal Apr { get; set; }
        public decimal May { get; set; }
        public decimal June { get; set; }
        public decimal July { get; set; }
        public decimal Aug { get; set; }
        public decimal Sep { get; set; }
        public decimal Oct { get; set; }
        public decimal Nov { get; set; }
        public decimal Dec { get; set; }
        public int Year { get; set; }
    }
}
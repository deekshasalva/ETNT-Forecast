namespace DataAccess.DbSets
{
    public class EventData : BaseEntity
    {
        public string Status { get; set; }
        public string FileName { get; set; }
        public string Errors { get; set; }
    }
}
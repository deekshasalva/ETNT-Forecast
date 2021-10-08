namespace DataAccess.DbSets
{
    public abstract class Lookup : BaseEntity
    {
        public Lookup(string value)
        {
            Value = value;
        }

        public string Value { get; set; }
    }
}
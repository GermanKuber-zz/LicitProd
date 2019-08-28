namespace LicitProd.Entities
{
    public class LogDbMapper : ObjectToDbMapper<Log>
    {
        public LogDbMapper() : base("Logs")
        {
        }
        protected override void Map()
        {
            Set(x => x.Id)
               .PrimaryKey();
        }
    }
}



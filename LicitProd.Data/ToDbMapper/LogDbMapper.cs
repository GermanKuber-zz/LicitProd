namespace LicitProd.Entities
{
    public class LogDbMapper : ObjectToDbMapper<Log>
    {

        protected override void Map()
        {
            SetTableName("Logs");

        }
    }
}



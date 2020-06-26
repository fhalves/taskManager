using System.Data;

namespace TaskManager.Infra.Common
{
    public class DapperParameters
    {
        public string Name { get; set; }

        public object Value { get; set; }

        public DbType Type { get; set; }
    }
}

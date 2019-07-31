using System.Collections.Generic;
using System.Linq;

namespace LicitProd.Data
{
    public class Parameters
    {
        private readonly Dictionary<string, string> parameters = new Dictionary<string, string>();

        protected Parameters(Dictionary<string, string> parameters)
        {
            this.parameters = parameters;
        }
        public Parameters()
        {

        }
        public Parameters Add(string key, string value) => new Parameters(parameters.Union(new Dictionary<string, string> {
            { key,value}
        }).ToDictionary(k => k.Key, v => v.Value));
        public Dictionary<string,string> Send()=> parameters;
    }
}

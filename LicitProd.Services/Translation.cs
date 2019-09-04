using System;

namespace LicitProd.Services
{
    public class Translation
    {
        public string Key { get; }
        public string Value { get; }

        public Translation(string key, string value)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}

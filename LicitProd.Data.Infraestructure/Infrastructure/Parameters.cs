using System;
using System.Collections.Generic;
using System.Data;

namespace LicitProd.Data.Infrastructure.Infrastructure
{
    public class Parameters
    {
        private readonly List<Parameter> _parameters = new List<Parameter>();

        protected Parameters(List<Parameter> parameters)
        {
            this._parameters = parameters;
        }
        public Parameters()
        {

        }
        public Parameters Add(string key, string value, SqlDbType type)
        {
            _parameters.Add(new Parameter(key, value, type));
            return new Parameters(_parameters); ;
        }
        public Parameters Add(string key, DateTime value )
        {
            _parameters.Add(new Parameter(key, value.ToString(), SqlDbType.DateTime));
            return new Parameters(_parameters); ;
        }
        public Parameters Add(string key, int value)
        {
            _parameters.Add(new Parameter(key, value.ToString(), SqlDbType.Int));
            return new Parameters(_parameters); ;
        }
        public Parameters Add(string key, bool value)
        {
            _parameters.Add(new Parameter(key, value.ToString(), SqlDbType.Bit));
            return new Parameters(_parameters); ;
        }
        public Parameters Add(string key, decimal value)
        {
            _parameters.Add(new Parameter(key, value.ToString(), SqlDbType.Decimal));
            return new Parameters(_parameters); ;
        }
        public Parameters Add(string key, string value)
        {
            _parameters.Add(new Parameter(key, value, SqlDbType.NVarChar));
            return new Parameters(_parameters); ;
        }
        public List<Parameter> Send() => _parameters;
    }
}

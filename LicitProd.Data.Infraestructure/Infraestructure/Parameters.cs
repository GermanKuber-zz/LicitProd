using System;
using System.Collections.Generic;
using System.Data;

namespace LicitProd.Data
{
    public class Parameters
    {
        private readonly List<Parameter> parameters = new List<Parameter>();

        protected Parameters(List<Parameter> parameters)
        {
            this.parameters = parameters;
        }
        public Parameters()
        {

        }
        public Parameters Add(string key, string value, SqlDbType type)
        {
            parameters.Add(new Parameter(key, value, type));
            return new Parameters(parameters); ;
        }
        public Parameters Add(string key, DateTime value )
        {
            parameters.Add(new Parameter(key, value.ToString(), SqlDbType.DateTime));
            return new Parameters(parameters); ;
        }
        public Parameters Add(string key, int value)
        {
            parameters.Add(new Parameter(key, value.ToString(), SqlDbType.Int));
            return new Parameters(parameters); ;
        }
        public Parameters Add(string key, bool value)
        {
            parameters.Add(new Parameter(key, value.ToString(), SqlDbType.Bit));
            return new Parameters(parameters); ;
        }
        public Parameters Add(string key, decimal value)
        {
            parameters.Add(new Parameter(key, value.ToString(), SqlDbType.Decimal));
            return new Parameters(parameters); ;
        }
        public Parameters Add(string key, string value)
        {
            parameters.Add(new Parameter(key, value, SqlDbType.NVarChar));
            return new Parameters(parameters); ;
        }
        public List<Parameter> Send() => parameters;
    }
}

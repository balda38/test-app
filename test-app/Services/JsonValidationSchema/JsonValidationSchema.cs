using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace test_app.Services
{
    public class JsonValidationSchema
    {
        private static string smartphonesSchema;
        private Dictionary<string, string> shemasAliases = new Dictionary<string, string>
        {
            { "smartphones", smartphonesSchema }
        };

        static JsonValidationSchema()
        {
            
        }

        public string GetSchema(string schemaName)
        {
            return shemasAliases[schemaName];
        }
    }
}